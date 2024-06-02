using System;
using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Dtos.Train.Seat
{
    /// <summary>
    /// Represents a data transfer object for seat details.
    /// </summary>
    public class SeatDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the seat.
        /// </summary>
        [Required(ErrorMessage = "Seat ID is required")]
        public Guid SeatId { get; set; }

        /// <summary>
        /// Gets or sets the seat number.
        /// </summary>
        [Required(ErrorMessage = "Seat number is required")]
        [StringLength(10, ErrorMessage = "Seat number cannot exceed 10 characters")]
        public string SeatNumber { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the passenger assigned to the seat.
        /// Nullable if the seat is unassigned.
        /// </summary>
        public Guid? PassengerId { get; set; }

        /// <summary>
        /// Gets or sets the status of the seat.
        /// </summary>
        [Required(ErrorMessage = "Seat status is required")]
        public SeatStatus Status { get; set; }
    }
}
