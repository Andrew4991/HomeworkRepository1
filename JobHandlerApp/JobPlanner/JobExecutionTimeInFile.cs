using System;
using System.Globalization;

namespace JobPlanner
{
    public class JobExecutionTimeInFile : IJob
    {
        private const string Path = "ExecutionTimeLog.txt";

        public bool IsFailed { get; set; }

        public DateTime StartJob { get; set; }

        public JobExecutionTimeInFile() : this(DateTime.MinValue)
        {

        }

        public JobExecutionTimeInFile(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture));  
        }
    }
}
