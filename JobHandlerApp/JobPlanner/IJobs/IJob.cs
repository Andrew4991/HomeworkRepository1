using System;
using System.Threading;
using System.Threading.Tasks;
using JobPlanner.Wrappers;

namespace JobPlanner
{
    public interface IJob
    {
        Task Execute(DateTime signalTime, IConsoleWrapper console, CancellationToken token);

        Task<bool> ShouldRun(DateTime signalTime);

        void MarkAsFailed();
    }
}
