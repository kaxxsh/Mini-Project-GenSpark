using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RailwayReservation.Repository
{
    /// <summary>
    /// Repository class for managing train data.
    /// </summary>
    public class TrainRepository : ITrainRepository
    {
        private readonly RailwayReservationdbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TrainRepository(RailwayReservationdbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new train to the database.
        /// </summary>
        /// <param name="item">The train to add.</param>
        /// <returns>The added train.</returns>
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

        /// <summary>
        /// Adds multiple seats to the database.
        /// </summary>
        /// <param name="seats">The seats to add.</param>
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

        /// <summary>
        /// Deletes a train from the database.
        /// </summary>
        /// <param name="key">The key of the train to delete.</param>
        /// <returns>The deleted train.</returns>
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

        /// <summary>
        /// Gets a train from the database by its key.
        /// </summary>
        /// <param name="key">The key of the train to get.</param>
        /// <returns>The fetched train.</returns>
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

        /// <summary>
        /// Gets all trains from the database.
        /// </summary>
        /// <returns>The fetched trains.</returns>
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

        /// <summary>
        /// Gets a seat from the database by its ID.
        /// </summary>
        /// <param name="seatId">The ID of the seat to get.</param>
        /// <returns>The fetched seat.</returns>
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

        /// <summary>
        /// Removes multiple seats from the database.
        /// </summary>
        /// <param name="seats">The seats to remove.</param>
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

        /// <summary>
        /// Updates a train in the database.
        /// </summary>
        /// <param name="item">The train to update.</param>
        /// <returns>The updated train.</returns>
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

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error saving changes: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving changes: {ex.Message}");
            }
        }
    }
}
