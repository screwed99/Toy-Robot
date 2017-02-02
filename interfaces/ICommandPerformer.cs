using System;

namespace ToyRobot
{
    public interface ICommandPerformer
    {
        IRobotState Perform(IRobotState currentState);
    }
}
