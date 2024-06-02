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
    /// Repository class for managing ticket-related data operations.
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        private readonly RailwayReservationdbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TicketRepository(RailwayReservationdbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new ticket to the database.
        /// </summary>
        /// <param name="item">The ticket to add.</param>
        /// <returns>The added ticket.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while adding the ticket.</exception>
        public async Task<Ticket> Add(Ticket item)
        {
            try
            {
                await _context.Tickets.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error adding ticket: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding ticket: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a ticket from the database.
        /// </summary>
        /// <param name="key">The unique identifier of the ticket to delete.</param>
        /// <returns>The deleted ticket.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while deleting the ticket.</exception>
        public async Task<Ticket> Delete(Guid key)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(key);
                if (ticket == null)
                {
                    return null;
                }
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return ticket;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error deleting ticket: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting ticket: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a ticket by its unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier of the ticket to retrieve.</param>
        /// <returns>The retrieved ticket.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the ticket.</exception>
        public async Task<Ticket> Get(Guid key)
        {
            try
            {
                var ticket = await _context.Tickets.Include(t => t.Passengers).FirstOrDefaultAsync(t => t.TicketId == key);
                return ticket;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error getting ticket: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting ticket: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all tickets from the database.
        /// </summary>
        /// <returns>A list of all tickets.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the tickets.</exception>
        public async Task<IEnumerable<Ticket>> GetAll()
        {
            try
            {
                var tickets = await _context.Tickets.Include(t => t.Passengers).ToListAsync();
                return tickets;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error getting tickets: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting tickets: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing ticket in the database.
        /// </summary>
        /// <param name="item">The ticket to update.</param>
        /// <returns>The updated ticket.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the ticket.</exception>
        public async Task<Ticket> Update(Ticket item)
        {
            try
            {
                _context.Tickets.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error updating ticket: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating ticket: {ex.Message}");
            }
        }
    }
}
