using Xunit;
using Moq;
using System;
 
namespace ToyRobot.Tests
{
    public sealed class ReportPerformerTests : IDisposable
    {
        private const string OrientationDescription = "OrientationDescription";
        private const string ReportString = "1,3,OrientationDescription";
        private const int xCoordinate = 1;
        private const int yCoordinate = 3;
        
        private MockRepository mockRepository;
        private Mock<ITextOutputter> textOutputter;
        private ReportPerformer reportPerformer;
        private Mock<IRobotState> currentState;
        private Mock<IOrientation> orientation;
        
        public ReportPerformerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.textOutputter = this.mockRepository.Create<ITextOutputter>();
            this.reportPerformer = new ReportPerformer(this.textOutputter.Object);
            this.currentState = this.mockRepository.Create<IRobotState>();
            this.orientation = this.mockRepository.Create<IOrientation>();
        }

        [Fact]
        public void Perform_IsPlaced_CallsOutputter()
        {
            this.currentState.Setup(d => d.IsPlaced()).Returns(true);
            this.currentState.Setup(d => d.GetX()).Returns(xCoordinate);
            this.currentState.Setup(d => d.GetY()).Returns(yCoordinate);
            this.currentState.Setup(d => d.GetOrientation()).Returns(this.orientation.Object);
            this.orientation.Setup(d => d.GetDescription()).Returns(OrientationDescription);
            this.textOutputter.Setup(o => o.WriteLine(ReportString));
            var outputState = this.reportPerformer.Perform(this.currentState.Object);
            Assert.Equal(outputState, currentState.Object);
            this.textOutputter.Verify(o => o.WriteLine(ReportString));
        }

        [Fact]
        public void Perform_IsNotPlaced_DoesNotCallOutputter()
        {
            this.currentState.Setup(d => d.IsPlaced()).Returns(false);
            var outputState = this.reportPerformer.Perform(this.currentState.Object);
            Assert.Equal(outputState, currentState.Object);
        }

        public void Dispose()
        {
        }
    }
}