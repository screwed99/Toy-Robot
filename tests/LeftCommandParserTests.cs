using Xunit;
using Moq;
using System;
 
namespace ToyRobot.Tests
{
    public sealed class LeftCommandParserTests : IDisposable
    {
        private const string LeftUnparsed = "LEFT";
        
        private MockRepository mockRepository;
        private Mock<ICommandPerformerFactory> commandPerformerFactory;
        private LeftCommandParser leftCommandParser;
        
        public LeftCommandParserTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.commandPerformerFactory = this.mockRepository.Create<ICommandPerformerFactory>();
            this.leftCommandParser = new LeftCommandParser(this.commandPerformerFactory.Object);
        }

        [Fact]
        public void TryGetCommandPerformer_WithLeftMessage_ReturnsLeftTurnPerformer()
        {
            var turnPerformer = this.mockRepository.Create<ICommandPerformer>();
            this.commandPerformerFactory.Setup(f => f.CreateTurnPerformer(TurnDirection.Left))
                .Returns(turnPerformer.Object);
            ICommandPerformer commandPerformer;
            Assert.True(
                this.leftCommandParser.TryGetCommandPerformer(LeftUnparsed, out commandPerformer));
            Assert.Equal(commandPerformer, turnPerformer.Object);
        }
        
        [Theory]
        [InlineData("'LEFT'")]
        [InlineData("LEFT ")]
        [InlineData("_LEFT")]
        [InlineData("")]
        [InlineData(" ")]
        public void TryGetCommandPerformer_WithIncorrectMessage_ReturnsFalse(string incorrectMessage)
        {
            ICommandPerformer commandPerformer;
            Assert.False(
                this.leftCommandParser.TryGetCommandPerformer(
                   incorrectMessage,
                    out commandPerformer));
            Assert.Null(commandPerformer);
        }

        public void Dispose()
        {
        }
    }
}