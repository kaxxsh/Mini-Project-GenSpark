using Microsoft.AspNetCore.Identity;
using RailwayReservation.Model.Dtos.Auth;

namespace RailwayReservation.Interface.Service
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterRequestDto registerRequestDto);
        Task<string> Login(LoginRequestDto loginRequestDto);
        Task<bool> ResetPassword(string email, string newPassword);
        Task<bool> DeleteUser(string userId);
        Task<bool> UpdateUserDetails(string userId, UpdateUserDetailsDto updateUserDetailsDto);
    }
}
