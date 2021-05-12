using System;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : IJob
    {
        public bool IsFailed { get; set; }

        public void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
