using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class AccountsCollection<T> : IEnumerable<T> where T : IAccount
    {
        private readonly List<T> _accounts;

        public int Count { get; private set; }

        public AccountsCollection()
        {
            _accounts = new();
            Count = _accounts.Count;
        }
        public AccountsCollection(List<T> accounts)
        {
            _accounts = accounts;
            Count = _accounts.Count;
        }

        public void Add(T newAccount)
        {
            _accounts.Add(newAccount);
            Count = _accounts.Count;
        }

        public T this[int index]
        {
            get => _accounts[index];
            set => _accounts[index] = value;
        }

        public AccountsCollectionEnumerator<T> GetEnumerator()
        {
            return new AccountsCollectionEnumerator<T>(_accounts);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }
    }
}
