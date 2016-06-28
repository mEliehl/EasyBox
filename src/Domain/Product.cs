using System;

namespace Domain
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        
        public Product(Guid Id,
            string Code,
            string Name)
        {
            this.Id = Id;
            this.Code = Code;
            this.Name = Name;
        }
    }
}
