using System;
using AnalyticsProgram.Jobs;
using ShopApp;

namespace JobPlanner
{
    public class DelayedJobExecutionOrdersInConsole : BaseDelayedJob
    {
        private readonly IRepository _repository;

        public DelayedJobExecutionOrdersInConsole(DateTime timeStart, IRepository repository) :base(timeStart)
        {
            _repository = repository;
        }

        public override void Execute(DateTime signalTime)
        {
            base.Execute(signalTime);

            foreach (var item in _repository.GetProductsPurchasedForAllCustomers())
            {
                Console.WriteLine($"Executed:{signalTime}.\t{item}");
            }
        }
    }
}
