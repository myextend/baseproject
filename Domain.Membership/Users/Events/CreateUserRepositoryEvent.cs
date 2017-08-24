using Domain.Membership.Users.Interfaces;
using Domain.SharedKernel.Interfaces;

namespace Domain.Membership.Users.Events
{
    public class CreateUserRepositoryEvent : IDomainEvent
    {
        public ICreateUserRepository Instance {get;set;}
    }
}
