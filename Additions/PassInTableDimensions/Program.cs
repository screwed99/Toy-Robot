using ToyRobot;

namespace PassInTableDimensions
{
    public static class Program
	{
		public static void Main(string[] args)
		{
			var textInputter = new TextInputter();
            var textOutputter = new TextOutputter();
		    int xCoordinate;
		    if (!int.TryParse(args[0], out xCoordinate))
		    {
		        return;
		    }
		    int yCoordinate;
		    if (!int.TryParse(args[1], out yCoordinate))
		    {
		        return;
		    }
		    var tableDimensions = new TableDimensions(xCoordinate, yCoordinate);
		    var toyRobotAssembly = new ToyRobotAssembly(textInputter, textOutputter, tableDimensions);
			var driver = toyRobotAssembly.ToyRobotDriver;
			driver.Run();
		}
	}
}