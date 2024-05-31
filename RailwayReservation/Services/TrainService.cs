
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Train;
using RailwayReservation.Model.Enum.Train;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayReservation.Services
{
    public class TrainService : ITrainService
    {
        private readonly ITrainRepository _trainRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IMapper _mapper;

        public TrainService(ITrainRepository trainRepository, IStationRepository stationRepository, IMapper mapper)
        {
            _trainRepository = trainRepository;
            _stationRepository = stationRepository;
            _mapper = mapper;
        }

        public async Task<TrainResponseDto> Add(TrainRequestDto trainRequestDto)
        {
            try
            {
                var train = _mapper.Map<Train>(trainRequestDto);
                train.AvailableSeats = train.TotalSeats;

                // Process stations separately
                train.TrainRoute.Stations = new List<Station>();
                foreach (var stationId in trainRequestDto.TrainRoute.StationIds)
                {
                    var station = await _stationRepository.Get(stationId);
                    if (station != null)
                    {
                        train.TrainRoute.Stations.Add(station);
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

                train.TrainRoute.SourceStation = sourceStation;
                train.TrainRoute.DestinationStation = destinationStation;

                train.Seats = AddSeats(train, trainRequestDto.TotalSeats);
                train = await _trainRepository.Add(train);

                return _mapper.Map<TrainResponseDto>(train);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding train: {ex.Message}");
            }
        }

        public async Task<TrainResponseDto> Delete(Guid id)
        {
            try
            {
                var train = await _trainRepository.Delete(id);
                return _mapper.Map<TrainResponseDto>(train);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting train: {ex.Message}");
            }
        }

        public async Task<List<TrainResponseDto>> GetAll()
        {
            try
            {
                var trains = await _trainRepository.GetAll();
                return _mapper.Map<List<TrainResponseDto>>(trains);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching trains: {ex.Message}");
            }
        }

        public async Task<TrainResponseDto> GetById(Guid id)
        {
            try
            {
                var train = await _trainRepository.Get(id);
                return _mapper.Map<TrainResponseDto>(train);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching train: {ex.Message}");
            }
        }

        public async Task<TrainResponseDto> Update(Guid id, TrainRequestDto trainRequestDto)
        {
            try
            {
                var train = await _trainRepository.Get(id);
                if (train == null)
                {
                    throw new Exception("Train not found");
                }

                _mapper.Map(trainRequestDto, train);
                train.Seats = UpdateSeats(train, trainRequestDto.TotalSeats);

                await _trainRepository.Update(train);

                return _mapper.Map<TrainResponseDto>(train);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("The train record you attempted to update was modified or deleted by another process. Please reload the record and try again.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating train: {ex.Message}");
            }
        }

        private ICollection<Seat> AddSeats(Train train, int totalSeats)
        {
            var seats = new List<Seat>();
            char currentRow = 'A';
            int currentSeatNumber = 1;

            for (int i = 0; i < totalSeats; i++)
            {
                if (currentSeatNumber > 10)
                {
                    currentSeatNumber = 1;
                    currentRow++;
                }

                seats.Add(new Seat
                {
                    SeatId = Guid.NewGuid(),
                    SeatNumber = $"{currentRow}{currentSeatNumber}",
                    Status = SeatStatus.Available,
                    TrainId = train.TrainId
                });

                currentSeatNumber++;
            }

            return seats;
        }

        private ICollection<Seat> UpdateSeats(Train train, int newTotalSeats)
        {
            var seats = train.Seats.ToList();
            int currentTotalSeats = seats.Count;

            if (newTotalSeats > currentTotalSeats)
            {
                char currentRow;
                int currentSeatNumber;

                if (seats.Any())
                {
                    currentRow = seats.Max(s => s.SeatNumber[0]);
                    currentSeatNumber = int.Parse(seats.Max(s => s.SeatNumber.Substring(1))) + 1;
                }
                else
                {
                    currentRow = 'A';
                    currentSeatNumber = 1;
                }

                for (int i = 0; i < newTotalSeats - currentTotalSeats; i++)
                {
                    if (currentSeatNumber > 10)
                    {
                        currentSeatNumber = 1;
                        currentRow++;
                    }

                    seats.Add(new Seat
                    {
                        SeatId = Guid.NewGuid(),
                        SeatNumber = $"{currentRow}{currentSeatNumber}",
                        Status = SeatStatus.Available,
                        TrainId = train.TrainId
                    });

                    currentSeatNumber++;
                }
            }
            else if (newTotalSeats < currentTotalSeats)
            {
                var seatsToRemove = seats
                                        .Where(s => s.Status == SeatStatus.Available)
                                        .OrderByDescending(s => s.SeatNumber)
                                        .Take(currentTotalSeats - newTotalSeats)
                                        .ToList();

                foreach (var seat in seatsToRemove)
                {
                    seats.Remove(seat);
                }
            }

            train.TotalSeats = newTotalSeats;
            train.AvailableSeats = seats.Count(s => s.Status == SeatStatus.Available);
            return seats;
        }
    }
}