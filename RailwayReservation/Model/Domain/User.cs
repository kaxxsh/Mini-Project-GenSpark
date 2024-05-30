using Microsoft.Extensions.Configuration.UserSecrets;

namespace RailwayReservation.Model.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double WalletBalance { get; set; } = 0.00;
    }
}
