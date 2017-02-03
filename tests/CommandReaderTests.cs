using Xunit;
using Moq;
using System;
using System.Collections.Generic;
 
namespace ToyRobot.Tests
{
    public sealed class CommandReaderTests : IDisposable
    {
        private const string UnparsedCommand = "UnparsedCommand";

        private static IReadOnlyCollection<string> UnparsedCommands = 
            new List<string> {UnparsedCommand};
        
        private MockRepository mockRepository;
        private Mock<IToyRobot> toyRobot;
        private Mock<ICommandParser> commandParser;
        private CommandReader commandReader;
        
        public CommandReaderTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.toyRobot = this.mockRepository.Create<IToyRobot>();
            this.commandParser = this.mockRepository.Create<ICommandParser>();
            this.commandReader = new CommandReader(
                this.toyRobot.Object,
                this.commandParser.Object);
        }

        [Fact]
        public void Read_CommandNotParsed_DoesNothing()
        {
            ICommandPerformer commandPerformer = null;
            this.commandParser.Setup(
                p => p.TryGetCommandPerformer(UnparsedCommand, out commandPerformer)).Returns(false);
            this.commandReader.Read(UnparsedCommands);
        }

        [Fact]
        public void Read_CommandParsed_CallsUpdate()
        {
            var commandPerformerMock = this.mockRepository.Create<ICommandPerformer>();
            var commandPerformer = commandPerformerMock.Object;
            this.commandParser.Setup(
                p => p.TryGetCommandPerformer(UnparsedCommand, out commandPerformer)).Returns(true);
            this.toyRobot.Setup(r => r.Update(commandPerformer));
            this.commandReader.Read(UnparsedCommands);
            this.toyRobot.Verify(r => r.Update(commandPerformer));
        }

        public void Dispose()
        {
        }
    }


    // {
    //     private const int xCoordinate = 1;
    //     private const int yCoordinate = 3;
    //     private const string xCoordinateString = "1";
    //     private const string yCoordinateString = "3";

    //     [Fact]
    //     public void Read_WithReportMessage_SendsReportPerformer()
    //     {
    //         var reportPerformer = this.mockRepository.Create<ICommandPerformer>();
    //         this.commandPerformerFactory.Setup(f => f.CreateReportPerformer())
    //             .Returns(reportPerformer.Object);
    //         this.toyRobot.Setup(r => r.Update(reportPerformer.Object));
    //         this.commandReader.Read(new[] { "REPORT" });
    //         this.toyRobot.Verify(r => r.Update(reportPerformer.Object));
    //     }

    //     [Fact]
    //     public void Read_WithMoveMessage_SendsMovePerformer()
    //     {
    //         var movePerformer = this.mockRepository.Create<ICommandPerformer>();
    //         this.commandPerformerFactory.Setup(
    //             f => f.CreateMovePerformer()).Returns(movePerformer.Object);
    //         this.toyRobot.Setup(r => r.Update(movePerformer.Object));
    //         this.commandReader.Read(new[] { "MOVE" });
    //         this.toyRobot.Verify(r => r.Update(movePerformer.Object));
    //     }

    //     [Fact]
    //     public void Read_WithRightMessage_SendsTurnPerformer()
    //     {
    //         var turnPerformer = this.mockRepository.Create<ICommandPerformer>();
    //         this.commandPerformerFactory.Setup(f => f.CreateTurnPerformer(TurnDirection.Right))
    //             .Returns(turnPerformer.Object);
    //         this.toyRobot.Setup(r => r.Update(turnPerformer.Object));
    //         this.commandReader.Read(new[] { "RIGHT" });
    //         this.toyRobot.Verify(r => r.Update(turnPerformer.Object));
    //     }

    //     [Theory]
    //     [InlineData(CompassDirection.North, "NORTH")]
    //     [InlineData(CompassDirection.South, "SOUTH")]
    //     [InlineData(CompassDirection.East, "EAST")]
    //     [InlineData(CompassDirection.West, "WEST")]
    //     public void Read_WithPlaceMessage_SendsPlacePerformer
    //         (CompassDirection direction, string unparsedDirection)
    //     {
    //         var orientation = this.mockRepository.Create<IOrientation>();
    //         this.orientationFactory.Setup(
    //             f => f.Create(unparsedDirection)).Returns(orientation.Object);
    //         orientation.Setup(o => o.IsValid()).Returns(true);
    //         var placePerformer = this.mockRepository.Create<ICommandPerformer>();
    //         this.commandPerformerFactory.Setup(
    //             f => f.CreatePlacePerformer(xCoordinate, yCoordinate, orientation.Object))
    //             .Returns(placePerformer.Object);
    //         var robotPlaceCommandUnparsed = string.Format
    //             ("PLACE {0},{1},{2}", xCoordinateString, yCoordinateString, unparsedDirection);
    //         this.toyRobot.Setup(r => r.Update(placePerformer.Object));
    //         this.commandReader.Read(new[] { robotPlaceCommandUnparsed });
    //         this.toyRobot.Verify(r => r.Update(placePerformer.Object));
    //     }

    //     [Theory]
    //     [InlineData("PLACE 1,2,3")]
    //     [InlineData("PLACE 1,2")]
    //     [InlineData("PLACE")]
    //     [InlineData("UP")]
    //     [InlineData("")]
    //     [InlineData("REPORT ")]
    //     public void Read_WithIncorrectMessage_DoesNothing(string incorrectMessage)
    //     {
    //         var orientation = this.mockRepository.Create<IOrientation>();
    //         this.orientationFactory.Setup(
    //             f => f.Create(It.IsAny<string>())).Returns(orientation.Object);
    //         orientation.Setup(o => o.IsValid()).Returns(false);
    //         this.commandReader.Read(new[] { incorrectMessage });
    //     }

        
    // }
}
