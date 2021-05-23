using System;
using System.Threading;
using System.Threading.Tasks;
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

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            base.Execute(signalTime, token);

            foreach (var item in _repository.GetProductsPurchasedForAllCustomers())
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    break;
                }

                Console.WriteLine($"Executed:{signalTime}.\t{item}");
            }

            return Task.CompletedTask;
        }
    }
}
