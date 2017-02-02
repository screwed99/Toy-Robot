namespace ToyRobot
{    
    public sealed class PlacePerformer : ICommandPerformer
    {
        private IRobotStateFactory robotStateFactory;
        private ITableDimensions tableDimensions;
        private int xCoordinate;
        private int yCoordinate;
        private IOrientation orientation;
        
        public PlacePerformer(
            IRobotStateFactory robotStateFactory,
            ITableDimensions tableDimensions,
            int xCoordinate,
            int yCoordinate,
            IOrientation orientation)
        {
            this.robotStateFactory = robotStateFactory;
            this.tableDimensions = tableDimensions;
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.orientation = orientation;
        }

        public IRobotState Perform(IRobotState currentState)
        {
            var newState = this.robotStateFactory.Create(
                this.xCoordinate,
                this.yCoordinate,
                this.orientation);
            if (this.tableDimensions.IsPositionAllowed(newState))
            {
                return newState;
            }

            return currentState;
        }
    }
}
