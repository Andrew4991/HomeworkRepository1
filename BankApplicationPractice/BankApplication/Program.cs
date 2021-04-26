using System;
using BankLibrary;

namespace BankApplication
{
    public class Program
    {
        private static readonly Bank<Account> _bank1 = new();

        public static void Main(string[] args)
        {
            bool alive = true;

            while (alive)
            {
                try
                {
                    switch (GetNumberItemOfMenu())
                    {
                        case 1:
                            OpenAccount();
                            break;
                        case 2:
                            Withdraw();
                            break;
                        case 3:
                            Put();
                            break;
                        case 4:
                            CloseAccount();
                            break;
                        case 5:
                            SkipDay();
                            break;
                        case 6:
                            alive = false;
                            continue;
                        default:
                            throw new ArgumentException("There is no such menu item!");
                    }
                    
                    Console.ReadKey();
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
            Console.WriteLine("1. Open Account \t 2. Withdraw sum \t 3. Add sum");
            Console.WriteLine("4. Close Account \t 5. Skip day \t 6. Exit program");
            Console.WriteLine("Enter the item number:");
            Console.ForegroundColor = color;
        }

        private static void OpenAccount() => _bank1.OpenAccount(new OpenAccountParameters
        {
            Amount = ReadSum("Specify the sum to create an account: "),
            Type = ReadAccountType("Select an account type: \n 1. On-Demand \n 2. Deposit"),
            AccountHandlerOpen = NotifyChangeAccount,
            AccountHandlerClose = NotifyChangeAccount,
            AccountHandlerPut = NotifyChangeAccount,
            AccountHandlerWithdraw = NotifyChangeAccount,
        });

        private static void Withdraw()
        {
            var id = ReadId("Enter account id: ");
            var sum = ReadSum("Specify the sum to withdraw from the account: ");

            _bank1.WithdrawFromAccount(id, sum);
        }

        private static void Put()
        {
            var id = ReadId("Enter account id: ");
            var sum = ReadSum("Specify the sum to withdraw from the account: ");

            _bank1.PutOnAccount(id, sum);
        }

        private static void CloseAccount()
        {
            var id = ReadId("Enter the account id to close: ");

            _bank1.CloseAccount(id);
        }

        private static void SkipDay() => _bank1.HandlerNextDay();

        private static AccountType ReadAccountType(string message)
        {
            Console.WriteLine(message);

            AccountType type;

            while (!Enum.TryParse(Console.ReadLine(), out type))
            {
                Console.Write("You entered the wrong type. Please try again: ");
            }

            return type;
        }

        private static decimal ReadSum(string message)
        {
            Console.WriteLine(message);

            decimal sum;

            while (!decimal.TryParse(Console.ReadLine(), out sum))
            {
                Console.Write("You entered the wrong sum. Please try again: ");
            }

            return sum;
        }

        private static int ReadId(string message)
        {
            Console.WriteLine(message);

            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("You entered the wrong Id. Please try again: ");
            }

            return id;
        }

        private static void NotifyChangeAccount(string message) => Console.WriteLine(message);
    }
}
