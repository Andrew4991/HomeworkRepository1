using System;
using System.Threading;
using System.Threading.Tasks;
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

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            foreach (var item in _repository.GetProductsPurchasedForAllCustomers())
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    break;
                }

                Console.WriteLine($"Executed:{signalTime}.\t{item}");

                Thread.Sleep(500);
            }

            return Task.CompletedTask;
        }
    }
}
