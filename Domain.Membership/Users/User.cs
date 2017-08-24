using System;
using Domain.Membership.Users.Interfaces;
using Domain.SharedKernel.Interfaces;
using Domain.SharedKernel;
using Domain.Membership.Users.Events;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Domain.Membership.Users
{
    public class User : IEntity<int>
    {
        private static Lazy<IPasswordEncryptorService> _passwordEncryptor = new Lazy<IPasswordEncryptorService>(
                                                                                        () => { return CreatePasswordEncryptorService(); });
        private static Lazy<IPasswordAdditionalValidatorService> _passwordValidator = new Lazy<IPasswordAdditionalValidatorService>(
                                                                                        () => { return CreatePasswordValidatorService(); });


        public int Id { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        
        public void ChangePassword(string newPassword)
        {
           // chyba wracamy do koncepcji eventów bo za dużo interfejsów w metodach - będzie upierdliwe używanie ich!!!!
        }

        protected User()
        {

        }

        /*
        public string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            // return MD5Helper.CreateSaltMD5Hash(password);
            return string.Empty;
        }
        */

        private static IPasswordEncryptorService CreatePasswordEncryptorService()
        {
            var createPasswordEncryptorServiceEvent = new CreatePasswordEncryptorServiceEvent();
            DomainEvents.Raise(createPasswordEncryptorServiceEvent);

            return createPasswordEncryptorServiceEvent.Instance;
        }

        private static IPasswordAdditionalValidatorService CreatePasswordValidatorService()
        {
            var createPasswordValidatorServiceEvent = new CreatePasswordValidatorServiceEvent();
            DomainEvents.Raise(createPasswordValidatorServiceEvent);

            return createPasswordValidatorServiceEvent.Instance;
        }

        
        private static ICreateUserRepository CreateUserRepository()
        {
            var CreateUserRepositoryEvent = new CreateUserRepositoryEvent();
            DomainEvents.Raise(CreateUserRepositoryEvent);

            return CreateUserRepositoryEvent.Instance;
        }

        //
        // Factory
        //
        public static User Create(string username, string password, out ReadOnlyCollection<string> errorMessages)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(username))
                errors.Add("Username - can't be empty");

            if (string.IsNullOrWhiteSpace(password))
                errors.Add("Password - can't be empty");

            var passwordValidatorService = CreatePasswordValidatorService();

            if (!passwordValidatorService.IsValid(password, out ReadOnlyCollection<string> passwordErrorMessages))
                errors.AddRange(passwordErrorMessages);

            if (errors.Count > 0)
            {
                errorMessages = errors.AsReadOnly();
                return null;
            }

            var passwordEncryptorService = CreatePasswordEncryptorService();

            User newUser = new User()
            {
                Username = username,
                Password = passwordEncryptorService.EncryptPassword(password)
            };

            errorMessages = errors.AsReadOnly();
            return CreateUserRepository().Create(newUser);
        }

    }
}
