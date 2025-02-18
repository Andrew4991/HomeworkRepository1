﻿namespace BankLibrary
{
    public class OnDemandAccount : Account
    {
        public OnDemandAccount(decimal amount)
            : base(amount)
        {
        }

        public override AccountType Type => AccountType.OnDemand;

        public override decimal Percentage => 0.02m;
    }
}