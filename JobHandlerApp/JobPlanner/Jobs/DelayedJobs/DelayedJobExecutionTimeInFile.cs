using System;
using System.Globalization;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class DelayedJobExecutionTimeInFile : BaseDelayedJob
    {
        private const string Path = "ExecutionTimeLogFromDelayedJob.txt";

        public DelayedJobExecutionTimeInFile(DateTime timeStart) : base(timeStart)
        {
        }

        public override void Execute(DateTime signalTime)
        {
            base.Execute(signalTime);

            FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture));  
        }
    }
}
