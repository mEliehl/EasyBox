using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Test.Products
{
    public class ProductsIntegrationTest : IClassFixture<DatabaseFixture>
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
    }
}
