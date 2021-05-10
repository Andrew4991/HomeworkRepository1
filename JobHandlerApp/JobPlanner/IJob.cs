using System;

namespace JobPlanner
{
    public interface IJob
    {
        public bool IsAlive { get; set; }

        void Execute(DateTime signalTime);
    }
}
