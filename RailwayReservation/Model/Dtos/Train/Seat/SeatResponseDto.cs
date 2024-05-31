using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Dtos.Train.Seat
{
    public class SeatResponseDto
    {
        public Guid SeatId { get; set; }
        public string SeatNumber { get; set; }
        public SeatStatus Status { get; set; }
    }
}
