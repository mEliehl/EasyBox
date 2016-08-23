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

        [Fact]
        public void ShouldCreateProductAndChangeName()
        {
            var p = new Product(Guid.NewGuid(), "USB-128", "Pendrive usb 128gb");
            var newName = "Pendrive usb 128gb(2)";
            p.ChangeName(newName);
            Assert.Equal(p.Name, newName);
        }

        [Fact]
        public void ShouldCreateProductAndChangeCode()
        {
            var p = new Product(Guid.NewGuid(), "USB-128", "Pendrive usb 128gb");
            var newCode = "USB-128(2)";
            p.ChangeCode(newCode);
            Assert.Equal(p.Code, newCode);
        }
    }
}
