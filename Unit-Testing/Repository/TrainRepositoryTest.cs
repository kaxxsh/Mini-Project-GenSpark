using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Enum.Train;
using RailwayReservation.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unit_Testing.Repository
{
    [TestFixture]
    public class TrainRepositoryTest
    {
        private RailwayReservationdbContext _context;
        private ITrainRepository _trainRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RailwayReservationdbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RailwayReservationdbContext(options);
            _trainRepository = new TrainRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddTrain_Success()
        {
            var train = CreateSampleTrain();
            var result = await _trainRepository.Add(train);
            Assert.IsNotNull(result);
            Assert.AreEqual(train.TrainId, result.TrainId);
        }

        [Test]
        public async Task GetTrainById_Success()
        {
            var train = CreateSampleTrain();
            await _trainRepository.Add(train);
            var result = await _trainRepository.Get(train.TrainId);
            Assert.IsNotNull(result);
            Assert.AreEqual(train.TrainId, result.TrainId);
        }

        [Test]
        public async Task GetAllTrains_Success()
        {
            var train1 = CreateSampleTrain();
            var train2 = CreateSampleTrain();
            await _trainRepository.Add(train1);
            await _trainRepository.Add(train2);
            var result = await _trainRepository.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateTrain_Success()
        {
            var train = CreateSampleTrain();
            await _trainRepository.Add(train);
            train.TrainName = "Updated Train Name";
            var result = await _trainRepository.Update(train);
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Train Name", result.TrainName);
        }

        [Test]
        public async Task DeleteTrain_Success()
        {
            var train = CreateSampleTrain();
            await _trainRepository.Add(train);
            var result = await _trainRepository.Delete(train.TrainId);
            Assert.IsNotNull(result);
            Assert.AreEqual(train.TrainId, result.TrainId);
            var deletedTrain = await _trainRepository.Get(train.TrainId);
            Assert.IsNull(deletedTrain);
        }

        [Test]
        public async Task AddSeats_Success()
        {
            var train = CreateSampleTrain();
            await _trainRepository.Add(train);
            var seats = CreateSampleSeats(train);
            await _trainRepository.AddSeats(seats);
            foreach (var seat in seats)
            {
                var fetchedSeat = await _trainRepository.GetSeat(seat.SeatId);
                Assert.IsNotNull(fetchedSeat);
                Assert.AreEqual(seat.SeatId, fetchedSeat.SeatId);
            }
        }

        [Test]
        public async Task RemoveSeats_Success()
        {
            var train = CreateSampleTrain();
            await _trainRepository.Add(train);
            var seats = CreateSampleSeats(train);
            await _trainRepository.AddSeats(seats);
            await _trainRepository.RemoveSeats(seats);
            foreach (var seat in seats)
            {
                var fetchedSeat = await _trainRepository.GetSeat(seat.SeatId);
                Assert.IsNull(fetchedSeat);
            }
        }

        [Test]
        public async Task GetTrainById_NotFound()
        {
            var result = await _trainRepository.Get(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteTrain_NotFound()
        {
            var result = await _trainRepository.Delete(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateTrain_NotFound()
        {
            var train = CreateSampleTrain();
            train.TrainId = Guid.NewGuid(); // Ensure this ID is not in the database
            try
            {
                var result = await _trainRepository.Update(train);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Error updating train: Train not found", ex.Message);
            }
        }

        [Test]
        public async Task GetSeat_NotFound()
        {
            var result = await _trainRepository.GetSeat(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task AddTrain_WithExistingId()
        {
            var train = CreateSampleTrain();
            await _trainRepository.Add(train);
            // Try adding another train with the same ID
            try
            {
                await _trainRepository.Add(train);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual($"Error adding train: An item with the same key has already been added. Key: {train.TrainId}", ex.Message);
            }
        }

        [Test]
        public async Task UpdateTrain_WithInvalidId()
        {
            var train = CreateSampleTrain();
            train.TrainId = Guid.NewGuid(); // Ensure this ID is not in the database
            try
            {
                var result = await _trainRepository.Update(train);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Error updating train: Train not found", ex.Message);
            }
        }

        [Test]
        public async Task DeleteTrain_WithInvalidId()
        {
            var train = CreateSampleTrain();
            train.TrainId = Guid.NewGuid(); // Ensure this ID is not in the database
            var result = await _trainRepository.Delete(train.TrainId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task AddSeats_WithNullTrain()
        {
            // Try adding seats with a null train
            try
            {
                var seats = CreateSampleSeats(null);
                await _trainRepository.AddSeats(seats);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
            }
        }


        private Train CreateSampleTrain()
        {
            var sourceStation = new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Source Station",
                StationCode = "SRC"
            };

            var destinationStation = new Station
            {
                StationId = Guid.NewGuid(),
                StationName = "Destination Station",
                StationCode = "DST"
            };

            return new Train
            {
                TrainId = Guid.NewGuid(),
                TrainName = "Sample Train",
                TrainNumber = 12345,
                TrainType = TrainType.Express,
                TrainStatus = TrainStatus.Active,
                TotalSeats = 100,
                AvailableSeats = 100,
                Fare = 50,
                TrainRoute = new Route
                {
                    RouteId = Guid.NewGuid(),
                    Source = sourceStation.StationId,
                    Destination = destinationStation.StationId,
                    Distance = 500,
                    Duration = 300,
                    Stations = new List<Station>
                    {
                        new Station { StationId = Guid.NewGuid(), StationName = "Station 1", StationCode = "ST1" },
                        new Station { StationId = Guid.NewGuid(), StationName = "Station 2", StationCode = "ST2" }
                    },
                    SourceStation = sourceStation,
                    DestinationStation = destinationStation
                },
                Seats = new List<Seat>()
            };
        }

        private IEnumerable<Seat> CreateSampleSeats(Train train)
        {
            return new List<Seat>
            {
                new Seat
                {
                    SeatId = Guid.NewGuid(),
                    SeatNumber = "S1",
                    TrainId = train.TrainId,
                    Status = SeatStatus.Available,
                    Train = train
                },
                new Seat
                {
                    SeatId = Guid.NewGuid(),
                    SeatNumber = "S2",
                    TrainId = train.TrainId,
                    Status = SeatStatus.Available,
                    Train = train
                }
            };
        }
    }
}
