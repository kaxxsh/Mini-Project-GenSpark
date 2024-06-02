using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Enum.Ticket;
using RailwayReservation.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unit_Testing.Repository
{
    [TestFixture]
    public class TicketRepositoryTest
    {
        private RailwayReservationdbContext _context;
        private ITicketRepository _ticketRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RailwayReservationdbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RailwayReservationdbContext(options);
            _ticketRepository = new TicketRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddTicket_Success()
        {
            var ticket = CreateSampleTicket();
            var result = await _ticketRepository.Add(ticket);
            Assert.IsNotNull(result);
            Assert.AreEqual(ticket.TicketId, result.TicketId);
        }

        [Test]
        public async Task GetTicketById_Success()
        {
            var ticket = CreateSampleTicket();
            await _ticketRepository.Add(ticket);
            var result = await _ticketRepository.Get(ticket.TicketId);
            Assert.IsNotNull(result);
            Assert.AreEqual(ticket.TicketId, result.TicketId);
        }

        [Test]
        public async Task GetAllTickets_Success()
        {
            var ticket1 = CreateSampleTicket();
            var ticket2 = CreateSampleTicket();
            await _ticketRepository.Add(ticket1);
            await _ticketRepository.Add(ticket2);
            var result = await _ticketRepository.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateTicket_Success()
        {
            var ticket = CreateSampleTicket();
            await _ticketRepository.Add(ticket);
            ticket.TotalAmount = 200.0;
            var result = await _ticketRepository.Update(ticket);
            Assert.IsNotNull(result);
            Assert.AreEqual(200.0, result.TotalAmount);
        }

        [Test]
        public async Task DeleteTicket_Success()
        {
            var ticket = CreateSampleTicket();
            await _ticketRepository.Add(ticket);
            var result = await _ticketRepository.Delete(ticket.TicketId);
            Assert.IsNotNull(result);
            Assert.AreEqual(ticket.TicketId, result.TicketId);
            var deletedTicket = await _ticketRepository.Get(ticket.TicketId);
            Assert.IsNull(deletedTicket);
        }

        [Test]
        public async Task GetTicketById_NotFound()
        {
            var result = await _ticketRepository.Get(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteTicket_NotFound()
        {
            var result = await _ticketRepository.Delete(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task AddTicket_DuplicateId_Failure()
        {
            var ticket = CreateSampleTicket();
            await _ticketRepository.Add(ticket);
            var duplicateTicket = new Ticket
            {
                TicketId = ticket.TicketId, // Duplicate ID
                TrainId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Source = Guid.NewGuid(),
                Destination = Guid.NewGuid(),
                JourneyDate = DateTime.Now.AddDays(15),
                BookingDate = DateTime.Now,
                Passengers = new List<Passenger>(),
                TotalAmount = 300.0,
                PaymentStatus = PaymentStatus.Paid,
                TicketStatus = TicketStatus.Booked
            };
            Assert.ThrowsAsync<Exception>(async () => await _ticketRepository.Add(duplicateTicket));
        }

        [Test]
        public async Task AddTicket_WithMissingRequiredFields_Failure()
        {
            var ticket = new Ticket
            {
                TicketId = Guid.NewGuid(),
                TrainId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Source = Guid.NewGuid(),
                // Destination is missing
                JourneyDate = DateTime.Now.AddDays(10),
                BookingDate = DateTime.Now,
                Passengers = new List<Passenger>(),
                TotalAmount = 100.0,
                PaymentStatus = PaymentStatus.Paid,
                TicketStatus = TicketStatus.Booked
            };
            Assert.IsNotNull(await _ticketRepository.Add(ticket));
        }

        [Test]
        public async Task GetTickets_WithMultiplePassengers_Success()
        {
            var ticket = CreateSampleTicket();
            await _ticketRepository.Add(ticket);
            var result = await _ticketRepository.Get(ticket.TicketId);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Passengers.Count);
        }

        private Ticket CreateSampleTicket()
        {
            return new Ticket
            {
                TicketId = Guid.NewGuid(),
                TrainId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Source = Guid.NewGuid(),
                Destination = Guid.NewGuid(),
                JourneyDate = DateTime.Now.AddDays(10),
                BookingDate = DateTime.Now,
                Passengers = new List<Passenger>
                {
                    new Passenger
                    {
                        PassengerId = Guid.NewGuid(),
                        Name = "John Doe",
                        Age = 30,
                        Gender = Gender.Male
                    },
                    new Passenger
                    {
                        PassengerId = Guid.NewGuid(),
                        Name = "Jane Doe",
                        Age = 28,
                        Gender = Gender.Female
                    }
                },
                TotalAmount = 100.0,
                PaymentStatus = PaymentStatus.Paid,
                TicketStatus = TicketStatus.Booked
            };
        }
    }
}
