using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Enum.Ticket;

namespace RailwayReservation.Model.Domain
{
    /// <summary>
    /// Represents a ticket for a train journey including details about the journey,
    /// booking date, passengers, total amount, and status.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Gets or sets the ticket ID.
        /// </summary>
        [Required]
        public Guid TicketId { get; set; }

        /// <summary>
        /// Gets or sets the train ID.
        /// </summary>
        [Required]
        public Guid TrainId { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the source station ID.
        /// </summary>
        [Required]
        public Guid Source { get; set; }

        /// <summary>
        /// Gets or sets the destination station ID.
        /// </summary>
        [Required]
        public Guid Destination { get; set; }

        /// <summary>
        /// Gets or sets the journey date.
        /// </summary>
        [Required]
        public DateTime JourneyDate { get; set; }

        /// <summary>
        /// Gets or sets the booking date. Defaults to the current date and time.
        /// </summary>
        [Required]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the list of passengers.
        /// </summary>
        [Required]
        public List<Passenger> Passengers { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the ticket.
        /// </summary>
        [Range(0.0, double.MaxValue, ErrorMessage = "Total amount must be a non-negative value.")]
        public double TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the payment status of the ticket.
        /// </summary>
        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the ticket status. Defaults to Pending.
        /// </summary>
        [Required]
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Pending;

        /// <summary>
        /// Gets or sets the train associated with the ticket.
        /// </summary>
        [Required]
        public Train Train { get; set; }

        /// <summary>
        /// Gets or sets the user who booked the ticket.
        /// </summary>
        [Required]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the source station.
        /// </summary>
        [Required]
        public Station SourceStation { get; set; }

        /// <summary>
        /// Gets or sets the destination station.
        /// </summary>
        [Required]
        public Station DestinationStation { get; set; }
    }
}
