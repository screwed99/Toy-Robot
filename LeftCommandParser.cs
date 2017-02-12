namespace ToyRobot
{
    public sealed class LeftCommandParser : ICommandParser
    {
        private readonly ICommandPerformerFactory commandPerformerFactory;

        public LeftCommandParser(ICommandPerformerFactory commandPerformerFactory)
        {
            this.commandPerformerFactory = commandPerformerFactory;
        }
        
        public bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer)
        {
            if (unparsedCommand == "LEFT")
            {
                commandPerformer = this.commandPerformerFactory.CreateLeftTurnPerformer();
                return true;
            }

            commandPerformer = null;
            return false;
        }
    }
}
