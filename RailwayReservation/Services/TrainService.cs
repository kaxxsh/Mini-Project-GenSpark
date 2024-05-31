using AutoMapper;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Train;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RailwayReservation.Services
{
    public class TrainService : ITrainService
    {
        private readonly ITrainRepository _train;
        private readonly IStationRepository _stationRepository;
        private readonly IMapper _mapper;

        public TrainService(ITrainRepository train, IStationRepository stationRepository, IMapper mapper)
        {
            _train = train;
            _stationRepository = stationRepository;
            _mapper = mapper;
        }

        public async Task<TrainResponseDto> Add(TrainRequestDto trainRequestDto)
        {
            try
            {
                var data = _mapper.Map<Train>(trainRequestDto);
                data.AvailableSeats = data.TotalSeats;

                // Process the stations separately
                data.TrainRoute.Stations.Clear();
                foreach (var stationId in trainRequestDto.TrainRoute.StationIds)
                {
                    var station = await _stationRepository.Get(stationId);
                    if (station != null)
                    {
                        data.TrainRoute.Stations.Add(station);
                    }
                    else
                    {
                        throw new Exception($"Station with ID {stationId} not found");
                    }
                }

                // Check if source and destination stations exist
                var sourceStation = await _stationRepository.Get(trainRequestDto.TrainRoute.Source);
                var destinationStation = await _stationRepository.Get(trainRequestDto.TrainRoute.Destination);

                if (sourceStation == null || destinationStation == null)
                {
                    throw new Exception("Source or Destination station not found");
                }

                data.TrainRoute.SourceStation = sourceStation;
                data.TrainRoute.DestinationStation = destinationStation;

                data = await _train.Add(data);
                return _mapper.Map<TrainResponseDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<TrainResponseDto> Delete(Guid id)
        {
            try
            {
                var data = await _train.Delete(id);
                return _mapper.Map<TrainResponseDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TrainResponseDto>> GetAll()
        {
            try
            {
                var data = await _train.GetAll();
                return _mapper.Map<List<TrainResponseDto>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TrainResponseDto> GetById(Guid id)
        {
            try
            {
                var data = await _train.Get(id);
                return _mapper.Map<TrainResponseDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TrainResponseDto> Update(Guid id, TrainRequestDto trainRequestDto)
        {
            try
            {
                var existingTrain = await _train.Get(id);
                if (existingTrain == null)
                {
                    throw new Exception("Train not found");
                }

                // Update train details
                _mapper.Map(trainRequestDto, existingTrain);

                // Handle the route and stations update manually
                existingTrain.TrainRoute.Source = trainRequestDto.TrainRoute.Source;
                existingTrain.TrainRoute.Destination = trainRequestDto.TrainRoute.Destination;
                existingTrain.TrainRoute.Distance = trainRequestDto.TrainRoute.Distance;
                existingTrain.TrainRoute.Duration = trainRequestDto.TrainRoute.Duration;

                // Clear existing stations and add new ones
                existingTrain.TrainRoute.Stations.Clear();
                foreach (var stationId in trainRequestDto.TrainRoute.StationIds)
                {
                    var station = await _stationRepository.Get(stationId);
                    if (station != null)
                    {
                        existingTrain.TrainRoute.Stations.Add(station);
                    }
                }

                existingTrain = await _train.Update(existingTrain);
                return _mapper.Map<TrainResponseDto>(existingTrain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
