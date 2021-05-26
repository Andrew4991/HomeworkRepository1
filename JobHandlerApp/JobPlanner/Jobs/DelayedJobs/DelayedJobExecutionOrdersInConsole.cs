using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;
using ShopApp;

namespace JobPlanner
{
    public class DelayedJobExecutionOrdersInConsole : BaseDelayedJob
    {
        private readonly IRepository _repository;

        public DelayedJobExecutionOrdersInConsole(IConsoleWrapper console, DateTime timeStart, IRepository repository) :base(console, timeStart)
        {
            _repository = repository;
        }

        public override Task Execute(DateTime signalTime, IConsoleWrapper console, CancellationToken token)
        {
            base.Execute(signalTime, console, token);

            foreach (var item in _repository.GetProductsPurchasedForAllCustomers())
            {
                if (token.IsCancellationRequested)
                {
                    _console.WriteLine($"Operation interrupted by token for: {GetType().Name}");
                    break;
                }

                _console.WriteLine($"Executed:{signalTime}.\t{item}");
            }

            return Task.CompletedTask;
        }
    }
}
