// using Xunit;
// using Moq;
// using System;
 
// namespace ToyRobot.Tests
// {
//     public sealed class MoveAttempterTests : IDisposable
//     {
//         private const int xCoordinate = 1;
//         private const int yCoordinate = 3;
        
//         private MockRepository mockRepository;
//         private Mock<IMoveStateTransformer> moveStateTransformer;
//         private Mock<ITableDimensions> tableDimensions;
//         private Mock<IRobotStateFactory> robotStateFactory;
//         private MoveAttempter moveAttempter;
        
//         public MoveAttempterTests()
//         {
//             this.mockRepository = new MockRepository(MockBehavior.Strict);
//             this.moveStateTransformer = this.mockRepository.Create<IMoveStateTransformer>();
//             this.tableDimensions = this.mockRepository.Create<ITableDimensions>();
//             this.robotStateFactory = this.mockRepository.Create<IRobotStateFactory>();
//             this.moveAttempter = new MoveAttempter(
//                 this.moveStateTransformer.Object,
//                 this.tableDimensions.Object,
//                 this.robotStateFactory.Object);
//         }

//         [Fact]
//         public void Attempt_PositionNotAllowed_ReturnsOriginalState()
//         {
//             var currentState = this.mockRepository.Create<IRobotState>();
//             var notAllowedState = this.mockRepository.Create<IRobotState>();
//             this.tableDimensions.Setup(
//                 d => d.IsPositionAllowed(notAllowedState.Object)).Returns(false);
//             this.moveStateTransformer.Setup(
//                 t => t.Transform(currentState.Object)).Returns(notAllowedState.Object);
//             Assert.Equal(this.moveAttempter.Attempt(currentState.Object), currentState.Object);
//         }
        
//         [Fact]
//         public void Attempt_PositionAllowed_ReturnsNewState()
//         {
//             var currentState = this.mockRepository.Create<IRobotState>();
//             var allowedState = this.mockRepository.Create<IRobotState>();
//             this.tableDimensions.Setup(
//                 d => d.IsPositionAllowed(allowedState.Object)).Returns(true);
//             this.moveStateTransformer.Setup(
//                 t => t.Transform(currentState.Object)).Returns(allowedState.Object);
//             Assert.Equal(this.moveAttempter.Attempt(currentState.Object), allowedState.Object);
//         }

//         public void Dispose()
//         {
//         }
//     }
// }