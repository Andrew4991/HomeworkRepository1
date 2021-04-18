using System;

namespace TasksApp
{
    internal class PriorityTask : RegularTask
    {
        internal TasksPriority Priority { get; set; }

        internal PriorityTask(string name, DateTime data, TasksPriority priority) : base(name)
        {
            Date = data;
            Priority = priority;
        }

        internal override string ToSaveFormat() => $"{Name},{Date.ToShortDateString()},{Date.ToLongTimeString()},{Priority}";

        public override string ToString() => $"{Id} -- {Name} - {Date.ToShortDateString()} - {Date.ToLongTimeString()} - {Priority}";
    }
}
