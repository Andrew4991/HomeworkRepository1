using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Jobs;
using JobPlanner.GitJson;
using System.Text.Json;
using JobPlanner.Wrappers;

namespace JobPlanner.Jobs.SimpleJobs
{
    public class JobGithubRepositoryParser : BaseJob
    {
        private const string JsonUrl = "https://api.github.com/orgs/dotnet/repos";

        public JobGithubRepositoryParser(IConsoleWrapper console) : base(console)
        {
        }

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            var result = await WebsiteUtils.DownloadJson(JsonUrl, token);

            var infoGit = JsonSerializer.Deserialize<InfoGit[]>(result);

            foreach (var item in infoGit.Where(x => DateTime.Parse(x.CreatedAt) >= new DateTime(2014, 1, 1)))
            {
                if (token.IsCancellationRequested)
                {
                    _console.WriteLine($"Operation interrupted by token for: {GetType().Name}");
                    break;
                }

                _console.WriteLine($"Id: {item.Id}   CreatedAt: {item.CreatedAt}");
            }
        }
    }
}
