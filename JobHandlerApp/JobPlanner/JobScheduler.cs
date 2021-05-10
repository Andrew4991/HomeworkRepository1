using System;
using System.Collections.Generic;
using System.Timers;

namespace JobPlanner
{
    public class JobScheduler
    {
        private readonly Timer _timer;
        private readonly List<IJob> _jobs = new();

        public JobScheduler(int intervalMs)
        {
            _timer = new Timer(intervalMs);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public void AddHandler(IJob job)
        {
            _jobs.Add(job);
        }

        public void Start()
        {
            if (_jobs.Count == 0)
            {
                throw new ArgumentException("Not added jobs!");
            }

            _timer.Start();
        }

        public void Stop() => _timer.Stop();

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            foreach (var job in _jobs)
            {
                if (job.IsAlive)
                {
                    try
                    {
                        job.Execute(@event.SignalTime);
                    }
                    catch
                    {
                        Console.WriteLine($"An error has occurred in class {job.GetType().Name}. DateTime: {DateTime.Now}");
                        job .IsAlive= false;
                    }
                }
            }
        }
    }
}
