using System;

namespace BankLibrary
{
    public class OpenAccountParameters
    {
        public AccountType Type { get; set; }

        public decimal Amount { get; set; }

        public Action<string> AccountHandlerOpen { get; set; }

        public Action<string> AccountHandlerClose { get; set; }

        public Action<string> AccountHandlerPut { get; set; }

        public Action<string> AccountHandlerWithdraw { get; set; }
    }
}
