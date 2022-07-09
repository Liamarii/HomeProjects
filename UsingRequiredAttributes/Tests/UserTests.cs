using UsingRequiredAttributes.Models;
using UsingRequiredAttributes.Support;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace UsingRequiredAttributes.Tests
{
    public class UserTests
    {
        private readonly IModelValidator _validator;

        public UserTests() => _validator = new ModelValidator();

        [Fact]
        public void User_FullUserModel_IsValid()
        {
            //Arrange
            User user = new()
            {
                Forename = "Charlie",
                Surname = "Day",
                Age = 32
            };

            //Act
            bool validModel = _validator.ValidateModel(user).Count == 0;

            //Assert
            Assert.True(validModel);
        }

        [Fact]
        [Description("Forename is a required attribute")]
        public void User_UserWithoutForename_IsInvalid()
        {
            //Arrange
            User user = new()
            {
                Surname = "Simpson",
                Age = 1
            };

            //Act
            bool validModel = _validator.ValidateModel(user).Count == 0;

            //Assert
            Assert.False(validModel);
        }

        [Fact]
        [Description("Age is a required attribute")]
        public void User_UserWithoutAge_IsInvalid()
        {
            //Arrange
            User user = new()
            {
                Forename = "Tony",
                Surname = "Soprano",
            };

            //Act
            bool validModel = _validator.ValidateModel(user).Count == 0;

            //Assert
            Assert.False(validModel);
        }

        [Fact]
        [Description("Surname is not a required attribute")]
        public void User_UserWithoutSurname_IsValid()
        {
            //Arrange
            User user = new()
            {
                Forename = "Tony",
                Age = 1
            };

            //Act
            bool validModel = _validator.ValidateModel(user).Count == 0;

            //Assert
            Assert.True(validModel);
        }

        [Fact]
        [Description("This shows how to see the expected errors")]
        public void User_InvalidModel_ReturnsExpectedErrors()
        {
            //Arrange
            User user = new();

            //Act
            List<ValidationResult>? result = new ModelValidator().ValidateModel(user);

            //Assert
            Assert.Contains(result, x => x.ErrorMessage == "The Forename field is required.");
            Assert.Contains(result, x => x.ErrorMessage == "The Age field is required.");
            Assert.True(result.Count == 2);
        }
    }
}