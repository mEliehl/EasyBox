using Api.ViewModels;
using Application.Interfaces;
using Application.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ReadModel;
using ReadModel.Products;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController
    {
        readonly ICommandHandler<CreateNewProduct> createNewProduct;
        readonly ICommandHandler<ChangeProduct> changeProduct;
        readonly IQueryHandler<FindProductByIdQuery,ProductInfo> findProductByIdQuery;

        public ProductController(ICommandHandler<CreateNewProduct> createNewProduct,
            ICommandHandler<ChangeProduct> changeProduct,
            IQueryHandler<FindProductByIdQuery, ProductInfo> findProductByIdQuery)
        {
            this.createNewProduct = createNewProduct;
            this.changeProduct = changeProduct;
            this.findProductByIdQuery = findProductByIdQuery;
        }

        [HttpGet("{id:Guid}")]
        public async Task<string> Get(Guid id)
        {
            var query = new FindProductByIdQuery(id);
            var info = await findProductByIdQuery.HandleAsync(query);
            return info.FullDescription;
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
