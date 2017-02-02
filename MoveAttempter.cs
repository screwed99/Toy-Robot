namespace ToyRobot
{
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
}
