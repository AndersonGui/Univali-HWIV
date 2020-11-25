using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task<TEntity> Add(TEntity entity);

        Task<TEntity> FindById(int id);

        Task<ICollection<TEntity>> FindAll();

        Task Remove(TEntity entity);

        Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
