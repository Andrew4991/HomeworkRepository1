using System;

namespace TasksApp
{
    internal class Program
    {
        private static readonly WeeklyTaskService _service = new();

        internal static void Main(string[] args)
        {
            ImportData();

            bool endApp = false;

            while (!endApp)
            {
                try
                {
                    endApp = SwitchMenu(GetNumberItemOfMenu());
                }
                catch (Exception e)
                {
                    ShowException(e);
                }
            }
        }

        private static void ImportData()
        {
            try
            {
                _service.ImportTasks();
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        private static ItemOfMenu GetNumberItemOfMenu()
        {
            ShowItemOfMenu();

            ItemOfMenu itemSelect;

            while (!Enum.TryParse(Console.ReadLine(), out itemSelect))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            return itemSelect;
        }

        private static void ShowItemOfMenu()
        {
            Console.Clear();
            Console.WriteLine("Console task list\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Please select an item:");
            Console.WriteLine("1. Entering a task");
            Console.WriteLine("2. Output of tasks");
            Console.WriteLine("3. Apply filter");
            Console.WriteLine("4. Edit task");
            Console.WriteLine("5. Delete task");
            Console.WriteLine("6. Days until the end of the task ");
            Console.WriteLine("7. End the program\n");
        }

        private static bool SwitchMenu(ItemOfMenu itemOfMenu)
        {
            switch (itemOfMenu)
            {
                case ItemOfMenu.EnterTask:
                    InputNewTask();
                    break;
                case ItemOfMenu.OutputTaskS:
                    PrintTasks();
                    break;
                case ItemOfMenu.Filter:
                    InputFilter();
                    break;
                case ItemOfMenu.Edit:
                    EditTask();
                    break;
                case ItemOfMenu.Delete:
                    DeleteTask();
                    break;
                case ItemOfMenu.DaysToEnd:
                    CalculateToEnd();
                    break;
                case ItemOfMenu.End:
                    _service.ExportTasks();
                    return true;
                default:
                    throw new Exception("There is no such task!");
            }

            return false;
        }

        private static void ShowException(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {e.Message}");
            Console.ReadKey();
            Console.ResetColor();
        }

        private static void InputNewTask()
        {
            Console.Clear();

            _service.AddTask(GetNewStringTask());
        }

        private static void PrintTasks()
        {
            _service.PrintAllTasks();

            Console.ReadKey();
        }

        private static void InputFilter()
        {
            _service.PrintAllTasks();

            Console.WriteLine("\nPlease enter filter:");
            Console.WriteLine("Format: filter {priority}/{date} {value}\n");

            _service.HandlerFilter(Console.ReadLine().Trim());
        }

        private static void EditTask()
        {
            _service.PrintAllTasks();

            var id = GetId();
            var stringTask = GetNewStringTask();

            ExeptionId(_service.CorrectId(id));
            _service.EditTask(id, stringTask);
        }

        private static void DeleteTask()
        {
            _service.PrintAllTasks();

            var id = GetId();

            ExeptionId(_service.CorrectId(id));
            _service.DeleteTask(id);
        }

        private static void CalculateToEnd()
        {
            _service.PrintAllTasks();

            var id = GetId();

            ExeptionId(_service.CorrectId(id));

            Console.WriteLine($"{_service.AlarmTask(id)}");
            Console.ReadKey();
        }

        private static string GetNewStringTask()
        {
            Console.WriteLine("Please enter new task:");
            Console.WriteLine("For example: Do homework, 10.01.2021, 19:00\n");

            return Console.ReadLine().Trim();
        }

        private static int GetId()
        {
            Console.WriteLine("Please enter Id: ");
            int id;

            while (!int.TryParse(Console.ReadLine().Trim(), out id))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            return id;
        }

        private static void ExeptionId(bool rezultFunction)
        {
            if (!rezultFunction)
            {
                throw new Exception("There is no such task!");
            }
        }
    }
}
