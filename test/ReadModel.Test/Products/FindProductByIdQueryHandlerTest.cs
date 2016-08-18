using Domain.Entities;
using Domain.Repositories;
using Infra.InMemoryDB.Repositories;
using ReadModel.Products;
using System;
using Xunit;

namespace ReadModel.Test.Products
{
    public class FindProductByIdQueryHandlerTest
    {
        readonly IProductRepository productRepository;

        public FindProductByIdQueryHandlerTest()
        {
            productRepository = new ProductRepository();
        }

        [Fact]
        public async void ShouldCreateProductCallQueryAndReturnProductInfo()
        {
            var product = new Product(Guid.NewGuid(), "Code", "Name");
            await productRepository.Save(product);
            var query = new FindProductByIdQuery(product.Id);
            IQueryHandler<FindProductByIdQuery, ProductInfo> handler = new FindProductByIdQueryHandler(productRepository);
            var productInfo = await handler.HandleAsync(query);
            Assert.NotNull(productInfo);
            Assert.Equal(productInfo.Id, product.Id);
            Assert.Equal(productInfo.Code, product.Code);
            Assert.Equal(productInfo.Name, product.Name);
            Assert.Contains(product.Code, productInfo.FullDescription);
            Assert.Contains(product.Name, productInfo.FullDescription);
        }

        [Fact]
        public async void ShouldCallQueryWithInvalidIdAndReturnNull()
        {
            var query = new FindProductByIdQuery(Guid.NewGuid());
            IQueryHandler<FindProductByIdQuery, ProductInfo> handler = new FindProductByIdQueryHandler(productRepository);
            var productInfo = await handler.HandleAsync(query);
            Assert.Null(productInfo);            
        }
    }
}
