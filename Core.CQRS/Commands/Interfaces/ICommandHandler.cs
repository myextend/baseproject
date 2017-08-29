using System.Threading.Tasks;

namespace Core.CQRS.Commands.Interfaces
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
