using System;
using System.Globalization;

namespace JobPlanner
{
    public class JobExecutionTimeInFile : IJob
    {
        private const string Path = "ExecutionTimeLog.txt";

        public bool IsFailed { get; set; }

        public void Execute(DateTime signalTime)
        {
            FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture));  
        }
    }
}
