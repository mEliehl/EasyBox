using Domain.Entities;
using Domain.Repositories;
using Raven.Client;
using System;
using System.Threading.Tasks;

namespace Infra.RavenDB.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly UnitOfWork unitOfWork;
        IAsyncDocumentSession session;

        public ProductRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = (UnitOfWork)unitOfWork;
            session = this.unitOfWork.session;
        }

        public async Task<Product> Get(Guid Id)
        {
            return await session.LoadAsync<Product>(Id);
        }

        public async Task Save(Product product)
        {
            await session.StoreAsync(product);
        }
    }
}
