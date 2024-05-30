namespace RailwayReservation.Model.Dtos.Auth.User
{
    public class UserRequestDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double WalletBalance { get; set; }
    }
}
