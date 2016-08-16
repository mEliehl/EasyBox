using Application.Interfaces;
using Application.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController
    {
        readonly ICommandHandler<CreateNewProduct> createNewProduct;

        public ProductController(ICommandHandler<CreateNewProduct> createNewProduct)
        {
            this.createNewProduct = createNewProduct;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var command = new CreateNewProduct(Guid.NewGuid(), "USB", "Pen-Drive 120gb");
            await createNewProduct.ExecuteAsync(command);
            return "so pra testae";
        }
    }
}
