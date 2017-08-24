using Autofac.Core;
using Domain.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.SharedKernel
{
    public static class DomainEvents
    { 
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> actions;
        
        public static Container Container { get; set; }

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
           if (actions == null)
              actions = new List<Delegate>();

           actions.Add(callback);
       }

       public static void ClearCallbacks()
       {
           actions = null;
       }

       public static void Raise<T>(T args) where T : IDomainEvent
       {
            if (Container != null)
            {
                var handlers = Container.ComponentRegistry.Registrations.SelectMany(x => x.Services).OfType<IHandles<T>>();

                if (handlers == null || !handlers.Any())
                    throw new Exception("There is no registration IoC types for: " + args.ToString());

                foreach (var handler in handlers)
                    handler.Handle(args);
            }

          if (actions != null)
             foreach (var action in actions)
                  if (action is Action<T>)
                      ((Action<T>)action)(args);

       }
    } 
}
