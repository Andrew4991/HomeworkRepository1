using System;
using System.Globalization;

namespace JobPlanner
{
    public class JobExecutionTimeInFile : IJob
    {
        private bool _isAlive = true;

        public void Execute(DateTime signalTime)
        {
            if (_isAlive)
            {
                try
                {
                    WriterFile.WriteToFile("ExecutionTimeLog.txt", signalTime.ToString(CultureInfo.InvariantCulture));
                }
                catch
                {
                    Console.WriteLine($"An error has occurred in class {GetType().Name}. DateTime: {DateTime.Now}");
                    _isAlive = false;
                }
            }   
        }
    }
}
