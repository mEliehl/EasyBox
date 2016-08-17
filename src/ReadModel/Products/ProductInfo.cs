using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
