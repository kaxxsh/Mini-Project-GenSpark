using AutoMapper;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Auth.User;

namespace RailwayReservation.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;

        public UserService(IUserRepository user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }
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
