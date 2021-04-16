using System;

namespace TasksApp
{
    internal class PriorityTask : WeeklyTask
    {
        internal DateTime Date { get; set; }
        internal TasksPriority Priority { get; set; }

        internal PriorityTask(string name, DateTime data, TasksPriority priority) : base(name)
        {
            Date = data;
            Priority = priority;
        }

        internal override string GetAlarm()
        {
            if (Date.ToShortDateString().Equals(DateTime.MinValue.ToShortDateString(), StringComparison.Ordinal))
            {
                return "You have not entered a date!";
            }

            var days = Date.Date.Subtract(DateTime.Now).Days;

            return days > 0 ? $"Until the end of the task is left: {days}" : "Task time is up!";
        }

        internal override string ToSave() => $"{Name},{Date.ToShortDateString()},{Date.ToLongTimeString()},{Priority}";

        public override string ToString() => $"{Id} -- {Name} - {Date.ToShortDateString()} - {Date.ToLongTimeString()} - {Priority}";
    }
}
