namespace ToyRobot
{
    public interface IRobotStateFactory
    {
        IRobotState Create(int xCoordinate, int yCoordinate, IOrientation orientation);
    }
}
