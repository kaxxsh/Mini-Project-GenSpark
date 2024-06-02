using System;
using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Enum.Ticket;

namespace RailwayReservation.Model.Domain
{
    /// <summary>
    /// Represents a passenger with details such as name, age, gender, and assigned seat.
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// Gets or sets the passenger ID.
        /// </summary>
        [Required]
        public Guid PassengerId { get; set; }

        /// <summary>
        /// Gets or sets the ticket ID.
        /// </summary>
        [Required]
        public Guid TicketId { get; set; }

        /// <summary>
        /// Gets or sets the name of the passenger.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the passenger.
        /// </summary>
        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150")]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the gender of the passenger.
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the seat ID.
        /// </summary>
        public Guid? SeatId { get; set; }

        /// <summary>
        /// Gets or sets the seat assigned to the passenger.
        /// </summary>
        public Seat Seat { get; set; }

        // Add any additional properties or methods here
    }
}
