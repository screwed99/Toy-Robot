using System;

namespace ToyRobot
{
    public enum TurnDirection
    {
        Left,
        Right
    }

    public interface ITableDimensions
    {
        bool IsPositionAllowed(IRobotState robotState);
    }

    public sealed class TableDimensions : ITableDimensions
    {
        public bool IsPositionAllowed(IRobotState robotState)
        {
            var xCoordinate = robotState.GetX();
            var yCoordinate = robotState.GetY();
            return CheckXCoordinate(xCoordinate) && CheckYCoordinate(yCoordinate);
        }

        private bool CheckXCoordinate(int xCoordinate)
        {
            return xCoordinate >= 0 && xCoordinate < 5;
        }

        private bool CheckYCoordinate(int yCoordinate)
        {
            return yCoordinate >= 0 && yCoordinate < 5;
        }
    }

    public interface ICommandPerformerFactory
    {
        ICommandPerformer CreateReportPerformer();
        
        ICommandPerformer CreateMovePerformer();

        ICommandPerformer CreateTurnPerformer(TurnDirection turnDirection);

        ICommandPerformer CreatePlacePerformer(
            int xCoordinate,
            int yCoordinate,
            IOrientation orientation);
    }
    
    public sealed class CommandPerformerFactory : ICommandPerformerFactory
    {
        private IRobotStateFactory robotStateFactory;
        private IOrientationTurner leftOrientationTurner;
        private IOrientationTurner rightOrientationTurner;
        private IMoveAttempter moveAttempter;
        private ITableDimensions tableDimensions;
        private ITextOutputter textOutputter;

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
        
        public ICommandPerformer CreateTurnPerformer(TurnDirection turnDirection)
        {
            switch (turnDirection)
            {
                case TurnDirection.Left:
                    return new TurnPerformer(this.leftOrientationTurner);
                case TurnDirection.Right:
                    return new TurnPerformer(this.rightOrientationTurner);
                default:
                    var exceptionMessage =
                        string.Format("unexpected turn direction {0}", turnDirection);
                    throw new Exception(exceptionMessage);
            }
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
