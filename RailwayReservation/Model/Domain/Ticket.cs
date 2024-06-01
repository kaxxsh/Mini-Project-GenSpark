using RailwayReservation.Model.Enum.Ticket;

namespace RailwayReservation.Model.Domain
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public Guid TrainId { get; set; }
        public Guid UserId { get; set; }
        public Guid Source { get; set; }
        public Guid Destination { get; set; }
        public DateTime JourneyDate { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public List<Passenger> Passengers { get; set; }
        public double TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Pending;

        public Train Train { get; set; }
        public User User { get; set; }
        public Station SourceStation { get; set; }
        public Station DestinationStation { get; set; }
    }
}
