using System;

namespace ToyRobot
{
    public sealed class LeftOrientationTurner : IOrientationTurner
    {
        IRobotStateBuilderFactory robotStateBuilderFactory;

        public LeftOrientationTurner(IRobotStateBuilderFactory robotStateBuilderFactory)
        {
            this.robotStateBuilderFactory = robotStateBuilderFactory;
        }

        public IRobotState Turn(IRobotState currentState)
        {
            var robotStateBuilder =
                this.robotStateBuilderFactory.CreateBuilderFromPrototype(currentState);
            var compassDirection = currentState.GetCompassDirection();
            switch (compassDirection)
            {
                case CompassDirection.North:
                    return robotStateBuilder.WithCompassDirection(CompassDirection.West).Build();
                case CompassDirection.East:
                    return robotStateBuilder.WithCompassDirection(CompassDirection.North).Build();
                case CompassDirection.South:
                    return robotStateBuilder.WithCompassDirection(CompassDirection.East).Build();
                case CompassDirection.West:
                    return robotStateBuilder.WithCompassDirection(CompassDirection.South).Build();
                default:
                    var exceptionMessage =
                        string.Format("unexpected compass direction {0}", compassDirection);
                    throw new Exception(exceptionMessage);
            }
        }
    }
}
