 using Xunit;
 using Moq;
 using System;
 using ToyRobot.Interfaces;

namespace ToyRobot.Tests
 {
     public sealed class PlaceCommandParserTests : IDisposable
     {
         private const string CommandUnparsed = "PLACE 1,3,SOUTH";
         private const string DirectionUnparsed = "SOUTH";
         private const int xCoordinate = 1;
         private const int yCoordinate = 3;

         private MockRepository mockRepository;
         private Mock<ICommandPerformerFactory> commandPerformerFactory;
         private Mock<IOrientationParser> orientationParser;
         private PlaceCommandParser placeCommandParser;

         public PlaceCommandParserTests()
         {
             this.mockRepository = new MockRepository(MockBehavior.Strict);
             this.commandPerformerFactory = this.mockRepository.Create<ICommandPerformerFactory>();
             this.orientationParser = this.mockRepository.Create<IOrientationParser>();
             this.placeCommandParser = new PlaceCommandParser(
                 this.commandPerformerFactory.Object,
                 this.orientationParser.Object);
         }

         [Fact]
         public void TryGetCommandPerformer_PlaceCommand_ReturnsPerformer()
         {
             var orientationMock = this.mockRepository.Create<IOrientation>();
             var orientation = orientationMock.Object;
             var commandPerformer = this.mockRepository.Create<ICommandPerformer>();
             this.orientationParser.Setup(
                 d => d.TryParse(DirectionUnparsed, out orientation)).Returns(true);
             this.commandPerformerFactory
                 .Setup(t => t.CreatePlacePerformer(xCoordinate, yCoordinate, orientation))
                 .Returns(commandPerformer.Object);
             ICommandPerformer actualCommandPerformer;
             var success = this.placeCommandParser.TryGetCommandPerformer(
                 CommandUnparsed, out actualCommandPerformer);
             Assert.Equal(success, true);
             Assert.Equal(actualCommandPerformer, commandPerformer.Object);
         }

         [Fact]
         public void TryGetCommandPerformer_InvalidDirection_ReturnsFalse()
         {
             const string invalidCommandUnparsed = "PLACE 1,2,3";
             const string invalidDirectionUnparsed = "3";
             var orientationMock = this.mockRepository.Create<IOrientation>();
             var orientation = orientationMock.Object;
             this.orientationParser.Setup(
                 d => d.TryParse(invalidDirectionUnparsed, out orientation)).Returns(false);
             ICommandPerformer actualCommandPerformer;
             var success = this.placeCommandParser.TryGetCommandPerformer(
                 invalidCommandUnparsed, out actualCommandPerformer);
             Assert.Equal(success, false);
             Assert.Null(actualCommandPerformer);
         }

         [Theory]
          [InlineData("PLACE 1,2")]
          [InlineData("PLACE")]
          [InlineData("UP")]
          [InlineData("")]
          [InlineData("REPORT ")]
          public void TryGetCommandPerformer_WithIncorrectMessage_ReturnsFalse(string incorrectMessage)
          {
              ICommandPerformer commandPerformer;
              Assert.False(
                  this.placeCommandParser.TryGetCommandPerformer(
                      incorrectMessage,
                      out commandPerformer));
              Assert.Null(commandPerformer);
          }

         public void Dispose()
         {
         }
     }
 }