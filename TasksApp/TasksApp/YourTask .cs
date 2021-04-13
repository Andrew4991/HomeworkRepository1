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
        medmiddle = 1,
        high = 2
    }

    public enum ItemOfMenu
    {
        EnterTask = 1,
        OutputTask = 2,
        End = 3
    }

    public enum ParamsForYourTask
    {
        Zero = 0,
        One =1,
        Two = 2,
        Three = 3,
        Four = 4
    }




    public class YourTask
    {
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
        }




    }
}
