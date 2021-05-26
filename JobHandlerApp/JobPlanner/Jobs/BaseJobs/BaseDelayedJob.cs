using System;
using System.Threading;
using System.Threading.Tasks;
using JobPlanner;
using JobPlanner.Wrappers;

namespace AnalyticsProgram.Jobs
{
    public abstract class BaseDelayedJob : BaseJob, IDelayedJob
    {
        private bool _hasRun;
        private readonly DateTime _startAt;

        protected BaseDelayedJob(IConsoleWrapper console, DateTime signalTime) : base(console)
        {
            _startAt = signalTime;
        }

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            _hasRun = true;
            return Task.CompletedTask;
        }

        public override async Task<bool> ShouldRun(DateTime signalTime)
        {
            return await base.ShouldRun(signalTime) && _startAt < signalTime && !_hasRun;
        }
    }
}
