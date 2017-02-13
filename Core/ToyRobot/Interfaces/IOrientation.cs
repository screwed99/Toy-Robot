namespace ToyRobot.Interfaces
{
    public interface IOrientation
    {
        CompassDirection GetCompassDirection();

        bool IsValid();

        string GetDescription();
    }
}
