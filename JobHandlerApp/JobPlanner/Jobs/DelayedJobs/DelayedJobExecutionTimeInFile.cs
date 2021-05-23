using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class DelayedJobExecutionTimeInFile : BaseDelayedJob
    {
        private const string Path = "ExecutionTimeLogFromDelayedJob.txt";

        public DelayedJobExecutionTimeInFile(DateTime timeStart) : base(timeStart)
        {
        }

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            await base.Execute(signalTime, token);

            await FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture), token);
        }
    }
}
