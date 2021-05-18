using System;
using System.Globalization;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class JobExecutionTimeInFile : BaseJob
    {
        private const string Path = "ExecutionTimeLog.txt";

        public override void Execute(DateTime signalTime)
        {
            FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture));  
        }
    }
}
