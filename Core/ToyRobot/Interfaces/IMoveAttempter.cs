namespace ToyRobot.Interfaces
{
    public interface IMoveAttempter
    {
        IRobotState Attempt(IRobotState currentState);
    }
}
