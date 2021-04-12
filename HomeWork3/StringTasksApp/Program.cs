using System;

namespace StringTasksApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.Write("Enter the namber task: ");
                    var nuberTask = int.Parse(Console.ReadLine());
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

        /// <summary>
        /// choose a task 
        /// </summary>
        public static void MethodSelection(int numberTask)
        {
            Description(numberTask);

            switch (numberTask)
            {
                case 1:
                    MethodNumber1();
                    break;
                case 2:
                    MethodNumber2();
                    break;
                case 3:
                    MethodNumber3();
                    break;
                case 4:
                    MethodNumber4();
                    break;
                case 5:
                    MethodNumber5();
                    break;
                case 6:
                    MethodNumber6();
                    break;
                case 7:
                    MethodNumber7();
                    break;
                case 8:
                    MethodNumber8();
                    break;
                case 9:
                    MethodNumber9();
                    break;
                case 10:
                    MethodNumber10();
                    break;
                default:
                    throw new Exception("There is no such task !");
            }
        }

        /// <summary>
        /// choice of description
        /// </summary>
        public static void Description(int numberTask)
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
                case 9:
                    Console.WriteLine("Write a program in C# Sharp to count a total number of vowel or consonant in a string.\n\n");
                    break;
                case 10:
                    Console.WriteLine("Write a program in C# Sharp to find maximum occurring character in a string.\n\n");
                    break;
                default:
                    throw new Exception("There is no such task !");
            }

            Console.ResetColor();
        }

        public static void MethodNumber1()
        {
            var str = EnterString();
            Console.WriteLine($"\nThe string you entered is : {str}");
        }

        public static void MethodNumber2()
        {
            var str = EnterString();
            Console.WriteLine($"\nLength of the string is : {GetLength(str)}");
        }

        public static void MethodNumber3()
        {
            var str = EnterString();
            Console.WriteLine($"\nThe characters of the string are :");

            foreach (var item in str)
            {
                Console.Write($"{item}  ");
            }
        }

        public static void MethodNumber4()
        {
            var str = EnterString();
            Console.WriteLine($"\nThe characters of the string in reverse are :");

            for (var i = str.Length-1; i >=0; i--)
            {
                Console.Write($"{str[i]}  ");
            }
        }

        public static void MethodNumber5()
        {
            string str = EnterString();
            Console.WriteLine($"\nTotal number of words in the string is : {GetCountWords(str)}");
        }

        public static void MethodNumber6()
        {
            var firstStr = EnterString("first string");
            var secondStr = EnterString("second string");

            switch (GetCompareLength(firstStr, secondStr))
            {
                case 1:
                    Console.WriteLine("\nThe length of the first line is greater than the second.");
                    break;
                case -1:
                    Console.WriteLine("\nThe length of the second line is greater than the first.");
                    break;
                case 0:
                    if (GetCompareString(firstStr, secondStr))
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

        public static void MethodNumber7()
        {
            var str = EnterString();
            var digigits = 0;
            var alphabets = 0;
            var special = 0;

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

        public static void MethodNumber8()
        {
            var str = EnterString();
            var strCopied = CopyString(str);
            Console.WriteLine($"\nThe First string is :{str}");
            Console.WriteLine($"\nThe Second string is :{strCopied}");
        }

        public static void MethodNumber9()
        {
            var str = EnterString().ToLower();

            Console.WriteLine($"\nThe total number of vowel in the string is : {GetCountVowel(str)} ");
            Console.WriteLine($"The total number of consonant in the string is : {GetCountConsonant(str)} ");
        }

        public static void MethodNumber10()
        {
            var str = EnterString();
            var arrayBool = new bool[str.Length];
            var arrayChecked = new int[str.Length];

            for (var i = 0; i < str.Length; i++)
            {
                arrayBool[i] = true;
                arrayChecked[i] = 1;
            }

            for (var i = 0; i < str.Length - 1; i++)
            {
                for (var j = i + 1; j < str.Length; j++)
                {
                    if (str[i] == str[j] && arrayBool[i])
                    {
                        arrayBool[j] = false;
                        arrayChecked[i]++;
                    }
                }
            }

            var index = 0;
            var maxCheck = 0;

            for (var i = 0; i < str.Length; i++)
            {
                if (arrayBool[i] && arrayChecked[i]>maxCheck)
                {
                    maxCheck = arrayChecked[i];
                    index = i;
                }
            }

            Console.WriteLine($"\nThe Highest frequency of character '{str[index]}' appears number of times: {arrayChecked[index]} ");
        }

        /// <summary>
        /// input string
        /// </summary>
        public static string EnterString(string nameString = "string")
        {
            Console.Write($"Input the {nameString} : ");
            return Console.ReadLine();
        }

        /// <summary>
        /// return length of string
        /// </summary>
        public static int GetLength(string str)
        {
            var length = 0;

            foreach (var item in str)
            {
                length++;
            }

            return length;
        }

        /// <summary>
        /// return count words
        /// </summary>
        public static int GetCountWords(string str)
        {
            str = str.Trim();
            var count = str == string.Empty ? 0 : 1;

            for (var i = 1; i < str.Length; i++)
            {
                if ((str[i] == ' ' || str[i] == '\n' || str[i] == '\t')&&str[i-1] != ' ' && str[i-1] != '\n' && str[i-1] != '\t')
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// compare 2 string: -1 for str1<str2, 0 for str1=str2 and 1 for str1>str2
        /// </summary>
        public static int GetCompareLength(string str1, string str2)
        {
            return str1.Length >= str2.Length ? (str1.Length > str2.Length ? 1 : 0) : (-1);
        }

        /// <summary>
        /// compare 2 string
        /// </summary>
        public static bool GetCompareString(string str1, string str2)
        {
            var rezult = true;
            if (str1.Length == str2.Length)
            {
                for (var i = 0; i < str1.Length; i++)
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

        /// <summary>
        /// copy one string to another string
        /// </summary>
        public static string CopyString(string str)
        {
            var symbols = new string[str.Length];
            for (var i = 0; i < str.Length; i++)
            {
                symbols[i] = str[i].ToString();
            }

            return string.Join("", symbols); ;
        }

        /// <summary>
        /// return count vowel in string
        /// </summary>
        public static int GetCountVowel(string str)
        {
            var count = 0;

            foreach (var item in str)
            {
                if (char.IsLetter(item) && IsVowel(item))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// return count consonant in string
        /// </summary>
        public static int GetCountConsonant(string str)
        {
            var count = 0;

            foreach (var item in str)
            {
                if (char.IsLetter(item) && !IsVowel(item))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// vowel check 
        /// </summary>
        public static bool IsVowel(char value)
        {
            var vowel =  "aeiouy";

            return vowel.Contains(value);
        }
    }
}
