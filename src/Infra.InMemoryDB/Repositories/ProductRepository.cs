using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.InMemoryDB.Repositories
{
    public class ProductRepository : IProductRepository
    {
        static ConcurrentBag<Product> products = new ConcurrentBag<Product>();

        public async Task<Product> Get(Guid Id)
        {
            return await Task.Factory.StartNew(() => products.FirstOrDefault(w => w.Id == Id));
        }

        public async Task Save(Product product)
        {
            await Task.Factory.StartNew(() =>
            {
                if (products.Any(p => p.Id == product.Id))
                {
                    var old = products.FirstOrDefault(p => p.Id == product.Id);
                    old = product;
                }else
                    products.Add(product);
            });
        }
    }
}
