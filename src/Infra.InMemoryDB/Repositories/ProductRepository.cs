using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infra.InMemoryDB.Repositories
{
    public class ProductRepository : IProductRepository
    {
        static IList<Product> products = new List<Product>();

        public async Task<Product> Get(Guid Id)
        {
            return products.FirstOrDefault(w => w.Id == Id);
        }

        public async Task Save(Product product)
        {
            products.Add(product);
        }
    }
}
