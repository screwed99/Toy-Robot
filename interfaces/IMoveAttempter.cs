namespace ToyRobot
{
    public interface IMoveAttempter
    {
        IRobotState Attempt(IRobotState currentState);
    }
}
