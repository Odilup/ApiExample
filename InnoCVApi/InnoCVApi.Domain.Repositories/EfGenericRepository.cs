using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InnoCVApi.Data.DbContext;
using InnoCVApi.Domain.Entities;
using InnoCVApi.Domain.Repositories.Interfaces;

namespace InnoCVApi.Domain.Repositories
{
    public class EfGenericRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseModel<TKey> 
    {
        private readonly BaseDbContext _dbContext;
        protected readonly DbSet<TEntity> DbSet;

        public EfGenericRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);

            return query.ToList();
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return Task.Run(() => GetAll(navigationProperties));
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);
            return query.AsQueryable().Where(predicate);
        }

        public virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return Task.Run(() => Find(predicate, navigationProperties));
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return DbSet.First(predicate);
        }

        public virtual TEntity GetById(TKey id, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);
            return query.AsQueryable().FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return DbSet.FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual TEntity Modify(TEntity entity)
        {
            var oldEntity = DbSet.Find(entity.Id);
            if (oldEntity == null) return null;

            _dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
            return entity;
        }

        private IEnumerable<TEntity> GetQuery(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = AsQueryable();
            foreach (var navigationProperty in navigationProperties)
            {
                query.Include(navigationProperty);
            }

            return query;
        }

    }
}