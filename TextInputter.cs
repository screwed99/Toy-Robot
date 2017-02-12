using System.Collections.Generic;
using System;

namespace ToyRobot
{
    public sealed class TextInputter : ITextInputter
	{
		public IReadOnlyCollection<string> GetAllLines()
		{
			var input = new List<string>();
			string line;
			while ((line = Console.ReadLine()) != null)
			{
				input.Add(line);
			}

			return input;
		}
	}
}
