using DemoApp.ApplicationCore.Enumerations;
using DemoApp.ApplicationCore.GeneralAbstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoApp.ApplicationCore.RepositoryContracts
{
    public class IAsyncRepository
    {
        public interface IRepository<TEntity> where TEntity : class
        {
            Task<RepositoryFailure> AddAsync(TEntity collection);
            Task<RepositoryFailure> RemoveAsync(TEntity collection);
            Task<Option<TEntity, RepositoryFailure>> GetAsync(int id);
            Task<Option<IEnumerable<TEntity>, RepositoryFailure>> GetAllAsync();
            Task<Option<TEntity, RepositoryFailure>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        }
    }
}
