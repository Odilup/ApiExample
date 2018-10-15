using System;
using System.Threading.Tasks;
using InnoCVApi.Domain.Entities.Users;

namespace InnoCVApi.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User, int> UsersRepository { get; set; }
        int Commit();
        Task<int> CommitAsync();

        int RollbackChanges();
        Task<int> RollbackChangesAsync();

    }
}