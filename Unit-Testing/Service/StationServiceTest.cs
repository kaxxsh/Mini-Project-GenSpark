using AutoMapper;
using Moq;
using NUnit.Framework;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Train.Station;
using RailwayReservation.Model.Enum.Train;
using RailwayReservation.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unit_Testing.Service
{
    [TestFixture]
    public class StationServiceTest
    {
        private Mock<IStationRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private IStationService _stationService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IStationRepository>();
            _mapperMock = new Mock<IMapper>();
            _stationService = new StationService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task AddStation_Success()
        {
            var stationRequest = CreateSampleStationRequest();
            var station = CreateSampleStation();
            var stationDto = CreateSampleStationDto();

            _mapperMock.Setup(m => m.Map<Station>(stationRequest)).Returns(station);
            _repositoryMock.Setup(r => r.Add(station)).ReturnsAsync(station);
            _mapperMock.Setup(m => m.Map<StationDto>(station)).Returns(stationDto);

            var result = await _stationService.Add(stationRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(stationDto.StationId, result.StationId);
        }

        [Test]
        public async Task GetStationById_Success()
        {
            var station = CreateSampleStation();
            var stationResponse = CreateSampleStationResponseDto();

            _repositoryMock.Setup(r => r.Get(station.StationId)).ReturnsAsync(station);
            _mapperMock.Setup(m => m.Map<StationResponseDto>(station)).Returns(stationResponse);

            var result = await _stationService.Get(station.StationId);

            Assert.IsNotNull(result);
            Assert.AreEqual(stationResponse.StationName, result.StationName);
        }

        [Test]
        public async Task GetAllStations_Success()
        {
            var stations = new List<Station> { CreateSampleStation(), CreateSampleStation() };
            var stationResponses = stations.Select(s => CreateSampleStationResponseDto()).ToList();

            _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(stations);
            _mapperMock.Setup(m => m.Map<List<StationResponseDto>>(stations)).Returns(stationResponses);

            var result = await _stationService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task UpdateStation_Success()
        {
            var stationRequest = CreateSampleStationRequest();
            var station = CreateSampleStation();
            var stationDto = CreateSampleStationDto();

            _mapperMock.Setup(m => m.Map<Station>(stationRequest)).Returns(station);
            _repositoryMock.Setup(r => r.Update(station)).ReturnsAsync(station);
            _mapperMock.Setup(m => m.Map<StationDto>(station)).Returns(stationDto);

            var result = await _stationService.Update(station.StationId, stationRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(stationDto.StationId, result.StationId);
        }

        [Test]
        public async Task DeleteStation_Success()
        {
            var station = CreateSampleStation();
            var stationDto = CreateSampleStationDto();

            _repositoryMock.Setup(r => r.Delete(station.StationId)).ReturnsAsync(station);
            _mapperMock.Setup(m => m.Map<StationDto>(station)).Returns(stationDto);

            var result = await _stationService.Delete(station.StationId);

            Assert.IsNotNull(result);
            Assert.AreEqual(stationDto.StationId, result.StationId);
        }

        [Test]
        public async Task GetStationById_NotFound()
        {
            var stationId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.Get(stationId)).ReturnsAsync((Station)null);

            var result = await _stationService.Get(stationId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteStation_NotFound()
        {
            var stationId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.Delete(stationId)).ReturnsAsync((Station)null);

            var result = await _stationService.Delete(stationId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task AddStation_InvalidStationName()
        {
            var stationRequest = CreateSampleStationRequest();
            stationRequest.StationName = new string('a', 101); // Invalid length

            Assert.IsNull(await _stationService.Add(stationRequest));
        }

        [Test]
        public async Task AddStation_InvalidStationCode()
        {
            var stationRequest = CreateSampleStationRequest();
            stationRequest.StationCode = new string('a', 11); // Invalid length

            Assert.IsNull(await _stationService.Add(stationRequest));
        }

        [Test]
        public async Task AddStation_InvalidPincode()
        {
            var stationRequest = CreateSampleStationRequest();
            stationRequest.Pincode = 123; // Invalid pincode

            Assert.IsNull(await _stationService.Add(stationRequest));
        }


        [Test]
        public async Task AddStation_WithoutStationName()
        {
            var stationRequest = CreateSampleStationRequest();
            stationRequest.StationName = null;

            Assert.IsNull(await _stationService.Add(stationRequest));
        }

        [Test]
        public async Task AddStation_WithoutStationCode()
        {
            var stationRequest = CreateSampleStationRequest();
            stationRequest.StationCode = null;

            Assert.IsNull(await _stationService.Add(stationRequest));
        }


        [Test]
        public async Task GetAllStations_Empty()
        {
            _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<Station>());

            var result = await _stationService.GetAll();

            Assert.IsNull(result);
        }

        [Test]
        public void UpdateStation_NullData()
        {
            var stationId = Guid.NewGuid();

            Assert.IsNotNull(_stationService.Update(stationId, null));
        }

        private StationRequestDto CreateSampleStationRequest()
        {
            return new StationRequestDto
            {
                StationName = "Sample Station",
                StationCode = "SS123",
                StationType = StationType.Terminal,
                Pincode = 123456
            };
        }

        private Station CreateSampleStation()
        {
            return new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Sample Station",
                StationCode = "SS123",
                StationType = StationType.Terminal,
                Pincode = 123456
            };
        }

        private StationDto CreateSampleStationDto()
        {
            return new StationDto
            {
                StationId = Guid.NewGuid(),
                StationName = "Sample Station",
                StationCode = "SS123",
                StationType = StationType.Terminal,
                Pincode = 123456
            };
        }

        private StationResponseDto CreateSampleStationResponseDto()
        {
            return new StationResponseDto
            {
                StationId = Guid.NewGuid(),
                StationName = "Sample Station",
                StationCode = "SS123",
                StationType = StationType.Terminal,
                Pincode = 123456
            };
        }
    }
}
