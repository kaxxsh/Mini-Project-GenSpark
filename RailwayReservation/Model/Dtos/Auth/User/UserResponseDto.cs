namespace RailwayReservation.Model.Dtos.Auth.User
{
    public class UserResponseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double WalletBalance { get; set; }
    }
}
