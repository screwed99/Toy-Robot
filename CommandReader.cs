using System.Collections.Generic;

namespace ToyRobot
{
    public sealed class CommandReader : ICommandReader
    {
        private IToyRobot toyRobot;
        private ICommandParser commandParser;
        
        public CommandReader(IToyRobot toyRobot, ICommandParser commandParser)
        {
            this.toyRobot = toyRobot;
            this.commandParser = commandParser;
        }

        public void Read(IReadOnlyCollection<string> unparsedCommands)
        {
            foreach (var unparsedCommand in unparsedCommands)
            {
                ICommandPerformer commandPerformer;
                if (this.commandParser.TryGetCommandPerformer(unparsedCommand, out commandPerformer))
                {
                    this.toyRobot.Update(commandPerformer); 
                }
            }
        }
    }
}
