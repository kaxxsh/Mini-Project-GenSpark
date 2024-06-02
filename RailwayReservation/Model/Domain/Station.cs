using System;
using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Domain
{
    /// <summary>
    /// Represents a railway station with details such as name, code, type, and pincode.
    /// </summary>
    public class Station
    {
        /// <summary>
        /// Gets or sets the station ID.
        /// </summary>
        [Required]
        public Guid StationId { get; set; }

        /// <summary>
        /// Gets or sets the station name.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Station name cannot be longer than 100 characters.")]
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        [Required]
        [StringLength(10, ErrorMessage = "Station code cannot be longer than 10 characters.")]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the station.
        /// </summary>
        [Required]
        public StationType StationType { get; set; }

        /// <summary>
        /// Gets or sets the pincode of the station.
        /// </summary>
        [Range(100000, 999999, ErrorMessage = "Pincode must be a 6-digit number.")]
        public int Pincode { get; set; }
    }
}
