using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public interface IAccount
    {
        event Action<string> AccountHandlerOpen;

        event Action<string> AccountHandlerClose;

        event Action<string> AccountHandlerPut;

        event Action<string> AccountHandlerWithdraw;

        int Days { get; }

        decimal Percentage { get; }

        int Id { get; }

        public AccountState State { get; }

        void Open();

        void Close();

        void Put(decimal amount);

        void Withdraw(decimal amount);

        void IncrementDays();

        void CalculatePercentage();
    }
}
