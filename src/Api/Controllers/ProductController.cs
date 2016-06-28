using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController
    {
        [HttpGet]
        public async Task<string> Get()
        {
            return "Hello!";
        }
    }
}
