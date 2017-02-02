using Xunit;
using Moq;
using System;
 
namespace ToyRobot.Tests
{
    public sealed class MovePerformerTests : IDisposable
    {
        private const int xCoordinate = 1;
        private const int yCoordinate = 3;
        
        private MockRepository mockRepository;
        private Mock<IMoveAttempter> moveAttempter;
        private MovePerformer movePerformer;
        
        public MovePerformerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.moveAttempter = this.mockRepository.Create<IMoveAttempter>();
            this.movePerformer = new MovePerformer(
                this.moveAttempter.Object);
        }

        [Fact]
        public void Perform_WithStateNotPlaced_StateStillNotPlaced()
        {
            var robotState = this.mockRepository.Create<IRobotState>();
            robotState.Setup(r => r.IsPlaced()).Returns(false);
            var outputRobotState = this.movePerformer.Perform(robotState.Object);
            Assert.Equal(robotState.Object, outputRobotState);
            robotState.Verify(r => r.IsPlaced());
        }

        [Fact]
        public void Perform_WithStatePlaced_ReturnsFromMoveAttempter()
        {
            var currentRobotState = this.mockRepository.Create<IRobotState>();
            currentRobotState.Setup(r => r.IsPlaced()).Returns(true);
            var newRobotState = this.mockRepository.Create<IRobotState>();
            this.moveAttempter.Setup(f => f.Attempt(currentRobotState.Object))
                .Returns(newRobotState.Object);
            var outputRobotState = this.movePerformer.Perform(currentRobotState.Object);
            Assert.Equal(newRobotState.Object, outputRobotState);
            currentRobotState.Verify(r => r.IsPlaced());
        }
        
        public void Dispose()
        {
        }
    }
}