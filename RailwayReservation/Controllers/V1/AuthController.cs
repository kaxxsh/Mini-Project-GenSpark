using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Dtos.Auth;

namespace RailwayReservation.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequestDto">The registration request data.</param>
        /// <returns>The result of the registration.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            try
            {
                // Check if the request body is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the Register method of the IAuthService to register the user
                var result = await _authService.Register(registerRequestDto);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {registerRequestDto.Username} registered successfully.");
                    return Ok(new { Message = "User registered successfully." });
                }

                _logger.LogWarning($"User registration failed for {registerRequestDto.Username}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequestDto">The login request data.</param>
        /// <returns>The authentication token.</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                // Call the Login method of the IAuthService to log in the user
                var token = await _authService.Login(loginRequestDto);
                if (!string.IsNullOrEmpty(token))
                {
                    _logger.LogInformation($"User {loginRequestDto.Username} logged in successfully.");
                    HttpContext.Response.Cookies.Append("token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict,
                        Secure = true
                    });
                    return Ok(new { Token = token });
                }

                _logger.LogWarning($"Login failed for user {loginRequestDto.Username}. Invalid credentials.");
                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in the user.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Resets the password for a user.
        /// </summary>
        /// <param name="resetPasswordRequestDto">The reset password request data.</param>
        /// <returns>The result of the password reset.</returns>
        [HttpPut("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
        {
            try
            {
                // Check if the request body is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the ResetPassword method of the IAuthService to reset the user's password
                var result = await _authService.ResetPassword(resetPasswordRequestDto.Email, resetPasswordRequestDto.NewPassword);
                if (result)
                {
                    _logger.LogInformation($"Password reset successfully for user with email {resetPasswordRequestDto.Email}.");
                    return Ok(new { Message = "Password reset successfully." });
                }

                _logger.LogWarning($"Password reset failed for user with email {resetPasswordRequestDto.Email}.");
                return BadRequest("Password reset failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while resetting password.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>The result of the user deletion.</returns>
        [HttpDelete("delete-user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                // Call the DeleteUser method of the IAuthService to delete the user
                var result = await _authService.DeleteUser(userId);
                if (result)
                {
                    _logger.LogInformation($"User with ID {userId} deleted successfully.");
                    return Ok(new { Message = "User deleted successfully." });
                }

                _logger.LogWarning($"Failed to delete user with ID {userId}.");
                return BadRequest("Failed to delete user.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting user with ID {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates the details of a user.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="updateUserDetailsDto">The updated user details.</param>
        /// <returns>The result of the user details update.</returns>
        [HttpPut("update-user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserDetails(string userId, [FromBody] UpdateUserDetailsDto updateUserDetailsDto)
        {
            try
            {
                // Check if the request body is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Call the UpdateUserDetails method of the IAuthService to update the user's details
                var result = await _authService.UpdateUserDetails(userId, updateUserDetailsDto);
                if (result)
                {
                    _logger.LogInformation($"User details updated successfully for user with ID {userId}.");
                    return Ok(new { Message = "User details updated successfully." });
                }

                _logger.LogWarning($"Failed to update user details for user with ID {userId}.");
                return BadRequest("Failed to update user details.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating user details for user with ID {userId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
