using System.Threading.Tasks;
using Core.CQRS.Commands.Interfaces;
using System;
using Core.IoC.Interfaces;

namespace Core.CQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private IIoCContainer _context;

        public CommandDispatcher(IIoCContainer context)
        {
            _context = context;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command),"Command can not be null.");

            ICommandHandler<T> handler = _context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}
