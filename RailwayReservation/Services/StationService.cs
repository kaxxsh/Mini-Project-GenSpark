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

        /// <summary>
        /// Add a new station.
        /// </summary>
        /// <param name="station">The station to add.</param>
        /// <returns>The added station.</returns>
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

        /// <summary>
        /// Delete a station by ID.
        /// </summary>
        /// <param name="id">The ID of the station to delete.</param>
        /// <returns>The deleted station.</returns>
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

        /// <summary>
        /// Get a station by ID.
        /// </summary>
        /// <param name="id">The ID of the station to get.</param>
        /// <returns>The retrieved station.</returns>
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

        /// <summary>
        /// Get all stations.
        /// </summary>
        /// <returns>A list of all stations.</returns>
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

        /// <summary>
        /// Update a station by ID.
        /// </summary>
        /// <param name="id">The ID of the station to update.</param>
        /// <param name="station">The updated station data.</param>
        /// <returns>The updated station.</returns>
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
