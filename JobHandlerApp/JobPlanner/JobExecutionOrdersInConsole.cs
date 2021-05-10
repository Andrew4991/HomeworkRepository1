using System;
using ShopApp;

namespace JobPlanner
{
    public class JobExecutionOrdersInConsole : IJob
    {
        public bool IsAlive { get; set; } = true;

        public void Execute(DateTime signalTime)
        {
             Repository repository = new();
             var products = repository.GetProductsPurchasedForAllCustomers();

             foreach (var item in products)
             {
                  Console.Write($"Executed:{DateTime.Now}.\t");
                  Console.WriteLine($"{item}");
             }
        }
    }
}
