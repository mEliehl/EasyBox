using Api.ViewModels;
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
        readonly ICommandHandler<ChangeProduct> changeProduct;

        public ProductController(ICommandHandler<CreateNewProduct> createNewProduct,
            ICommandHandler<ChangeProduct> changeProduct)
        {
            this.createNewProduct = createNewProduct;
            this.changeProduct = changeProduct;
        }

        [HttpGet("{id:Guid}")]
        public async Task<string> Get(Guid id)
        {
            return id.ToString();
        }

        [HttpPost]
        public async Task Post(NewProductViewModel viewModel)
        {
            var command = new CreateNewProduct(viewModel.Id, viewModel.Code, viewModel.Name);
            await createNewProduct.ExecuteAsync(command);
        }

        [HttpPut]
        public async Task Put(ChangeProductViewModel viewModel)
        {
            var command = new ChangeProduct(viewModel.Id, viewModel.Code, viewModel.Name);
            await changeProduct.ExecuteAsync(command);
        }
    }
}
