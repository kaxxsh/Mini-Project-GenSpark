using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Domain
{
    public class Train
    {
        public Guid TrainId { get; set; }
        public string TrainName { get; set; }
        public int TrainNumber { get; set; }
        public TrainType TrainType { get; set; }
        public TrainStatus TrainStatus { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public int Fare { get; set; }
        public Route TrainRoute { get; set; }
    }

}
