namespace ToyRobot.Interfaces
{
    public interface IRobotState
    {
        int GetX();

        int GetY();

        IOrientation GetOrientation();

        CompassDirection GetCompassDirection();

        bool IsPlaced();
    }
}
