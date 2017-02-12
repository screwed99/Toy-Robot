namespace ToyRobot
{
    public sealed class MoveCommandParser : ICommandParser
    {
        private readonly ICommandPerformerFactory commandPerformerFactory;

        public MoveCommandParser(ICommandPerformerFactory commandPerformerFactory)
        {
            this.commandPerformerFactory = commandPerformerFactory;
        }
        
        public bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer)
        {
            if (unparsedCommand == "MOVE")
            {
                commandPerformer = this.commandPerformerFactory.CreateMovePerformer();
                return true;
            }

            commandPerformer = null;
            return false;
        }
    }
}
