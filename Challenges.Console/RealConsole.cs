using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenges.Interfaces;

namespace Challenges.Console
{
    public class RealConsole : IConsole
    {
        public void Clear()
        {
            System.Console.Clear();
        }
        public ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }
        public void WriteLine(string s)
        {
            System.Console.WriteLine(s);
        }
        public void WriteLine(object o)
        {
            System.Console.WriteLine(o);
        }
    }
}
