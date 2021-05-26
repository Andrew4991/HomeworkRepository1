using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : BaseJob
    {
        public JobExecutionTimeInConsole(IConsoleWrapper console) : base(console)
        {
        }

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            _console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
