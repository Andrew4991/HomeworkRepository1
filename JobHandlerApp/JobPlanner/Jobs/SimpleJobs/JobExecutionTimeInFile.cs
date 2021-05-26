using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class JobExecutionTimeInFile : BaseJob
    {
        private const string Path = "ExecutionTimeLog.txt";
        
        public JobExecutionTimeInFile(IConsoleWrapper console) : base(console)
        {
        }

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            await FileUtils.WriteToFile(Path, signalTime.ToString(CultureInfo.InvariantCulture), token);
        }
    }
}
