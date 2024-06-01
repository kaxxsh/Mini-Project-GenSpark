using RailwayReservation.Model.Domain;

namespace RailwayReservation.Model.Dtos.Ticket.Passenger
{
    public class PassengerResponseDto
    {
        public Guid PassengerId { get; set; }
        public Guid TicketId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid SeatId { get; set; }

        public Seat Seat { get; set; }
    }
}
