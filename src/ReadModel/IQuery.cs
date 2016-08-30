using System.Threading.Tasks;

namespace ReadModel
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> ExecuteAsync(TQuery query);
    }
}
