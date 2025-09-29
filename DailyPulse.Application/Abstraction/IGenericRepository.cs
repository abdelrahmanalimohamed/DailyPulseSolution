using DailyPulse.Application.Common;
using DailyPulse.Domain.Common;
using Dapper;
using System.Linq.Expressions;

namespace DailyPulse.Application.Abstraction
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes, CancellationToken cancellationToken = default);
		Task<T> FindWithIncludeSingleOrDefault(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes, CancellationToken cancellationToken = default);
		Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
		Task<T> AddAsyncWithReturnEntity (T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<TResult>> CallStoredProc<TResult>(string storedProcName, DynamicParameters parameters, CancellationToken cancellationToken = default);
        Task<PagedList<T>> FindWithIncludePaginated(
            Expression<Func<T, bool>> predicate,
            List<Expression<Func<T, object>>> includes,
            RequestParameters requestParameters,
            CancellationToken cancellationToken = default);
	}
}