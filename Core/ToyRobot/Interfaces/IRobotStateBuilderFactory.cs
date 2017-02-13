namespace ToyRobot.Interfaces
{
    public interface IRobotStateBuilderFactory
    {
        IRobotStateBuilder CreateBuilderFromPrototype(IRobotState robotState);
    }
}
