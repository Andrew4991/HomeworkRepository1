using System;

namespace DayOfWeekApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayOfWeek day;
            string textDay;

            while (true)
            {
                Console.Clear();
                Console.Write("Enter the day of the week : ");
                textDay = Console.ReadLine().ToLower();

                try
                {
                    day = GetDay(textDay);
                    PrintDay(day);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }

                Console.WriteLine("Please click ENTER to exit ");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
        }

        static DayOfWeek GetDay(string day)//return  our day
        {
            DayOfWeek rezult;
            switch (day)
            {
                case "monday" or "mon":
                    rezult = DayOfWeek.Monday;
                    break;
                case "tuesday" or "tue":
                    rezult = DayOfWeek.Tuesday;
                    break;
                case "wednesday" or "wed":
                    rezult = DayOfWeek.Wednesday;
                    break;
                case "thursday" or "thu":
                    rezult = DayOfWeek.Thursday;
                    break;
                case "friday" or "fri":
                    rezult = DayOfWeek.Friday;
                    break;
                case "saturday" or "sat":
                    rezult = DayOfWeek.Saturday;
                    break;
                case "sanday" or "san":
                    rezult = DayOfWeek.Sunday;
                    break;
                default:
                    throw new Exception("You entered the wrong day!");
            }

            return rezult;
        }

        static void PrintDay(DayOfWeek day)// print to console 
        {
            var today = DateTime.Now.DayOfWeek;
            int numberToWeekend = GetNamberToWeekend(day);
            bool isToday = IsToday(day);
            DateTime nextDay = NextDay(day);
            SetColorConsole(day);

            if (CompareDays(day, DayOfWeek.Sunday))
            {
                Console.WriteLine($"{day} -- 7 day of week");
            }
            else
            {
                Console.WriteLine($"{day} -- {(int)day} day of week");
            }

            if (CompareDays(day, today))
            {
                Console.WriteLine($"{day} -- It's today!");
            }
            else
            {
                Console.WriteLine($"{day} -- It's not today!");
            }

            Console.WriteLine($"Until the weekend {numberToWeekend} days");
            Console.WriteLine($"Next {day} -- It's {NextDay(day).ToShortDateString()}");
        }

        static int GetNamberToWeekend(DayOfWeek day)//return number of days to weekend
        {
            if (day < DayOfWeek.Saturday && day != DayOfWeek.Sunday)
            {
                return DayOfWeek.Saturday - day;
            }
            return 0;
        }

        static bool IsToday(DayOfWeek day)
        {
            return CompareDays(day, DateTime.Now.DayOfWeek);
        }//comparison for equality with today 

        static bool CompareDays(DayOfWeek day1, DayOfWeek day2)
        {
            return day1 == day2;
        }//comparison for equality 2 days 

        static DateTime NextDay(DayOfWeek day)//return next day
        {
            var today = DateTime.Now.DayOfWeek;
            int countDays = today >= day ? day + 7 - today : day - today;
            return DateTime.Now.AddDays(countDays);
        }

        static void SetColorConsole(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case DayOfWeek.Monday:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case DayOfWeek.Tuesday:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case DayOfWeek.Wednesday:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case DayOfWeek.Thursday:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case DayOfWeek.Friday:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case DayOfWeek.Saturday:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }// sets the console color 
    }
}
