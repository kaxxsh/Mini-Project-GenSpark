using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Ticket.Passenger;
using RailwayReservation.Model.Enum.Ticket;

namespace RailwayReservation.Model.Dtos.Ticket
{
    public class TicketRequestDto
    {
        public Guid TrainId { get; set; }
        public Guid UserId { get; set; }
        public Guid Source { get; set; }
        public Guid Destination { get; set; }
        public DateTime JourneyDate { get; set; }
        public List<PassengerRequestDto> Passengers { get; set; }
    }
}
