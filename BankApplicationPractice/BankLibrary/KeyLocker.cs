using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class KeyLocker
    {
        private readonly string _keyword;
        private readonly int _id;

        public KeyLocker(int id, string keyword)
        {
            _id = id;
            _keyword = keyword;
        }

        public int Id => _id;

        public bool Matches(int id, string keyword)
        {
            return _id == id && _keyword.Equals(keyword);
        }

        public override int GetHashCode()
        {
            return _keyword.GetHashCode() ^ Id.GetHashCode(); 
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as KeyLocker);
        }

        public bool Equals(KeyLocker obj)
        {
            return obj != null && obj._id == _id && obj._keyword.Equals(_keyword);
        }
    }
}
