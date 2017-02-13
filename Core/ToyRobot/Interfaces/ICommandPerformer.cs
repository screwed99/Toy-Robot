namespace ToyRobot.Interfaces
{
    public interface ICommandPerformer
    {
        IRobotState Perform(IRobotState currentState);
    }
}
