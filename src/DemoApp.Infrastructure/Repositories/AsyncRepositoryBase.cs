using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
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
        public AsyncRepositoryBase(DbContext ctx) 
            => Ctx = ctx;

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
                //TODO: add logging
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
            var queryResults = await Ctx
                .Set<TEntity>()
                .ToListAsync();

            return new Result<IEnumerable<TEntity>, DataAccessResult>(queryResults.AsReadOnly());
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
