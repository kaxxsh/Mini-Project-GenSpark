using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;

namespace RailwayReservation.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly RailwayReservationdbContext _context;

        public TicketRepository(RailwayReservationdbContext context)
        {
            _context = context;
        }
        public async Task<Ticket> Add(Ticket item)
        {
            try
            {
                await _context.Tickets.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding ticket: {ex.Message}");
            }
        }

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
            catch (Exception ex)
            {
                throw new Exception($"Error deleting ticket: {ex.Message}");
            }
        }

        public async Task<Ticket> Get(Guid key)
        {
            try
            {
                var ticket = await _context.Tickets.Include(t => t.Passengers).FirstOrDefaultAsync(t => t.TicketId == key);
                return ticket;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting ticket: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            try
            {
                var tickets = await _context.Tickets.Include(t => t.Passengers).ToListAsync();
                return tickets;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting tickets: {ex.Message}");
            }
        }

        public async Task<Ticket> Update(Ticket item)
        {
            try
            {
                _context.Tickets.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating ticket: {ex.Message}");
            }
        }
    }
}
