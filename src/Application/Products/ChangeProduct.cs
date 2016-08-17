using Application.Interfaces;
using Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.Products
{
    public class ChangeProduct
    {
        public ChangeProduct(Guid Id,
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

    public class ChangeProductHandler : ICommandHandler<ChangeProduct>
    {
        readonly IProductRepository productRepository;

        public ChangeProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task ExecuteAsync(ChangeProduct command)
        {
            var product = await productRepository.Get(command.Id);
            product.ChangeName(command.Name);
            product.ChangeCode(command.Code);
            await productRepository.Save(product);
        }
    }
}
