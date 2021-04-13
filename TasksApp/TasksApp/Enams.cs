using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp
{
    public enum TasksPriority
    {
        low = 0,
        middle = 1,
        high = 2
    }

    public enum ItemOfMenu
    {
        EnterTask = 1,
        OutputTask = 2,
        End = 3
    }

    public enum ItemOfOutMenu
    {
        Filter = 1,
        Edit = 2,
        Delete = 3,
        End = 4
    }

    public enum ParamsForYourTask
    {
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }
}
