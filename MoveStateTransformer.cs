using System;

namespace ToyRobot
{
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
