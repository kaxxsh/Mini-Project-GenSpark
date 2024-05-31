using RailwayReservation.Model.Dtos.Train.Route;
using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Dtos.Train
{
    public class TrainRequestDto
    {
        public string TrainName { get; set; }
        public int TrainNumber { get; set; }
        public TrainType TrainType { get; set; }
        public TrainStatus TrainStatus { get; set; }
        public int TotalSeats { get; set; }
        public int Fare { get; set; }
        public RouteRequestDto TrainRoute { get; set; }
    }
}
