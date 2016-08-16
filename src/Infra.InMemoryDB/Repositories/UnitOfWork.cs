using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.InMemoryDB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task Commit()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
