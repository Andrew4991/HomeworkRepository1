namespace TasksApp
{
    internal abstract class WeeklyTask : IWeeklyTask
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public WeeklyTask(string name)
        {
            Name = name;
        }

        public abstract string GetAlarm();

        public abstract string ToSaveFormat();
    }
}
