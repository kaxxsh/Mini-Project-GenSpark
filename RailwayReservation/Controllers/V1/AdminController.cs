using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Ticket;
using RailwayReservation.Model.Dtos.Train;
using RailwayReservation.Model.Dtos.Train.Station;
using RailwayReservation.Model.Enum.Ticket;
using RailwayReservation.Model.Error;

namespace RailwayReservation.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IStationService _station;
        private readonly IUserService _user;
        private readonly ITrainService _train;
        private readonly ITicketService _ticket;

        public AdminController(IStationService station, IUserService user, ITrainService train, ITicketService ticket)
        {
            _station = station;
            _user = user;
            _train = train;
            _ticket = ticket;
        }

        /// <summary>
        /// Add money to a user's account.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="Amount">The amount of money to add.</param>
        /// <returns>The updated user object.</returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserAddMoney(Guid id, double Amount)
        {
            try
            {
                var data = await _user.AddMoney(id, Amount);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Add a new station.
        /// </summary>
        /// <param name="station">The station details.</param>
        /// <returns>The added station object.</returns>
        [HttpPost]
        [Route("Station")]
        [ProducesResponseType(typeof(StationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddStation(StationRequestDto station)
        {
            try
            {
                var data = await _station.Add(station);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing station.
        /// </summary>
        /// <param name="id">The ID of the station to update.</param>
        /// <param name="station">The updated station details.</param>
        /// <returns>The updated station object.</returns>
        [HttpPut]
        [Route("Station/{id}")]
        [ProducesResponseType(typeof(StationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStation(Guid id, StationRequestDto station)
        {
            try
            {
                var data = await _station.Update(id, station);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a station.
        /// </summary>
        /// <param name="id">The ID of the station to delete.</param>
        /// <returns>The deleted station object.</returns>
        [HttpDelete]
        [Route("Station/{id}")]
        [ProducesResponseType(typeof(StationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStation(Guid id)
        {
            try
            {
                var data = await _station.Delete(id);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all trains.
        /// </summary>
        /// <returns>A list of all train objects.</returns>
        [HttpGet]
        [Route("Train")]
        [ProducesResponseType(typeof(List<TrainResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTrain()
        {
            try
            {
                var data = await _train.GetAll();
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add a new train.
        /// </summary>
        /// <param name="train">The train details.</param>
        /// <returns>The added train object.</returns>
        [HttpPost]
        [Route("Train")]
        [ProducesResponseType(typeof(TrainResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTrain(TrainRequestDto train)
        {
            try
            {
                var data = await _train.Add(train);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing train.
        /// </summary>
        /// <param name="id">The ID of the train to update.</param>
        /// <param name="train">The updated train details.</param>
        /// <returns>The updated train object.</returns>
        [HttpPut]
        [Route("Train/{id}")]
        [ProducesResponseType(typeof(TrainResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTrain(Guid id, TrainRequestDto train)
        {
            try
            {
                var data = await _train.Update(id, train);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a train.
        /// </summary>
        /// <param name="id">The ID of the train to delete.</param>
        /// <returns>The deleted train object.</returns>
        [HttpDelete]
        [Route("Train/{id}")]
        [ProducesResponseType(typeof(TrainResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTrain(Guid id)
        {
            try
            {
                var data = await _train.Delete(id);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Get all tickets.
        /// </summary>
        /// <returns>A list of all ticket objects.</returns>
        [HttpGet]
        [Route("Ticket")]
        [ProducesResponseType(typeof(List<TicketResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTicket()
        {
            try
            {
                var data = await _ticket.GetAll();
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to update.</param>
        /// <param name="ticket">The updated ticket details.</param>
        /// <returns>The updated ticket object.</returns>
        [HttpPut]
        [Route("Ticket/{id}")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTicket(Guid id, TicketRequestDto ticket)
        {
            try
            {
                var data = await _ticket.Update(id, ticket);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete a ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to delete.</param>
        /// <returns>The deleted ticket object.</returns>
        [HttpDelete]
        [Route("Ticket/{id}")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            try
            {
                var data = await _ticket.Delete(id);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all pending tickets.
        /// </summary>
        /// <returns>A list of all pending ticket objects.</returns>
        [HttpGet]
        [Route("Ticket/Pending")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PendingTicket()
        {
            try
            {
                var data = await _ticket.PendingTicket();
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Approve a ticket.
        /// </summary>
        /// <param name="id">The ID of the ticket to approve.</param>
        /// <param name="ticket">The ticket status.</param>
        /// <returns>The approved ticket object.</returns>
        [HttpGet]
        [Route("Ticket/Approve/{id}")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApproveTicket(Guid id, TicketStatus ticket)
        {
            try
            {
                var data = await _ticket.ApproveTicket(id, ticket);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all booked tickets for a train.
        /// </summary>
        /// <param name="TrainId">The ID of the train.</param>
        /// <returns>A list of all booked ticket objects for the specified train.</returns>
        [HttpGet]
        [Route("Ticket/Booked/{TrainId}")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BookedTicket(Guid TrainId)
        {
            try
            {
                var data = await _ticket.BookedTicket(TrainId);
                return Ok(data);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
