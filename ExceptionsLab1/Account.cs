using System;
using System.Net;
using System.Security.Policy;

namespace ExceptionsLab1
{
    public class Account
    {
        public int AccNo { get; set; }
        public int CurrentBalance { get; set; }
        public int MinimumBalance { get; set; }
        public int Pin { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Deposits valid amount into a valid account
        /// </summary>
        /// <param name="accNo"></param>
        /// <param name="amount"></param>
        /// <exception cref="InvalidAccountNumberException"></exception>
        public void Deposit(int accNo, int amount)
        {
            // valid accNo 
            if (this.AccNo != accNo)
                throw new InvalidAccountNumberException("Account number not found");
            // valid amount (amount > 0)
            if (amount <= 0)
                throw new InvalidAmountException("Amount is invalid");
            // should be active acc
            if (!this.IsActive)
                throw new InActiveAccountException("The account is inactive");

            this.CurrentBalance += amount;



        }
        public void Withdraw(int accNo, int amount, int pin)
        {
            // valid accno
            if (this.AccNo != accNo)
                throw new InvalidAccountNumberException("Account number not found");
            // valid amount (> 0)
            if (amount <= 0)
                throw new InvalidAmountException("Amount is invalid");
            // sufficcient balance
            if (amount > this.CurrentBalance)
                throw new InsufficientBalanceException("Balance is not sufficient");
            // must maintain min balance 5000-1000 <= 1000
            if (this.CurrentBalance - amount <= this.MinimumBalance)
                throw new NoMinimumBalanceException("The account will not have minimum balance");
            // must be active account
            if (!this.IsActive)
                throw new InActiveAccountException("The account is inactive");
            // valid pin
            if (this.Pin != pin)
                throw new InvalidPinException("Invalid PIN");
            

            this.CurrentBalance -= amount;  

        }
        public void Transfer(int fromAccNo, int toAccNo, int amount, int fromAccPin)
        {
            if (this.AccNo != fromAccNo)
                throw new InvalidAccountNumberException("from account number not found");
            if (!this.IsActive)
                throw new InActiveAccountException("from account is inactive");
            if (this.Pin != fromAccPin)
                throw new InvalidPinException("Invalid PIN for from account");


            Withdraw(fromAccNo, amount, fromAccPin);

        }
        public void Close(int accNo, int pin)
        {
            // Valid accno
            if (this.AccNo != accNo)
                throw new InvalidAccountNumberException("Account number not found");
            // Valid pin
            if (this.Pin != pin)
                throw new InvalidPinException("Invalid PIN");
            // Balance must be zero
            if (this.CurrentBalance != 0)
                throw new InvalidClosingBalanceException("Cannot close account with non-zero balance");
            // Should be active account
            if (!this.IsActive)
                throw new InActiveAccountException("The account is inactive");

        }
        public class InvalidAccountNumberException : ApplicationException
        {
            public InvalidAccountNumberException(string msg) : base(msg) { }
        }
        public class InvalidAmountException : ApplicationException
        {
            public InvalidAmountException(string msg) : base(msg) { }
        }
        public class InActiveAccountException : ApplicationException
        {
            public InActiveAccountException(string msg) : base(msg) { }
        }
        public class InsufficientBalanceException : ApplicationException
        {
            public InsufficientBalanceException(string msg) : base(msg) { }
        }
        public class NoMinimumBalanceException : ApplicationException
        {
            public NoMinimumBalanceException(string msg) : base(msg) { }
        }
        public class InvalidPinException : ApplicationException
        {
            public InvalidPinException(string msg) : base(msg) { }
        }
        public class InvalidClosingBalanceException : ApplicationException
        {
            public InvalidClosingBalanceException(string msg) : base(msg) { }
        }
    }
}
