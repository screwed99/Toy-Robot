using System;
using ToyRobot.Interfaces;

namespace ToyRobot
{
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
