﻿using System;
using System.Threading;
using System.Threading.Tasks;
using JobPlanner;
using JobPlanner.Wrappers;

namespace AnalyticsProgram.Jobs
{
    public abstract class BaseJob : IJob
    {
        private bool _isFailed;
        
        protected IConsoleWrapper _console;

        public BaseJob(IConsoleWrapper console)
        {
            _console = console;
        }

        public abstract Task Execute(DateTime signalTime, CancellationToken token);

        public virtual Task<bool> ShouldRun(DateTime signalTime)
        {
            return Task.FromResult(!_isFailed);
        }

        public virtual void MarkAsFailed()
        {
            _isFailed = true;
        }
    }
}
