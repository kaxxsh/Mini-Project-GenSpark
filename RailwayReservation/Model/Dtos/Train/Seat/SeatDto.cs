using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Dtos.Train.Seat
{
    public class SeatDto
    {
        public Guid SeatId { get; set; }
        public string SeatNumber { get; set; }
        public Guid? PassengerId { get; set; } // Nullable to allow unassigned seats
        public SeatStatus Status { get; set; }
    }
}
