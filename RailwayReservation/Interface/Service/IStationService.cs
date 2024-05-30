using RailwayReservation.Model.Dtos.Train.Station;

namespace RailwayReservation.Interface.Service
{
    public interface IStationService
    {
        public Task<List<StationResponseDto>> GetAll();
        public Task<StationResponseDto> Get(Guid id);
        public Task<StationDto> Add(StationRequestDto station);
        public Task<StationDto> Update(Guid id, StationRequestDto station);
        public Task<StationDto> Delete(Guid id);
    }
}
