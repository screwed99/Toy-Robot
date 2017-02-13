using System.Collections.Generic;

namespace ToyRobot.Interfaces
{
    public interface ICommandReader
    {
        void Read(IReadOnlyCollection<string> commands);
    }
}
