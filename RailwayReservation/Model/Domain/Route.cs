namespace RailwayReservation.Model.Domain
{
    public class Route
    {
        public Guid RouteId { get; set; }
        public Guid TrainId { get; set; }
        public Guid Source { get; set; }
        public Guid Destination { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public List<Station> Stations { get; set; }

        public Station SourceStation { get; set; }
        public Station DestinationStation { get; set; }

        public Route()
        {
            Stations = new List<Station>();
        }
    }

}
