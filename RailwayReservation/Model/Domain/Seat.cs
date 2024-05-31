using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Domain
{
    public class Seat
    {
        public Guid SeatId { get; set; }
        public string SeatNumber { get; set; }
        public Guid TrainId { get; set; }
        public Train Train { get; set; }
        public Guid? PassengerId { get; set; } // Nullable to allow unassigned seats
        public SeatStatus Status { get; set; }
    }
}
