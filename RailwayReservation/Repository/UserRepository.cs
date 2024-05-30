using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;

namespace RailwayReservation.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RailwayReservationdbContext _context;

        public UserRepository(RailwayReservationdbContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            try
            {
                _context.Users.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> Delete(Guid key)
        {
            try
            {
                var user = await _context.Users.FindAsync(key);
                if (user == null)
                {
                    return null;
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> Get(Guid key)
        {
            try
            {
                var user = await _context.Users.FindAsync(key);
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> Update(User item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
