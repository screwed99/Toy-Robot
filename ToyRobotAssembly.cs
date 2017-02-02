namespace ToyRobot
{
    public sealed class ToyRobotAssembly
    {
        public readonly ICommandReader CommandReader;

        public ToyRobotAssembly(ITextOutputter textOutputter)
        {
            var toyRobot = new ToyRobot();
            var robotStateFactory = new RobotStateFactory();
            var robotStateBuilderFactory = new RobotStateBuilderFactory();
            var leftOrientationTurner = new LeftOrientationTurner(robotStateBuilderFactory);
            var rightOrientationTurner = new RightOrientationTurner(robotStateBuilderFactory);
            var moveStateTransformer = new MoveStateTransformer(robotStateBuilderFactory);
            var tableDimensions = new TableDimensions();
            var moveAttempter = new MoveAttempter(
                moveStateTransformer, tableDimensions, robotStateFactory);
            var commandPerformerFactory = new CommandPerformerFactory(
                robotStateFactory,
                leftOrientationTurner,
                rightOrientationTurner,
                moveAttempter,
                tableDimensions,
                textOutputter);
            var orientationFactory = new OrientationFactory();
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
            this.CommandReader = new CommandReader(toyRobot, masterCommandParser);
        } 
    }
}
