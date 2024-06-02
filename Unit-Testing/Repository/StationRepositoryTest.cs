using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Enum.Train;
using RailwayReservation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unit_Testing.Repository
{
    [TestFixture]
    public class StationRepositoryTest
    {
        private RailwayReservationdbContext _context;
        private IStationRepository _stationRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RailwayReservationdbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RailwayReservationdbContext(options);
            _stationRepository = new StationRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddStation_Success()
        {
            // Arrange
            var station = new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Test Station",
                StationCode = "TST",
                StationType = StationType.Junction,
                Pincode = 123456
            };

            // Act
            var result = await _stationRepository.Add(station);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(station.StationId, result.StationId);
        }

        [Test]
        public async Task GetStationById_Success()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var station = new Station
            {
                StationId = stationId,
                StationName = "Test Station",
                StationCode = "TST",
                StationType = StationType.Junction,
                Pincode = 123456
            };

            await _stationRepository.Add(station);

            // Act
            var result = await _stationRepository.Get(stationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(stationId, result.StationId);
        }

        [Test]
        public async Task GetAllStations_Success()
        {
            // Arrange
            var station1 = new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Station 1",
                StationCode = "ST1",
                StationType = StationType.Terminal,
                Pincode = 111111
            };

            var station2 = new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Station 2",
                StationCode = "ST2",
                StationType = StationType.Junction,
                Pincode = 222222
            };

            await _stationRepository.Add(station1);
            await _stationRepository.Add(station2);

            // Act
            var result = await _stationRepository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateStation_Success()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var station = new Station
            {
                StationId = stationId,
                StationName = "Old Station Name",
                StationCode = "OLD",
                StationType = StationType.Junction,
                Pincode = 123456
            };

            await _stationRepository.Add(station);

            station.StationName = "Updated Station Name";
            station.StationCode = "UPD";

            // Act
            var result = await _stationRepository.Update(station);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(station.StationId, result.StationId);
            Assert.AreEqual("Updated Station Name", result.StationName);
            Assert.AreEqual("UPD", result.StationCode);
        }


        [Test]
        public async Task GetStationsByIds_Success()
        {
            // Arrange
            var station1 = new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Station 1",
                StationCode = "ST1",
                StationType = StationType.Terminal,
                Pincode = 111111
            };

            var station2 = new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Station 2",
                StationCode = "ST2",
                StationType = StationType.Junction,
                Pincode = 222222
            };

            await _stationRepository.Add(station1);
            await _stationRepository.Add(station2);

            var stationIds = new List<Guid> { station1.StationId, station2.StationId };

            // Act
            var result = await _stationRepository.Get(station1.StationId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DeleteStation_NotFound()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _stationRepository.Delete(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("Error occurred while deleting station: Station not found"));
        }
    }
}
