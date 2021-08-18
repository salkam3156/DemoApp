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
        Task<DataAccessResult> AddAsync(TEntity collection);
        Task<DataAccessResult> RemoveAsync(TEntity collection);
        Task<Result<TEntity, DataAccessResult>> GetAsync(int id);
        Task<Result<IEnumerable<TEntity>, DataAccessResult>> GetAllAsync();
        Task<Result<TEntity, DataAccessResult>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
