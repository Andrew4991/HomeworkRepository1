﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using JobPlanner;
using JobPlanner.Wrappers;
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
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x =>x.ShouldRun(It.IsAny<DateTime>())).Returns(Task<bool>.FromResult(true));

            var scheduler = new JobScheduler(mockedConsole.Object, 200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Start_ShouldNotRun_DoesNotExecuteJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(Task<bool>.FromResult(false));

            var scheduler = new JobScheduler(mockedConsole.Object, 200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Start_ThrowsException_MarksJobAsFailed()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(Task<bool>.FromResult(true));
            mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var scheduler = new JobScheduler(mockedConsole.Object, 200);
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
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).ReturnsAsync(true);

            var scheduler = new JobScheduler(mockedConsole.Object, 200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Start_ShouldNotRun_DoesNotExecuteDelayedJob()
        {
            // arrange
            var mockedJob = new Mock<IDelayedJob>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).ReturnsAsync(false);

            var scheduler = new JobScheduler(mockedConsole.Object, 200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Start_ThrowsException_MarksDelayedJobAsFailed()
        {
            // arrange
            var mockedJob = new Mock<IDelayedJob>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).ReturnsAsync(true);
            mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var scheduler = new JobScheduler(mockedConsole.Object, 200);
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
            var mockedConsole = new Mock<IConsoleWrapper>();
            var scheduler = new JobScheduler(mockedConsole.Object, 200);

            // act
            Action act = () => scheduler.Start();

            // assert
            act.Should().Throw<ArgumentException>("Not added jobs!");
        }

        [Fact]
        public async Task Stop_ShouldRunAndCallStartStop_ExecutesJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).ReturnsAsync(true);

            var scheduler = new JobScheduler(mockedConsole.Object, 100);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);
            scheduler.Stop();

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Stop_ShouldNotRunAndCallStartStop_DoesNotExecuteJob()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).ReturnsAsync(true);

            var scheduler = new JobScheduler(mockedConsole.Object, 100);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(90);
            scheduler.Stop();

            // assert
            mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Stop_ThrowsException_CallMarkAsFailed()
        {
            // arrange
            var mockedJob = new Mock<IJob>();
            var mockedConsole = new Mock<IConsoleWrapper>();
            mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).ReturnsAsync(true);
            mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>(), It.IsAny<IConsoleWrapper>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            var scheduler = new JobScheduler(mockedConsole.Object, 100);
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
