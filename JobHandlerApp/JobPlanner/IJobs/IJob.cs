using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobPlanner
{
    public interface IJob
    {
        Task Execute(DateTime signalTime, CancellationToken token);

        Task<bool> ShouldRun(DateTime signalTime);

        void MarkAsFailed();
    }
}
