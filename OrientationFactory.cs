using System;

namespace ToyRobot
{
    public sealed class OrientationFactory : IOrientationFactory
    {
        public IOrientation Create(string unparsedDirection)
        {
            switch (unparsedDirection)
            {
                case ("NORTH"):
                    return new Orientation(CompassDirection.North);
                case ("SOUTH"):
                    return new Orientation(CompassDirection.South);
                case ("EAST"):
                    return new Orientation(CompassDirection.East);
                case ("WEST"):
                    return new Orientation(CompassDirection.West);
                default:
                    return new InvalidOrientation();
            }
        }

        public IOrientation Create(CompassDirection compassDirection)
        {
            switch (compassDirection)
            {
                case (CompassDirection.North):
                    return new Orientation(CompassDirection.North);
                case (CompassDirection.South):
                    return new Orientation(CompassDirection.South);
                case (CompassDirection.East):
                    return new Orientation(CompassDirection.East);
                case (CompassDirection.West):
                    return new Orientation(CompassDirection.West);
                default:
                    var exceptionMessage =
                        string.Format("unexpected compass direction {0}", compassDirection);
                    throw new Exception(exceptionMessage);
            }
        }
    }
}
