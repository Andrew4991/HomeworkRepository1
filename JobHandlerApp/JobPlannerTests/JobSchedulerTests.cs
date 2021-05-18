using System;
using System.Threading.Tasks;
using FluentAssertions;
using JobPlanner;
using Moq;
using Xunit;

namespace JobPlannerTests
{
    public class JobSchedulerTests
    {
        [Fact]
        public async Task Start_ShouldRun_ExecutesJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);

            var scheduler = new JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task Start_ShouldNotRun_DoesNotExecuteJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(false);

            var scheduler = new JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Start_ThrowsException_MarksJobAsFailed()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);
            mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>())).Throws<Exception>();

            var scheduler = new JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(300);

            // assert
            mockedJob.Verify(x => x.MarkAsFailed(), Times.AtLeast(1));
        }

        [Fact]
        public async Task Start_ShouldRun_ExecutesDelayedJob()
        {
            // arrange
            var mockedJob = new Mock<IDelayedJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);

            var scheduler = new JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task Start_ShouldNotRun_DoesNotExecuteDelayedJob()
        {
            // arrange
            var mockedJob = new Mock<IDelayedJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(false);

            var scheduler = new JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Start_ThrowsException_MarksDelayedJobAsFailed()
        {
            // arrange
            var mockedJob = new Mock<IDelayedJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);
            mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>())).Throws<Exception>();

            var scheduler = new JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(300);

            // assert
            mockedJob.Verify(x => x.MarkAsFailed(), Times.AtLeast(1));
        }

        [Fact]
        public void Start_NotJobs_ThrowsException()
        {
            // arrange
            var scheduler = new JobScheduler(200);

            // act
            Action act = () => scheduler.Start();

            // assert
            act.Should().Throw<ArgumentException>("Not added jobs!");
        }

        [Fact]
        public async Task Stop_ShouldRun_ExecutesJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);

            var scheduler = new JobScheduler(100);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);
            scheduler.Stop();

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Stop_ShouldNotRun_DoesNotExecuteJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(false);

            var scheduler = new JobScheduler(100);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(50);
            scheduler.Stop();

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Stop_ThrowsException_MarksJobAsFailed()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);
            mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>())).Throws<Exception>();

            var scheduler = new JobScheduler(100);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);
            scheduler.Stop();

            // assert
            mockedJob.Verify(x => x.MarkAsFailed(), Times.AtLeast(1));
        }

    }
}
