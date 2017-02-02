namespace ToyRobot
{
    public sealed class LeftCommandParser : ICommandParser
    {
        private ICommandPerformerFactory commandPerformerFactory;
        public LeftCommandParser(ICommandPerformerFactory commandPerformerFactory)
        {
            this.commandPerformerFactory = commandPerformerFactory;
        }
        
        public bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer)
        {
            if (unparsedCommand == "LEFT")
            {
                commandPerformer = this.commandPerformerFactory.CreateTurnPerformer
                    (TurnDirection.Left);
                return true;
            }

            commandPerformer = null;
            return false;
        }
    }
}
