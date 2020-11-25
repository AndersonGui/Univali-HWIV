using HW.Database.Context;
using HW.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW.Database.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly HWContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(HWContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> FindById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
            return entity;
        }

        public virtual async Task Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        private void DetachLocal(Func<TEntity, bool> predicate)
        {
            var local = Db.Set<TEntity>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                Db.Entry(local).State = EntityState.Detached;
            }
        }

    }
}
