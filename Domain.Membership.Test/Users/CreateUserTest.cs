using Domain.Membership.Users;
using Domain.Membership.Users.Events;
using Domain.Membership.Users.Interfaces;
using Domain.SharedKernel;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Domain.Membership.Test.Users
{
    public class CreateUserTest
    {
       private string _username;
       private string _password;

       private Mock<IPasswordAdditionalValidatorService> _mockPasswordAdditionalValidatorService;
       private Mock<IPasswordEncryptorService> _mockPasswordEncryptorService;
       private Mock<ICreateUserRepository> _mockCreateUserRepository;

       public CreateUserTest()
       {
            _mockPasswordAdditionalValidatorService = new Mock<IPasswordAdditionalValidatorService>();
            _mockPasswordEncryptorService = new Mock<IPasswordEncryptorService>();
            _mockCreateUserRepository = new Mock<ICreateUserRepository>();

            DomainEvents.Register<CreatePasswordValidatorServiceEvent>((e) => { e.Instance = _mockPasswordAdditionalValidatorService.Object; });
            DomainEvents.Register<CreatePasswordEncryptorServiceEvent>((e) => { e.Instance = _mockPasswordEncryptorService.Object; });
            DomainEvents.Register<CreateUserRepositoryEvent>((e) => { e.Instance = _mockCreateUserRepository.Object; });
        }

       [Fact]
       public void CreateUser_UsernameCannotBeEmpty()
       {
            // Prepare
            _username = "";
            _password = "pass";

            // Pass additional requirements
            // - no requirements
            ReadOnlyCollection<string> errorMessagesEmpty = new List<string>().AsReadOnly();

            _mockPasswordAdditionalValidatorService.Setup(x => 
                                                    x.IsValid(
                                                                It.IsAny<string>(),
                                                                out errorMessagesEmpty))
                                                    .Returns(true);

            // Action
            var newUser = User.Create(_username, _password, out ReadOnlyCollection<string> errorMessages);

            // Excpected result
            Assert.Null(newUser);
            Assert.True(errorMessages.Count == 1);
       }

        [Fact]
        public void CreateUser_PasswordCannotBeEmpty()
        {
            // Prepare
            _username = "user";
            _password = "";

            // Pass additional requirements
            // - no requirements
            ReadOnlyCollection<string> errorMessagesEmpty = new List<string>().AsReadOnly();

            _mockPasswordAdditionalValidatorService.Setup(x =>
                                                    x.IsValid(
                                                                It.IsAny<string>(),
                                                                out errorMessagesEmpty))
                                                    .Returns(true);

            // Action
            var newUser = User.Create(_username, _password, out ReadOnlyCollection<string> errorMessages);

            // Excpected result
            Assert.Null(newUser);
            Assert.True(errorMessages.Count == 1);
        }

        [Fact]
        public void CreateUser_PasswordCannotBeEmptyAndWithMinLength()
        {
            // Prepare
            _username = "user";
            _password = "";

            // Pass requirements
            // - min password length 5 chars
            ReadOnlyCollection<string> errorMessagesEmpty = new List<string>(new string[] { "Password min. length is 5 chars" }).AsReadOnly();

            _mockPasswordAdditionalValidatorService.Setup(x =>
                                                    x.IsValid(
                                                                It.Is<string>(v => v.Length < 5),
                                                                out errorMessagesEmpty))
                                                    .Returns(false);

            // Action
            var newUser = User.Create(_username, _password, out ReadOnlyCollection<string> errorMessages);

            // Excpected result
            Assert.Null(newUser);
            Assert.True(errorMessages.Count == 2);
        }

        [Fact]
        public void CreateUser_PasswordCustomAdditionalValidation()
        {
            // Prepare
            _username = "user";
            _password = "test";

            // Pass requirements
            // - min password length 5 chars
            ReadOnlyCollection<string> errorMessagesEmpty = new List<string>(new string[] { "Password min. length is 5 chars" }).AsReadOnly();

            _mockPasswordAdditionalValidatorService.Setup(x =>
                                                    x.IsValid(
                                                                It.Is<string>(v => v.Length < 5),
                                                                out errorMessagesEmpty))
                                                    .Returns(false);

            // Action
            var newUser = User.Create(_username, _password, out ReadOnlyCollection<string> errorMessages);
            

            // Expected result
            Assert.Null(newUser);
            Assert.True(errorMessages.Count == 1);
            Assert.Contains(errorMessages, x => {return x == "Password min. length is 5 chars"; });
        }
        
        [Fact]
        public void CreateUser_Success()
        {
            // Prepare
            _username = "user";
            _password = "pass";
            TestUser createdUser = new TestUser(1, _username, "_encryptedPassword_");
            _mockPasswordEncryptorService.Setup(x => 
                                                    x.EncryptPassword(_password))
                                         .Returns("_encryptedPassword_");
            
            // Pass is correct
            ReadOnlyCollection<string> noErrorMessages = new List<string>().AsReadOnly();

            _mockPasswordAdditionalValidatorService.Setup(x =>
                                                    x.IsValid(
                                                                It.Is<string>(v => v == _password),
                                                                out noErrorMessages))
                                          .Returns(true);

            _mockCreateUserRepository.Setup(x => 
                                                x.Create(It.Is<User>(f => 
                                                                        f.Id == 0 &&
                                                                        f.Username == createdUser.Username &&
                                                                        f.Password == createdUser.Password)))
                                     .Returns(createdUser);
            // Action
            User newUser = User.Create(_username, _password, out ReadOnlyCollection<string> errorMessages);

            // Expected result
            Assert.NotNull(newUser);
            Assert.Equal(newUser.Id, createdUser.Id);
            Assert.Equal(newUser.Password, createdUser.Password);
            Assert.Equal(newUser.Username, createdUser.Username);
            Assert.Empty(errorMessages);
        }
    }


}
