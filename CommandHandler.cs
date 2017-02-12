namespace ToyRobot
{
    public sealed class CommandHandler : ICommandHandler
    {
        private readonly IToyRobot toyRobot;
        private readonly ICommandParser commandParser;

        public CommandHandler(IToyRobot toyRobot, ICommandParser commandParser)
        {
            this.toyRobot = toyRobot;
            this.commandParser = commandParser;
        }

        public void Handle(string unparsedCommand)
        {
            ICommandPerformer commandPerformer;
            if (this.commandParser.TryGetCommandPerformer(unparsedCommand, out commandPerformer))
            {
                this.toyRobot.Update(commandPerformer);
            }
        }
    }
}