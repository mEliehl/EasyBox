using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}
