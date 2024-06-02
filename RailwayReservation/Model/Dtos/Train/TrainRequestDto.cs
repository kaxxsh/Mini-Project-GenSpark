using System.ComponentModel.DataAnnotations;
using RailwayReservation.Model.Dtos.Train.Route;
using RailwayReservation.Model.Enum.Train;

namespace RailwayReservation.Model.Dtos.Train
{
    /// <summary>
    /// Represents a data transfer object for train details.
    /// </summary>
    public class TrainRequestDto
    {
        /// <summary>
        /// Gets or sets the name of the train.
        /// </summary>
        [Required(ErrorMessage = "Train name is required")]
        [StringLength(100, ErrorMessage = "Train name cannot exceed 100 characters")]
        public string TrainName { get; set; }

        /// <summary>
        /// Gets or sets the train number.
        /// </summary>
        [Required(ErrorMessage = "Train number is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Train number must be a positive integer")]
        public int TrainNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the train.
        /// </summary>
        [Required(ErrorMessage = "Train type is required")]
        public TrainType TrainType { get; set; }

        /// <summary>
        /// Gets or sets the status of the train.
        /// </summary>
        [Required(ErrorMessage = "Train status is required")]
        public TrainStatus TrainStatus { get; set; }

        /// <summary>
        /// Gets or sets the total number of seats on the train.
        /// </summary>
        [Required(ErrorMessage = "Total seats is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Total seats must be a positive integer")]
        public int TotalSeats { get; set; }

        /// <summary>
        /// Gets or sets the fare for the train.
        /// </summary>
        [Required(ErrorMessage = "Fare is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Fare must be a non-negative value")]
        public int Fare { get; set; }

        /// <summary>
        /// Gets or sets the route details for the train.
        /// </summary>
        [Required(ErrorMessage = "Train route is required")]
        public RouteRequestDto TrainRoute { get; set; }
    }
}
