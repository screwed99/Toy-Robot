namespace ToyRobot.Interfaces
{
    public interface ICommandParser
    {
        bool TryGetCommandPerformer(
            string unparsedCommand, out ICommandPerformer commandPerformer);
    }
}
