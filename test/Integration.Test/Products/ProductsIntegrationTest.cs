using Api;
using Api.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Test.Products
{
    public class ProductsIntegrationTest : IClassFixture<DatabaseFixture> , IDisposable
    {
        private readonly TestServer server;
        private readonly HttpClient client;
        private readonly DatabaseFixture fixture;

        public ProductsIntegrationTest(DatabaseFixture fixture)
        {
            server = new TestServer(new WebHostBuilder().UseEnvironment("IntegrationTest")
                .UseStartup<Startup>());
            client = server.CreateClient();

            this.fixture = fixture;
        }

        [Fact]
        public async Task ShouldCallGetReturnNotFoundWhenPassingNotStoredeGuid()
        {
            // Act
            var id = Guid.NewGuid();
            var response = await client.GetAsync("/api/product/" + id.ToString());

            // Assert
            var expected = HttpStatusCode.NotFound;
            Assert.Equal(expected, response.StatusCode);
        }

        [Fact]
        public async Task ShouldCallGetReturnOKWhenPassingStoredeGuid()
        {
            // Act
            var response = await client.GetAsync("/api/product/" + fixture.ValidGuid.ToString());

            // Assert
            var expected = HttpStatusCode.OK;
            Assert.Equal(expected, response.StatusCode);
        }

        [Fact]
        public async Task ShouldCallPostReturnOKWhenPassingNewProductViewModel()
        {
            var viewModel = new NewProductViewModel()
            {
                Id = Guid.NewGuid(),
                Code = "Post Code",
                Name = "Post Name"
            };

            // Act
            var jsonInString = JsonConvert.SerializeObject(viewModel);
            var response = await client.PostAsync("/api/product/", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            // Assert
            var expected = HttpStatusCode.OK;
            Assert.Equal(expected, response.StatusCode);
        }

        [Fact]
        public async Task ShouldCallPutReturnOKWhenPassingNewProductViewModel()
        {
            var viewModel = new ChangeProductViewModel()
            {
                Id = fixture.ValidGuid,
                Code = "Put Code",
                Name = "Put Name"
            };

            // Act
            var jsonInString = JsonConvert.SerializeObject(viewModel);
            var response = await client.PutAsync("/api/product/", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            // Assert
            var expected = HttpStatusCode.OK;
            Assert.Equal(expected, response.StatusCode);
        }

        public void Dispose()
        {
            client?.Dispose();
            server?.Dispose();
        }
    }
}
