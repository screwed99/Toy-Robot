using Xunit;
using Moq;
using System;
using ToyRobot.Interfaces;

namespace ToyRobot.Tests
{
    public sealed class TurnPerformerTests : IDisposable
    {
        private const int xCoordinate = 1;
        private const int yCoordinate = 3;
        
        private MockRepository mockRepository;
        private Mock<IOrientationTurner> orientationTurner;
        private TurnPerformer turnPerformer;
        
        public TurnPerformerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.orientationTurner = this.mockRepository.Create<IOrientationTurner>();
            this.turnPerformer = new TurnPerformer(this.orientationTurner.Object);
        }

        [Fact]
        public void Perform_WithStateNotPlaced_StateStillNotPlaced()
        {
            var robotState = this.mockRepository.Create<IRobotState>();
            robotState.Setup(r => r.IsPlaced()).Returns(false);
            var outputRobotState = this.turnPerformer.Perform(robotState.Object);
            Assert.Equal(robotState.Object, outputRobotState);
            robotState.Verify(r => r.IsPlaced());
        }

        [Fact]
        public void Perform_WithStatePlaced_CreatesNewState()
        {
            var currentRobotState = this.mockRepository.Create<IRobotState>();
            currentRobotState.Setup(r => r.IsPlaced()).Returns(true);
            var newOrientation = this.mockRepository.Create<IOrientation>();
            var newRobotState = this.mockRepository.Create<IRobotState>();
            this.orientationTurner.Setup(t => t.Turn(currentRobotState.Object))
                .Returns(newRobotState.Object);
            var outputRobotState = this.turnPerformer.Perform(currentRobotState.Object);
            Assert.Equal(newRobotState.Object, outputRobotState);
            currentRobotState.Verify(r => r.IsPlaced());
        }

        public void Dispose()
        {
        }
    }
}