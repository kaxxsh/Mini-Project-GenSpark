using RailwayReservation.Model.Enum.Train;
using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.Model.Dtos.Train.Station
{
    /// <summary>
    /// Represents a data transfer object for a station request.
    /// </summary>
    public class StationRequestDto
    {
        /// <summary>
        /// Gets or sets the name of the station.
        /// </summary>
        [Required(ErrorMessage = "Station Name is required")]
        [StringLength(100, ErrorMessage = "Station Name cannot exceed 100 characters")]
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the code of the station.
        /// </summary>
        [Required(ErrorMessage = "Station Code is required")]
        [StringLength(10, ErrorMessage = "Station Code cannot exceed 10 characters")]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the station.
        /// </summary>
        [Required(ErrorMessage = "Station Type is required")]
        public StationType StationType { get; set; }

        /// <summary>
        /// Gets or sets the pincode of the station location.
        /// </summary>
        [Required(ErrorMessage = "Pincode is required")]
        [Range(100000, 999999, ErrorMessage = "Pincode must be a 6-digit number")]
        public int Pincode { get; set; }
    }
}
