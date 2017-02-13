using System.Collections.Generic;
using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class MasterCommandParser : ICommandParser
    {
        private readonly IReadOnlyCollection<ICommandParser> commandParsers;

        public MasterCommandParser(IReadOnlyCollection<ICommandParser> commandParsers)
        {
            this.commandParsers = commandParsers;
        }

        public bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer)
        {
            foreach (var commandParser in this.commandParsers)
            {
                if (commandParser.TryGetCommandPerformer(unparsedCommand, out commandPerformer))
                {
                    return true;
                }
            }

            commandPerformer = null;
            return false;
        }
    }
}
