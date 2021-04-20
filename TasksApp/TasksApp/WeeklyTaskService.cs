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

        internal delegate void WriteOutput(string message);
        internal WriteOutput _del;

        internal void SetDelegete(WriteOutput del) => _del = del;

        internal void ExportTasks()
        {
            using var writer = new BinaryWriter(File.Open(_path, FileMode.OpenOrCreate));

            foreach (var t in _listOfTasks)
            {
                writer.Write(t.ToSaveFormat());
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
                    throw new ArgumentException("Wrong filter format !");
                }
            }
            else
            {
                throw new ArgumentException("Wrong filter format !");
            }
        }

        internal void EditTask(int id, string newtask)
        {
            _listOfTasks[id] = GetTaskFromString(newtask);
            _listOfTasks[id].Id = id;
            _del?.Invoke($"Task {id} has been changed!");
        }

        internal void DeleteTask(int id)
        {
            _listOfTasks.RemoveAt(id);
            ReassignIds();

            _del?.Invoke($"Task {id} has been deleted!");
        }

        internal string AlarmTask(int id) => _listOfTasks[id].GetAlarm();

        internal bool IsCorrectId(int id) => _listOfTasks.Any(x => x.Id == id);

        private void FilterIsEmpty(IEnumerable<WeeklyTask> list)
        {
            if (!list.Any() && _del != null)
            {
                _del("There are no tasks!");
            }
        }

        private void PrintFilterDate(DateTime date)
        {
            var list = _listOfTasks.Where(d => (d as IRegularTask)?.Date > date);

            PrintWithForeach(list);
        }

        private void PrintFilterPriority(TasksPriority priority)
        {
            var list = _listOfTasks.Where(p => (p as IPriorityTask)?.Priority == priority);

            PrintWithForeach(list);
        }

        private void PrintWithForeach(IEnumerable<WeeklyTask> list)
        {
            FilterIsEmpty(list);

            foreach (var t in list)
            {
                _del?.Invoke($"{t}");
            }
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

        private void ReassignIds()
        {
            for (int i = 0; i < _listOfTasks.Count; i++)
            {
                _listOfTasks[i].Id = i;
            }
        }

        private DateTime GetDateTime(string date) => DateTime.Parse(date);

        private DateTime GetDateTime(string date, string time) => DateTime.Parse($"{date} {time}");

        private TasksPriority GetTasksPriority(string priority)
        {
            var rezult = Enum.Parse<TasksPriority>(priority);

            if (!Enum.IsDefined(typeof(TasksPriority), rezult))
            {
                throw new ArgumentException("Invalid format for priority!");
            }

            return rezult;
        }
    }
}
