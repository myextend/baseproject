namespace Domain.Membership.Users.Interfaces
{
    public interface IPasswordEncryptorService
    {
        string EncryptPassword(string password);
    }
}
