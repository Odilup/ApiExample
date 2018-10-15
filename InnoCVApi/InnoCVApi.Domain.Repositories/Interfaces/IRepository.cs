using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InnoCVApi.Domain.Entities;

namespace InnoCVApi.Domain.Repositories.Interfaces
{
    public interface IRepository
    {
    }
    public interface IRepository<TEntity, TKey> : IRepository where TEntity : BaseModel<TKey>
    {
        IQueryable<TEntity> AsQueryable();
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] navigationProperties);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        TEntity First(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        TEntity GetById(TKey id, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<TEntity> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] navigationProperties);

        void Add(TEntity entity);
        void Delete(TEntity entity);
        TEntity Modify(TEntity entity);
    }
}