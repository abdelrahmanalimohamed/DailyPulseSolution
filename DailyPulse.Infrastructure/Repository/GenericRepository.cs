using DailyPulse.Application.Abstraction;
using DailyPulse.Application.Common;
using DailyPulse.Domain.Common;
using DailyPulse.Infrastructure.Extensions;
using DailyPulse.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Linq.Expressions;

namespace DailyPulse.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext _context)
        {
            this._context = _context;   
        }
        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
			var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
			return entity ?? throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found");
		}
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
        }
        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate ,cancellationToken);
        }
        public async Task<IEnumerable<T>> CallStoredProc<T>(string storedProcName, DynamicParameters parameters, CancellationToken cancellationToken = default)
        {
            using var connection = new MySqlConnection(_context.Database.GetConnectionString());

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                await connection.OpenAsync(cancellationToken);
            }

            return await connection.QueryAsync<T>(
                storedProcName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<T>> FindWithIncludeAsync(
           Expression<Func<T, bool>> predicate = null,
           List<Expression<Func<T, object>>> includes = null,
           CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<T> AddAsyncWithReturnEntity(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
			// Attach the entity to the context (if not already tracked)
			_context.Attach(entity);

			// Loop through the properties and mark only the ones that are changed as modified
			var entityEntry = _context.Entry(entity);

			foreach (var property in entityEntry.OriginalValues.Properties)
			{
				// Only mark the property as modified if it's explicitly set in the current entity
				if (entityEntry.Property(property.Name).IsModified)
				{
					entityEntry.Property(property.Name).IsModified = true;
				}
			}

			// Save the changes to the database
			await _context.SaveChangesAsync(cancellationToken);
		}
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

		public async Task<PagedList<T>> FindWithIncludePaginated(
			Expression<Func<T, bool>> predicate,
			List<Expression<Func<T, object>>> includes,
			RequestParameters requestParameters,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _context.Set<T>();

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return await query.ToPagedListAsync(
			requestParameters.PageNumber,
			requestParameters.PageSize,
			cancellationToken
		);
		}
		public async Task<T> FindWithIncludeSingleOrDefault(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes, CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _context.Set<T>();

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return await query.SingleOrDefaultAsync(cancellationToken);
		}
	}
}