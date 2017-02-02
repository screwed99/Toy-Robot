using System;

namespace ToyRobot
{
    //extract to new file with compass direction
    public interface IOrientation
    {
        CompassDirection GetCompassDirection();

        bool IsValid();

        string GetDescription();
    }

    public sealed class Orientation : IOrientation
    {
        private CompassDirection compassDirection;

        public Orientation(CompassDirection compassDirection)
        {
            this.compassDirection = compassDirection;
        }

        public CompassDirection GetCompassDirection()
        {
            if (this.IsValid())
            {
                return this.compassDirection;
            }
            throw new Exception("getting compass direction but invalid");
        }

        public bool IsValid()
        {
            return true;
        }

        public string GetDescription()
        {
            switch (this.compassDirection)
            {
                case (CompassDirection.North):
                    return "NORTH";
                case (CompassDirection.South):
                    return "SOUTH";
                case (CompassDirection.East):
                    return "EAST";
                case (CompassDirection.West):
                    return "WEST";
                default:
                    var exceptionMessage =
                        string.Format("unexpected compass direction {0}", this.compassDirection);
                    throw new Exception(exceptionMessage);
            }
        }  
    }

    public sealed class InvalidOrientation : IOrientation
    {
        public CompassDirection GetCompassDirection()
        {
            throw new Exception("getting compass direction but invalid");
        }

        public bool IsValid()
        {
            return false;
        }

        public string GetDescription()
        {
            throw new Exception("getting description but invalid");
        }  
    }

    public enum CompassDirection
    {
        North,
        South,
        East,
        West
    }

    //extract to new file
    public interface IOrientationFactory
    {
        IOrientation Create(string unparsedDirection);

        IOrientation Create(CompassDirection compassDirection);        
    }

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
    
    public interface IRobotState
    {
        int GetX();

        int GetY();

        IOrientation GetOrientation();

        CompassDirection GetCompassDirection();

        bool IsPlaced();
    }
    
    // extract x and y into Position class
    // implementing equality (IEquatable)
    public sealed class RobotState : IRobotState
    {
        private int xCoordinate;
        private int yCoordinate;
        private IOrientation orientation;
        
        public RobotState(int xCoordinate, int yCoordinate, IOrientation orientation)
        {
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.orientation = orientation;
        }

        public int GetX()
        {
            return this.xCoordinate;
        }

        public int GetY()
        {
            return this.yCoordinate;
        }

        public IOrientation GetOrientation()
        {
            return this.orientation;
        }

        public CompassDirection GetCompassDirection()
        {
            return this.orientation.GetCompassDirection();
        }

        public bool IsPlaced()
        {
            return true;
        }
    }

    //make singleton
    public sealed class NotPlacedRobotState : IRobotState
    {
        public int GetX()
        {
            throw new Exception("invalid query for not placed robot state");
        }

        public int GetY()
        {
            throw new Exception("invalid query for not placed robot state");
        }

        public IOrientation GetOrientation()
        {
            throw new Exception("invalid query for not placed robot state");
        }

        public CompassDirection GetCompassDirection()
        {
            throw new Exception("invalid query for not placed robot state");
        }

        public bool IsPlaced()
        {
            return false;
        }
    }
}
