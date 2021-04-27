using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class BankCell<T>
    {
        public T Data { get; set; }

        public int Id{ get; set; }

        public  Action<string> _handlerCell;

        public BankCell()
        {
        }

        public void Open()
        {
            _handlerCell?.Invoke($"Cell {Id} created!"); 
        }
    }
}
