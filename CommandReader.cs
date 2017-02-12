using System.Collections.Generic;

namespace ToyRobot
{
    public sealed class CommandReader : ICommandReader
    {
        private readonly ICommandHandler commandHandler;
        
        public CommandReader(ICommandHandler commandHandler)
        {
            this.commandHandler = commandHandler;
        }

        public void Read(IReadOnlyCollection<string> unparsedCommands)
        {
            foreach (var unparsedCommand in unparsedCommands)
            {
                this.commandHandler.Handle(unparsedCommand);
            }
        }
    }
}
