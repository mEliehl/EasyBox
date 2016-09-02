using Domain.Entities;
using Infra.InMemoryDB.Repositories;
using System;
using System.Threading.Tasks;

namespace Integration.Test
{
    public class DatabaseFixture
    {
        public Guid ValidGuid = Guid.NewGuid();

        public DatabaseFixture()
        {
            var productRepository = new ProductRepository();
            Task.Factory.StartNew(() => productRepository.Save(new Product(ValidGuid, "Code", "Name")));
        }
    }
}
