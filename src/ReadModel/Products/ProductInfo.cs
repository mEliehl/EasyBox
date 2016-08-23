using System;

namespace ReadModel.Products
{
    public class ProductInfo
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullDescription { get { return $"{Code} - {Name}"; } }
    }
}
