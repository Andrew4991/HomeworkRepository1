using System;
using System.Threading;
using System.Threading.Tasks;
using JobPlanner;

namespace AnalyticsProgram.Jobs
{
    public abstract class BaseDelayedJob : BaseJob, IDelayedJob
    {
        private bool _hasRun;
        private readonly DateTime _startAt;

        protected BaseDelayedJob(DateTime signalTime)
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
