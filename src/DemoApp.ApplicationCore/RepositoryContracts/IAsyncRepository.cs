using DemoApp.ApplicationCore.Enums;
using DemoApp.ApplicationCore.GeneralAbstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoApp.ApplicationCore.RepositoryContracts
{
    public interface IAsyncReposiroty<TEntity> where TEntity : class
    {
        Task<RepositoryFailure> AddAsync(TEntity collection);
        Task<RepositoryFailure> RemoveAsync(TEntity collection);
        Task<Result<TEntity, RepositoryFailure>> GetAsync(int id);
        Task<Result<IEnumerable<TEntity>, RepositoryFailure>> GetAllAsync();
        Task<Result<TEntity, RepositoryFailure>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
