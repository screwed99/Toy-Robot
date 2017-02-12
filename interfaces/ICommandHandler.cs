namespace ToyRobot
{
    public interface ICommandHandler
    {
        void Handle(string unparsedCommand);
    }
}