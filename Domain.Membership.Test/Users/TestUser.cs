using Domain.Membership.Users;

namespace Domain.Membership.Test.Users
{
    public class TestUser : User
    {
        public TestUser(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }

}
