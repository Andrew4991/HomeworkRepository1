using System;
using System.Threading.Tasks;
using Currencies;
using Currencies.Entities;

namespace Portal
{
    public class Program
    {
        private static readonly CurrenciesApi _api = new();
        private static readonly CurrenciesConvertor _convertor = new();

        public static async Task Main(string[] args)
        {
            var alive = true;

            while (alive)
            {
                try
                {
                    switch (GetNumberItemOfMenu())
                    {
                        case 1:
                            await PrintCurrencyById();
                            break;
                        case 2:
                            await PrintCurrencyByAbbreviation();
                            break;
                        case 3:
                            await PrintCurrencyByIdAndDate();
                            break;
                        case 4:
                            await PrintCurrencyByAbbreviationAndDate();
                            break;
                        case 5:
                            await ConvertToBYN();
                            break;
                        case 6:
                            await ConvertFromBYN();
                            break;
                        case 7:
                            alive = false;
                            continue;
                        default:
                            throw new ArgumentException("There is no such menu item!");
                    }

                    Console.ReadKey();
                }
                catch (CurrencyNotAvailableException ex)
                {
                    ShowException(ex);
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
            Console.WriteLine("1. Print currency by id to the console");
            Console.WriteLine("2. Print currency by abbreviation to the console");
            Console.WriteLine("3. Print currency by id and date to the console");
            Console.WriteLine("4. Print currency by abbreviation and date to the console");
            Console.WriteLine("5. Convert to BYN");
            Console.WriteLine("6. Convert from BYN");
            Console.WriteLine("7. Exit program");

            Console.WriteLine("Enter the item number:");
            Console.ForegroundColor = color;
        }

        private static async Task PrintCurrencyById()
        {
            PrintCurrency(await _api.GetCurrencyRate(ReadId()));
        }

        private static async Task PrintCurrencyByAbbreviation()
        {

            PrintCurrency(await _api.GetCurrencyRate(ReadAbbreviation()));
        }

        private static async Task PrintCurrencyByIdAndDate()
        {
            PrintCurrency(await _api.GetCurrencyRate(ReadId(), ReadDate()));
        }

        private static async Task PrintCurrencyByAbbreviationAndDate()
        {

            PrintCurrency(await _api.GetCurrencyRate(ReadAbbreviation(), ReadDate()));
        }

        private static async Task ConvertToBYN()
        {
            PrintonvertMoney(await _convertor.ConvertToByn(ReadAbbreviation(), ReadAmount()), "BYN");
        }

        private static async Task ConvertFromBYN()
        {
            var abbreviation = ReadAbbreviation();

            PrintonvertMoney(await _convertor.ConvertFromByn(abbreviation, ReadAmount()), abbreviation);
        }

        private static void PrintCurrency(CurrencyRate rate)
        {
            Console.WriteLine(rate);
        }

        private static void PrintonvertMoney(decimal amount, string abbreviation)
        {
            Console.WriteLine($"{amount:0.0000} {abbreviation.ToUpper()}");
        }

        private static int ReadId()
        {
            Console.WriteLine("Please enter the currency Id:");

            int interval;

            while (!int.TryParse(Console.ReadLine(), out interval))
            {
                Console.Write("You entered the wrong Id. Please try again: ");
            }

            return interval;
        }

        private static DateTime ReadDate()
        {
            Console.WriteLine("Please enter date:");

            DateTime startDate;

            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.Write("You entered the wrong date. Please try again: ");
            }

            return startDate;
        }

        private static string ReadAbbreviation()
        {
            Console.WriteLine("Please enter the currency abbreviation:");

            string abbreviation;

            while (true)
            {
                abbreviation = Console.ReadLine().Trim();

                if (abbreviation.Length == 3)
                {
                    return abbreviation;
                }

                Console.Write("You entered the wrong abbreviation. Please try again: ");
            }

            return abbreviation;
        }

        private static decimal ReadAmount()
        {
            Console.WriteLine("Please enter the amount:");

            decimal amount;

            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.Write("You entered the wrong amount. Please try again: ");
            }

            return amount;
        }
    }
}
