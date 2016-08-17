using Application.Interfaces;
using Application.Products;
using Domain.Repositories;
using Infra.InMemoryDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Products
{
    public class CreateNewProductHandlerTest
    {
        readonly IProductRepository productRepository;

        public CreateNewProductHandlerTest()
        {
            productRepository = new ProductRepository();
        }

        [Fact]
        public async void ShouldCreateNewProduct()
        {
            var command = new CreateNewProduct(Guid.NewGuid(),
            "USB-128",
            "Pendrive USB 128GB");

            ICommandHandler<CreateNewProduct> handler = new CreateNewProductHandler(productRepository);
            await handler.ExecuteAsync(command);

            var product = await productRepository.Get(command.Id);
            Assert.NotNull(product);
            Assert.Equal(command.Id, product.Id);
            Assert.Equal(command.Code, product.Code);
            Assert.Equal(command.Name, product.Name);
        }
    }
}
