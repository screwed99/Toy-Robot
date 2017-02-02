namespace ToyRobot
{
    public sealed class TurnPerformer : ICommandPerformer
    {
        private IOrientationTurner orientationTurner;
        
        public TurnPerformer(IOrientationTurner orientationTurner)
        {
            this.orientationTurner = orientationTurner;
        }

        public IRobotState Perform(IRobotState currentState)
        {
            if (!currentState.IsPlaced())
            {
                return currentState;
            }
            return this.orientationTurner.Turn(currentState);
        }
    }
}
