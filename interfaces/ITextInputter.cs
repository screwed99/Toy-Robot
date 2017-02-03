using System.Collections.Generic;

namespace ToyRobot
{
    public interface ITextInputter
	{
		IReadOnlyCollection<string> GetAllLines();
	}
}
