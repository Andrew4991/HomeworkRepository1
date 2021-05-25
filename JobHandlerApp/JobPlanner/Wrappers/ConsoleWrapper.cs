using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlanner.Wrappers
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
