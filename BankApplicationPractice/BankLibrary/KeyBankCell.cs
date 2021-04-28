using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class KeyBankCell
    {
        public int Id { get; set; }

        public string Codeword { get; set; }

        public KeyBankCell(int id, string codeword)
        {
            Id = id;
            Codeword = codeword;
        }

        public override int GetHashCode()
        {
            return Codeword.GetHashCode() + Id.GetHashCode(); 
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as KeyBankCell);
        }

        public bool Equals(KeyBankCell obj)
        {
            return obj != null && obj.Id == Id && obj.Codeword == Codeword;
        }
    }
}
