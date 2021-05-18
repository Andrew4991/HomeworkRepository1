using System;

namespace JobPlanner
{
    public interface IJob
    {
        void Execute(DateTime signalTime);

        bool ShouldRun(DateTime signalTime);

        void MarkAsFailed();
    }
}
