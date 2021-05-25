using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace JobPlanner
{
    public static class WebsiteUtils
    {
        private static readonly HttpClient Client = new();

        public static async Task<string> DownloadJson(string websitePath, CancellationToken token)
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            return await Client.GetStringAsync(websitePath, token);
        }

        public static async Task<string> DownloadHttp(string websitePath, CancellationToken token)
        {
            Client.DefaultRequestHeaders.Accept.Clear();

            return await Client.GetStringAsync(websitePath, token);
        }

        public static string GetDownloadUrl(string websitePath)
        {
            return websitePath.StartsWith("https://") ? websitePath : "https://" + websitePath;
        }
    }
}
