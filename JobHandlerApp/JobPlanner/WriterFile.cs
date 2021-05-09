﻿using System;
using System.IO;
using System.Text;

namespace JobPlanner
{
    public static class WriterFile
    {
        public static void WriteToFile(string path, string text)
        {
            if (!File.Exists(path))
            {
                using var stream = File.Create(path);
                byte[] info = new UTF8Encoding(true).GetBytes($"{text}\n");
                stream.Write(info, 0, info.Length);
            }
            else
            {
                File.AppendAllText(path, text);
                File.AppendAllText(path, Environment.NewLine);
            }
        }
    }
}
