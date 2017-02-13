namespace ToyRobot.Interfaces
{
    public interface IOrientationTurner
    {
        IRobotState Turn(IRobotState currentState);
    }
}
