using System.Collections.Generic;

namespace ToyRobot.Interfaces
{
    public interface ITextInputter
	{
		IReadOnlyCollection<string> GetAllLines();
	}
}
