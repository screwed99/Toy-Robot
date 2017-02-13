using Xunit;
using Moq;
using System;
using ToyRobot.Interfaces;

namespace NRobotsCannotOverlap.Tests
{
    public sealed class IntegrationTests : IDisposable
    {
        private const int NumRobots = 3;

        private MockRepository mockRepository;
        private Mock<ITextInputter> textInputter;
        private Mock<ITextOutputter> textOutputter;
        private ToyRobotAssembly toyRobotAssembly;
        
        public IntegrationTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.textInputter = this.mockRepository.Create<ITextInputter>();
            this.textOutputter = this.mockRepository.Create<ITextOutputter>();
            this.toyRobotAssembly = new ToyRobotAssembly(
                this.textInputter.Object,
                this.textOutputter.Object,
                NumRobots);
        }

        [Fact]
        public void Read_Always_ReturnsExpectedOutput()
        {
            var input = new[]
				{
					"PLACE 2,1,EAST",
					"REPORT",
					"MOVE",
					"LEFT",
					"PLACE 4,4,NORTH",
					"MOVE",
					"LEFT",
					"MOVE",
					"REPORT",
				    "REPORT",
				    "REPORT",
				    "REPORT"
				};
            this.textInputter.Setup(i => i.GetAllLines()).Returns(input);
			this.textOutputter.Setup(o => o.WriteLine("2,1,WEST"));
            this.textOutputter.Setup(o => o.WriteLine("4,4,NORTH"));
            this.toyRobotAssembly.ToyRobotDriver.Run();
			this.textOutputter.Verify(o => o.WriteLine("2,1,WEST"));
            this.textOutputter.Verify(o => o.WriteLine("4,4,NORTH"));
        }

		public void Dispose()
        {
        }
	}
}