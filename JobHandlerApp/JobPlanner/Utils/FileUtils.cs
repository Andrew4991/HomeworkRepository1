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
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Операция прервана токеном");
                return;
            }

            using var writer = new StreamWriter(path, true);
            await writer.WriteLineAsync(text.AsMemory(), token);
        }
    }
}
