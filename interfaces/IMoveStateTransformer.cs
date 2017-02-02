namespace ToyRobot
{
    public interface IMoveStateTransformer
    {
        IRobotState Transform(IRobotState currentState);
    }
}
