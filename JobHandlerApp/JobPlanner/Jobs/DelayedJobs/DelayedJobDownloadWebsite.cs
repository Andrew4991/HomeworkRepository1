using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class DelayedJobDownloadWebsite : BaseDelayedJob
    {
        private readonly string _path;
        private readonly string _fileName;

        public DelayedJobDownloadWebsite(IConsoleWrapper console, string path, DateTime timeStart) : base(console, timeStart)
        {
            _path = WebsiteUtils.GetDownloadUrl(path);
            _fileName = FileUtils.GetPathSaveUrl(_path);
        }

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            await base.Execute(signalTime, token);

            var httpText = await WebsiteUtils.DownloadHttp(_path, token);
            await FileUtils.WriteToFile(_fileName, httpText, token);
        }
    }
}
