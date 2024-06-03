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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IStationService _station;
        private readonly IUserService _user;
        private readonly ITrainService _train;
        private readonly ITicketService _ticket;

        public UserController(IStationService station, IUserService user, ITrainService train, ITicketService ticket)
        {
            _station = station;
            _user = user;
            _train = train;
            _ticket = ticket;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var data = await _user.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var data = await _user.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get booked tickets by user ID.
        /// </summary>
        [HttpGet]
        [Route("User/Ticket/{id}")]
        [ProducesResponseType(typeof(List<TicketResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookedTicketbyUser(Guid id)
        {
            try
            {
                var data = await _ticket.GetBookedTicketbyUser(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get canceled tickets by user ID.
        /// </summary>
        [HttpGet]
        [Route("User/Ticket/Cancel/{id}")]
        [ProducesResponseType(typeof(List<TicketResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCanceledTicketByUser(Guid id)
        {
            try
            {
                var data = await _ticket.GetCanceledTicketByUser(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all stations.
        /// </summary>
        [HttpGet]
        [Route("Station")]
        [ProducesResponseType(typeof(StationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStation()
        {
            try
            {
                var data = await _station.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a station by ID.
        /// </summary>
        [HttpGet]
        [Route("Station/{id}")]
        [ProducesResponseType(typeof(StationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStation(Guid id)
        {
            try
            {
                var data = await _station.Get(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get trains by query parameters.
        /// </summary>
        [HttpGet]
        [Route("Train")]
        [ProducesResponseType(typeof(TrainResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrainByQuiry(String From, String To)
        {
            try
            {
                var data = await _train.GetTrain(From, To);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a train by ID.
        /// </summary>
        [HttpGet]
        [Route("Train/{id}")]
        [ProducesResponseType(typeof(TrainResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrain(Guid id)
        {
            try
            {
                var data = await _train.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a ticket by ID.
        /// </summary>
        [HttpGet]
        [Route("Ticket/{id}")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            try
            {
                var data = await _ticket.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Add a new ticket.
        /// </summary>
        [HttpPost]
        [Route("Ticket")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTicket(TicketRequestDto ticket)
        {
            try
            {
                var data = await _ticket.Add(ticket);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update a ticket status.
        /// </summary>
        [HttpPut]
        [Route("Ticket/Cancel/{id}")]
        [ProducesResponseType(typeof(TicketResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTicket(Guid id, TicketStatus ticket)
        {
            try
            {
                var data = await _ticket.CancelTicket(id, ticket);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
