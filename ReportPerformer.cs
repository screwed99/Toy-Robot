using System;

namespace ToyRobot
{
    public sealed class ReportPerformer : ICommandPerformer
    {
        private ITextOutputter textOutputter;

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

    public interface ITextOutputter
    {
        void WriteLine(string output);
    }

    public sealed class TextOutputter : ITextOutputter
    {
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
