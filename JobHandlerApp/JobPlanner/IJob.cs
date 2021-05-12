using System;

namespace JobPlanner
{
    public interface IJob
    {
        bool IsFailed { get; set; }

        void Execute(DateTime signalTime);
    }
}
