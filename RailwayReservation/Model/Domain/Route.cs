using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.Model.Domain
{
    /// <summary>
    /// Represents a train route including source and destination stations,
    /// distance, duration, and a list of stations on the route.
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Gets or sets the route ID.
        /// </summary>
        [Required]
        public Guid RouteId { get; set; }

        /// <summary>
        /// Gets or sets the train ID.
        /// </summary>
        [Required]
        public Guid TrainId { get; set; }

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
        /// Gets or sets the distance of the route.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Distance must be greater than 0.")]
        public int Distance { get; set; }

        /// <summary>
        /// Gets or sets the duration of the route.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the list of stations in the route.
        /// </summary>
        [Required]
        public List<Station> Stations { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="Route"/> class.
        /// </summary>
        public Route()
        {
            Stations = new List<Station>();
        }
    }
}
