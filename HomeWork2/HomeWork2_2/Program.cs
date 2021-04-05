using System;

namespace HomeWork2_2
{
    public enum DayOfWeek
    {
        
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday ,
    }
    class Program
    {
        static int GetNamberDay(string day)
        {

            if (day == "1" || day == "monday" || day == "mon" || day == "mo") return 1;
            if (day == "2" || day == "tuesday" || day == "tue" || day == "tu") return 2;
            if (day == "3" || day == "wednesday" || day == "wed" || day == "we") return 3;
            if (day == "4" || day == "thursday" || day == "thu" || day == "th") return 4;
            if (day == "5" || day == "friday" || day == "fri" || day == "fr") return 5;
            if (day == "6" || day == "saturday" || day == "sat" || day == "sa") return 6;
            if (day == "7" || day == "sanday" || day == "san" || day == "su") return 7;

            return -1;
        }

        static int GetNamberToWeekend(int day, int weekend)
        {
            if (day < weekend) return weekend - day;
            return 0;
        }

        static string NextDay(DayOfWeek day)
        {
            var dayTime = DateTime.Now.AddDays(1);
            while (dayTime.DayOfWeek.ToString() != day.ToString())
            {
                dayTime=dayTime.AddDays(1);
            }
            return dayTime.ToShortDateString();
        }


        static void PrintDay(DayOfWeek day, string today, int numberToWeekend)
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
            }
            Console.WriteLine($"{day} -- {(int)day} day of week");
            Console.WriteLine($"Until the weekend {numberToWeekend} days");
            if(day.ToString()==today) Console.WriteLine($"{day} -- It's today!");
            Console.WriteLine($"Next {day} -- It's {NextDay(day)}");
            Console.ResetColor();
            Console.ReadKey();

        }

        static void Main(string[] args)
        {
            DayOfWeek day;
            string textDay;
            int numberOfDay;
            string today = DateTime.Now.DayOfWeek.ToString();
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
                PrintDay(day,today,GetNamberToWeekend(numberOfDay,(int)DayOfWeek.Saturday));
                NextDay(day);

            }
        }
    }
}
