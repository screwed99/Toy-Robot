namespace ToyRobot
{
    public sealed class MoveAttempter : IMoveAttempter
    {
        private readonly IMoveStateTransformer moveStateTransformer;
        private readonly ITableDimensions tableDimensions;

        public MoveAttempter(
            IMoveStateTransformer moveStateTransformer,
            ITableDimensions tableDimensions)
        {
            this.moveStateTransformer = moveStateTransformer;
            this.tableDimensions = tableDimensions;
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
