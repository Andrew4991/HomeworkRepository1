using System;
using System.Globalization;

namespace JobPlanner
{
    public class JobExecutionTimeInFile : IJob
    {
        private const string _path = "ExecutionTimeLog.txt";

        public bool IsAlive { get; set; } = true;

        public void Execute(DateTime signalTime)
        {
            FileUtils.WriteToFile(_path, signalTime.ToString(CultureInfo.InvariantCulture));  
        }
    }
}
