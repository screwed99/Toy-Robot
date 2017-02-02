using System;

namespace ToyRobot
{
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
}
