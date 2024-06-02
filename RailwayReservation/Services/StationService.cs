using AutoMapper;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Train.Station;

namespace RailwayReservation.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _repository;
        private readonly IMapper _mapper;

        public StationService(IStationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StationDto> Add(StationRequestDto station)
        {
            try
            {
                var data = _mapper.Map<Station>(station);
                data = await _repository.Add(data);
                return _mapper.Map<StationDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StationDto> Delete(Guid id)
        {
            try
            {
                var data = await _repository.Delete(id);
                return _mapper.Map<StationDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StationResponseDto> Get(Guid id)
        {
            try
            {
                var data = await _repository.Get(id);
                return _mapper.Map<StationResponseDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<StationResponseDto>> GetAll()
        {
            try
            {
                var data = await _repository.GetAll();
                return _mapper.Map<List<StationResponseDto>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StationDto> Update(Guid id, StationRequestDto station)
        {
            try
            {
                var data = _mapper.Map<Station>(station);
                if (data == null) throw new ArgumentNullException(nameof(data));
                data.StationId = id;
                data = await _repository.Update(data);
                return _mapper.Map<StationDto>(data);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("Data cannot be null.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
