namespace ToyRobot
{
    public static class Program 
	{
		public static void Main(string[] args) 
		{
			var textInputter = new TextInputter();
            var textOutputter = new TextOutputter();
			var toyRobotAssembly = new ToyRobotAssembly(textInputter, textOutputter);
			var driver = toyRobotAssembly.ToyRobotDriver;
			driver.Run();
		}
	}
}