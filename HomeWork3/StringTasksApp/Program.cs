using System;

namespace StringTasksApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter the namber task: ");
                    int nuberTask = Int32.Parse(Console.ReadLine());
                    MethodSelection(nuberTask);

                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {e.Message}");
                    Console.ResetColor();
                }

                Console.Write("\nPlease click ESC to exit ");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        static void MethodSelection(int numberTask)//choose a task 
        {
            Description(numberTask);

            switch (numberTask)
            {
                case 1:
                    MethodNamber_1();
                    break;
                case 2:
                    MethodNamber_2();
                    break;
                case 3:
                    MethodNamber_3();
                    break;
                case 4:
                    MethodNamber_4();
                    break;
                case 5:
                    MethodNamber_5();
                    break;
                case 6:
                    MethodNamber_6();
                    break;
                case 7:
                    MethodNamber_7();
                    break;
                case 8:
                    MethodNamber_8();
                    break;
                default:
                    throw new Exception("There is no such task !");
            }
        }

        static void Description(int numberTask)//choice of description
        {
            Console.ForegroundColor = ConsoleColor.Green;

            switch (numberTask)
            {
                case 1:
                    Console.WriteLine("Write a program in C# Sharp to input a string and print it.\n\n");
                    break;
                case 2:
                    Console.WriteLine("Write a program in C# Sharp to find the length of a string without using library function.\n\n");
                    break;
                case 3:
                    Console.WriteLine("Write a program in C# Sharp to separate the individual characters from a string.\n\n");
                    break;
                case 4:
                    Console.WriteLine("Write a program in C# Sharp to print individual characters of the string in reverse order.\n\n");
                    break;
                case 5:
                    Console.WriteLine("Write a program in C# Sharp to count the total number of words in a string.\n\n");
                    break;
                case 6:
                    Console.WriteLine("Write a program in C# Sharp to compare two string without using string library functions.\n\n");
                    break;
                case 7:
                    Console.WriteLine("Write a program in C# Sharp to count a total number of alphabets, digits and special characters in a string.\n\n");
                    break;
                case 8:
                    Console.WriteLine("Write a program in C# Sharp to copy one string to another string.\n\n");
                    break;
                default:
                    throw new Exception("There is no such task !");
            }

            Console.ResetColor();
        }

        static void MethodNamber_1()
        {
            string str = EnterString();
            Console.WriteLine($"\nThe string you entered is : {str}");
        }

        static void MethodNamber_2()
        {
            string str = EnterString();
            Console.WriteLine($"\nLength of the string is : {GetLength(str)}");
        }

        static void MethodNamber_3()
        {
            string str = EnterString();
            Console.WriteLine($"\nThe characters of the string are :");

            foreach (char item in str)
            {
                Console.Write($"{item}  ");
            }
        }

        static void MethodNamber_4()
        {
            string str = EnterString();
            Console.WriteLine($"\nThe characters of the string in reverse are :");

            for (int i = str.Length-1; i >=0; i--)
            {
                Console.Write($"{str[i]}  ");
            }
        }

        static void MethodNamber_5()
        {
            string str = EnterString();
            Console.WriteLine($"\nTotal number of words in the string is : {GetCountWords(str)}");
        }

        static void MethodNamber_6()
        {
            string firstStr = EnterString("first string");
            string secondStr = EnterString("second string");

            switch (GetCompareLength(firstStr, secondStr))
            {
                case 1:
                    Console.WriteLine("\nThe length of the first line is greater than the second.");
                    break;
                case -1:
                    Console.WriteLine("\nThe length of the second line is greater than the first.");
                    break;
                case 0:
                    if (GetCompereString(firstStr, secondStr))
                    {
                        Console.WriteLine("\nThe length of both strings are equal and also, both strings are equal.");
                    }
                    else
                    {
                        Console.WriteLine("\nThe length of both strings are equal but the lines are different.");
                    }
                    break;
                default:
                    throw new Exception("GetCompareLength return unknown value!");
            }
        }

        static void MethodNamber_7()
        {
            string str = EnterString();
            int digigits = 0;
            int alphabets = 0;
            int special = 0;

            foreach (char item in str)
            {
                if (char.IsDigit(item))
                {
                    digigits++;
                }
                else if (char.IsLetter(item))
                {
                    alphabets++;
                }
                else
                {
                    special++;
                }
            }

            Console.WriteLine($"\nNumber of Alphabets in the string is : {alphabets}");
            Console.WriteLine($"Number of Digits in the string is : {digigits}");
            Console.WriteLine($"Number of Special characters in the string is : {special}");
        }

        static void MethodNamber_8()
        {
            string str = EnterString();
            string strCopied = CopyString(str);
            Console.WriteLine($"\nThe First string is :{str}");
            Console.WriteLine($"\nThe Second string is :{strCopied}");
        }

        static string EnterString(string nameString = "string")//input string
        {
            Console.Write($"Input the {nameString} : ");
            return Console.ReadLine();
        }

        static int GetLength(string str)// return length of string
        {
            int length = 0;

            foreach (var item in str)
            {
                length++;
            }

            return length;
        }

        static int GetCountWords(string str)//return count words
        {
            str = str.Trim();
            int count = str == string.Empty ? 0 : 1;

            for (int i = 1; i < str.Length; i++)
            {
                if ((str[i] == ' ' || str[i] == '\n' || str[i] == '\t')&&(str[i-1] != ' ' && str[i-1] != '\n' && str[i-1] != '\t'))
                {
                    count++;
                }
            }

            return count;
        }

        static int GetCompareLength(string str1, string str2)//compare 2 string: -1 for str1<str2, 0 for str1=str2 and 1 for str1>str2
        {
            return str1.Length >= str2.Length ? (str1.Length > str2.Length ? (1) : (0)) : (-1);
        }

       static bool GetCompereString(string str1, string str2)//compare 2 string
        {
            bool rezult = true;
            if (str1.Length == str2.Length)
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] != str2[i])
                    {
                        rezult = false;
                    }
                }
            }
            else
            {
                throw new Exception("The lengths of the first and second lines are different!");
            }

            return rezult;
        }

       static string CopyString(string str)//copy one string to another string
        {
            string[] symbols = new string[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                symbols[i] = str[i].ToString();
            }

            return String.Join("", symbols); ;
        }
    }
}
