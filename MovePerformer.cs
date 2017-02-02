using System;

namespace ToyRobot
{    
    public sealed class MovePerformer : ICommandPerformer
    {
        private IMoveAttempter moveAttempter;
        
        public MovePerformer(IMoveAttempter moveAttempter)
        {
            this.moveAttempter = moveAttempter;
        }

        public IRobotState Perform(IRobotState currentState)
        {
            if (currentState.IsPlaced())
            {
                return this.moveAttempter.Attempt(currentState);
            }

            return currentState;
        }
    }

    public interface IMoveAttempter
    {
        IRobotState Attempt(IRobotState currentState);
    }

    public sealed class MoveAttempter : IMoveAttempter
    {
        private IMoveStateTransformer moveStateTransformer;
        private ITableDimensions tableDimensions;
        private IRobotStateFactory robotStateFactory;

        public MoveAttempter(
            IMoveStateTransformer moveStateTransformer,
            ITableDimensions tableDimensions,
            IRobotStateFactory robotStateFactory)
        {
            this.moveStateTransformer = moveStateTransformer;
            this.tableDimensions = tableDimensions;
            this.robotStateFactory = robotStateFactory;
        }

        public IRobotState Attempt(IRobotState currentState)
        {
            var newState = this.moveStateTransformer.Transform(currentState);
            if (this.tableDimensions.IsPositionAllowed(newState))
            {
                return newState;
            }

            return currentState;
        }
    }

    public interface IMoveStateTransformer
    {
        IRobotState Transform(IRobotState currentState);
    }

    // make singleton
    public sealed class MoveStateTransformer : IMoveStateTransformer
    {
        private IRobotStateBuilderFactory robotStateBuilderFactory;

        public MoveStateTransformer(IRobotStateBuilderFactory robotStateBuilderFactory)
        {
            this.robotStateBuilderFactory = robotStateBuilderFactory;
        }
        public IRobotState Transform(IRobotState currentState)
        {
            var robotStateBuilder =
                this.robotStateBuilderFactory.CreateBuilderFromPrototype(currentState);
            var compassDirection = currentState.GetCompassDirection();
            switch (compassDirection)
            {
                case CompassDirection.North:
                    return SetNewY(robotStateBuilder, currentState.GetY() + 1);
                case CompassDirection.East:
                    return SetNewX(robotStateBuilder, currentState.GetX() + 1);
                case CompassDirection.South:
                    return SetNewY(robotStateBuilder, currentState.GetY() - 1);
                case CompassDirection.West:
                    return SetNewX(robotStateBuilder, currentState.GetX() - 1);
                default:
                    var exceptionMessage = string.Format(
                        "unexpected compass direction: {{0}} for attempting move",
                        compassDirection);
                    throw new Exception(exceptionMessage);
            }
        }

        private IRobotState SetNewX(IRobotStateBuilder robotStateBuilder, int newX)
        {
            return robotStateBuilder.WithX(newX).Build();
        }
        private IRobotState SetNewY(IRobotStateBuilder robotStateBuilder, int newY)
        {
            return robotStateBuilder.WithY(newY).Build();
        }
    }
}
