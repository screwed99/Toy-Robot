using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class RobotStateBuilder : IRobotStateBuilder
    {
        private int xCoordinate;
        private int yCoordinate;
        private IOrientation orientation;

        public RobotStateBuilder(int xCoordinate, int yCoordinate, IOrientation orientation)
        {
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
            this.orientation = orientation;
        }

        public IRobotStateBuilder WithX(int newX)
        {
            this.xCoordinate = newX;
            return this;
        }

        public IRobotStateBuilder WithY(int newY)
        {
            this.yCoordinate = newY;
            return this;
        }

        public IRobotStateBuilder WithCompassDirection(CompassDirection newCompassDirection)
        {
            this.orientation = new Orientation(newCompassDirection);
            return this;
        }

        public IRobotState Build()
        {
            return new RobotState(this.xCoordinate, this.yCoordinate, this.orientation);
        } 
    }
}
