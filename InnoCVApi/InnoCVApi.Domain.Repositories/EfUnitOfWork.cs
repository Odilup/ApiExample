using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InnoCVApi.Data.DbContext;
using InnoCVApi.Domain.Entities.Users;
using InnoCVApi.Domain.Repositories.Interfaces;

namespace InnoCVApi.Domain.Repositories
{
    /// <summary>
    /// Default unit of work pattern implementation
    /// </summary>
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _dbContext;

        public virtual IRepository<User, int> UsersRepository { get; set; }

        /// <summary>
        /// Initializes a new instance of EfUnitOfWork class
        /// </summary>
        /// <param name="dbContext"></param>
        public EfUnitOfWork(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public virtual Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public virtual int RollbackChanges()
        {
            return _dbContext.RollbackChanges();
        }

        public virtual Task<int> RollbackChangesAsync()
        {
            return _dbContext.RollbackChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
            }
        }
    }

}