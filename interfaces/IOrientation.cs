namespace ToyRobot
{
    public interface IOrientation
    {
        CompassDirection GetCompassDirection();

        bool IsValid();

        string GetDescription();
    }
}
