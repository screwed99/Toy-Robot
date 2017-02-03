namespace ToyRobot
{
     public sealed class ToyRobotDriver
    {
        private readonly ITextInputter textInputter;
        private readonly ICommandReader commandReader;

        public ToyRobotDriver(ITextInputter textInputter, ICommandReader commandReader)
        {
            this.textInputter = textInputter;
            this.commandReader = commandReader;
        }

        public void Run()
        {
            var input = this.textInputter.GetAllLines();
            this.commandReader.Read(input);
        }
    }
}
