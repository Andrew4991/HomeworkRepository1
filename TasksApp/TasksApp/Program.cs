using System;
using System.Collections.Generic;

namespace TasksApp
{
   public class Program
    {
       public static void Main(string[] args)
        {
            bool endApp = false;
            var listOfTasks = new List<YourTask>();

            while (!endApp)
            {
                Console.Clear();
                Console.WriteLine("Console task list\r");
                Console.WriteLine("------------------------\n");

                var itemOfMenu = GetNumberItemOfMenu();

                switch (itemOfMenu)
                {
                    case ItemOfMenu.EnterTask:
                        listOfTasks.Add(InputNewTask());
                        break;
                    case ItemOfMenu.OutputTask:
                        PintTasks(listOfTasks);
                        break;
                    case ItemOfMenu.End:
                        endApp = true;
                        break;
                    default:
                        Console.WriteLine("There is no such task\n");
                        break;
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
            YourTask newTask = GetYourTask(arrayOfTextTask);

            return newTask;
        }


        public static YourTask GetYourTask(string[] arrayParams)
        {
            YourTask newTask;
            ParamsForYourTask countParams = (ParamsForYourTask)arrayParams.Length;

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


        public static void PintTasks(List<YourTask> listOfTasks)
        {
            foreach (var task in listOfTasks)
            {
                Console.WriteLine($"{task.Name}/{task.Date}/{task.Priority}");
            }

            Console.ReadKey();
        }
    }
}
