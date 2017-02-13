 using Xunit;
 using Moq;
 using System;
 using ToyRobot.Interfaces;

namespace ToyRobot.Tests
 {
     public sealed class MasterCommandParserTests : IDisposable
     {
         private const string UnparsedCommand = "UnparsedCommand";

         private MockRepository mockRepository;
         private Mock<ICommandParser> commandParser1;
         private Mock<ICommandParser> commandParser2;
         private MasterCommandParser masterCommandParser;

         public MasterCommandParserTests()
         {
             this.mockRepository = new MockRepository(MockBehavior.Strict);
             this.commandParser1 = this.mockRepository.Create<ICommandParser>();
             this.commandParser2 = this.mockRepository.Create<ICommandParser>();
             var commandParsers = new[]
                 {
                     this.commandParser1.Object,
                     this.commandParser2.Object
                 };
             this.masterCommandParser = new MasterCommandParser(commandParsers);
         }

         [Fact]
         public void TryGetCommandPerformer_AllParsersFalse_ReturnsFalse()
         {
             var commandPerformerMock = this.mockRepository.Create<ICommandPerformer>();
             var commandPerformer = commandPerformerMock.Object;
             this.commandParser1.Setup(p => p.TryGetCommandPerformer(UnparsedCommand, out commandPerformer))
                 .Returns(false);
             this.commandParser2.Setup(p => p.TryGetCommandPerformer(UnparsedCommand, out commandPerformer))
                 .Returns(false);
             ICommandPerformer actualCommandPerformer;
             var success =
                 this.masterCommandParser.TryGetCommandPerformer(UnparsedCommand, out actualCommandPerformer);
             Assert.Equal(success, false);
             Assert.Null(actualCommandPerformer);
         }

         public void Dispose()
         {
         }
     }
 }