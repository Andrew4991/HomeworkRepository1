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



        /*private const string UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

        public static void Download(string websitePath, string fileName)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", UserAgent);
            string reply = client.DownloadString(websitePath);

            FileUtils.WriteToFile(fileName, reply);
        }*/

        

    }
}
