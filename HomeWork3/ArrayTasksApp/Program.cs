﻿using System;

namespace ArrayTasksApp
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
                catch(Exception e)
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
                    Console.WriteLine("Write a program in C# Sharp to store elements in an array and print it. \n\n");
                    break;
                case 2:
                    Console.WriteLine("Write a program in C# Sharp to read n number of values in an array and display it in reverse order.\n\n");
                    break;
                case 3:
                    Console.WriteLine("Write a program in C# Sharp to find the sum of all elements of the array.\n\n");
                    break;
                case 4:
                    Console.WriteLine("Write a program in C# Sharp to copy the elements one array into another array.\n\n");
                    break;
                case 5:
                    Console.WriteLine("Write a program in C# Sharp to count a total number of duplicate elements in an array.\n\n");
                    break;
                case 6:
                    Console.WriteLine("Write a program in C# Sharp to print all unique elements in an array.\n\n");
                    break;
                case 7:
                    Console.WriteLine("Write a program in C# Sharp to merge two arrays of same size sorted in ascending order.\n\n");
                    break;
                case 8:
                    Console.WriteLine("Write a program in C# Sharp to count the frequency of each element of an array.\n\n");
                    break;
                case 9:
                    Console.WriteLine("Write a program in C# Sharp to find maximum and minimum element in an array.\n\n");
                    break;
                case 10:
                    Console.WriteLine("Write a programin C# Sharp to separate odd and even integers in separate arrays.\n\n");
                    break;
                default:
                    throw new Exception("There is no such task !");
            }

            Console.ResetColor();
        }

        public static void MethodNumber1()
        {
            int[] array = EnterArray(GetLength());

            Console.WriteLine("\nElements in array are:");
            PrintArray(array);
        }

        public static void MethodNumber2()
        {
            int[] array = EnterArray(GetLength());

            Console.WriteLine("\nThe values store into the array are:");
            PrintArray(array);
            Console.WriteLine("\nThe values store into the array in reverse are :");
            PrintArrayReverse(array, "ReverseArray");
        }

        public static void MethodNumber3()
        {
            int[] array = EnterArray(GetLength());

            Console.WriteLine($"\nSum of all elements of the array: {GetSumArray(array)}");
        }

        public static void MethodNumber4()
        {
            int[] firstArray = EnterArray(GetLength());
            int[] secondArray = new int[firstArray.Length];

            for (int i = 0; i < firstArray.Length; i++)
            {
                secondArray[i] = firstArray[i];
            }

            Console.WriteLine("\nThe elements stored in the first array are :");
            PrintArray(firstArray, "FirstArray");
            Console.WriteLine("\nThe elements copied into the second array are :");
            PrintArray(secondArray, "SecondArray");
        }

        public static void MethodNumber5()
        {
            int[] array = EnterArray(GetLength());

            Console.WriteLine($"\nTotal number of duplicate elements found in the array is : {GetCountDuplicate(array)}");
        }

        public static void MethodNumber6()
        {
            int[] array = EnterArray(GetLength());
            bool[] arrayBool = new bool[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                arrayBool[i] = true;
            }
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] == array[j])
                    {
                        arrayBool[i] = false;
                        arrayBool[j] = false;
                    }
                }
            }

            Console.WriteLine("\nThe unique elements found in the array are :");
            PrintCheckedArray(array, arrayBool);
        }

        public static void MethodNumber7()
        {
            int[] firstArray = EnterArray(GetLength("FirstArray"),"FirstArray");
            int[] secondArray = EnterArray(GetLength("SecondArray"), "SecondArray");
            int[] array = AddArray(firstArray, secondArray);
            SortArray(array);

            Console.WriteLine("\nThe merged array in ascending order is :");
            PrintArray(array);
        }

        public static void MethodNumber8()
        {
            int[] array = EnterArray(GetLength());
            bool[] arrayBool = new bool[array.Length];
            int[] arrayChecked = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                arrayBool[i] = true;
                arrayChecked[i] = 1;
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] == array[j] && arrayBool[i])
                    {
                        arrayBool[j] = false;
                        arrayChecked[i]++;
                    }
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (arrayBool[i])
                {
                    Console.WriteLine($"{array[i]}  occurs {arrayChecked[i]} times");
                }
            }
        }

        public static void MethodNumber9()
        {
            int[] array = EnterArray(GetLength());

            Console.WriteLine($"\nMaximum element is : {GetMaxElement(array)}");
            Console.WriteLine($"Minimum element is : {GetMinElement(array)}");
        }

        public static void MethodNumber10()
        {
            int[] array = EnterArray(GetLength());

            Console.WriteLine("\nThe Even elements are:");
            PrintArray(GetEvenArray(array), "EvenArray");
            Console.WriteLine("\nThe Odd elements are:");
            PrintArray(GetOddArray(array), "OddArray");
        }

        /// <summary>
        /// input of the number of array elements 
        /// </summary>
        public static int GetLength(string nameArray = "Array")
        {
            Console.Write($"Enter the namber of {nameArray}: ");
            return int.Parse(Console.ReadLine().Trim());
        }

        /// <summary>
        /// input array elements 
        /// </summary>
        public static int[] EnterArray(int length, string nameArray = "Array")
        {
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                Console.Write($"{nameArray}[{i}] ");
                array[i] = int.Parse(Console.ReadLine().Trim());
            }
            return array;
        }

        /// <summary>
        /// output array elements 
        /// </summary>
        public static void PrintArray(int[] array, string nameArray = "Array")
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"{nameArray}[{i}] = {array[i]} ");
            }
        }

        /// <summary>
        /// reverse output array elements 
        /// </summary>
        public static void PrintArrayReverse(int[] array, string nameArray = "Array")
        {
            for (int i = array.Length-1; i >=0; i--)
            {
                Console.WriteLine($"{nameArray}[{i}] = {array[i]} ");
            }
        }

        /// <summary>
        /// calculating the sum of array elements 
        /// </summary>
        public static int GetSumArray(int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        /// <summary>
        /// found number of duplicate elements
        /// </summary>
        public static int GetCountDuplicate(int[] array)
        {
            int countDuplicate = 0;

            for (int i = 0; i < array.Length - 1; i++)
            {
                bool isNotFound = true;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] == array[j] && isNotFound)
                    {
                        bool repeated = false;
                        for (int k = 0; k < i; k++)
                        {
                            if (array[i] == array[k])
                            {
                                repeated = true;
                            }
                        }
                        if (!repeated)
                        {
                            countDuplicate++;
                            isNotFound = false;
                        }
                    }
                }
            }

            return countDuplicate;
        }

        /// <summary>
        /// output array elements with check array
        /// </summary>
        public static void PrintCheckedArray(int[] array, bool[] checkArray, string nameArray = "Array")
        {
            if(array.Length == checkArray.Length)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (checkArray[i])
                    {
                        Console.WriteLine($"{nameArray}[{i}] = {array[i]} ");
                    }
                }
            }
            else
            {
                throw new Exception("The length of the array does not match the length of the reference array !");
            }

            
        }

        /// <summary>
        /// addition of arrays 
        /// </summary>
        public static int[] AddArray(int[] array1, int[] array2)
        {
            int[] array = new int[array1.Length+ array2.Length];

            for (int i = 0; i < array1.Length; i++)
            {
                array[i] = array1[i];
            }
            for (int i = 0; i < array2.Length; i++)
            {
                array[array1.Length+i] = array2[i];
            }
            return array;
        }

        /// <summary>
        /// sorted in ascending order
        /// </summary>
        public static void SortArray (int[] array)
        {
            /*
             * it could have been like that 
             * Array.Sort(array);
            */

            int temp;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// return max value of array
        /// </summary>
        public static int GetMaxElement(int[] array)
        {
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }

        /// <summary>
        /// return min value of array
        /// </summary>
        public static int GetMinElement(int[] array)
        {
            int min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                }
            }
            return min;
        }

        /// <summary>
        /// return even elements
        /// </summary>
        public static int[] GetEvenArray(int[] array)
        {
            int[] evenArray = new int[array.Length];
            int index = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if(array[i]%2 == 0)
                {
                    evenArray[index++] = array[i];
                }
            }
            Array.Resize(ref evenArray, index);
            return evenArray;
        }

        /// <summary>
        /// return odd elements
        /// </summary>
        public static int[] GetOddArray(int[] array)
        {
            int[] oddArray = new int[array.Length];
            int index = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 != 0)
                {
                    oddArray[index++] = array[i];
                }
            }
            Array.Resize(ref oddArray, index);
            return oddArray;
        }

    }
}
