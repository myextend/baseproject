using System.Threading.Tasks;

namespace Core.CQRS.Queries.Interfaces
{
    public interface IQueryHandler<in TQuery, TQueryResult>
        where TQueryResult : IQueryResult
        where TQuery : IQuery
    {
        Task<TQueryResult> RetrieveAsync(TQuery query);
    }
}
