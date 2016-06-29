using System;
using Xunit;
using Domain.Entities;

namespace Domain.Test.Entities
{
    public class ProductTest
    {
        [Fact]
        public void ShouldCreateProduct()
        {
            var p = new Product(Guid.NewGuid(), "USB-128", "Pendrive usb 128gb");
            Assert.NotNull(p);
            Assert.NotEqual(p.Id, Guid.Empty);
            Assert.NotEqual(p.Code, string.Empty);
            Assert.NotEqual(p.Name, string.Empty);
        }
    }
}
