using System;
using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class Orientation : IOrientation
    {
        private readonly CompassDirection compassDirection;

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
}
