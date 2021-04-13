using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TasksApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool endApp = false;
            var listOfTasks = ImportTasks();

            while (!endApp)
            {
                Console.Clear();
                Console.WriteLine("Console task list\r");
                Console.WriteLine("------------------------\n");
                try
                {
                    var itemOfMenu = GetNumberItemOfMenu();

                    switch (itemOfMenu)
                    {
                        case ItemOfMenu.EnterTask:
                            listOfTasks.Add(InputNewTask());
                            break;
                        case ItemOfMenu.OutputTask:
                            PrintOutputMenu(listOfTasks);
                            break;
                        case ItemOfMenu.End:
                            ExportTasks(listOfTasks);
                            endApp = true;
                            break;
                        default:
                            throw new Exception("There is no such task !");
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ReadKey();
                    Console.ResetColor();
                }

            }
        }

        public static ItemOfMenu GetNumberItemOfMenu()
        {
            Console.WriteLine("Please select an item:");
            Console.WriteLine("1. Entering a task");
            Console.WriteLine("2. Output of tasks");
            Console.WriteLine("3. End the program\n");

            ItemOfMenu itemSelect;

            while (!Enum.TryParse(Console.ReadLine(), out itemSelect))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            return itemSelect;
        }

        public static YourTask InputNewTask()
        {
            Console.Clear();
            Console.WriteLine("Please enter new task:");
            Console.WriteLine("For example: Do homework, 10.01.2021, 19:00\n");

            string textTask = Console.ReadLine().Trim();
            string[] arrayOfTextTask = textTask.Split(',');

            return GetYourTask(arrayOfTextTask);
        }

        public static YourTask GetYourTask(string[] arrayParams)
        {
            YourTask newTask;
            var countParams = (ParamsForYourTask)arrayParams.Length;

            switch (countParams)
            {
                case ParamsForYourTask.Zero:
                    newTask = new YourTask();
                    break;
                case ParamsForYourTask.One:
                    newTask = new YourTask(arrayParams[0]);
                    break;
                case ParamsForYourTask.Two:
                    newTask = new YourTask(arrayParams[0], GetDateTime(arrayParams[1]));
                    break;
                case ParamsForYourTask.Three:
                    newTask = new YourTask(arrayParams[0], GetDateTime(arrayParams[1], arrayParams[2]));
                    break;
                case ParamsForYourTask.Four:
                    newTask = new YourTask(arrayParams[0], GetDateTime(arrayParams[1], arrayParams[2]), GetTasksPriority(arrayParams[3]));
                    break;
                default:
                    newTask = new YourTask();
                    break;
            }
            return newTask;
        }

        public static DateTime GetDateTime(string date)
        {
            return DateTime.Parse(date);
        }

        public static DateTime GetDateTime(string date, string time)
        {
            return DateTime.Parse($"{date} {time}");
        }

        public static TasksPriority GetTasksPriority(string priority)
        {
            return (TasksPriority)Enum.Parse(typeof(TasksPriority), priority);
        }

        public static void PrintTasks(List<YourTask> listOfTasks)
        {
            Console.Clear();

            foreach (var task in listOfTasks)
            {
                PrintTask(task);
            }

            Console.WriteLine();
        }
        public static void PrintTasks(List<YourTask> listOfTasks, DateTime date)
        {
            Console.Clear();

            foreach (var task in listOfTasks.Where(d => d.Date>date))
            {
                PrintTask(task);
            }

            Console.ReadKey();
        }
        public static void PrintTasks(List<YourTask> listOfTasks, TasksPriority priority)
        {
            Console.Clear();

            foreach (var task in listOfTasks.Where(p => p.Priority == priority))
            {
                PrintTask(task);
            }

            Console.ReadKey();
        }

        public static void PrintTask(YourTask task)
        {
            Console.WriteLine($"{task.Id} --- {task.Name}/{task.Date.ToShortDateString()}/{task.Date.ToLongTimeString()}/{task.Priority}");
        }

        public static void PrintOutputMenu(List<YourTask> listOfTasks)
        {
            bool endApp = false;

            while (!endApp)
            {
                PrintTasks(listOfTasks);

                var itemOfOutMenu = GetNumberItemOfOutMenu();

                switch (itemOfOutMenu)
                {
                    case ItemOfOutMenu.Filter:
                        InputFilter(listOfTasks);
                        break;
                    case ItemOfOutMenu.Edit:
                        EditTasks(listOfTasks);
                        break;
                    case ItemOfOutMenu.Delete:
                        DeleteTasks(ref listOfTasks);
                        break;
                    case ItemOfOutMenu.End:
                        endApp = true;
                        break;
                    default:
                        throw new Exception("There is no such task !");
                }
            }
        }

        public static ItemOfOutMenu GetNumberItemOfOutMenu()
        {
            Console.WriteLine("\nPlease select an item:");
            Console.WriteLine("1. Apply filter");
            Console.WriteLine("2. Edit task");
            Console.WriteLine("3. Delete task");
            Console.WriteLine("4. Return to main menu\n");

            ItemOfOutMenu itemSelect;

            while (!Enum.TryParse(Console.ReadLine(), out itemSelect))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            return itemSelect;
        }

        public static void InputFilter(List<YourTask> listOfTasks)
        {
            Console.WriteLine("Please enter filter:");
            Console.WriteLine("Format: filter {priority}/{date} {value}\n");

            string textFilter = Console.ReadLine().Trim();
            string[] arrayOftextFilter = textFilter.Split(' ');

            if (arrayOftextFilter.Length == 3 && arrayOftextFilter[0] == "filter")
            {
                if (arrayOftextFilter[1] == "priority")
                {
                    PrintTasks(listOfTasks, GetTasksPriority(arrayOftextFilter[2]));
                }
                else if (arrayOftextFilter[1] == "date")
                {
                    PrintTasks(listOfTasks, GetDateTime(arrayOftextFilter[2]));
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

        private static void DeleteTasks(ref List<YourTask> listOfTasks)
        {
            Console.WriteLine("Please enter Id: ");
            int id;

            while (!int.TryParse(Console.ReadLine().Trim(), out id))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            for (int i = 0; i < listOfTasks.Count; i++)
            {
                if (listOfTasks[i].Id == id)
                {
                    listOfTasks.RemoveAt(i);
                }
            }
        }

        private static void EditTasks( List<YourTask> listOfTasks)
        {
            Console.WriteLine("Please enter Id: ");
            int id;

            while (!int.TryParse(Console.ReadLine().Trim(), out id))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            var tempTask = InputNewTask();
            YourTask.Count--;

            foreach (var item in listOfTasks)
            {
                if (item.Id == id)
                {
                    item.Date = tempTask.Date;
                    item.Name = tempTask.Name;
                    item.Priority = tempTask.Priority;
                }
            }
        }

        private static void ExportTasks(List<YourTask> listOfTasks)
        {
            string path = @"C:\Users\MONCEY\source\repos\HomeworkRepository\TasksApp\Tasks.bat";
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (var t in listOfTasks)
                {
                    writer.Write(t.Name);
                    writer.Write(t.Date.ToString());
                    writer.Write((int)t.Priority);
                }
            }
        }

        private static List<YourTask> ImportTasks()
        {
            string path = @"C:\Users\MONCEY\source\repos\HomeworkRepository\TasksApp\Tasks.bat";
            var listOfTasks= new List<YourTask>();
            YourTask.Count = 0;
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate)))
            {
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    string date = reader.ReadString();
                    int priority = reader.ReadInt32();
                    var tempTasks = new YourTask(name, GetDateTime(date), GetTasksPriority(priority.ToString()));
                    listOfTasks.Add(tempTasks);
                }
            }

            return listOfTasks;
        }
    }
}
