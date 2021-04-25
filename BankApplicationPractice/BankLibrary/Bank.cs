using System;
using System.Collections.Generic;
using System.Linq;

namespace BankLibrary
{
    public class Bank<T> where T : IAccount
    {
        private readonly List<T> _accounts = new();

        public void OpenAccount(OpenAccountParameters parameters)
        {
            AssertValidType(parameters);

            IAccount account = parameters.Type == AccountType.Deposit
                ? new DepositAccount(parameters.Amount)
                : new OnDemandAccount(parameters.Amount);

            account.accountHandler += parameters.accountHandler;
            account.Open();
            _accounts.Add((T)account);
        }

        public void WithdrawFromAccount(int accountId, decimal sum) => HandlerAccount(accountId, acc => acc.Withdraw(sum));

        public void PutOnAccount(int accountId, decimal sum) => HandlerAccount(accountId, acc => acc.Put(sum));

        public void CloseAccount(int accountId) => HandlerAccount(accountId, acc => acc.Close());

        public void HandlerNextDay()
        {
            if (_accounts.Count == 0)
            {
                throw new InvalidOperationException($"There are no accounts in the bank!");
            }

            foreach (var acc in _accounts)
            {
                if (acc.State != AccountState.Closed)
                {
                    acc.IncrementDays();
                    acc.CalculatePercentage();
                }
            }
        }

        private void AssertValidType(OpenAccountParameters parameters)
        {
            var typeAccount = GetType().GenericTypeArguments[0];

            if (typeAccount != typeof(Account) && 
                ((parameters.Type == AccountType.Deposit && typeAccount != typeof(DepositAccount)) ||
                (parameters.Type == AccountType.OnDemand && typeAccount != typeof(OnDemandAccount))))
            {
                throw new InvalidOperationException($"Invalid account type: {parameters.Type}");
            }
        }

        private int GetIndexAccount(int accountId)
        {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if(_accounts[i].Id == accountId)
                {
                    return i;
                }
            }

            return -1;
        }

        private void HandlerAccount(int accountId, Action<T> action)
        {
            AssertValidId(accountId);

            var indexAccount = GetIndexAccount(accountId);
            var account = _accounts[indexAccount];
            action(account);
            _accounts.Insert(indexAccount, account);
        }

        private void AssertValidId(int accountId)
        {
            if (!IsCorrectId(accountId))
            {
                throw new ArgumentException("Invalid id!");
            }
        }

        private bool IsCorrectId(int accountId) => _accounts.Any(x => x.Id == accountId);
    }
}