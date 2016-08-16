using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.Products
{
    public class CreateNewProduct
    {
        public CreateNewProduct(Guid Id,
            string Code,
            String Name)
        {
            this.Id = Id;
            this.Code = Code;
            this.Name = Name;
        }

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
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
