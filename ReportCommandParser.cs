namespace ToyRobot
{
    public sealed class ReportCommandParser : ICommandParser
    {
        private ICommandPerformerFactory commandPerformerFactory;
        public ReportCommandParser(ICommandPerformerFactory commandPerformerFactory)
        {
            this.commandPerformerFactory = commandPerformerFactory;
        }

        public bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer)
        {
            if (unparsedCommand == "REPORT")
            {
                commandPerformer = this.commandPerformerFactory.CreateReportPerformer();
                return true;
            }

            commandPerformer = null;
            return false;
        }
    }
}
