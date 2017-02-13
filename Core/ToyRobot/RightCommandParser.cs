using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class RightCommandParser : ICommandParser
    {
        private readonly ICommandPerformerFactory commandPerformerFactory;

        public RightCommandParser(ICommandPerformerFactory commandPerformerFactory)
        {
            this.commandPerformerFactory = commandPerformerFactory;
        }
        
        public bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer)
        {
            if (unparsedCommand == "RIGHT")
            {
                commandPerformer = this.commandPerformerFactory.CreateRightTurnPerformer();
                return true;
            }

            commandPerformer = null;
            return false;
        }
    }
}
