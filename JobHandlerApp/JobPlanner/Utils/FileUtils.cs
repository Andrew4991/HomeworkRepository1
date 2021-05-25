using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace JobPlanner
{
    public static class FileUtils
    {
        public static async Task WriteToFile(string path, string text, CancellationToken token)
        {
            using var writer = new StreamWriter(path, true);
            await writer.WriteLineAsync(text.AsMemory(), token);
        }

        public static string GetPathSaveUrl(string websitePath)
        {
            return websitePath.Replace("https://", "") + ".txt";
        }
    }
}
