using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenges.Console;
using Challenges.Interfaces;

namespace ChallengeThree.UI
{
    class Program
    {
        static void Main(string[] args)
        {
                IConsole console = new RealConsole();
                MenuUI ui = new MenuUI(console);
                ui.Run();
        }
    }
}
