using ToyRobot.Interfaces;

namespace ToyRobot
{    
    public sealed class MovePerformer : ICommandPerformer
    {
        private readonly IMoveAttempter moveAttempter;
        
        public MovePerformer(IMoveAttempter moveAttempter)
        {
            this.moveAttempter = moveAttempter;
        }

        public IRobotState Perform(IRobotState currentState)
        {
            if (currentState.IsPlaced())
            {
                return this.moveAttempter.Attempt(currentState);
            }

            return currentState;
        }
    }
}
