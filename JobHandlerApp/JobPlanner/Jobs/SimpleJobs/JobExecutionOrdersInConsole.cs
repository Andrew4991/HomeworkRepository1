using System;
using AnalyticsProgram.Jobs;

namespace JobPlanner
{
    public class JobExecutionOrdersInConsole : BaseJob
    {
        public override void Execute(DateTime signalTime)
        {
            Console.WriteLine(OrdersUtils.GetOrders(signalTime));
        }

    }
}
