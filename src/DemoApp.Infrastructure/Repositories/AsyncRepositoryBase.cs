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

        public Task<RepositoryFailure> AddAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<Result<TEntity, RepositoryFailure>> FindAsync(Expression<Func<TEntity, bool>> predicate)
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
                return new Result<TEntity, RepositoryFailure>(RepositoryFailure.GeneralExecutionFailure);
            }

            return queryResult switch
            {
                TEntity => new Result<TEntity, RepositoryFailure>(queryResult),
                null => new Result<TEntity, RepositoryFailure>(RepositoryFailure.NotFound),
            };
        }

        public async Task<Result<IEnumerable<TEntity>, RepositoryFailure>> GetAllAsync()
        {
            var queryResults = await Ctx
                .Set<TEntity>()
                .ToListAsync();

            return new Result<IEnumerable<TEntity>, RepositoryFailure>(queryResults.AsReadOnly());
        }

        public Task<Result<TEntity, RepositoryFailure>> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<RepositoryFailure> RemoveAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
