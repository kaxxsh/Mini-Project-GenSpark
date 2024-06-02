using System;
using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Domain
{
    /// <summary>
    /// Represents a seat in a train with details such as seat number, train ID, and seat status.
    /// </summary>
    public class Seat
    {
        /// <summary>
        /// Gets or sets the seat ID.
        /// </summary>
        [Required]
        public Guid SeatId { get; set; }

        /// <summary>
        /// Gets or sets the seat number.
        /// </summary>
        [Required]
        [StringLength(10, ErrorMessage = "Seat number cannot be longer than 10 characters.")]
        public string SeatNumber { get; set; }

        /// <summary>
        /// Gets or sets the train ID.
        /// </summary>
        [Required]
        public Guid TrainId { get; set; }

        /// <summary>
        /// Gets or sets the train.
        /// </summary>
        [Required]
        public Train Train { get; set; }

        /// <summary>
        /// Gets or sets the status of the seat.
        /// </summary>
        [Required]
        public SeatStatus Status { get; set; }
    }
}
