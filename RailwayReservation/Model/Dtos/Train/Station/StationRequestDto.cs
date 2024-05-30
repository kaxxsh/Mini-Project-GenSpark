using RailwayReservation.Model.Enum.Train;
using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.Model.Dtos.Train.Station
{
    public class StationRequestDto
    {
        [Required(ErrorMessage = "Station Name is required")]
        public string StationName { get; set; }

        [Required(ErrorMessage = "Station Code is required")]
        public string StationCode { get; set; }

        [Required(ErrorMessage = "Station Type is required")]
        public StationType StationType { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [Range(100000, 999999, ErrorMessage = "Pincode must be a 6-digit number")]
        public int Pincode { get; set; }
    }
}
