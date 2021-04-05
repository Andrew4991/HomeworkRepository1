using System;

namespace HomeWork2_2
{
    class Program
    {
        static int GetNamberDay(string day)
        {

            if (day == "0" || day == "sanday" || day == "san" || day == "su") return 0;
            if (day == "1" || day == "monday" || day == "mon" || day == "mo") return 1;
            if (day == "2" || day == "tuesday" || day == "tue" || day == "tu") return 2;
            if (day == "3" || day == "wednesday" || day == "wed" || day == "we") return 3;
            if (day == "4" || day == "thursday" || day == "thu" || day == "th") return 4;
            if (day == "5" || day == "friday" || day == "fri" || day == "fr") return 5;
            if (day == "6" || day == "saturday" || day == "sat" || day == "sa") return 6;

            return -1;
        }

        static void PrintDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sunday");
                    break;
                case DayOfWeek.Monday:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Monday");
                    break;
                case DayOfWeek.Tuesday:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Tuesday");
                    break;
                case DayOfWeek.Wednesday:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Wednesday");
                    break;
                case DayOfWeek.Thursday:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Thursday");
                    break;
                case DayOfWeek.Friday:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Friday");
                    break;
                case DayOfWeek.Saturday:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Saturday");
                    break;
            }
            Console.ResetColor();
            Console.ReadKey();

        }

        static void Main(string[] args)
        {
            DayOfWeek day;
            string textDay;
            int numberOfDay;
            while (true)
            {
                Console.Clear();
                Console.Write("Enter the day of the week : ");
                textDay = Console.ReadLine().ToLower();
                numberOfDay = GetNamberDay(textDay);
                if (numberOfDay == -1)
                {
                    Console.WriteLine("You entered the wrong day!");
                    Console.ReadKey();
                    continue;
                }
                day = (DayOfWeek)numberOfDay;
                PrintDay(day);

            }
        }
    }
}
