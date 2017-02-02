using System.Collections.Generic;

namespace ToyRobot
{
    public interface ICommandReader
    {
        void Read(IReadOnlyCollection<string> commands);
    }
}
