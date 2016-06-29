using Api.Controllers;
using Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace Api.Test.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task ShouldCallGetAndReturnProduct()
        {
            var controller = new ProductController();
            var product = await controller.Get();
            Assert.NotNull(product);
            Assert.IsType<Product>(product);
        }
    }
}
