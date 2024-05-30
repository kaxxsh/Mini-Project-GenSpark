using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;

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
            _context.Stations.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Station> Delete(Guid key)
        {
            var station = await _context.Stations.FindAsync(key);
            if (station == null)
            {
                return null;
            }

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();
            return station;
        }

        public async Task<Station> Get(Guid key)
        {
            return await _context.Stations.FindAsync(key);
        }

        public async Task<IEnumerable<Station>> GetAll()
        {
            return await _context.Stations.ToListAsync();
        }

        public async Task<Station> Update(Station item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
