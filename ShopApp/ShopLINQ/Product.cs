using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    public class Product
    {
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            if(obj == null && !(obj is Product))
            {
                return false;
            }

            var product = (Product)obj;

            return Id.Equals(product.Id) && Name.Equals(product.Name) && Price.Equals(product.Price);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ Price.GetHashCode();
        }
    }
}
