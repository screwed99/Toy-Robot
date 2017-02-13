using Xunit;
using Moq;
using System;
using ToyRobot.Interfaces;

namespace ToyRobot.Tests
{
    public sealed class MoveStateTransformerTests : IDisposable
    {
        private const int xCoordinate = 1;
        private const int yCoordinate = 3;
        
        private MockRepository mockRepository;
        private Mock<IRobotStateBuilderFactory> robotStateBuilderFactory;
        private MoveStateTransformer moveStateTransformer;
        
        public MoveStateTransformerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.robotStateBuilderFactory = this.mockRepository.Create<IRobotStateBuilderFactory>();
            this.moveStateTransformer =
                new MoveStateTransformer(robotStateBuilderFactory.Object);
        }
        
        [Theory]
        [InlineData(CompassDirection.North, xCoordinate, yCoordinate, 1, 4)]
        [InlineData(CompassDirection.East, xCoordinate, yCoordinate, 2, 3)]
        [InlineData(CompassDirection.South, xCoordinate, yCoordinate, 1, 2)]
        [InlineData(CompassDirection.West, xCoordinate, yCoordinate, 0, 3)]
        public void Transform_NotInvalidCompassDirection_ReturnsNewPosition(
            CompassDirection currentCompassDirection,
            int currentX,
            int currentY,
            int expectedOutputX,
            int expectedOutputY)
        {
            var currentState = this.mockRepository.Create<IRobotState>();
            currentState.Setup(s => s.GetCompassDirection()).Returns(currentCompassDirection);
            currentState.Setup(s => s.GetX()).Returns(currentX);
            currentState.Setup(s => s.GetY()).Returns(currentY);
            var robotStateBuilder = this.mockRepository.Create<IRobotStateBuilder>();
            this.robotStateBuilderFactory
                .Setup(f => f.CreateBuilderFromPrototype(currentState.Object))
                .Returns(robotStateBuilder.Object);
            robotStateBuilder.Setup(
                b => b.WithX(expectedOutputX)).Returns(robotStateBuilder.Object);
            robotStateBuilder.Setup(
                b => b.WithY(expectedOutputY)).Returns(robotStateBuilder.Object);
            var expectedState = this.mockRepository.Create<IRobotState>();
            robotStateBuilder.Setup(b => b.Build()).Returns(expectedState.Object);
            var outputState = this.moveStateTransformer.Transform(currentState.Object);
            Assert.Equal(outputState, expectedState.Object);
        }

        public void Dispose()
        {
        }
    }
}