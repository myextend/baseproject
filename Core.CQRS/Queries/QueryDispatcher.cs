using Core.CQRS.Queries.Interfaces;
using Core.IoC.Interfaces;
using System;
using System.Threading.Tasks;

namespace Core.CQRS.Queries
{
    public class QueryDispatcher
    {
        private readonly IIoCContainer _container;

        public QueryDispatcher(IIoCContainer container)
        {
            _container = container;
        }

        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
           where TQuery : IQuery
           where TResult : IQueryResult
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            IQueryHandler<TQuery, TResult> handler = _container.Resolve<IQueryHandler<TQuery, TResult>>();

            return handler.RetrieveAsync(query);
        }

    }
}
