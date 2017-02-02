namespace ToyRobot
{
    public sealed class ToyRobot : IToyRobot
    {
        private IRobotState robotState = new NotPlacedRobotState();

        public void Update(ICommandPerformer commandPerformer)
        {
            this.robotState = commandPerformer.Perform(this.robotState);
        }
    }
}
