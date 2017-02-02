namespace ToyRobot
{
    public interface IOrientationFactory
    {
        IOrientation Create(string unparsedDirection);

        IOrientation Create(CompassDirection compassDirection);        
    }
}
