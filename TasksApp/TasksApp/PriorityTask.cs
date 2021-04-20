using System;

namespace TasksApp
{
    internal class PriorityTask : RegularTask, IPriorityTask
    {
        public TasksPriority Priority { get; set; }

        internal PriorityTask(string name, DateTime date, TasksPriority priority) : base(name, date)
        {
            Priority = priority;
        }

        public override string ToSaveFormat() => $"{Name},{Date.ToShortDateString()},{Date.ToLongTimeString()},{Priority}";

        public override string ToString() => $"{Id} -- {Name} - {Date.ToShortDateString()} - {Date.ToLongTimeString()} - {Priority}";
    }
}
