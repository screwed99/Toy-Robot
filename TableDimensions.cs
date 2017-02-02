namespace ToyRobot
{
    public sealed class TableDimensions : ITableDimensions
    {
        public bool IsPositionAllowed(IRobotState robotState)
        {
            var xCoordinate = robotState.GetX();
            var yCoordinate = robotState.GetY();
            return CheckXCoordinate(xCoordinate) && CheckYCoordinate(yCoordinate);
        }

        private bool CheckXCoordinate(int xCoordinate)
        {
            return xCoordinate >= 0 && xCoordinate < 5;
        }

        private bool CheckYCoordinate(int yCoordinate)
        {
            return yCoordinate >= 0 && yCoordinate < 5;
        }
    }
}
