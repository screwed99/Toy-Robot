using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class RobotStateBuilderFactory : IRobotStateBuilderFactory
    {
        public IRobotStateBuilder CreateBuilderFromPrototype(IRobotState robotState)
        {
            var xCoordinate = robotState.GetX();
            var yCoordinate = robotState.GetY();
            var orientation = new Orientation(robotState.GetCompassDirection());
            return new RobotStateBuilder(xCoordinate, yCoordinate, orientation);
        }
    }
}