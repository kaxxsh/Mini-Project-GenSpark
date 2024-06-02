using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Dtos.Ticket.Passenger;
using RailwayReservation.Model.Enum.Ticket;

namespace RailwayReservation.Model.Dtos.Ticket
{
    /// <summary>
    /// Represents a data transfer object for ticket requests.
    /// </summary>
    public class TicketRequestDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the train.
        /// </summary>
        [Required(ErrorMessage = "Train ID is required")]
        public Guid TrainId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the source station.
        /// </summary>
        [Required(ErrorMessage = "Source ID is required")]
        public Guid Source { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the destination station.
        /// </summary>
        [Required(ErrorMessage = "Destination ID is required")]
        public Guid Destination { get; set; }

        /// <summary>
        /// Gets or sets the date of the journey.
        /// </summary>
        [Required(ErrorMessage = "Journey date is required")]
        public DateTime JourneyDate { get; set; }

        /// <summary>
        /// Gets or sets the list of passengers for the ticket.
        /// </summary>
        [Required(ErrorMessage = "At least one passenger is required")]
        [MinLength(1, ErrorMessage = "At least one passenger is required")]
        public List<PassengerRequestDto> Passengers { get; set; }
    }
}
