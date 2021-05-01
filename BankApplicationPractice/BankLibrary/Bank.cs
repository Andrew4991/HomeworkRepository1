using System;
using System.Collections.Generic;
using System.Linq;

namespace BankLibrary
{
    public class Bank<T> where T : IAccount
    {
        private const string KgkPassPhrase = "CleanUp";

        private readonly AccountsCollection<T> _accounts = new();

        private readonly Dictionary<KeyLocker, object> _lockers = new();

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

        public int AddLocker(string keyword, object data)
        {
            var keyLocker = new KeyLocker(_lockers.Count + 1, keyword);

            _lockers.Add(keyLocker, data);

            return keyLocker.Id;
        }

        public object GetLockerData(int id, string keyword)
        {
            var key = new KeyLocker(id, keyword);

            if (!_lockers.Keys.Any(k => k.Equals(key)))
            {
                throw new ArgumentException($"Cannot find locker with ID: {id} or keyword does not match");
            }

            var data = _lockers[key];

            if (data == null)
            {
                throw new ArgumentException($"Locker with ID: {id} contains no data.");
            }

            return data;
        }

        public TU GetLockerData<TU>(int id, string keyword)
        {
            return (TU)GetLockerData(id, keyword);
        }

        public void VisitKgk(string passPhrase)
        {
            if (passPhrase.Equals(KgkPassPhrase))
            {
                foreach (var locker in _lockers)
                {
                    _lockers[locker.Key] = null;
                }
            }
            else
            {
                throw new ArgumentException($"The passPhrase({passPhrase}) is not correct.");
            }
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
