namespace ToyRobot
{
    public interface IRobotStateBuilder
    {
        IRobotStateBuilder WithX(int newX);
        IRobotStateBuilder WithY(int newY);
        IRobotStateBuilder WithCompassDirection(CompassDirection newCompassDirection);
        IRobotState Build();
    }
}
