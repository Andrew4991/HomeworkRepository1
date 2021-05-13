using System;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : IJob
    {
        public bool IsFailed { get; set; }

        public DateTime StartJob { get; set; }

        public JobExecutionTimeInConsole() : this(DateTime.MinValue)
        {

        }

        public JobExecutionTimeInConsole(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
