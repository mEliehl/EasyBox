using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(Guid Id);
        Task Save(Product product);
    }
}
