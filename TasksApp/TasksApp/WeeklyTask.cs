namespace TasksApp
{
    internal abstract class WeeklyTask
    {
        internal int Id { get; set; }
        internal string Name { get; set; }

        internal WeeklyTask(string name)
        {
            Name = name;
        }

        internal abstract string GetAlarm();

        internal abstract string ToSaveFormat();
    }
}
