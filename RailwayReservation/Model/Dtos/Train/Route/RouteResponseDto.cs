using RailwayReservation.Model.Dtos.Train.Station;

namespace RailwayReservation.Model.Dtos.Train.Route
{
    public class RouteResponseDto
    {
        public Guid RouteId { get; set; }
        public Guid Source { get; set; }
        public Guid Destination { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public List<StationResponseDto> Stations { get; set; }
    }

}
