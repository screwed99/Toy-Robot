namespace ToyRobot.Interfaces
{
    public interface ICommandHandler
    {
        void Handle(string unparsedCommand);
    }
}