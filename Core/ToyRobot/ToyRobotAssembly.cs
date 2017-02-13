using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class ToyRobotAssembly
    {
        public readonly ToyRobotDriver ToyRobotDriver;

        public ToyRobotAssembly(ITextInputter textInputter, ITextOutputter textOutputter)
        {
            var initialRobotState = new NotPlacedRobotState();
            var toyRobot = new ToyRobot(initialRobotState);
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
            var commandHander = new CommandHandler(toyRobot, masterCommandParser);
            var commandReader = new CommandReader(commandHander);
            this.ToyRobotDriver = new ToyRobotDriver(textInputter, commandReader);
        } 
    }
}
