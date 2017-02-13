using System.Collections.Generic;
using System.Linq;
using ToyRobot.Interfaces;

namespace NRobotsCannotOverlap
{
    public sealed class CommandReader : ICommandReader
    {
        private readonly IReadOnlyCollection<ICommandHandler> commandHandlers;
        
        public CommandReader(IReadOnlyCollection<ICommandHandler> commandHandlers)
        {
            this.commandHandlers = commandHandlers;
        }

        public void Read(IReadOnlyCollection<string> unparsedCommands)
        {
            var i = 0;
            foreach (var unparsedCommand in unparsedCommands)
            {
                var handlerIndex = i % commandHandlers.Count;
                this.commandHandlers.ElementAt(handlerIndex).Handle(unparsedCommand);
                i++;
            }
        }
    }
}
