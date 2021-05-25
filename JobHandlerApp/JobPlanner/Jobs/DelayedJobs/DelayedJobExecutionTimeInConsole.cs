using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class DelayedJobExecutionTimeInConsole : BaseDelayedJob
    {
        public DelayedJobExecutionTimeInConsole(DateTime timeStart) : base(timeStart)
        {
        }

        public override Task Execute(DateTime signalTime, IConsoleWrapper console, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                console.WriteLine($"Operation interrupted by token for: {GetType().Name}");
                return Task.CompletedTask;
            }

            base.Execute(signalTime, console, token);
            
            console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
