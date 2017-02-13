using ToyRobot;
using ToyRobot.Interfaces;

namespace NRobotsCannotOverlap
{
    public sealed class ToyRobotAssembly
    {
        public readonly ToyRobotDriver ToyRobotDriver;

        public ToyRobotAssembly(ITextInputter textInputter, ITextOutputter textOutputter, int numRobots)
        {
            var robotStateFactory = new RobotStateFactory();
            var robotStateBuilderFactory = new RobotStateBuilderFactory();
            var leftOrientationTurner =
                new LeftOrientationTurner(robotStateBuilderFactory);
            var rightOrientationTurner =
                new RightOrientationTurner(robotStateBuilderFactory);
            var moveStateTransformer =
                new MoveStateTransformer(robotStateBuilderFactory);
            var tableDimensions = new TableDimensions();
            var moveAttempter = new MoveAttempter(moveStateTransformer, tableDimensions);
            var commandPerformerFactory = new CommandPerformerFactory(
                robotStateFactory,
                leftOrientationTurner,
                rightOrientationTurner,
                moveAttempter,
                tableDimensions,
                textOutputter);
            var orientationFactory = new OrientationParser();
            var reportCommandParser = new ReportCommandParser(commandPerformerFactory);
            var moveCommandParser = new MoveCommandParser(commandPerformerFactory);
            var leftCommandParser = new LeftCommandParser(commandPerformerFactory);
            var rightCommandParser = new RightCommandParser(commandPerformerFactory);
            var placeCommandParser = new PlaceCommandParser(
                commandPerformerFactory, orientationFactory);
            var commandParsers = new ICommandParser[]
                {
                    reportCommandParser,
                    moveCommandParser,
                    leftCommandParser,
                    rightCommandParser,
                    placeCommandParser
                };
            var masterCommandParser = new MasterCommandParser(commandParsers);
            var commandReader = InitializeCommandReader(numRobots, masterCommandParser);
            this.ToyRobotDriver = new ToyRobotDriver(textInputter, commandReader);
        }

        private static ICommandReader InitializeCommandReader(
            int numRobots,
            ICommandParser commandParser)
        {
            var commandHandlers = new ICommandHandler[numRobots];
            for (var i = 0; i < numRobots; i++)
            {
                var initialRobotState = new NotPlacedRobotState();
                var toyRobot = new ToyRobot.ToyRobot(initialRobotState);
                commandHandlers[i] = new CommandHandler(toyRobot, commandParser);
            }

            return new CommandReader(commandHandlers);
        }
    }
}
