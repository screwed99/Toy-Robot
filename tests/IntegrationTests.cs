using Xunit;
using Moq;
using System;
 
namespace ToyRobot.Tests
{
    public sealed class IntegrationTests : IDisposable
    {
        private MockRepository mockRepository;
        private Mock<ITextOutputter> textOutputter;
        private ToyRobotAssembly toyRobotAssembly;
        
        public IntegrationTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.textOutputter = this.mockRepository.Create<ITextOutputter>();
            this.toyRobotAssembly = new ToyRobotAssembly(this.textOutputter.Object);
        }

        [Fact]
        public void Read_Always_ReturnsExpectedOutput()
        {
            var input = new[]
				{
					"PLACE -1,-1,EAST",
					"REPORT",
					"MOVE",
					"LEFT",
					"PLACE 4,4,NORTH",
					"MOVE",
					"LEFT",
					"MOVE",
					"REPORT"
				};
			this.textOutputter.Setup(o => o.WriteLine("3,4,WEST"));
			this.toyRobotAssembly.CommandReader.Read(input);
			this.textOutputter.Verify(o => o.WriteLine("3,4,WEST"));
        }

		public void Dispose()
        {
        }
	}
}