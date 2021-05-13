using System;

namespace JobPlanner
{
    public interface IJob
    {
        bool IsFailed { get; set; }

        DateTime StartJob { get; set; }

        void Execute(DateTime signalTime);
    }
}
