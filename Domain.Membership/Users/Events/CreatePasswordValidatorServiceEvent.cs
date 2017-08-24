using Domain.Membership.Users.Interfaces;
using Domain.SharedKernel.Interfaces;

namespace Domain.Membership.Users.Events
{
    public class CreatePasswordValidatorServiceEvent : IDomainEvent
    {
        public IPasswordAdditionalValidatorService Instance {get;set;}
    }
}
