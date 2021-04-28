using System.Collections;
using System.Collections.Generic;

namespace BankLibrary
{
    public class AccountsCollectionEnumerator<T> : IEnumerator<T> where T : IAccount
    {
        private readonly List<T> _accounts;

        private int position = -1;

        public AccountsCollectionEnumerator(List<T> accounts)
        {
            _accounts = accounts;
        }

        public object Current
        {
            get
            {
                return _accounts[position];
            }
        }

        T IEnumerator<T>.Current
        {
            get
            {
                return _accounts[position];
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            position++;

            return position < _accounts.Count;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
