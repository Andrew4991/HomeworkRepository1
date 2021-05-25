using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class JobDownloadWebsite : BaseJob
    {
        private readonly string _path;
        private readonly string _fileName;

        public JobDownloadWebsite(string path)
        {
            _path = WebsiteUtils.GetDownloadUrl(path);
            _fileName = FileUtils.GetPathSaveUrl(_path);
        }

        public override async Task Execute(DateTime signalTime, IConsoleWrapper console, CancellationToken token)
        {
            var httpText = await WebsiteUtils.DownloadHttp(_path, token);
            await FileUtils.WriteToFile(_fileName, httpText, token);
        }
    }
}
