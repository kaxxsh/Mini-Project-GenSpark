using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.Model.Dtos.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [MinLength(10, ErrorMessage = "Phone number must be at least 10 characters")]
        [MaxLength(10, ErrorMessage = "Phone number must be at most 10 characters")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "At least one role is required")]
        public string[] Roles { get; set; }

        // Optional: Add a constructor to initialize the Roles array
        public RegisterRequestDto()
        {
            Roles = new string[] { };
        }
    }
}
