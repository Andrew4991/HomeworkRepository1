using System;
using ShopApp;

namespace JobPlanner
{
    public class JobExecutionOrdersInConsole : IJob
    {
        private bool _isAlive = true;

        public void Execute(DateTime signalTime)
        {
            if (_isAlive)
            {
                try
                {
                    Repository repository = new();
                    var products = repository.GetProductsPurchasedForAllCustomers();

                    foreach (var item in products)
                    {
                        Console.Write($"Executed:{DateTime.Now}.\t");
                        Console.Write($"Customer name: {item.customerName}\t");
                        Console.Write($"Product name: {item.productName}\t");
                        Console.Write($"Product price: {item.price}\t");
                        Console.WriteLine($"Product amount: {item.numberOfPurchases}\n");
                    }
                }
                catch
                {
                    Console.WriteLine($"An error has occurred in class {GetType().Name}. DateTime: {DateTime.Now}");
                    _isAlive = false;
                }
            }
        }
    }
}
