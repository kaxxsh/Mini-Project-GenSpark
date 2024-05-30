using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Train.Station;
using RailwayReservation.Model.Error;

namespace RailwayReservation.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IStationService _station;
        private readonly IUserService _user;

        public AdminController(IStationService station, IUserService user)
        {
            _station = station;
            _user = user;
        }

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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
