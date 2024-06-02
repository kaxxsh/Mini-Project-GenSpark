using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Domain
{
    /// <summary>
    /// Represents a train with details such as name, number, type, status, total seats, available seats, fare, and route.
    /// </summary>
    public class Train
    {
        /// <summary>
        /// Gets or sets the train ID.
        /// </summary>
        [Required]
        public Guid TrainId { get; set; }

        /// <summary>
        /// Gets or sets the train name.
        /// </summary>
        [Required(ErrorMessage = "Train name is required")]
        [StringLength(100, ErrorMessage = "Train name cannot be longer than 100 characters.")]
        public string TrainName { get; set; }

        /// <summary>
        /// Gets or sets the train number.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Train number must be a positive integer.")]
        public int TrainNumber { get; set; }

        /// <summary>
        /// Gets or sets the train type.
        /// </summary>
        [Required]
        public TrainType TrainType { get; set; }

        /// <summary>
        /// Gets or sets the train status.
        /// </summary>
        [Required]
        public TrainStatus TrainStatus { get; set; }

        /// <summary>
        /// Gets or sets the total number of seats on the train.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Total seats must be a positive integer.")]
        public int TotalSeats { get; set; }

        /// <summary>
        /// Gets or sets the number of available seats on the train.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Available seats cannot be negative.")]
        public int AvailableSeats { get; set; }

        /// <summary>
        /// Gets or sets the fare for the train.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Fare must be a non-negative value.")]
        public int Fare { get; set; }

        /// <summary>
        /// Gets or sets the route of the train.
        /// </summary>
        [Required]
        public Route TrainRoute { get; set; }

        /// <summary>
        /// Gets or sets the collection of seats on the train.
        /// </summary>
        [Required]
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
