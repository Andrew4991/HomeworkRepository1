using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TasksApp
{
    internal class WeeklyTaskService
    {
        private readonly string _path = @"C:\Users\MONCEY\source\repos\HomeworkRepository\TasksApp\Tasks.t";
        private readonly List<WeeklyTask> _listOfTasks = new();

        internal void ExportTasks()
        {
            using var writer = new BinaryWriter(File.Open(_path, FileMode.OpenOrCreate));

            foreach (var t in _listOfTasks)
            {
                writer.Write(t.ToSave());
            }
        }

        internal void ImportTasks()
        {
            using var reader = new BinaryReader(File.Open(_path, FileMode.OpenOrCreate));
            while (reader.PeekChar() > -1)
            {
                AddTask(reader.ReadString());
            }
        }

        internal void AddTask(string task)
        {
            var tempTasks = GetTaskFromString(task);
            tempTasks.Id = _listOfTasks.Count;

            _listOfTasks.Add(tempTasks);
        }

        internal void PrintAllTasks()
        {
            PrintIsEmpty();
            PrintWithForeach(_listOfTasks);
        }

        internal void HandlerFilter(string filter)
        {
            string[] parts = filter.Split(' ');

            if (parts.Length == 3 && parts[0] == "filter")
            {
                if (parts[1] == "priority")
                {
                    PrintFilterPriority(GetTasksPriority(parts[2]));
                }
                else if (parts[1] == "date")
                {
                    PrintFilterDate(GetDateTime(parts[2]));
                }
                else
                {
                    throw new Exception("Wrong filter format !");
                }
            }
            else
            {
                throw new Exception("Wrong filter format !");
            }
        }

        internal void EditTask(int id, string newtask) => _listOfTasks[id] = GetTaskFromString(newtask);

        internal void DeleteTask(int id)
        {
            _listOfTasks.RemoveAt(id);
            ReassignmentId();
        }

        internal string AlarmTask(int id) => _listOfTasks[id].GetAlarm();

        internal bool CorrectId(int id) => id < _listOfTasks.Count;

        private void PrintIsEmpty()
        {
            Console.Clear();

            if (_listOfTasks.Count == 0)
            {
                Console.WriteLine("There are no tasks!");
            }
        }

        private void FilterIsEmpty(IEnumerable<WeeklyTask> list)
        {
            Console.Clear();

            if (!list.Any())
            {
                Console.WriteLine("There are no tasks!");
            }
        }

        private void PrintFilterDate(DateTime date)
        {
            var list = _listOfTasks.Where(d => (d as PriorityTask)?.Date > date).Concat(_listOfTasks.Where(d => (d as RegularTask)?.Date > date));

            FilterIsEmpty(list);
            PrintWithForeach(list);

            Console.ReadKey();
        }

        private void PrintWithForeach(IEnumerable<WeeklyTask> list)
        {
            foreach (var t in list)
            {
                Console.WriteLine($"{t}");
            }
        }

        private void PrintFilterPriority(TasksPriority priority)
        {
            var list = _listOfTasks.Where(p => (p as PriorityTask)?.Priority == priority);

            FilterIsEmpty(list);
            PrintWithForeach(list);

            Console.ReadKey();
        }

        private WeeklyTask GetTaskFromString(string strForTask)
        {
            string[] parts = strForTask.Split(',');

            return parts.Length == 1 && parts[0].Equals(string.Empty, StringComparison.Ordinal)
                ? new RegularTask()
                : parts.Length switch
                {
                    0 => new RegularTask(),
                    1 => new RegularTask(parts[0]),
                    2 => new RegularTask(parts[0], GetDateTime(parts[1])),
                    3 => new RegularTask(parts[0], GetDateTime(parts[1], parts[2])),
                    4 => new PriorityTask(parts[0], GetDateTime(parts[1], parts[2]), GetTasksPriority(parts[3])),
                    _ => new RegularTask(),
                };
        }

        private void ReassignmentId()
        {
            for (int i = 0; i < _listOfTasks.Count; i++)
            {
                _listOfTasks[i].Id = i;
            }
        }

        private DateTime GetDateTime(string date) => DateTime.Parse(date);

        private DateTime GetDateTime(string date, string time) => DateTime.Parse($"{date} {time}");

        private TasksPriority GetTasksPriority(string priority) => Enum.Parse<TasksPriority>(priority);
    }
}
