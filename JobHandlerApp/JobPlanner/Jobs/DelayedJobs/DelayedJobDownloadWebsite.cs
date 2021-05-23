using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class DelayedJobDownloadWebsite : BaseDelayedJob
    {
        private readonly string _path;
        private readonly string _fileName;

        public DelayedJobDownloadWebsite(string path, DateTime timeStart) : base(timeStart)
        {
            _path = "https://" + path.Replace("https://", "");
            _fileName = _path.Replace("https://", "") + ".txt";
        }

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            await base.Execute(signalTime, token);

            var httpText = await WebsiteUtils.DownloadHttp(_path, token);
            await FileUtils.WriteToFile(_fileName, httpText, token);
        }
    }
}
