using System;
using System.Net;

namespace JobPlanner
{
    public class JobDownloadWebsite : IJob
    {
        private readonly string _path;
        private bool _isAlive = true;

        public JobDownloadWebsite(string path)
        {
            _path = "https://" + path.Replace("https://", "");
        }

        public void Execute(DateTime signalTime)
        {
            if (_isAlive)
            {
                try
                {
                    var client = new WebClient();
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    string reply = client.DownloadString(_path);

                    var name = _path.Replace("https://", "") + ".txt";
                    WriterFile.WriteToFile(name, reply);
                }
                catch
                {
                    Console.WriteLine($"An error has occurred in class {GetType().Name}. DateTime: {DateTime.Now}. Path: {_path}");
                    _isAlive = false;
                }
            }
        }
    }
}
