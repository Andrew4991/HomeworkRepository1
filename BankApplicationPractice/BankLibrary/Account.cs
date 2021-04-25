﻿using System;

namespace BankLibrary
{
    public delegate void AccountHandler(string message);

    public abstract class Account : IAccount
    {
        private static int _counter = 0;

        public decimal Amount { get; private set; }

        public int Id { get; private set; }

        public int Days { get; private set; }

        public AccountState State { get; private set; }

        public virtual decimal Percentage { get; private set; }

        public virtual AccountType Type { get; private set; }
        
        public event AccountHandler accountHandler;

        public Account(decimal amount)
        {
            Amount = amount;
            State = AccountState.Created;
            Id = ++_counter;
        }

        public virtual void Open()
        {
            HandlerAccount(AccountState.Created, $"{Type} account opened. Id: {Id}");

            State = AccountState.Opened;
            IncrementDays();
        }

        public virtual void Close()
        {
            HandlerAccount(AccountState.Opened, $"{Type} account closed. Id: {Id}");

            State = AccountState.Closed;
        }

        public virtual void Put(decimal amount)
        {
            Amount += amount;

            HandlerAccount(AccountState.Opened, $"The account received: {amount:f2}. Account balance: {Amount:f2}.");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (Amount < amount)
            {
                throw new InvalidOperationException("Not enough money");
            }

            Amount -= amount;

            HandlerAccount(AccountState.Opened, $"Withdrawn from the account: {amount:f2}. Account balance: {Amount:f2}.");
        }

        public void IncrementDays()
        {
            Days++;
        }

        public virtual void CalculatePercentage()
        {
            decimal increment = Amount * Percentage;
            Amount += increment;

            HandlerAccount(AccountState.Opened, $"Id: {Id}. Interest in the amount of: {increment:f2}. Account balance: {Amount:f2}.");
        }

        private void AssertValidState(AccountState validState)
        {
            if (State != validState)
            {
                throw new InvalidOperationException($"Invalid account state: {State}");
            }
        }

        private void HandlerAccount(AccountState validState, string message)
        {
            AssertValidState(validState);
            accountHandler?.Invoke(message);
        }
    }
}