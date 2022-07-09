using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsingMocks.Data;
using UsingMocks.Models;
using Xunit;

namespace UsingMocks.Tests
{
    public class UsersServiceTests
    {
        private readonly UsersService _sut;
        private readonly Mock<IUsersDb> _usersDb;

        public UsersServiceTests()
        {
            _usersDb = new Mock<IUsersDb>();
            _sut = new UsersService(_usersDb.Object);
        }

        [Fact]
        public async void UsersService_NoForename_ThrowsArgumentNullException()
        {
            //Arrange
            User user = new(surname: "Man");

            _usersDb
                .Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();
            
            //Act /Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.GreetUser(user));
        }

        [Fact]
        public async void UsersService_NoSurname_ThrowsArgumentNullException()
        {
            //Arrange
            User user = new("Spider");

            _usersDb
                .Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

            //Act /Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.GreetUser(user));
        }

        [Fact]
        public async void UsersService_InvalidUserReturned_ThrowsGenericException()
        {
            //Arrange
            User user = new("Spider", "Man");

            _usersDb
                .Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

            //Act /Assert
            await Assert.ThrowsAsync<Exception>(async () => await _sut.GreetUser(user));
        }


        [Fact]
        public void UsersService_FoundUser_ReturnsExpectedMessage()
        {
            //Arrange
            User user = new("Spider", "Man");
            List<User> users = new() { user };

            _usersDb
                .Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(users);

            //Act
            Task<string> result = _sut.GreetUser(user);

            //Assert
            Assert.True(result.Result == "Welcome back Spider Man");
        }

        [Fact]
        public void UsersService_NewUser_ReturnsExpectedMessage()
        {
            //Arrange
            User user = new("Peter", "Parker");

            _usersDb
                .Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<User>());

            //Act
            Task<string> result = _sut.GreetUser(user);

            //Assert
            Assert.True(result.Result == "Welcome Peter Parker");
        }
    }
}