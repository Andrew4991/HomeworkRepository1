using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp
{
    internal interface IPriorityTask : IRegularTask
    {
        TasksPriority Priority { get; set; }
    }
}
