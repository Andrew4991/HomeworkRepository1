using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    public class CustomerOverView
    {
        public string Name { get; set; }
        public int TotalProductsPurchased { get; set; }
        // return product name maximum number of purchases for a single product
        public string FavoriteProductName { get; set; }
        // max amount of money spent for a single product
        public decimal MaxAmountSpentPerProducts { get; set; }
        public decimal TotalMoneySpent { get; set; }
    }
}
