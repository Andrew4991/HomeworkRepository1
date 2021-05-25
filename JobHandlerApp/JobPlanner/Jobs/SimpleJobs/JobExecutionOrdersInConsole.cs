using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;
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

        public override Task Execute(DateTime signalTime, IConsoleWrapper console, CancellationToken token)
        {
            foreach (var item in _repository.GetProductsPurchasedForAllCustomers())
            {
                if (token.IsCancellationRequested)
                {
                    console.WriteLine($"Operation interrupted by token for: {GetType().Name}");
                    break;
                }

                console.WriteLine($"Executed:{signalTime}.\t{item}");

                Thread.Sleep(500);
            }

            return Task.CompletedTask;
        }
    }
}
