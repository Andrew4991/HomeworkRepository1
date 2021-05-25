using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class DelayedJobExecutionTimeInFile : BaseDelayedJob
    {
        private const string Path = "ExecutionTimeLogFromDelayedJob.txt";

        public DelayedJobExecutionTimeInFile(DateTime timeStart) : base(timeStart)
        {
        }

        public override async Task Execute(DateTime signalTime, IConsoleWrapper console, CancellationToken token)
        {
            await base.Execute(signalTime, console, token);

            await FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture), token);
        }
    }
}
