using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.Model.Dtos.Train.Route
{
    /// <summary>
    /// Represents a data transfer object for a route request.
    /// </summary>
    public class RouteRequestDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the source station.
        /// </summary>
        [Required(ErrorMessage = "Source station ID is required")]
        public Guid Source { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the destination station.
        /// </summary>
        [Required(ErrorMessage = "Destination station ID is required")]
        public Guid Destination { get; set; }

        /// <summary>
        /// Gets or sets the distance of the route in kilometers.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Distance must be greater than 0")]
        public int Distance { get; set; }

        /// <summary>
        /// Gets or sets the duration of the route in minutes.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the list of station IDs included in the route.
        /// </summary>
        [Required(ErrorMessage = "Station IDs are required")]
        [MinLength(1, ErrorMessage = "At least one station ID is required")]
        public List<Guid> StationIds { get; set; }
    }
}
