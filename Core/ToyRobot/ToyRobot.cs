using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class ToyRobot : IToyRobot
    {
        private IRobotState robotState;

        public ToyRobot(IRobotState robotState)
        {
            this.robotState = robotState;
        }

        public void Update(ICommandPerformer commandPerformer)
        {
            this.robotState = commandPerformer.Perform(this.robotState);
        }
    }
}
