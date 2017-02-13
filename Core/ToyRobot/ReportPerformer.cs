using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class ReportPerformer : ICommandPerformer
    {
        private readonly ITextOutputter textOutputter;

        public ReportPerformer(ITextOutputter textOutputter)
        {
            this.textOutputter = textOutputter;
        }

        public IRobotState Perform(IRobotState currentState)
        {
            if (currentState.IsPlaced())
            {
                var reportString = string.Format(
                    "{0},{1},{2}",
                    currentState.GetX(),
                    currentState.GetY(),
                    currentState.GetOrientation().GetDescription());
                this.textOutputter.WriteLine(reportString);
            }

            return currentState;
        }
    }
}
