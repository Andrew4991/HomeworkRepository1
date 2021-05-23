using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class DelayedJobExecutionTimeInConsole : BaseDelayedJob
    {
        public DelayedJobExecutionTimeInConsole(DateTime timeStart) : base(timeStart)
        {
        }

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Операция прервана токеном");
                return Task.CompletedTask;
            }

            base.Execute(signalTime, token);

            Console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
