using System;
using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.Model.Domain
{
    /// <summary>
    /// Represents a user in the railway reservation system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the wallet balance of the user.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Wallet balance cannot be negative")]
        public double WalletBalance { get; set; }
    }
}
