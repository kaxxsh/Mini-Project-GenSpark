using AutoMapper;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Ticket;
using RailwayReservation.Model.Enum.Ticket;
using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITrainRepository _trainRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _email;

        public TicketService(
            ITicketRepository ticketRepository,
            ITrainRepository trainRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IEmailSender email)
        {
            _ticketRepository = ticketRepository;
            _trainRepository = trainRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _email = email;
        }

        public async Task<TicketResponseDto> Add(TicketRequestDto ticketRequestDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketRequestDto);
            var train = await _trainRepository.Get(ticket.TrainId);

            if (train == null)
            {
                throw new Exception("Train not found");
            }

            ticket.TotalAmount = ticket.Passengers.Count * train.Fare;

            var availableSeats = train.Seats
                .Where(s => s.Status == SeatStatus.Available)
                .OrderBy(s => s.SeatNumber)
                .Take(ticket.Passengers.Count)
                .ToList();

            if (availableSeats.Count < ticket.Passengers.Count)
            {
                throw new Exception("Not enough available seats");
            }

            for (int i = 0; i < ticket.Passengers.Count; i++)
            {
                var passenger = ticket.Passengers[i];
                if (passenger.PassengerId == Guid.Empty)
                {
                    passenger.PassengerId = Guid.NewGuid();
                }

                passenger.SeatId = availableSeats[i].SeatId;
                passenger.TicketId = ticket.TicketId;
            }

            if (ticket.UserId == null)
            {
                var ticketUser = await _userRepository.Get(ticket.UserId);
                if (ticketUser == null)
                {
                    throw new Exception("User not found");
                }

                if (ticketUser.WalletBalance < ticket.TotalAmount)
                {
                    throw new Exception("Insufficient balance");
                }

                ticketUser.WalletBalance -= ticket.TotalAmount;
                await _userRepository.Update(ticketUser);
                ticket.PaymentStatus = PaymentStatus.Paid;
            }

            var result = await _ticketRepository.Add(ticket);

            // Send email notification
            var emailContent = CreateEmailContent(ticket, train);
            var userEmail = await _userRepository.Get(ticket.UserId);
            await _email.SendEmailAsync(userEmail.Email, "Ticket Booking Confirmation", emailContent);

            return _mapper.Map<TicketResponseDto>(result);
        }

        public async Task<TicketResponseDto> ApproveTicket(Guid id, TicketStatus ticketStatus)
        {
            var ticket = await _ticketRepository.Get(id);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            ticket.TicketStatus = ticketStatus;
            if (ticketStatus == TicketStatus.Booked)
            {
                var train = await _trainRepository.Get(ticket.TrainId);
                var availableSeats = train.Seats.Where(s => s.Status == SeatStatus.Available).OrderBy(s => s.SeatNumber).ToList();

                if (ticket.Passengers != null && availableSeats.Count < ticket.Passengers.Count)
                {
                    throw new Exception("Not enough available seats");
                }

                foreach (var passenger in ticket.Passengers)
                {
                    passenger.SeatId = availableSeats.First().SeatId;
                    availableSeats.First().Status = SeatStatus.Booked;
                    availableSeats.RemoveAt(0);
                }
                await _trainRepository.Update(train);
            }

            var result = await _ticketRepository.Update(ticket);

            // Send email notification
            var emailContent = CreateEmailContent(ticket, await _trainRepository.Get(ticket.TrainId));
            var userEmail = await _userRepository.Get(ticket.UserId);
            await _email.SendEmailAsync(userEmail.Email, "Ticket Approval Notification", emailContent);

            return _mapper.Map<TicketResponseDto>(result);
        }

        public async Task<List<TicketResponseDto>> BookedTicket(Guid TrainId)
        {
            var result = await _ticketRepository.GetAll();
            return _mapper.Map<List<TicketResponseDto>>(result.Where(t => t.TrainId == TrainId && t.TicketStatus == TicketStatus.Booked));
        }

        public async Task<TicketResponseDto> CancelTicket(Guid id, TicketStatus ticketStatus)
        {
            var ticket = await _ticketRepository.Get(id);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            ticket.TicketStatus = ticketStatus;
            if (ticket.PaymentStatus == PaymentStatus.Paid)
            {
                var user = await _userRepository.Get(ticket.UserId);
                user.WalletBalance += ticket.TotalAmount;
                await _userRepository.Update(user);
            }

            if (ticket.TicketStatus == TicketStatus.Booked)
            {
                var train = await _trainRepository.Get(ticket.TrainId);
                var seats = train.Seats.Where(s => s.Status == SeatStatus.Booked).ToList();
                foreach (var passenger in ticket.Passengers)
                {
                    var seat = seats.FirstOrDefault(s => s.SeatId == passenger.SeatId);
                    seat.Status = SeatStatus.Available;
                    seats.Remove(seat);
                }
                await _trainRepository.Update(train);
            }

            var result = await _ticketRepository.Update(ticket);

            // Send email notification
            var emailContent = CreateEmailContent(ticket, await _trainRepository.Get(ticket.TrainId));
            var userEmail = await _userRepository.Get(ticket.UserId);
            await _email.SendEmailAsync(userEmail.Email, "Ticket Cancellation Notification", emailContent);

            return _mapper.Map<TicketResponseDto>(result);
        }

        public async Task<TicketResponseDto> Delete(Guid id)
        {
            var ticket = await _ticketRepository.Get(id);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            var result = await _ticketRepository.Delete(id);
            return _mapper.Map<TicketResponseDto>(result);
        }

        public async Task<List<TicketResponseDto>> GetAll()
        {
            var result = await _ticketRepository.GetAll();
            return _mapper.Map<List<TicketResponseDto>>(result);
        }

        public async Task<List<TicketResponseDto>> GetBookedTicketbyUser(Guid id)
        {
            try
            {
                var result = await _ticketRepository.GetAll();
                return _mapper.Map<List<TicketResponseDto>>(result.Where(t => t.UserId == id && t.TicketStatus == TicketStatus.Booked));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching tickets: {ex.Message}");
            }
        }

        public async Task<TicketResponseDto> GetById(Guid id)
        {
            var ticket = await _ticketRepository.Get(id);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            return _mapper.Map<TicketResponseDto>(ticket);
        }

        public async Task<List<TicketResponseDto>> GetCanceledTicketByUser(Guid id)
        {
            try
            {
                var result = await _ticketRepository.GetAll();
                return _mapper.Map<List<TicketResponseDto>>(result.Where(t => t.UserId == id && t.TicketStatus == TicketStatus.Cancelled));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching tickets: {ex.Message}");
            }
        }

        public async Task<List<TicketResponseDto>> PendingTicket()
        {
            var result = await _ticketRepository.GetAll();
            return _mapper.Map<List<TicketResponseDto>>(result.Where(t => t.TicketStatus == TicketStatus.Pending));
        }

        public async Task<TicketResponseDto> Update(Guid id, TicketRequestDto ticketRequestDto)
        {
            var ticket = await _ticketRepository.Get(id);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            var updatedTicket = _mapper.Map(ticketRequestDto, ticket);
            var result = await _ticketRepository.Update(updatedTicket);

            // Send email notification
            var emailContent = CreateEmailContent(updatedTicket, await _trainRepository.Get(ticket.TrainId));
            var userEmail = await _userRepository.Get(ticket.UserId);
            await _email.SendEmailAsync(userEmail.Email, "Ticket Update Notification", emailContent);

            return _mapper.Map<TicketResponseDto>(result);
        }

        private string CreateEmailContent(Ticket ticket, Train train)
        {
            return $@"
                <html>
                    <body>
                        <h2>Ticket Confirmation</h2>
                        <p><strong>PNR:</strong> {ticket.TicketId}</p>
                        <p><strong>Train Name:</strong> {train.TrainName}</p>
                        <p><strong>Source Station:</strong> {train.TrainRoute.SourceStation.StationName}</p>
                        <p><strong>Destination Station:</strong> {train.TrainRoute.DestinationStation.StationName}</p>
                        <p><strong>Journey Date:</strong> {ticket.JourneyDate:dd-MM-yyyy}</p>
                        <p><strong>Booking Date:</strong> {ticket.BookingDate:dd-MM-yyyy}</p>
                        <p><strong>Total Amount:</strong> {ticket.TotalAmount}</p>
                        <p><strong>Payment Status:</strong> {ticket.PaymentStatus}</p>
                        <p><strong>Ticket Status:</strong> {ticket.TicketStatus}</p>
                        <h3>Passenger Details:</h3>
                        <ul>
                            {string.Join("", ticket.Passengers.Select(p => $@"
                            <li>
                                <strong>Name:</strong> {p.Name}, 
                                <strong>Age:</strong> {p.Age}, 
                                <strong>Seat:</strong> {(p.Seat?.SeatNumber != null ? p.Seat.SeatNumber : "Not Assigned")}
                            </li>"))}
                        </ul>
                    </body>
                </html>";
        }
    }
}
