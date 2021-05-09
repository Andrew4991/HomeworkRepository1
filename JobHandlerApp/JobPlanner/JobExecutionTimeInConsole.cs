using System;

namespace JobPlanner
{
    public class JobExecutionTimeInConsole : IJob
    {
        private bool _isAlive = true;

        public void Execute(DateTime signalTime)
        {
            if (_isAlive)
            {
                try
                {
                    Console.WriteLine($"Executed: {signalTime}");
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
