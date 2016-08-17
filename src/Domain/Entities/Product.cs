using System;

namespace Domain.Entities
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

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void ChangeCode(string code)
        {
            this.Code = code;
        }
    }
}
