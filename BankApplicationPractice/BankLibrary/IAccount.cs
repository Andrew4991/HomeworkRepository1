using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public interface IAccount
    {
        event AccountHandler accountHandler;

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
