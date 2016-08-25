using Application.Interfaces;
using Application.Products;
using Domain.Entities;
using Domain.Repositories;
using Infra.InMemoryDB.Repositories;
using System;
using Xunit;

namespace Application.Test.Products
{
    public class ChangeProductHandlerTest
    {
        readonly IProductRepository productRepository;

        public ChangeProductHandlerTest()
        {
            productRepository = new ProductRepository();
        }

        [Fact]
        public async void ShouldCreateNewProductAndChangeIt()
        {
            var product  = new Product(Guid.NewGuid(), "Code", "Name");
            await productRepository.Save(product );

            var newName = "New Name";
            var newCode = "New Code";
            var command = new ChangeProduct(product .Id, newCode, newName);
            ICommandHandler<ChangeProduct> handler = new ChangeProductHandler(productRepository);

            await handler.ExecuteAsync(command);
            product = await productRepository.Get(command.Id);
            Assert.NotNull(product);
            Assert.Equal(command.Code, product.Code);
            Assert.Equal(command.Name, product.Name);
        }

    }
}
