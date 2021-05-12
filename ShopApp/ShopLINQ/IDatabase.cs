using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopApp;

namespace ShopLINQ
{
    public interface IDatabase
    {
        List<Customer> Customers { get; set; }
        List<Order> Orders { get; set; }
        List<Product> Products { get; set; }
    }
}
