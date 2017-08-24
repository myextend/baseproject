using System.Collections.ObjectModel;

namespace Domain.Membership.Users.Interfaces
{
    public interface IPasswordAdditionalValidatorService
    {
        bool IsValid(string password, out ReadOnlyCollection<string> errorMessages);
    }
}
