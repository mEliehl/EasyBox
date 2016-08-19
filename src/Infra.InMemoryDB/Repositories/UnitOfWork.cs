using Domain.Repositories;
using System.Threading.Tasks;

namespace Infra.InMemoryDB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task Commit()
        {
           await Task.Factory.StartNew(() => 1 + 1);
        }

        public void Dispose()
        {
            
        }
    }
}
