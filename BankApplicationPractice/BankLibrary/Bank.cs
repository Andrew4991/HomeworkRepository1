using System;
using System.Collections.Generic;
using System.Linq;

namespace BankLibrary
{
    public class Bank<T> where T : IAccount
    {
        private readonly AccountsCollection<T> _accounts = new();

        private readonly Dictionary<KeyBankCell, BankCell<int>> _bankCell = new();

        public void OpenAccount(OpenAccountParameters parameters)
        {
            AssertValidType(parameters);

            IAccount account = parameters.Type == AccountType.Deposit
                ? new DepositAccount(parameters.Amount)
                : new OnDemandAccount(parameters.Amount);

            account.AccountHandlerOpen += parameters.AccountHandlerOpen;
            account.AccountHandlerClose += parameters.AccountHandlerClose;
            account.AccountHandlerPut += parameters.AccountHandlerPut;
            account.AccountHandlerWithdraw += parameters.AccountHandlerWithdraw;
            account.Open();
            _accounts.Add((T)account);
        }

        public void WithdrawFromAccount(int accountId, decimal sum) => HandleAccountChange(accountId, acc => acc.Withdraw(sum));

        public void PutOnAccount(int accountId, decimal sum) => HandleAccountChange(accountId, acc => acc.Put(sum));

        public void CloseAccount(int accountId) => HandleAccountChange(accountId, acc => acc.Close());

        public void HandlerNextDay()
        {
            if (_accounts.Count == 0)
            {
                throw new InvalidOperationException($"There are no accounts in the bank!");
            }

            foreach (T acc in _accounts)
            {
                if (acc.State != AccountState.Closed)
                {
                    acc.IncrementDays();
                    acc.CalculatePercentage();
                }
            }
        }

        public void OpenCell(int data, string key, Action<string> handler)
        {
            var newCell = new BankCell<int>()
            {
                Id = _bankCell.Count,
                Data = data,
                _handlerCell = handler
            };

            newCell.Open();

            _bankCell[new KeyBankCell(_bankCell.Count, key)] = newCell;
        }

        public int GetCell(int id, string key)
        {
            var cell = _bankCell[new KeyBankCell(id, key)];

            if (cell == null)
            {
                throw new InvalidOperationException($"There are no cell in the bank!");
            }

            return cell.Data;
        }

        private void AssertValidType(OpenAccountParameters parameters)
        {
            var typeAccount = typeof(T);

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

        private void HandleAccountChange(int accountId, Action<T> action)
        {
            AssertValidId(accountId);

            var indexAccount = GetIndexAccount(accountId);
            var account = _accounts[indexAccount];
            action(account);
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
