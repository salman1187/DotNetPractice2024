using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Business
{
    public class Account
    {
        public int AccNo { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public int PIN { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set;}
    }
    public class AccountManager
    {
        StreamWriter writer = null;
        public AccountManager()
        {
            writer = new StreamWriter("C:\\Users\\MOHAMMAD SALMAN\\Documents\\banktransactions.txt", true);
        }
        public AccountManager(StreamWriter sw)
        {
            writer = sw;
        }
        public void OpenAccount(Account account)
        {
            if (account.IsActive)
                throw new Exception("Account already open");
            account.IsActive = true;
            account.OpeningDate = DateTime.Today;
            account.Balance = 0;
            writer.WriteLine($"{account.AccNo} account opened on {account.OpeningDate}");
            
        }
        public void CloseAccount(Account account)
        {
            if (!account.IsActive)
                throw new Exception("Account already closed");
            if (account.Balance > 0)
                throw new Exception("Cannot close account with balance");
            account.IsActive = false;
            account.ClosingDate = DateTime.Today;
            writer.WriteLine($"{account.AccNo} account closed on {account.ClosingDate}");
            
        }
        public void Deposit(Account account, int amount)
        {
            if (!account.IsActive)
                throw new Exception("cannot deposit in an inactive account");

            account.Balance += amount;
            writer.WriteLine($"Rs.{amount} has been deposited to {account.AccNo}");
        }
        public void Withdraw(Account account, int amount, int pin)
        {
            if (!account.IsActive)
                throw new Exception("cannot withdraw from an inactive account");
            if(account.Balance < amount)
                throw new Exception("balance less than withdraw amount");
            if(account.PIN != pin)
                throw new Exception("PIN doesn't match");

            account.Balance -= amount;
            writer.WriteLine($"Rs.{amount} has been withdrawn from {account.AccNo}");
        }
        public void Transfer(Account acc1, Account acc2, int amount, int pin)
        {
            if (!acc1.IsActive || !acc2.IsActive)
                throw new Exception("accounts must be active to transfer");
            if (acc1.Balance < amount)
                throw new Exception("balance less than withdraw amount");
            if (acc1.PIN != pin)
                throw new Exception("PIN doesn't match");
            acc1.Balance -= amount;
            acc2.Balance += amount;
            writer.WriteLine($"Rs.{amount} has been transfered from {acc1.AccNo} to {acc2.AccNo}");
        }
    }
}
