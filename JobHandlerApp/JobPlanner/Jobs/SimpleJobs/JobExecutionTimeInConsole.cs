using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : BaseJob
    {
        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            Console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
