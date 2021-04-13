using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksApp
{
    public class YourTask
    {
        public static int Count = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public TasksPriority Priority { get; set; }

        public YourTask():this("Unknown ")
        {

        }
        public YourTask(string name):this(name, DateTime.Now)
        {

        }
        public YourTask(string name, DateTime data):this(name, data, TasksPriority.low)
        {

        }
        public YourTask(string name, DateTime data, TasksPriority priority)
        {
            Name = name;
            Date = data;
            Priority = priority;
            Id = Count++;
        }

    }
}
