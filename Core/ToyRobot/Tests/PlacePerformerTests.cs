using Xunit;
using Moq;
using System;
using ToyRobot.Interfaces;

namespace ToyRobot.Tests
{
    public sealed class PlacePerformerTests : IDisposable
    {
        private const int xCoordinate = 1;
        private const int yCoordinate = 3;
        
        private MockRepository mockRepository;
        private Mock<IRobotStateFactory> robotStateFactory;
        private Mock<ITableDimensions> tableDimensions;
        private Mock<IOrientation> orientation;
        private PlacePerformer placePerformer;
        private Mock<IRobotState> currentState;
        
        public PlacePerformerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.robotStateFactory = this.mockRepository.Create<IRobotStateFactory>();
            this.tableDimensions = this.mockRepository.Create<ITableDimensions>();
            this.orientation = this.mockRepository.Create<IOrientation>();
            this.placePerformer = new PlacePerformer(
                this.robotStateFactory.Object,
                this.tableDimensions.Object,
                xCoordinate,
                yCoordinate,
                this.orientation.Object);
            this.currentState = this.mockRepository.Create<IRobotState>();
        }

        [Fact]
        public void Perform_PositionAllowed_ReturnNewState()
        {
            var newRobotState = this.mockRepository.Create<IRobotState>();
            this.robotStateFactory.Setup(
                f => f.Create(xCoordinate, yCoordinate, this.orientation.Object))
                .Returns(newRobotState.Object);
            this.tableDimensions.Setup(d => d.IsPositionAllowed(newRobotState.Object)).Returns(true);
            var outputState = this.placePerformer.Perform(this.currentState.Object);
            Assert.Equal(outputState, newRobotState.Object);
        }

        [Fact]
        public void Perform_PositionNotAllowed_ReturnCurrentState()
        {
            var newRobotState = this.mockRepository.Create<IRobotState>();
            this.robotStateFactory.Setup(
                f => f.Create(xCoordinate, yCoordinate, this.orientation.Object))
                .Returns(newRobotState.Object);
            this.tableDimensions.Setup(d => d.IsPositionAllowed(newRobotState.Object)).Returns(false);
            var outputState = this.placePerformer.Perform(this.currentState.Object);
            Assert.Equal(outputState, currentState.Object);
        }

        public void Dispose()
        {
        }
    }
}