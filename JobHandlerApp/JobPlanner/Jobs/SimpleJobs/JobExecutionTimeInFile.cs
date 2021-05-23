using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class JobExecutionTimeInFile : BaseJob
    {
        private const string Path = "ExecutionTimeLog.txt";

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            await FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture), token);
        }
    }
}
