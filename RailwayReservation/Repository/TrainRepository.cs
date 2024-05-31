using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RailwayReservation.Repository
{
    public class TrainRepository : ITrainRepository
    {
        private readonly RailwayReservationdbContext _context;

        public TrainRepository(RailwayReservationdbContext context)
        {
            _context = context;
        }

        public async Task<Train> Add(Train item)
        {
            try
            {
                await _context.Trains.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding train: {ex.Message}");
            }
        }

        public async Task AddSeats(IEnumerable<Seat> seats)
        {
            try
            {
                await _context.Seats.AddRangeAsync(seats);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding seats: {ex.Message}");
            }
        }

        public async Task<Train> Delete(Guid key)
        {
            try
            {
                var train = await _context.Trains
                    .Include(t => t.TrainRoute)
                        .ThenInclude(r => r.Stations)
                    .Include(t => t.TrainRoute.SourceStation)
                    .Include(t => t.TrainRoute.DestinationStation)
                    .Include(t => t.Seats)
                    .FirstOrDefaultAsync(t => t.TrainId == key);

                if (train == null)
                {
                    return null;
                }

                _context.Trains.Remove(train);
                await _context.SaveChangesAsync();
                return train;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting train: {ex.Message}");
            }
        }

        public async Task<Train> Get(Guid key)
        {
            try
            {
                var train = await _context.Trains
                    .Include(t => t.TrainRoute)
                        .ThenInclude(r => r.Stations)
                    .Include(t => t.TrainRoute.SourceStation)
                    .Include(t => t.TrainRoute.DestinationStation)
                    .Include(t => t.Seats)
                    .FirstOrDefaultAsync(t => t.TrainId == key);

                return train;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching train: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Train>> GetAll()
        {
            try
            {
                return await _context.Trains
                    .Include(t => t.TrainRoute)
                        .ThenInclude(r => r.Stations)
                    .Include(t => t.TrainRoute.SourceStation)
                    .Include(t => t.TrainRoute.DestinationStation)
                    .Include(t => t.Seats)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching trains: {ex.Message}");
            }
        }

        public async Task<Seat> GetSeat(Guid seatId)
        {
            try
            {
                var seat = await _context.Seats
                    .Include(s => s.Train)
                    .FirstOrDefaultAsync(s => s.SeatId == seatId);

                return seat;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching seat: {ex.Message}");
            }
        }

        public async Task RemoveSeats(IEnumerable<Seat> seats)
        {
            try
            {
                _context.Seats.RemoveRange(seats);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing seats: {ex.Message}");
            }
        }

        public async Task<Train> Update(Train item)
        {
            try
            {
                var existingTrain = await _context.Trains
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TrainId == item.TrainId);

                if (existingTrain == null)
                {
                    throw new Exception("Train not found");
                }

                _context.Trains.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Error updating train: The train record you attempted to update was modified or deleted by another process. Please reload the record and try again.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating train: {ex.Message}");
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving changes: {ex.Message}");
            }
        }
    }
}
