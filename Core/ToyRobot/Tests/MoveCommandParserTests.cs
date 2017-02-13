 using Xunit;
 using Moq;
 using System;
 using ToyRobot.Interfaces;

namespace ToyRobot.Tests
 {
     public sealed class MoveCommandParserTests : IDisposable
     {
         private const string UnparsedMoveCommand = "MOVE";

         private MockRepository mockRepository;
         private Mock<ICommandPerformerFactory> commandPerformerFactory;
         private MoveCommandParser moveCommandParser;

         public MoveCommandParserTests()
         {
             this.mockRepository = new MockRepository(MockBehavior.Strict);
             this.commandPerformerFactory =
                 this.mockRepository.Create<ICommandPerformerFactory>();
             this.moveCommandParser =
                 new MoveCommandParser(this.commandPerformerFactory.Object);
         }

         [Fact]
         public void TryGetCommandPerformer_MoveCommand_ReturnsTrueAndPerformer()
         {
             var commandPerformer = this.mockRepository.Create<ICommandPerformer>();
             this.commandPerformerFactory.Setup(
                 f => f.CreateMovePerformer()).Returns(commandPerformer.Object);
             ICommandPerformer actualCommandPerformer;
             var success = this.moveCommandParser.TryGetCommandPerformer(
                 UnparsedMoveCommand, out actualCommandPerformer);
             Assert.Equal(success, true);
             Assert.Equal(actualCommandPerformer, commandPerformer.Object);
         }

         [Theory]
         [InlineData("'MOVE'")]
         [InlineData("MOVE ")]
         [InlineData("_MOVE")]
         [InlineData("")]
         [InlineData(" ")]
         public void TryGetCommandPerformer_WithIncorrectMessage_ReturnsFalse(string incorrectMessage)
         {
             ICommandPerformer commandPerformer;
             Assert.False(
                 this.moveCommandParser.TryGetCommandPerformer(
                     incorrectMessage,
                     out commandPerformer));
             Assert.Null(commandPerformer);
         }

         public void Dispose()
         {
         }
     }
 }