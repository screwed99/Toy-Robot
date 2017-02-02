namespace ToyRobot
{
    public interface IOrientationTurner
    {
        IRobotState Turn(IRobotState currentState);
    }
}
