using Xunit;
using Moq;
using System;

namespace ToyRobot.Tests
{
    public sealed class RightCommandParserTests : IDisposable
    {
        private const string RightUnparsed = "RIGHT";

        private MockRepository mockRepository;
        private Mock<ICommandPerformerFactory> commandPerformerFactory;
        private RightCommandParser rightCommandParser;

        public RightCommandParserTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.commandPerformerFactory =
                this.mockRepository.Create<ICommandPerformerFactory>();
            this.rightCommandParser = new RightCommandParser(
                this.commandPerformerFactory.Object);
        }

        [Fact]
        public void TryGetCommandPerformer_WithLeftMessage_ReturnsRightTurnPerformer()
        {
            var turnPerformer = this.mockRepository.Create<ICommandPerformer>();
            this.commandPerformerFactory.Setup(f => f.CreateRightTurnPerformer())
                .Returns(turnPerformer.Object);
            ICommandPerformer commandPerformer;
            Assert.True(
                this.rightCommandParser.TryGetCommandPerformer(RightUnparsed, out commandPerformer));
            Assert.Equal(commandPerformer, turnPerformer.Object);
        }

        [Theory]
        [InlineData("'RIGHT'")]
        [InlineData("RIGHT ")]
        [InlineData("_RIGHT")]
        [InlineData("")]
        [InlineData(" ")]
        public void TryGetCommandPerformer_WithIncorrectMessage_ReturnsFalse(string incorrectMessage)
        {
            ICommandPerformer commandPerformer;
            Assert.False(
                this.rightCommandParser.TryGetCommandPerformer(
                    incorrectMessage,
                    out commandPerformer));
            Assert.Null(commandPerformer);
        }

        public void Dispose()
        {
        }
    }
}