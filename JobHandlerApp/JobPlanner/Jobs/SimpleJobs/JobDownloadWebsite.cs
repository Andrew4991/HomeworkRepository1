using System;
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

        public override void Execute(DateTime signalTime)
        {
            WebsiteUtils.Download(_path, _fileName);
        }
    }
}
