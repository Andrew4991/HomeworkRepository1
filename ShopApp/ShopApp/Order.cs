using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    public class Order
    {
        public Order(int id, int productId, int customerId)
        {
            Id = id;
            ProductId = productId;
            CustomerId = customerId;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}
