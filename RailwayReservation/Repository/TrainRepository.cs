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

        public async Task<Train> Delete(Guid key)
        {
            try
            {
                var train = await _context.Trains
                    .Include(t => t.TrainRoute)
                        .ThenInclude(r => r.Stations)
                    .Include(t => t.TrainRoute.SourceStation)
                    .Include(t => t.TrainRoute.DestinationStation)
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
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching trains: {ex.Message}");
            }
        }

        public async Task<Train> Update(Train item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating train: {ex.Message}");
            }
        }
    }
}
