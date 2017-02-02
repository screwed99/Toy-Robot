namespace ToyRobot
{
    public interface ICommandParser
    {
        bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer);
    }
}
