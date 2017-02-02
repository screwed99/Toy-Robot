using System.Collections.Generic;
using System;

namespace ToyRobot
{
    public static class Program 
	{
		public static void Main(string[] args) 
		{
            var textOutputter = new TextOutputter();
			var toyRobotAssembly = new ToyRobotAssembly(textOutputter);
			var commandReader = toyRobotAssembly.CommandReader;
			commandReader.Read(GetAllLines());			
		}

		private static IReadOnlyCollection<string> GetAllLines()
		{
			List<string> input = new List<string>();
			string line;
			while ((line = Console.ReadLine()) != null)
			{
				input.Add(line);
			}

			return input;
		}
	}
}