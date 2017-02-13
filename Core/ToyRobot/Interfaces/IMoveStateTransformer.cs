namespace ToyRobot.Interfaces
{
    public interface IMoveStateTransformer
    {
        IRobotState Transform(IRobotState currentState);
    }
}
