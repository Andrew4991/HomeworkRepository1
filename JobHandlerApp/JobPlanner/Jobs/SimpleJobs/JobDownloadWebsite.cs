using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class JobDownloadWebsite : BaseJob
    {
        private readonly string _path;
        private readonly string _fileName;

        public JobDownloadWebsite(string path)
        {
            _path = "https://" + path.Replace("https://", "");
            _fileName = _path.Replace("https://", "") + ".txt";
        }

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            var httpText = await WebsiteUtils.DownloadHttp(_path, token);
            await FileUtils.WriteToFile(_fileName, httpText, token);
        }
    }
}
