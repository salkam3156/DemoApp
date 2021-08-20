using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Repositories
{
    public abstract class AsyncRepositoryBase<TEntity> : IAsyncReposiroty<TEntity> where TEntity : class
    {
        protected readonly DbContext Ctx;
        private readonly ILogger<AsyncRepositoryBase<TEntity>> _logger;

        public AsyncRepositoryBase(DbContext ctx, ILogger<AsyncRepositoryBase<TEntity>> logger) 
            => (Ctx, _logger) = (ctx, logger);

        public Task<DataAccessResult> AddAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<Result<TEntity, DataAccessResult>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity queryResult = default;
            try
            {
                queryResult = await Ctx
                    .Set<TEntity>()
                    .Where(predicate)
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex) when (ex is InvalidOperationException io || ex is  ArgumentNullException an)
            {
                _logger.LogWarning(ex, $"{nameof(AsyncRepositoryBase<TEntity>)}: Could not find the resource");
                return new Result<TEntity, DataAccessResult>(DataAccessResult.GeneralExecutionFailure);
            }

            return queryResult switch
            {
                TEntity => new Result<TEntity, DataAccessResult>(queryResult),
                null => new Result<TEntity, DataAccessResult>(DataAccessResult.NotFound),
            };
        }

        public async Task<Result<IEnumerable<TEntity>, DataAccessResult>> GetAllAsync()
        {

            IEnumerable<TEntity> queryResults = default;
            try 
            { 
                queryResults = await Ctx
                    .Set<TEntity>()
                    .ToListAsync();
            }
            catch (Exception ex) when (ex is ArgumentNullException an)
            {
                _logger.LogWarning(ex, $"{nameof(AsyncRepositoryBase<TEntity>)}: Could not find the resources");
                return new Result<IEnumerable<TEntity>, DataAccessResult>(DataAccessResult.GeneralExecutionFailure);
            }

            return queryResults.Any() switch
            {
                true => new Result<IEnumerable<TEntity>, DataAccessResult>(queryResults),
                _ => new Result<IEnumerable<TEntity>, DataAccessResult>(DataAccessResult.NotFound),
            };
        }

        public Task<Result<TEntity, DataAccessResult>> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<DataAccessResult> RemoveAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
