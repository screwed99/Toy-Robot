using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class RobotState : IRobotState
    {
        private readonly int xCoordinate;
        private readonly int yCoordinate;
        private readonly IOrientation orientation;
        
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
}
