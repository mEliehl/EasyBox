﻿using Domain.Repositories;
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

        public async Task<ProductInfo> HandleAsync(FindProductByIdQuery query)
        {
            var product = await productRepository.Get(query.Id);
            return new ProductInfo()
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name
            };
        }
    }
}