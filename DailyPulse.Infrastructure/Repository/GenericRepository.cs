using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using DailyPulse.Application.Abstraction;
using DailyPulse.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        //private formattablestring createinterpolatedsql(string storedprocname, object[] parameters)
        //{
        //    var paramplaceholders = string.join(", ", parameters.select((_, index) => $"{{{index}}}"));
        //    return formattablestringfactory.create($"call {storedprocname}({paramplaceholders})", parameters);
        //}


        private (FormattableString, object[]) CreateInterpolatedSql(string storedProcName, object parameters)
        {
            var parametersDictionary = new Dictionary<string, object>();
            if (parameters != null)
            {
                foreach (var property in parameters.GetType().GetProperties())
                {
                    parametersDictionary.Add(property.Name, property.GetValue(parameters));
                }
            }

            var paramPlaceholders = string.Join(", ", parametersDictionary.Select((_, index) => $"{{{index}}}"));
            var sql = FormattableStringFactory.Create($"EXEC {storedProcName} {paramPlaceholders}", parametersDictionary.Values.ToArray());
            return (sql, parametersDictionary.Values.ToArray());
        }
    }
}