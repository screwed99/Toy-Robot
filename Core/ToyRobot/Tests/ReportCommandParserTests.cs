using Xunit;
using Moq;
using System;
using ToyRobot.Interfaces;

namespace ToyRobot.Tests
{
    public sealed class ReportCommandParserTests : IDisposable
    {
        private const string ReportUnparsed = "REPORT";

        private MockRepository mockRepository;
        private Mock<ICommandPerformerFactory> commandPerformerFactory;
        private ReportCommandParser reportCommandParser;

        public ReportCommandParserTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.commandPerformerFactory = this.mockRepository.Create<ICommandPerformerFactory>();
            this.reportCommandParser = new ReportCommandParser(this.commandPerformerFactory.Object);
        }

        [Fact]
        public void TryGetCommandPerformer_WithReportMessage_ReturnsReportPerformer()
        {
            var reportPerformer = this.mockRepository.Create<ICommandPerformer>();
            this.commandPerformerFactory.Setup(f => f.CreateReportPerformer())
                .Returns(reportPerformer.Object);
            ICommandPerformer commandPerformer;
            Assert.True(
                this.reportCommandParser.TryGetCommandPerformer(ReportUnparsed, out commandPerformer));
            Assert.Equal(commandPerformer, reportPerformer.Object);
        }

        [Theory]
        [InlineData("'REPORT'")]
        [InlineData("REPORT ")]
        [InlineData("_REPORT")]
        [InlineData("")]
        [InlineData(" ")]
        public void TryGetCommandPerformer_WithIncorrectMessage_ReturnsFalse(string incorrectMessage)
        {
            ICommandPerformer commandPerformer;
            Assert.False(
                this.reportCommandParser.TryGetCommandPerformer(
                    incorrectMessage,
                    out commandPerformer));
            Assert.Null(commandPerformer);
        }

        public void Dispose()
        {
        }
    }
}