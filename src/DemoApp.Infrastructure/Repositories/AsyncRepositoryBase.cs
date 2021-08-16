using DemoApp.ApplicationCore.Enumerations;
using DemoApp.ApplicationCore.GeneralAbstractions;
using DemoApp.ApplicationCore.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Repositories
{
    public class AsyncRepositoryBase<TEntity> : IAsyncReposiroty<TEntity> where TEntity : class
    {
        public Task<RepositoryFailure> AddAsync(TEntity collection)
        {
            throw new System.NotImplementedException();
        }
        
        public Task<Option<TEntity, RepositoryFailure>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new System.NotImplementedException();
        }

        public Task<Option<IEnumerable<TEntity>, RepositoryFailure>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Option<TEntity, RepositoryFailure>> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<RepositoryFailure> RemoveAsync(TEntity collection)
        {
            throw new System.NotImplementedException();
        }
    }
}
