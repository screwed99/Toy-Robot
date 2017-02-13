using ToyRobot.Interfaces;

namespace PassInTableDimensions
{
    public sealed class TableDimensions : ITableDimensions
    {
        private readonly int limitXCoordinate;
        private readonly int limitYCoordinate;

        public TableDimensions(int limitXCoordinate, int limitYCoordinate)
        {
            this.limitXCoordinate = limitXCoordinate;
            this.limitYCoordinate = limitYCoordinate;
        }

        public bool IsPositionAllowed(IRobotState robotState)
        {
            var xCoordinate = robotState.GetX();
            var yCoordinate = robotState.GetY();
            return CheckXCoordinate(xCoordinate) && CheckYCoordinate(yCoordinate);
        }

        private bool CheckXCoordinate(int xCoordinate)
        {
            return xCoordinate >= 0 && xCoordinate < this.limitXCoordinate;
        }

        private bool CheckYCoordinate(int yCoordinate)
        {
            return yCoordinate >= 0 && yCoordinate < this.limitYCoordinate;
        }
    }
}
