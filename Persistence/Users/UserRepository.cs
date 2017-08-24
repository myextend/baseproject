using Domain.Membership.Users.Interfaces;
using System;
using Domain.Membership.Users;

namespace Persistence.Users
{
    public class UserRepository : ICreateUserRepository
    {
        public User Create(User user)
        {
            throw new NotImplementedException();
        }
    }
}
