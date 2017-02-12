using Xunit;
using Moq;
using System;

namespace ToyRobot.Tests
{
    public sealed class CommandHandlerTests : IDisposable
    {
        private const string UnparsedCommand = "UnparsedCommand";
        
        private MockRepository mockRepository;
        private Mock<IToyRobot> toyRobot;
        private Mock<ICommandParser> commandParser;
        private CommandHandler commandHandler;
        
        public CommandHandlerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.toyRobot = this.mockRepository.Create<IToyRobot>();
            this.commandParser = this.mockRepository.Create<ICommandParser>();
            this.commandHandler = new CommandHandler(
                this.toyRobot.Object,
                this.commandParser.Object);
        }

        [Fact]
        public void Handle_CommandNotParsed_DoesNothing()
        {
            ICommandPerformer commandPerformer = null;
            this.commandParser.Setup(
                p => p.TryGetCommandPerformer(UnparsedCommand, out commandPerformer)).Returns(false);
            this.commandHandler.Handle(UnparsedCommand);
        }

        [Fact]
        public void Handle_CommandParsed_CallsUpdate()
        {
            var commandPerformerMock = this.mockRepository.Create<ICommandPerformer>();
            var commandPerformer = commandPerformerMock.Object;
            this.commandParser.Setup(
                p => p.TryGetCommandPerformer(UnparsedCommand, out commandPerformer)).Returns(true);
            this.toyRobot.Setup(r => r.Update(commandPerformer));
            this.commandHandler.Handle(UnparsedCommand);
            this.toyRobot.Verify(r => r.Update(commandPerformer));
        }

        public void Dispose()
        {
        }
    }
}
