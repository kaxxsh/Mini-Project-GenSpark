using RailwayReservation.Model.Dtos.Ticket.Passenger;
using RailwayReservation.Model.Enum.Ticket;

namespace RailwayReservation.Model.Dtos.Ticket
{
    public class TicketResponseDto
    {
        public Guid TicketId { get; set; }
        public Guid TrainId { get; set; }
        public Guid UserId { get; set; }
        public Guid Source { get; set; }
        public Guid Destination { get; set; }
        public DateTime JourneyDate { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public List<PassengerResponseDto> Passengers { get; set; }
        public double TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public TicketStatus TicketStatus { get; set; }
    }
}
