using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp
{
    internal interface IWeeklyTask
    {
        int Id { get; set; }

        string Name { get; set; }

        string GetAlarm();

        string ToSaveFormat();
    }
}
