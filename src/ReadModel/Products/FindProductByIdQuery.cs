using Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace ReadModel.Products
{
    public class FindProductByIdQuery : IQuery<ProductInfo>
    {
        public FindProductByIdQuery(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; private set; }
    }

    public class FindProductByIdQueryHandler : IQueryHandler<FindProductByIdQuery, ProductInfo>
    {
        readonly IProductRepository productRepository;

        public FindProductByIdQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductInfo> ExecuteAsync(FindProductByIdQuery query)
        {
            var product = await productRepository.Get(query.Id);
            if (product != null)
            {
                return new ProductInfo()
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name
                };
            }
            return null;
        }
    }
}
