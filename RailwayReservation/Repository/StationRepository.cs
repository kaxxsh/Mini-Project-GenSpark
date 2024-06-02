using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwayReservation.Repository
{
    /// <summary>
    /// Repository class for managing station-related data operations.
    /// </summary>
    public class StationRepository : IStationRepository
    {
        private readonly RailwayReservationdbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public StationRepository(RailwayReservationdbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new station to the database.
        /// </summary>
        /// <param name="item">The station to add.</param>
        /// <returns>The added station.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while adding the station.</exception>
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

        /// <summary>
        /// Deletes a station from the database.
        /// </summary>
        /// <param name="key">The unique identifier of the station to delete.</param>
        /// <returns>The deleted station.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the station.</exception>
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

        /// <summary>
        /// Gets a station by its unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier of the station to retrieve.</param>
        /// <returns>The retrieved station.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the station.</exception>
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

        /// <summary>
        /// Gets all stations from the database.
        /// </summary>
        /// <returns>A list of all stations.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the stations.</exception>
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

        /// <summary>
        /// Updates an existing station in the database.
        /// </summary>
        /// <param name="item">The station to update.</param>
        /// <returns>The updated station.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the station.</exception>
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

        /// <summary>
        /// Gets a list of stations by their unique identifiers.
        /// </summary>
        /// <param name="stationIds">The list of unique identifiers of the stations to retrieve.</param>
        /// <returns>A list of stations.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the stations.</exception>
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
