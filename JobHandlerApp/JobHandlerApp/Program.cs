﻿using System;
using System.Threading.Tasks;
using JobPlanner;
using JobPlanner.Jobs.SimpleJobs;
using JobPlanner.Wrappers;
using ShopApp;

namespace JobHandlerApp
{
    public class Program
    {
        private static JobScheduler _scheduler;
        private static readonly IConsoleWrapper _console = new ConsoleWrapper();

        public static void Main(string[] args)
        {
           _scheduler = new(_console, ReadInterval());

            bool alive = true;

            while (alive)
            {
                try
                {
                    switch (GetNumberItemOfMenu())
                    {
                        case 1:
                            AddLogToConsole();
                            break;
                        case 2:
                            AddLogToFile();
                            break;
                        case 3:
                            AddDownloadWebsite();
                            break;
                        case 4:
                            AddPrintOrders();
                            break;
                        case 5:
                            AddPrintGitHub();
                            break;
                        case 6:
                            AddLogToConsoleOnce();
                            break;
                        case 7:
                            AddLogToFileOnce();
                            break;
                        case 8:
                            AddDownloadWebsiteOnce();
                            break;
                        case 9:
                            AddPrintOrdersOnce();
                            break;
                        case 10:
                            Start();
                            break;
                        case 11:
                            alive = false;
                            continue;
                        default:
                            throw new ArgumentException("There is no such menu item!");
                    }
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        private static int GetNumberItemOfMenu()
        {
            PrintMenu();

            int itemSelect;

            while (!int.TryParse(Console.ReadLine(), out itemSelect))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            return itemSelect;
        }

        private static void ShowException(Exception ex)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = color;
            Console.ReadKey();
        }

        private static void PrintMenu()
        {
            Console.Clear();
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Add logging to the console");
            Console.WriteLine("2. Add logging to the file");
            Console.WriteLine("3. Add download website to file");
            Console.WriteLine("4. Add print orders to the console");
            Console.WriteLine("5. Add print github to the console");
            Console.WriteLine("6. Add logging to console by date");
            Console.WriteLine("7. Add logging to file by date");
            Console.WriteLine("8. Add download website to file by date");
            Console.WriteLine("9. Add print orders to console by date");
            Console.WriteLine("10. Start scheduler");
            Console.WriteLine("11. Exit program");

            Console.WriteLine("Enter the item number:");
            Console.ForegroundColor = color;
        }

        private static void AddLogToConsole()
        {
            _scheduler.RegisterJob(new JobExecutionTimeInConsole(_console));
        }

        private static void AddLogToFile()
        {
            _scheduler.RegisterJob(new JobExecutionTimeInFile(_console));
        }

        private static void AddDownloadWebsite()
        {
            Console.WriteLine("Input website address: ");

            var path = Console.ReadLine();

            _scheduler.RegisterJob(new JobDownloadWebsite(_console, path));
        }

        private static void AddPrintOrders()
        {
            _scheduler.RegisterJob(new JobExecutionOrdersInConsole(_console, new Repository()));
        }

        private static void AddPrintGitHub()
        {
            _scheduler.RegisterJob(new JobGithubRepositoryParser(_console));
        }

        private static void AddLogToConsoleOnce()
        {
            _scheduler.RegisterJob(new DelayedJobExecutionTimeInConsole(_console, ReadStartDate()));
        }

        private static void AddLogToFileOnce()
        {
            _scheduler.RegisterJob(new DelayedJobExecutionTimeInFile(_console, ReadStartDate()));
        }

        private static void AddDownloadWebsiteOnce()
        {
            Console.WriteLine("Input website address: ");

            var path = Console.ReadLine();

            _scheduler.RegisterJob(new DelayedJobDownloadWebsite(_console, path, ReadStartDate()));
        }

        private static void AddPrintOrdersOnce()
        {
            _scheduler.RegisterJob(new DelayedJobExecutionOrdersInConsole(_console, ReadStartDate(), new Repository()));
        }

        private static void Start()
        {
            Console.Clear();
            Console.WriteLine("Please press Enter for start program");
            Console.WriteLine("Please press ESC for stop program");
            Console.WriteLine("Please press Q for stop running jobs");

            AwaitPress(ConsoleKey.Enter);

            Console.Clear();

            _scheduler.Start();

            AwaitStop();

            Console.ReadKey();
        }

        private static void AwaitStop()
        {
            while (true)
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.Escape)
                {
                    _scheduler.Stop();
                    break;
                }
                else if(key == ConsoleKey.Q)
                {
                    _scheduler.CancelJobs();
                }
            }
        }

        private static void AwaitPress(ConsoleKey key)
        {
            while (Console.ReadKey().Key != key)
            {
            }
        }

        private static int ReadInterval()
        {
            Console.WriteLine("Please enter the program run interval in milliseconds:");

            int interval;

            while (!int.TryParse(Console.ReadLine(), out interval))
            {
                Console.Write("You entered the wrong interval. Please try again: ");
            }

            return interval;
        }

        private static DateTime ReadStartDate()
        {
            Console.WriteLine("Please enter start date:");

            DateTime startDate;

            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.Write("You entered the wrong start date. Please try again: ");
            }

            return startDate;
        }
    }
}

