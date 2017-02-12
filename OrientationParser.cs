namespace ToyRobot
{
    public sealed class OrientationParser : IOrientationParser
    {
        public bool TryParse(string unparsedDirection, out IOrientation orientation)
        {
            switch (unparsedDirection)
            {
                case ("NORTH"):
                    orientation = new Orientation(CompassDirection.North);
                    return true;
                case ("SOUTH"):
                    orientation = new Orientation(CompassDirection.South);
                    return true;
                case ("EAST"):
                    orientation = new Orientation(CompassDirection.East);
                    return true;
                case ("WEST"):
                    orientation = new Orientation(CompassDirection.West);
                    return true;
                default:
                    orientation = null;
                    return false;
            }
        }
    }
}
