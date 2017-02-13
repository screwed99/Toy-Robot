namespace ToyRobot.Interfaces
{
    public interface ICommandPerformerFactory
    {
        ICommandPerformer CreateReportPerformer();
        
        ICommandPerformer CreateMovePerformer();

        ICommandPerformer CreateLeftTurnPerformer();

        ICommandPerformer CreateRightTurnPerformer();

        ICommandPerformer CreatePlacePerformer(
            int xCoordinate,
            int yCoordinate,
            IOrientation orientation);
    }
}
