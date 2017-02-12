namespace ToyRobot
{
    public interface IOrientationParser
    {
        bool TryParse(string unparsedDirection, out IOrientation orientation);
    }
}
