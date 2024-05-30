using Microsoft.AspNetCore.Identity;

namespace RailwayReservation.Interface.Service
{
    public interface ITokenService
    {
        /// <summary>
        /// Creates a token for the specified user with the given roles.
        /// </summary>
        /// <param name="user">The user for whom the token is created.</param>
        /// <param name="roles">The roles assigned to the user.</param>
        /// <returns>The created token.</returns>
        string CreateTokenAsync(IdentityUser user, List<string> roles);
    }
}
