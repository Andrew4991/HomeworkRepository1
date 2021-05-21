using System;
using AnalyticsProgram.Jobs;
using ShopApp;

namespace JobPlanner
{
    public class JobExecutionOrdersInConsole : BaseJob
    {
        private readonly IRepository _repository;

        public JobExecutionOrdersInConsole(IRepository repository)
        {
            _repository = repository;
        }

        public override void Execute(DateTime signalTime)
        {
            foreach (var item in _repository.GetProductsPurchasedForAllCustomers())
            {
                Console.WriteLine($"Executed:{signalTime}.\t{item}");
            }
        }
    }
}
