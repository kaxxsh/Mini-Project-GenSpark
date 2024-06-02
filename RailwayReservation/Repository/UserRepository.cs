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
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while adding the user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred.", ex);
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
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while deleting the user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred.", ex);
            }
        }

        public async Task<User> Get(Guid key)
        {
            try
            {
                var user = await _context.Users.FindAsync(key);
                return user;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while retrieving the user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while retrieving all users.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred.", ex);
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
            catch (DbUpdateException ex)
            {
                throw new Exception("Error occurred while updating the user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred.", ex);
            }
        }
    }
}
