using System.Threading.Tasks;

namespace Core.CQRS.Commands.Interfaces
{
    public interface ICommandWithResultDispatcher
    {
        Task<ICommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command)
            where TCommand : ICommand
            where TCommandResult : ICommandResult;
    }
}
