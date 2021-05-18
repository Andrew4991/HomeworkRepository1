using System;
using System.Net;
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

        public override void Execute(DateTime signalTime)
        {
            base.Execute(signalTime);
            WebsiteUtils.Download(_path, _fileName);
        }
    }
}
