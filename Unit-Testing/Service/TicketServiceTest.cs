using AutoMapper;
using Moq;
using NUnit.Framework;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Interface.Service;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Ticket;
using RailwayReservation.Model.Enum.Ticket;
using RailwayReservation.Model.Enum.Train;
using RailwayReservation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unit_Testing.Service
{
    [TestFixture]
    public class TicketServiceTest
    {
        private Mock<ITicketRepository> _ticketRepositoryMock;
        private Mock<ITrainRepository> _trainRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IEmailSender> _emailSenderMock;
        private ITicketService _ticketService;

        [SetUp]
        public void Setup()
        {
            _ticketRepositoryMock = new Mock<ITicketRepository>();
            _trainRepositoryMock = new Mock<ITrainRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _emailSenderMock = new Mock<IEmailSender>();
            _ticketService = new TicketService(
                _ticketRepositoryMock.Object,
                _trainRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _emailSenderMock.Object);
        }

        [Test]
        public async Task AddTicket_ShouldThrowException_WhenTrainNotFound()
        {
            var ticketRequestDto = CreateSampleTicketRequestDto();
            _trainRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((Train)null);

            var ex = Assert.ThrowsAsync<NullReferenceException>(async () => await _ticketService.Add(ticketRequestDto));
            Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
        }

        [Test]
        public async Task AddTicket_ShouldThrowException_WhenNotEnoughSeats()
        {
            var ticketRequestDto = CreateSampleTicketRequestDto();
            var train = CreateSampleTrain(1);
            _trainRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(train);

            var ex = Assert.ThrowsAsync<System.NullReferenceException>(async () => await _ticketService.Add(ticketRequestDto));
            Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
        }

        [Test]
        public async Task AddTicket_ShouldThrowException_WhenUserNotFound()
        {
            var ticketRequestDto = CreateSampleTicketRequestDto();
            var train = CreateSampleTrain(10);
            _trainRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(train);
            _userRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((User)null);

            var ex = Assert.ThrowsAsync<NullReferenceException>(async () => await _ticketService.Add(ticketRequestDto));
            Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
        }

        [Test]
        public async Task AddTicket_ShouldThrowException_WhenInsufficientBalance()
        {
            var ticketRequestDto = CreateSampleTicketRequestDto();
            var train = CreateSampleTrain(10, 100);
            var user = CreateSampleUser(50);
            _trainRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(train);
            _userRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(user);

            var ex = Assert.ThrowsAsync<NullReferenceException>(async () => await _ticketService.Add(ticketRequestDto));
            Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message);
        }



        [Test]
        public async Task ApproveTicket_ShouldThrowException_WhenTicketNotFound()
        {
            var ticketId = Guid.NewGuid();
            _ticketRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((Ticket)null);

            var ex = Assert.ThrowsAsync<Exception>(async () => await _ticketService.ApproveTicket(ticketId, TicketStatus.Booked));
            Assert.AreEqual("Ticket not found", ex.Message);
        }

        [Test]
        public async Task CancelTicket_ShouldThrowException_WhenTicketNotFound()
        {
            var ticketId = Guid.NewGuid();
            _ticketRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((Ticket)null);

            Assert.IsNotNull(_ticketService.CancelTicket(ticketId, TicketStatus.Cancelled));
        }

        [Test]
        public async Task DeleteTicket_ShouldThrowException_WhenTicketNotFound()
        {
            var ticketId = Guid.NewGuid();
            _ticketRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((Ticket)null);

            Assert.IsNotNull(_ticketService.Delete(ticketId));
        }

        [Test]
        public async Task DeleteTicket_ShouldDeleteTicketSuccessfully()
        {
            var ticketId = Guid.NewGuid();
            var ticket = CreateSampleTicket();
            var ticketResponseDto = CreateSampleTicketResponseDto();

            _ticketRepositoryMock.Setup(repo => repo.Get(ticketId)).ReturnsAsync(ticket);
            _ticketRepositoryMock.Setup(repo => repo.Delete(ticketId)).ReturnsAsync(ticket);
            _mapperMock.Setup(mapper => mapper.Map<TicketResponseDto>(ticket)).Returns(ticketResponseDto);

            var result = await _ticketService.Delete(ticketId);

            Assert.IsNotNull(result);
            Assert.AreEqual(ticketResponseDto.TicketId, result.TicketId);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllTickets()
        {
            var tickets = new List<Ticket> { CreateSampleTicket(), CreateSampleTicket() };
            var ticketResponseDtos = tickets.Select(t => CreateSampleTicketResponseDto()).ToList();

            _ticketRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(tickets);
            _mapperMock.Setup(mapper => mapper.Map<List<TicketResponseDto>>(tickets)).Returns(ticketResponseDtos);

            var result = await _ticketService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(ticketResponseDtos.Count, result.Count);
            Assert.AreEqual(ticketResponseDtos, result);
        }

        [Test]
        public async Task GetById_ShouldThrowException_WhenTicketNotFound()
        {
            var ticketId = Guid.NewGuid();
            _ticketRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((Ticket)null);

            Assert.IsNotNull(_ticketService.GetById(ticketId));
        }

        [Test]
        public async Task GetById_ShouldReturnTicket()
        {
            var ticketId = Guid.NewGuid();
            var ticket = CreateSampleTicket();
            var ticketResponseDto = CreateSampleTicketResponseDto();

            _ticketRepositoryMock.Setup(repo => repo.Get(ticketId)).ReturnsAsync(ticket);
            _mapperMock.Setup(mapper => mapper.Map<TicketResponseDto>(ticket)).Returns(ticketResponseDto);

            var result = await _ticketService.GetById(ticketId);

            Assert.IsNotNull(result);
            Assert.AreEqual(ticketResponseDto.TicketId, result.TicketId);
        }

        [Test]
        public async Task BookedTicket_ShouldReturnBookedTickets()
        {
            var trainId = Guid.NewGuid();
            var tickets = new List<Ticket>
        {
            new Ticket { TicketId = Guid.NewGuid(), TrainId = trainId, TicketStatus = TicketStatus.Booked },
            new Ticket { TicketId = Guid.NewGuid(), TrainId = trainId, TicketStatus = TicketStatus.Pending }
        };
            var bookedTickets = tickets.Where(t => t.TicketStatus == TicketStatus.Booked).ToList();
            var ticketResponseDtos = bookedTickets.Select(t => CreateSampleTicketResponseDto()).ToList();

            _ticketRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(tickets);
            _mapperMock.Setup(mapper => mapper.Map<List<TicketResponseDto>>(bookedTickets)).Returns(ticketResponseDtos);

            var result = await _ticketService.BookedTicket(trainId);

            Assert.IsNotNull(result);
            Assert.AreEqual(ticketResponseDtos.Count, result.Count);
            Assert.AreEqual(ticketResponseDtos, result);
        }

        [Test]
        public async Task GetBookedTicketByUser_ShouldReturnBookedTicketsForUser()
        {
            var userId = Guid.NewGuid();
            var tickets = new List<Ticket>
        {
            new Ticket { TicketId = Guid.NewGuid(), UserId = userId, TicketStatus = TicketStatus.Booked },
            new Ticket { TicketId = Guid.NewGuid(), UserId = userId, TicketStatus = TicketStatus.Pending }
        };
            var bookedTickets = tickets.Where(t => t.TicketStatus == TicketStatus.Booked).ToList();
            var ticketResponseDtos = bookedTickets.Select(t => CreateSampleTicketResponseDto()).ToList();

            _ticketRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(tickets);
            _mapperMock.Setup(mapper => mapper.Map<List<TicketResponseDto>>(bookedTickets)).Returns(ticketResponseDtos);

            var result = await _ticketService.GetBookedTicketbyUser(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(ticketResponseDtos.Count, result.Count());
            Assert.AreEqual(ticketResponseDtos, result);
        }

        [Test]
        public async Task GetCanceledTicketByUser_ShouldReturnCanceledTicketsForUser()
        {
            var userId = Guid.NewGuid();
            var tickets = new List<Ticket>
        {
            new Ticket { TicketId = Guid.NewGuid(), UserId = userId, TicketStatus = TicketStatus.Cancelled },
            new Ticket { TicketId = Guid.NewGuid(), UserId = userId, TicketStatus = TicketStatus.Pending }
        };
            var cancelledTickets = tickets.Where(t => t.TicketStatus == TicketStatus.Cancelled).ToList();
            var ticketResponseDtos = cancelledTickets.Select(t => CreateSampleTicketResponseDto()).ToList();

            _ticketRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(tickets);
            _mapperMock.Setup(mapper => mapper.Map<List<TicketResponseDto>>(cancelledTickets)).Returns(ticketResponseDtos);

            var result = await _ticketService.GetCanceledTicketByUser(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(ticketResponseDtos.Count, result.Count);
            Assert.AreEqual(ticketResponseDtos, result);
        }

        [Test]
        public async Task PendingTicket_ShouldReturnPendingTickets()
        {
            var tickets = new List<Ticket>
        {
            new Ticket { TicketId = Guid.NewGuid(), TicketStatus = TicketStatus.Pending },
            new Ticket { TicketId = Guid.NewGuid(), TicketStatus = TicketStatus.Booked }
        };
            var pendingTickets = tickets.Where(t => t.TicketStatus == TicketStatus.Pending).ToList();
            var ticketResponseDtos = pendingTickets.Select(t => CreateSampleTicketResponseDto()).ToList();

            _ticketRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(tickets);
            _mapperMock.Setup(mapper => mapper.Map<List<TicketResponseDto>>(pendingTickets)).Returns(ticketResponseDtos);

            var result = await _ticketService.PendingTicket();

            Assert.IsNotNull(result);
            Assert.AreEqual(ticketResponseDtos.Count, result.Count);
            Assert.AreEqual(ticketResponseDtos, result);
        }

        [Test]
        public async Task Update_ShouldThrowException_WhenTicketNotFound()
        {
            var ticketId = Guid.NewGuid();
            var ticketRequestDto = CreateSampleTicketRequestDto();
            _ticketRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync((Ticket)null);

            var ex = Assert.ThrowsAsync<Exception>(async () => await _ticketService.Update(ticketId, ticketRequestDto));
            Assert.AreEqual("Ticket not found", ex.Message);
        }


        private TicketRequestDto CreateSampleTicketRequestDto()
        {
            return new TicketRequestDto
            {
                TrainId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
        }

        private Ticket CreateSampleTicket()
        {
            return new Ticket
            {
                TicketId = Guid.NewGuid(),
                TrainId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                TicketStatus = TicketStatus.Pending
            };
        }

        private TicketResponseDto CreateSampleTicketResponseDto()
        {
            return new TicketResponseDto
            {
                TicketId = Guid.NewGuid(),
                TrainId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                TicketStatus = TicketStatus.Pending
            };
        }

        private Train CreateSampleTrain(int availableSeats, decimal ticketPrice = 0)
        {
            return new Train
            {
                TrainId = Guid.NewGuid(),
                TrainName = "Sample Train",
                AvailableSeats = availableSeats
            };
        }

        private User CreateSampleUser(decimal walletBalance, decimal bookedTicketAmount = 0)
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                Email = "sample@example.com"
            };
        }
    }
}
