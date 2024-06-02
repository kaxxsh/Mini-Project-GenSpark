using AutoMapper;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Auth.User;

namespace RailwayReservation.Services
{
    /// <summary>
    /// Service class for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="user">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        public UserService(IUserRepository user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds money to the user's wallet balance.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="Amount">The amount to add.</param>
        /// <returns>The updated user object.</returns>
        public async Task<User> AddMoney(Guid id, double Amount)
        {
            try
            {
                var data = await _user.Get(id);
                if (data == null)
                {
                    throw new Exception("User not found");
                }
                data.WalletBalance += Amount;
                data = await _user.Update(data);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of user response DTOs.</returns>
        public async Task<List<UserResponseDto>> GetAll()
        {
            try
            {
                var data = await _user.GetAll();
                return _mapper.Map<List<UserResponseDto>>(data);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>The user response DTO.</returns>
        public async Task<UserResponseDto> GetById(Guid id)
        {
            try
            {
                var data = await _user.Get(id);
                if (data == null)
                {
                    throw new Exception("User not found");
                }
                return _mapper.Map<UserResponseDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
