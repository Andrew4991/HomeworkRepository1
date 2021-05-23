using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.GitJson;
using Newtonsoft.Json;

namespace JobPlanner.Jobs.SimpleJobs
{
    public class JobGithubRepositoryParser : BaseJob
    {
        private const string JsonUrl = "https://api.github.com/orgs/dotnet/repos";

        private CancellationToken _token;

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            _token = token;

            var result = await WebsiteUtils.DownloadJson(JsonUrl, token);

            var infoGit = JsonConvert.DeserializeObject<List<InfoGit>>(result);

            foreach (var item in infoGit)
            {
                Console.WriteLine($"Id: {item.Id}");
            }
        }

        public override async Task<bool> ShouldRun(DateTime signalTime)
        {
            var isConnected = await WebsiteUtils.DownloadJson("https://api.github.com", _token);
            return await base.ShouldRun(signalTime) && isConnected.Length > 0;
        }
    }
}
