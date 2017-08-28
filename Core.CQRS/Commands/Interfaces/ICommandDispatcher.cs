using System.Threading.Tasks;

namespace Core.CQRS.Commands.Interfaces
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
