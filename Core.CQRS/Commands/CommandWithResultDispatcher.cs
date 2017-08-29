using System.Threading.Tasks;
using Core.CQRS.Commands.Interfaces;
using System;
using Core.IoC.Interfaces;

namespace Core.CQRS.Commands
{
    public class CommandWithResultDispatcher : ICommandWithResultDispatcher
    {
        private readonly IIoCContainer _container;

        public CommandWithResultDispatcher(IIoCContainer container)
        {
            _container = container;
        }

        public async Task<ICommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command)
           where TCommand : ICommand
           where TCommandResult : ICommandResult
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command), "Command can not be null.");

            ICommandWithResultHandler<TCommand, TCommandResult> handler = _container.Resolve<ICommandWithResultHandler<TCommand, TCommandResult>>();
            return await handler.HandleAsync(command);
        }

    }
}
