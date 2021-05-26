using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public class JobScheduler
    {
        private readonly System.Timers.Timer _timer;
        private readonly List<IJob> _jobs = new();
        private readonly List<IDelayedJob> _delayedJobs = new();
        private CancellationTokenSource _cancelTokenSource;
        private IConsoleWrapper _console;

        public JobScheduler(IConsoleWrapper console, int intervalMs)
        {
            _console = console;
            _timer = new System.Timers.Timer(intervalMs);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public void RegisterJob(IJob job)
        {
            _jobs.Add(job);
        }

        public void RegisterJob(IDelayedJob job)
        {
            _delayedJobs.Add(job);
        }

        public void Start()
        {
            if (_jobs.Count == 0 && _delayedJobs.Count == 0)
            {
                throw new ArgumentException("Not added jobs!");
            }

            _timer.Start();
        }

        public void CancelJobs()
        {
            if (_cancelTokenSource != null)
            {
                _cancelTokenSource.Cancel();
            }
        }

        public void Stop()
        {
            if (_timer.Enabled)
            {
                CancelJobs();
                _timer.Stop();
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            _cancelTokenSource = new();

            OnTimedEventAsync(@event).GetAwaiter().GetResult();
        }

        private async Task OnTimedEventAsync(ElapsedEventArgs @event)
        {
            await ExecuteSimpleJobs(@event);
            await ExecuteDelayedJobs(@event);
        }

        private async Task ExecuteSimpleJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_jobs, @event.SignalTime);
        }

        private async Task ExecuteDelayedJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_delayedJobs.Select(x => x as IJob), @event.SignalTime);
        }

        private async Task ExecuteJobs(IEnumerable<IJob> jobs, DateTime startAt)
        {
            foreach (var job in jobs)
            {
                if (await job.ShouldRun(startAt))
                {
                   await ExecuteJob(job, startAt);
                } 
            }
        }

        private async Task ExecuteJob(IJob job, DateTime signalTime)
        {
            try
            {
               await job.Execute(signalTime, _cancelTokenSource.Token);
            }
            catch (OperationCanceledException e)
            {
                _console.WriteLine($"Operation was cancelled with exception. {e.Message}");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                job.MarkAsFailed();
            }
        }
    }
}
