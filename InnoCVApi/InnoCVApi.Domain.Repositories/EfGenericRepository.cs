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

        public IQueryable<TEntity> AsQueryable()
        {
            return DbSet.AsQueryable();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);

            return query.ToList();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return Task.Run(() => GetAll(navigationProperties));
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);
            return query.AsQueryable().Where(predicate);
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return Task.Run(() => Find(predicate, navigationProperties));
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);

            return query.AsQueryable().First(predicate);
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);
            return query.AsQueryable().FirstAsync(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);

            return query.AsQueryable().FirstOrDefault(predicate);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return DbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return DbSet.First(predicate);
        }

        public TEntity GetById(TKey id, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var query = GetQuery(navigationProperties);
            return query.AsQueryable().FirstOrDefault(entity => entity.Id.Equals(id));
        }

        public Task<TEntity> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return DbSet.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public TEntity Modify(TEntity entity)
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