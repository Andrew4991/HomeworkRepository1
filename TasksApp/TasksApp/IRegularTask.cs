using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp
{
    internal interface IRegularTask : IWeeklyTask
    {
         DateTime Date { get; set; }
    }
}
