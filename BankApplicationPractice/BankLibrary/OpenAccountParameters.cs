namespace BankLibrary
{
    public class OpenAccountParameters
    {
        public AccountType Type { get; set; }

        public decimal Amount { get; set; }

        public AccountHandler AccountHandlerOpen { get; set; }

        public AccountHandler AccountHandlerClose { get; set; }

        public AccountHandler AccountHandlerPut { get; set; }

        public AccountHandler AccountHandlerWithdraw { get; set; }
    }
}
