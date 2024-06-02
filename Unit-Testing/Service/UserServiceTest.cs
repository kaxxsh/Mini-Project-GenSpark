using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using RailwayReservation.Model.Dtos.Auth.User;
using RailwayReservation.Services;

namespace Unit_Testing.Service
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task AddMoney_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userService.AddMoney(userId, 100));
            Assert.AreEqual("User not found", ex.Message);
        }

        [Test]
        public async Task AddMoney_ShouldIncreaseWalletBalance()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 50.0;
            var amountToAdd = 100.0;
            var expectedBalance = initialBalance + amountToAdd;
            var user = new User { UserId = userId, WalletBalance = initialBalance };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.Update(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddMoney(userId, amountToAdd);

            // Assert
            Assert.AreEqual(expectedBalance, result.WalletBalance);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User> { new User(), new User() };
            var userResponseDtos = new List<UserResponseDto> { new UserResponseDto(), new UserResponseDto() };
            _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(users);
            _mapperMock.Setup(mapper => mapper.Map<List<UserResponseDto>>(users)).Returns(userResponseDtos);

            // Act
            var result = await _userService.GetAll();

            // Assert
            Assert.AreEqual(userResponseDtos.Count, result.Count);
        }

        [Test]
        public async Task GetById_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userService.GetById(userId));
            Assert.AreEqual("User not found", ex.Message);
        }

        [Test]
        public async Task GetById_ShouldReturnUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { UserId = userId };
            var userResponseDto = new UserResponseDto { UserId = userId };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<UserResponseDto>(user)).Returns(userResponseDto);

            // Act
            var result = await _userService.GetById(userId);

            // Assert
            Assert.AreEqual(userId, result.UserId);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 50.0;
            var amountToAdd = 100.0;
            var expectedBalance = initialBalance + amountToAdd;
            var user = new User { UserId = userId, WalletBalance = initialBalance };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.Update(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddMoney(userId, amountToAdd);

            // Assert
            Assert.AreEqual(expectedBalance, result.WalletBalance);
        }

        [Test]
        public async Task Test2()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 50.0;
            var amountToAdd = 100.0;
            var expectedBalance = initialBalance + amountToAdd;
            var user = new User { UserId = userId, WalletBalance = initialBalance };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.Update(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddMoney(userId, amountToAdd);

            // Assert
            Assert.AreEqual(expectedBalance, result.WalletBalance);
        }

        [Test]
        public async Task Test3()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 50.0;
            var amountToAdd = 100.0;
            var expectedBalance = initialBalance + amountToAdd;
            var user = new User { UserId = userId, WalletBalance = initialBalance };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.Update(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddMoney(userId, amountToAdd);

            // Assert
            Assert.AreEqual(expectedBalance, result.WalletBalance);
        }

        [Test]
        public async Task Test4()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 50.0;
            var amountToAdd = 100.0;
            var expectedBalance = initialBalance + amountToAdd;
            var user = new User { UserId = userId, WalletBalance = initialBalance };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.Update(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddMoney(userId, amountToAdd);

            // Assert
            Assert.AreEqual(expectedBalance, result.WalletBalance);
        }

        [Test]
        public async Task Test5()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var initialBalance = 50.0;
            var amountToAdd = 100.0;
            var expectedBalance = initialBalance + amountToAdd;
            var user = new User { UserId = userId, WalletBalance = initialBalance };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.Update(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddMoney(userId, amountToAdd);

            // Assert
            Assert.AreEqual(expectedBalance, result.WalletBalance);
        }
    }
}
