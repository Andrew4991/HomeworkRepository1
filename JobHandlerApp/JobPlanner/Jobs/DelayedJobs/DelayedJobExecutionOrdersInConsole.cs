using System;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class DelayedJobExecutionOrdersInConsole : BaseDelayedJob
    {
        public DelayedJobExecutionOrdersInConsole(DateTime timeStart) :base(timeStart)
        {
        }

        public override void Execute(DateTime signalTime)
        {
            base.Execute(signalTime);

            Console.WriteLine(OrdersUtils.GetOrders(signalTime));
        }
    }
}
