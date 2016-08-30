using Api.Controllers;
using Api.ViewModels;
using Application.Interfaces;
using Application.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReadModel;
using ReadModel.Products;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Test.Controllers
{
    public class ProductControllerTest
    {
        readonly Guid ValidId = Guid.NewGuid();
        readonly Guid InvalidId = Guid.NewGuid();

        readonly ProductController controller;
        readonly Mock<ICommandHandler<CreateNewProduct>> createNewProduct;
        readonly Mock<ICommandHandler<ChangeProduct>> changeProduct;
        readonly Mock<IQueryHandler<FindProductByIdQuery, ProductInfo>> findProductByIdQuery;


        public ProductControllerTest()
        {
            // Arrange
            createNewProduct = new Mock<ICommandHandler<CreateNewProduct>>();
            changeProduct = new Mock<ICommandHandler<ChangeProduct>>();
            findProductByIdQuery = new Mock<IQueryHandler<FindProductByIdQuery, ProductInfo>>();

            controller = new ProductController(createNewProduct.Object,
                changeProduct.Object,
                findProductByIdQuery.Object);
        }

        #region Get Id
        [Fact]
        public async void ShouldCallGetAndReturnOkObject()
        {
            findProductByIdQuery.Setup(x => x.ExecuteAsync(It.IsAny<FindProductByIdQuery>())).Returns(Task.FromResult<ProductInfo>(new ProductInfo()));

            var actual = await controller.Get(ValidId);
            Assert.IsAssignableFrom<OkObjectResult>(actual);
        }

        [Fact]
        public async void ShouldCallGetAndReturnNotFoundResult()
        {
            findProductByIdQuery.Setup(x => x.ExecuteAsync(It.IsAny<FindProductByIdQuery>())).Returns(Task.FromResult<ProductInfo>(null));

            var actual = await controller.Get(ValidId);
            Assert.IsAssignableFrom<NotFoundResult>(actual);
        }

        [Fact]
        public async void ShouldCallGetAndReturnBadRequestObjectResult()
        {
            findProductByIdQuery.Setup(x => x.ExecuteAsync(It.IsAny<FindProductByIdQuery>())).Throws(new Exception());

            var actual = await controller.Get(ValidId);
            Assert.IsAssignableFrom<BadRequestObjectResult>(actual);
        }

        #endregion

        #region Post
        [Fact]
        public async void ShouldCallPostAndReturnOkResult()
        {
            var viewModel = new NewProductViewModel()
            {
                Id = ValidId,
                Code = "Code",
                Name = "Name"
            };
            var actual = await controller.Post(viewModel);
            Assert.IsAssignableFrom<OkResult>(actual);
        }

        [Fact]
        public async void ShouldCallPostAndReturnBadRequestObjectResult()
        {
            var viewModel = new NewProductViewModel();            

            createNewProduct.Setup(s => s.ExecuteAsync(It.IsAny<CreateNewProduct>())).Throws(new Exception());

            var actual = await controller.Post(viewModel);
            Assert.IsAssignableFrom<BadRequestObjectResult>(actual);
        }
        #endregion

        #region Put
        [Fact]
        public async void ShouldCallPutAndReturnOkResult()
        {
            var viewModel = new ChangeProductViewModel()
            {
                Id = ValidId,
                Code = "Code",
                Name = "Name"
            };
            var actual = await controller.Put(viewModel);
            Assert.IsAssignableFrom<OkResult>(actual);
        }

        [Fact]
        public async void ShouldCallPutAndReturnBadRequestObjectResult()
        {
            var viewModel = new ChangeProductViewModel();

            changeProduct.Setup(s => s.ExecuteAsync(It.IsAny<ChangeProduct>())).Throws(new Exception());

            var actual = await controller.Put(viewModel);
            Assert.IsAssignableFrom<BadRequestObjectResult>(actual);
        }
        #endregion
    }
}
