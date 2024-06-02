using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.Model.Dtos.Ticket.Passenger
{
    /// <summary>
    /// Represents a data transfer object for passenger details in a ticket request.
    /// </summary>
    public class PassengerRequestDto
    {
        /// <summary>
        /// Gets or sets the name of the passenger.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age of the passenger.
        /// </summary>
        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120")]
        public int Age { get; set; }
    }
}
