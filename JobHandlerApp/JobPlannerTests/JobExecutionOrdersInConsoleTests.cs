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
    public class JobExecutionOrdersInConsoleTests
    {
        [Fact]
        public void Execute_Always_CallGetProductsPurchasedForAllCustomersOneTime()
        {
            // arrange
            var mockedRepository = new Mock<IRepository>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            var time = DateTime.Now;
            mockedRepository.Setup(x => x.GetProductsPurchasedForAllCustomers()).Returns(new List<ProductsOverView>
            {
                new ProductsOverView("Tom", "Phone", 100, 1)
            });
            var job = new JobExecutionOrdersInConsole(mockedRepository.Object);

            // act
            job.Execute(time, mockedConsole.Object, It.IsAny<CancellationToken>());

            // assert
            mockedRepository.Verify(x => x.GetProductsPurchasedForAllCustomers(), Times.Once);
            mockedConsole.Verify(x => x.WriteLine($"Executed:{time}.\tCustomer name: Tom\tProduct name: Phone\tProduct price: {100}\tProduct amount: {1}\n"));
        }
    }
}
