namespace ToyRobot
{
    public interface IRobotStateBuilderFactory
    {
        IRobotStateBuilder CreateBuilderFromPrototype(IRobotState robotState);
    }
}
