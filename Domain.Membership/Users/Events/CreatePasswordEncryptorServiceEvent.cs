using Domain.Membership.Users.Interfaces;
using Domain.SharedKernel.Interfaces;

namespace Domain.Membership.Users.Events
{
    public class CreatePasswordEncryptorServiceEvent : IDomainEvent
    {
        public IPasswordEncryptorService Instance {get;set;}
    }
}
