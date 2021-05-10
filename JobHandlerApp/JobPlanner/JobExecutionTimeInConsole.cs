using System;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : IJob
    {
        public bool IsAlive { get; set; } = true;

        public void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
