using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Dtos.Train.Station
{
    public class StationDto
    {
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        public string StationCode { get; set; }
        public StationType StationType { get; set; }
        public int Pincode { get; set; }
    }
}
