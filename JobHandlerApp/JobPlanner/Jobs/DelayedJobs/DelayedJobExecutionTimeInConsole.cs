using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class DelayedJobExecutionTimeInConsole : BaseDelayedJob
    {
        public DelayedJobExecutionTimeInConsole(IConsoleWrapper console, DateTime timeStart) : base(console, timeStart)
        {
        }

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                _console.WriteLine($"Operation interrupted by token for: {GetType().Name}");
                return Task.CompletedTask;
            }

            base.Execute(signalTime, token);
            
            _console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
