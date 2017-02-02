namespace ToyRobot
{
    public sealed class RobotStateFactory : IRobotStateFactory
    {
        public IRobotState Create(int xCoordinate, int yCoordinate, IOrientation orientation)
        {
            return new RobotState(xCoordinate, yCoordinate, orientation);
        }
    }
}
