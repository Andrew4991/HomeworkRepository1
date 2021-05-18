using System;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : BaseJob
    {
        public override void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
