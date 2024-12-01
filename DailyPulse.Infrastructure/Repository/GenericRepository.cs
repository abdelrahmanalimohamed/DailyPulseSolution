using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using DailyPulse.Application.Abstraction;
using DailyPulse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> CallStoredProc(string storedProcName, object[] parameters, CancellationToken cancellationToken = default)
        {
            var sql = CreateInterpolatedSql(storedProcName, parameters);
            return await _context.Set<T>().FromSqlInterpolated(sql).ToListAsync(cancellationToken);
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
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        private FormattableString CreateInterpolatedSql(string storedProcName, object[] parameters)
        {
            var paramPlaceholders = string.Join(", ", parameters.Select((_, index) => $"{{{index}}}"));
            return FormattableStringFactory.Create($"CALL {storedProcName}({paramPlaceholders})", parameters);
        }
    }
}
