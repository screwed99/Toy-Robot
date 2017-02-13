using System;
using ToyRobot.Interfaces;

namespace ToyRobot
{
    public sealed class TextOutputter : ITextOutputter
    {
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
