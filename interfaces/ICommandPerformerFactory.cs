namespace ToyRobot
{
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
}
