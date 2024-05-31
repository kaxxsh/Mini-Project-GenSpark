namespace RailwayReservation.Model.Dtos.Train.Route
{

    public class RouteRequestDto
    {
        public Guid Source { get; set; }
        public Guid Destination { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public List<Guid> StationIds { get; set; }
    }

}
