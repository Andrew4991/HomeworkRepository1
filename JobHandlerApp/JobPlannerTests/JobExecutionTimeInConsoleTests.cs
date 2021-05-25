using System;
using JobPlanner;
using Moq;
using Xunit;
using ShopApp;
using System.Threading;
using System.Collections.Generic;
using JobPlanner.Wrappers;

namespace JobPlannerTests
{
    public class JobExecutionTimeInConsoleTests
    {
        [Fact]
        public void Execute_Always_CallConsoleWrapperOneTime()
        {
            // arrange
            var mockedConsole = new Mock<IConsoleWrapper>();
            var time = DateTime.Now;

            var job = new JobExecutionTimeInConsole();

            // act
            job.Execute(time, mockedConsole.Object, It.IsAny<CancellationToken>());

            // assert
            mockedConsole.Verify(x => x.WriteLine($"Executed: {time}"));
        }
    }
}
