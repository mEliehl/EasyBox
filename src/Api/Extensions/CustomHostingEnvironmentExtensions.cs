using Microsoft.AspNetCore.Hosting;

namespace Api
{
    public static class CustomHostingEnvironmentExtensions
    {
        public static bool IsIntegrationTest(this IHostingEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment("IntegrationTest");
        }
    }
}
