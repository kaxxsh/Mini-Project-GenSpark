using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RailwayReservation.Context;
using RailwayReservation.Interface.Repository;
using RailwayReservation.Model.Domain;
using RailwayReservation.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unit_Testing.Repository
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private RailwayReservationdbContext _context;
        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RailwayReservationdbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new RailwayReservationdbContext(options);
            _userRepository = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddUser_Success()
        {
            var user = CreateSampleUser();
            var result = await _userRepository.Add(user);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserId, result.UserId);
        }

        [Test]
        public async Task GetUserById_Success()
        {
            var user = CreateSampleUser();
            await _userRepository.Add(user);
            var result = await _userRepository.Get(user.UserId);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserId, result.UserId);
        }

        [Test]
        public async Task GetAllUsers_Success()
        {
            var user1 = CreateSampleUser();
            var user2 = CreateSampleUser();
            await _userRepository.Add(user1);
            await _userRepository.Add(user2);
            var result = await _userRepository.GetAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateUser_Success()
        {
            var user = CreateSampleUser();
            await _userRepository.Add(user);
            user.UserName = "Updated User Name";
            var result = await _userRepository.Update(user);
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated User Name", result.UserName);
        }

        [Test]
        public async Task DeleteUser_Success()
        {
            var user = CreateSampleUser();
            await _userRepository.Add(user);
            var result = await _userRepository.Delete(user.UserId);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserId, result.UserId);
            var deletedUser = await _userRepository.Get(user.UserId);
            Assert.IsNull(deletedUser);
        }

        [Test]
        public async Task GetUserById_NotFound()
        {
            var result = await _userRepository.Get(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteUser_NotFound()
        {
            var result = await _userRepository.Delete(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateUser_NotFound()
        {
            var user = CreateSampleUser();
            user.UserId = Guid.NewGuid(); // Ensure this ID is not in the database
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userRepository.Update(user));
            Assert.AreEqual("Error occurred while updating the user.", ex.Message);
        }

        [Test]
        public async Task AddUser_WithExistingId()
        {
            var user = CreateSampleUser();
            await _userRepository.Add(user);
            // Try adding another user with the same ID
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userRepository.Add(user));
            Assert.AreEqual("An error occurred.", ex.Message);
        }

        [Test]
        public async Task AddUser_InvalidEmail()
        {
            var user = CreateSampleUser();
            user.Email = "invalidemail";
            Assert.IsNotNull(await _userRepository.Add(user));
        }

        [Test]
        public async Task AddUser_InvalidPhoneNumber()
        {
            var user = CreateSampleUser();
            user.PhoneNumber = "invalidphone";
            Assert.IsNotNull(await _userRepository.Add(user));
        }

        [Test]
        public async Task AddUser_NegativeWalletBalance()
        {
            var user = CreateSampleUser();
            user.WalletBalance = -1;
            Assert.IsNotNull(await _userRepository.Add(user));
        }

        [Test]
        public async Task UpdateUser_InvalidEmail()
        {
            var user = CreateSampleUser();
            await _userRepository.Add(user);
            user.Email = "invalidemail";
            Assert.IsNotNull(await _userRepository.Update(user));
        }

        [Test]
        public async Task UpdateUser_InvalidPhoneNumber()
        {
            var user = CreateSampleUser();
            await _userRepository.Add(user);
            user.PhoneNumber = "invalidphone";
            Assert.IsNotNull(await _userRepository.Update(user));
        }

        [Test]
        public async Task UpdateUser_NegativeWalletBalance()
        {
            var user = CreateSampleUser();
            await _userRepository.Add(user);
            user.WalletBalance = -1;
            Assert.IsNotNull(await _userRepository.Update(user));

        }

        [Test]
        public async Task AddUser_WithoutUserName()
        {
            var user = CreateSampleUser();
            user.UserName = null;
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userRepository.Add(user));
            Assert.AreEqual("Error occurred while adding the user.", ex.Message);
        }

        [Test]
        public async Task AddUser_WithoutEmail()
        {
            var user = CreateSampleUser();
            user.Email = null;
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userRepository.Add(user));
            Assert.AreEqual("Error occurred while adding the user.", ex.Message);
        }

        [Test]
        public async Task AddUser_WithoutPhoneNumber()
        {
            var user = CreateSampleUser();
            user.PhoneNumber = null;
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userRepository.Add(user));
            Assert.AreEqual("Error occurred while adding the user.", ex.Message);
        }

        [Test]
        public async Task GetAllUsers_Empty()
        {
            var result = await _userRepository.GetAll();
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        private User CreateSampleUser()
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                UserName = "Sample User",
                Email = "sampleuser@example.com",
                PhoneNumber = "1234567890",
                WalletBalance = 100.0
            };
        }
    }
}
