using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesDemo3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Account acc1 = new Account();
            acc1.Notify += Notification.SendMail;
            acc1.Notify += Notification.SendSMS;

            acc1.Deposit(1000);
            Console.WriteLine("Balance: " + acc1.Balance);
            acc1.Withdrawal(500);
            Console.WriteLine("Balance: " + acc1.Balance);

            //acc1.Notify("Money Deposited: 100000");
        }
    }
    public delegate void NotifyDelegate(string msg);
    public class Account //business class
    {
        public int Balance { get; set; }
        public event NotifyDelegate Notify = null;
        public void Deposit(int amount)
        {
            Balance += amount;
            string msg = $"Deposited amount: {amount}";
            Notify(msg);
        }
        public void Withdrawal(int amount)
        {
            Balance -= amount;
            string msg = $"Withdrawn amount: {amount}";
            Notify(msg);
        }
    }
    public class Notification
    {
        public static void SendMail(string msg)
        {
            Console.WriteLine($"Email: {msg}");
        }
        public static void SendSMS(string msg)
        {
            Console.WriteLine($"SMS: {msg}");
        }
        public static void SendWA(string msg)
        {
            Console.WriteLine($"WA: {msg}");
        }
    }


}