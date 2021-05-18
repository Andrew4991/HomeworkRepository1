using System;
using System.IO;
using System.Text;
using ShopApp;

namespace JobPlanner
{
    public static class OrdersUtils
    {
        public static string GetOrders(DateTime signalTime)
        {
            Repository repository = new();
            var products = repository.GetProductsPurchasedForAllCustomers();
            var orders = string.Empty;

            foreach (var item in products)
            {
                orders += $"Executed:{signalTime}.\t{item}\n";
            }

            return orders;
        }
    }
}
