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
    public class ProductsIntegrationTest
    {
        private readonly TestServer server;
        private readonly HttpClient client;
        public ProductsIntegrationTest()
        {
            // Arrange
            server = new TestServer(new WebHostBuilder().UseEnvironment("IntegrationTest")
                .UseStartup<Startup>());
           client = server.CreateClient();
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // Act
            var id = Guid.NewGuid();
            var response = await client.GetAsync("/api/product/" +id.ToString());

            // Assert
            var expected = HttpStatusCode.NotFound;
            Assert.Equal(expected, response.StatusCode);
        }
    }
}
