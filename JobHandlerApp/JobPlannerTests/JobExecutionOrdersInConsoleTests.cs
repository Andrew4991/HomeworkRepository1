using System;
using JobPlanner;
using Moq;
using Xunit;
using ShopApp;
using System.Threading;

namespace JobPlannerTests
{
    public class JobExecutionOrdersInConsoleTests
    {
        [Fact]
        public void Execute_Always_CallGetProductsPurchasedForAllCustomersOneTime()
        {
            // arrange
            var mockedRepository = new Mock<IRepository>();
            mockedRepository.Setup(x => x.GetProductsPurchasedForAllCustomers()).Returns(new System.Collections.Generic.List<ProductsOverView>
            {
                new ProductsOverView("Tom", "Phone", 100, 1)
            });
            var job = new JobExecutionOrdersInConsole(mockedRepository.Object);

            // act
            job.Execute(DateTime.Now, It.IsAny<CancellationToken>());

            // assert
            mockedRepository.Verify(x => x.GetProductsPurchasedForAllCustomers(), Times.Once);
        }
    }
}
