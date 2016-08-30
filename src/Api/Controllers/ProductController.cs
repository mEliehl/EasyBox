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
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var query = new FindProductByIdQuery(id);
                var info = await findProductByIdQuery.ExecuteAsync(query);
                if (info == null)
                    return new NotFoundResult();

                return new OkObjectResult(info);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewProductViewModel viewModel)
        {
            try
            {
                var command = new CreateNewProduct(viewModel.Id, viewModel.Code, viewModel.Name);
                await createNewProduct.ExecuteAsync(command);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ChangeProductViewModel viewModel)
        {
            try
            {
                var command = new ChangeProduct(viewModel.Id, viewModel.Code, viewModel.Name);
                await changeProduct.ExecuteAsync(command);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
