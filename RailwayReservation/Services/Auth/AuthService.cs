using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Context;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Auth;

namespace RailwayReservation.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;
        private readonly RailwayReservationdbContext _context;

        public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService, ILogger<AuthService> logger, RailwayReservationdbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequestDto">The registration request data.</param>
        /// <returns>The result of the registration operation.</returns>
        public async Task<IdentityResult> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = registerRequestDto.Username,
                    Email = registerRequestDto.Email,
                    PhoneNumber = registerRequestDto.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

                if (result.Succeeded)
                {
                    _context.Users.Add(new User
                    {
                        UserId = Guid.Parse(user.Id),
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
                    await _context.SaveChangesAsync();

                    foreach (var role in registerRequestDto.Roles)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, role);
                        if (!roleResult.Succeeded)
                        {
                            _logger.LogWarning($"Adding role {role} failed for user {registerRequestDto.Username}. Errors: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }

                    _logger.LogInformation($"User {registerRequestDto.Username} registered successfully.");
                }
                else
                {
                    _logger.LogWarning($"User creation failed for {registerRequestDto.Username}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user.");
                throw new InvalidOperationException("An error occurred while registering the user.", ex);
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequestDto">The login request data.</param>
        /// <returns>The authentication token for the logged-in user.</returns>
        public async Task<string> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequestDto.Username)
                           ?? await _userManager.FindByNameAsync(loginRequestDto.Username);

                if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
                {
                    _logger.LogWarning($"Login failed for user {loginRequestDto.Username}. Invalid credentials.");
                    return null;
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.CreateTokenAsync(user, roles.ToList());

                _logger.LogInformation($"User {loginRequestDto.Username} logged in successfully.");
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in the user.");
                throw new InvalidOperationException("An error occurred while logging in the user.", ex);
            }
        }

        /// <summary>
        /// Resets the password for a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>True if the password reset was successful, otherwise false.</returns>
        public async Task<bool> ResetPassword(string email, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogWarning($"User with email {email} not found.");
                    return false;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Password reset successfully for user with email {email}.");
                    return true;
                }

                _logger.LogWarning($"Password reset failed for user with email {email}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while resetting password for user with email {email}.");
                throw new InvalidOperationException($"An error occurred while resetting password for user with email {email}.", ex);
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>True if the user was deleted successfully, otherwise false.</returns>
        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    return false;
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User with ID {userId} deleted successfully.");
                    return true;
                }
                _logger.LogWarning($"Failed to delete user with ID {userId}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting user with ID {userId}.");
                throw new InvalidOperationException($"An error occurred while deleting user with ID {userId}.", ex);
            }
        }

        /// <summary>
        /// Updates the details of a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="updateUserDetailsDto">The updated user details.</param>
        /// <returns>True if the user details were updated successfully, otherwise false.</returns>
        public async Task<bool> UpdateUserDetails(string userId, UpdateUserDetailsDto updateUserDetailsDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    return false;
                }

                user.UserName = updateUserDetailsDto.Username;
                user.Email = updateUserDetailsDto.Email;
                user.PhoneNumber = updateUserDetailsDto.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User details updated successfully for user with ID {userId}.");
                    return true;
                }

                _logger.LogWarning($"Failed to update user details for user with ID {userId}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating user details for user with ID {userId}.");
                throw new InvalidOperationException($"An error occurred while updating user details for user with ID {userId}.", ex);
            }
        }
    }
}
