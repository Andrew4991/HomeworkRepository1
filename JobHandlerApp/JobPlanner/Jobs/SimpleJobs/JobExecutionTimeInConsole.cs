using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : BaseJob
    {
        public override Task Execute(DateTime signalTime, IConsoleWrapper console, CancellationToken token)
        {
            console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
