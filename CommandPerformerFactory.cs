using System;

namespace ToyRobot
{
    public sealed class CommandPerformerFactory : ICommandPerformerFactory
    {
        private readonly IRobotStateFactory robotStateFactory;
        private readonly IOrientationTurner leftOrientationTurner;
        private readonly IOrientationTurner rightOrientationTurner;
        private readonly IMoveAttempter moveAttempter;
        private readonly ITableDimensions tableDimensions;
        private readonly ITextOutputter textOutputter;

        public CommandPerformerFactory(
            IRobotStateFactory robotStateFactory,
            IOrientationTurner leftOrientationTurner,
            IOrientationTurner rightOrientationTurner,
            IMoveAttempter moveAttempter,
            ITableDimensions tableDimensions,
            ITextOutputter textOutputter)
        {
            this.robotStateFactory = robotStateFactory;
            this.leftOrientationTurner = leftOrientationTurner;
            this.rightOrientationTurner = rightOrientationTurner;
            this.moveAttempter = moveAttempter;
            this.tableDimensions = tableDimensions;
            this.textOutputter = textOutputter;
        }
        
        public ICommandPerformer CreateReportPerformer()
        {
            return new ReportPerformer(this.textOutputter);
        }
        
        public ICommandPerformer CreateMovePerformer()
        {
            return new MovePerformer(this.moveAttempter);
        }
        
        public ICommandPerformer CreateLeftTurnPerformer()
        {
            return new TurnPerformer(this.leftOrientationTurner);
        }

        public ICommandPerformer CreateRightTurnPerformer()
        {
            return new TurnPerformer(this.rightOrientationTurner);
        }

        public ICommandPerformer CreatePlacePerformer(
            int xCoordinate,
            int yCoordinate,
            IOrientation orientation)
        {
            return new PlacePerformer(
                this.robotStateFactory,
                this.tableDimensions,
                xCoordinate,
                yCoordinate,
                orientation);
        }
    }
}
