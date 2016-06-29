using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController
    {
        readonly IList<Product> products;

        public ProductController()
        {
            products = new List<Product>();
            products.Add(new Product(Guid.NewGuid(), "usb-128", "Pendrive USB 128Gb"));
        }

        [HttpGet]
        public async Task<Product> Get()
        {
            return products.FirstOrDefault();
        }
    }
}
