using System;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class DelayedJobExecutionTimeInConsole : BaseDelayedJob
    {
        public DelayedJobExecutionTimeInConsole(DateTime timeStart) : base(timeStart)
        {
        }

        public override void Execute(DateTime signalTime)
        {
            base.Execute(signalTime);

            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
