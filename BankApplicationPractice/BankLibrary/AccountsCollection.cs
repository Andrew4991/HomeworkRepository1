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

        public AccountsCollection(List<T> accounts)
        {
            _accounts = accounts;
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
