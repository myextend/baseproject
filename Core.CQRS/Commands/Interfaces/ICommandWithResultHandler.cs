using System.Threading.Tasks;

namespace Core.CQRS.Commands.Interfaces
{
    public interface ICommandWithResultHandler<in TCommand, TCommandResult> 
        where TCommand : ICommand
        where TCommandResult : ICommandResult
    {
        Task<TCommandResult> HandleAsync(TCommand command);
    }
}
