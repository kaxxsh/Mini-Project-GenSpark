using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RailwayReservation.Repository
{
    public class StationRepository : IStationRepository
    {
        private readonly RailwayReservationdbContext _context;

        public StationRepository(RailwayReservationdbContext context)
        {
            _context = context;
        }

        public async Task<Station> Add(Station item)
        {
            try
            {
                _context.Stations.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred while adding station: " + e.Message);
            }
        }

        public async Task<Station> Delete(Guid key)
        {
            try
            {
                var station = await _context.Stations.FindAsync(key);
                if (station == null)
                {
                    throw new Exception("Station not found");
                }
                _context.Stations.Remove(station);
                await _context.SaveChangesAsync();
                return station;
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred while deleting station: " + e.Message);
            }
        }

        public async Task<Station> Get(Guid key)
        {
            try
            {
                var station = await _context.Stations.FindAsync(key);
                if (station == null)
                {
                    throw new Exception("Station not found");
                }
                return station;
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred while getting station: " + e.Message);
            }
        }

        public async Task<IEnumerable<Station>> GetAll()
        {
            try
            {
                return await _context.Stations.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred while getting all stations: " + e.Message);
            }
        }

        public async Task<Station> Update(Station item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred while updating station: " + e.Message);
            }
        }

        public async Task<List<Station>> GetStationsByIds(List<Guid> stationIds)
        {
            try
            {
                return await _context.Stations
                    .Where(s => stationIds.Contains(s.StationId))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred while getting stations by IDs: " + e.Message);
            }
        }
    }
}
