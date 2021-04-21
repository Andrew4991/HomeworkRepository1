using System;

namespace TasksApp
{
    internal class RegularTask : WeeklyTask, IRegularTask
    {
        public DateTime Date { get; set; }

        internal RegularTask() : this("Unknown")
        {
        }

        internal RegularTask(string name) : this(name, new DateTime())
        {
        }

        internal RegularTask(string name, DateTime data) : base(name)
        {
            Date = data;
        }

        public override string GetAlarm()
        {
            if (Date == default)
            {
                return "You have not entered a date!";
            }

            int days = Date.Subtract(DateTime.Now).Days;

            return days > 0 ? $"Until the end of the task is left: {days}" : "Task time is up!";
        }

        public override string ToSaveFormat() => $"{Name},{Date.ToShortDateString()},{Date.ToLongTimeString()}";

        public override string ToString() => $"{Id} -- {Name}{DateTimeToString()}";

        private string DateTimeToString()
        {
            if (!Date.ToLongTimeString().Equals(DateTime.MinValue.ToLongTimeString(), StringComparison.Ordinal))
            {
                return $" - {Date.ToShortDateString()} - {Date.ToLongTimeString()}";
            }
            else if (!Date.ToShortDateString().Equals(DateTime.MinValue.ToShortDateString(), StringComparison.Ordinal))
            {
                return $" - {Date.ToShortDateString()}";
            }
            else
            {
                return "";
            }
        }
    }
}
