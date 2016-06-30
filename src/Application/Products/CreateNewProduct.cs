using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.Products
{
    public class CreateNewProduct
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class CreateNewProductHandler : ICommandHandler<CreateNewProduct>
    {
        readonly IProductRepository productRepository;

        public CreateNewProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task ExecuteAsync(CreateNewProduct command)
        {
            var product = new Product(command.Id, command.Code, command.Name);
            await productRepository.Save(product);
        }
    }
}
