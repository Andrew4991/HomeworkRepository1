using System;
using ShopApp;

namespace JobPlanner
{
    public class JobExecutionOrdersInConsole : IJob
    {
        public bool IsFailed { get; set; }

        public DateTime StartJobAt { get; set; }

        public JobExecutionOrdersInConsole() : this(DateTime.MinValue)
        {

        }

        public JobExecutionOrdersInConsole(DateTime timeStart)
        {
            StartJobAt = timeStart;
        }

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
