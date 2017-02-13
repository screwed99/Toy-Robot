using ToyRobot;

namespace NRobotsCannotOverlap
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var textInputter = new TextInputter();
            var textOutputter = new TextOutputter();
            int numRobots;
            if (!int.TryParse(args[0], out numRobots))
            {
                return;
            }
            var toyRobotAssembly = new ToyRobotAssembly(textInputter, textOutputter, numRobots);
            var driver = toyRobotAssembly.ToyRobotDriver;
            driver.Run();
        }
    }
}