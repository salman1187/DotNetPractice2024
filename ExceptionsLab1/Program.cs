using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExceptionsLab1.Account;

namespace ExceptionsLab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Account acc1 = new Account { AccNo = 111, CurrentBalance = 0, IsActive = true, MinimumBalance = 1000, Pin = 1234 };

                acc1.Deposit(111, 2000);
                acc1.Withdraw(111, 1500, 1234);
                acc1.Close(111, 1234);
            }
            catch (InvalidAccountNumberException ex)
            {
                Console.WriteLine("Caught InvalidAccountNumberException: " + ex.Message);
            }
            catch (InvalidAmountException ex)
            {
                Console.WriteLine("Caught InvalidAmountException: " + ex.Message);
            }
            catch (InActiveAccountException ex)
            {
                Console.WriteLine("Caught InActiveAccountException: " + ex.Message);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine("Caught InsufficientBalanceException: " + ex.Message);
            }
            catch (NoMinimumBalanceException ex)
            {
                Console.WriteLine("Caught NoMinimumBalanceException: " + ex.Message);
            }
            catch (InvalidPinException ex)
            {
                Console.WriteLine("Caught InvalidPinException: " + ex.Message);
            }
            catch (InvalidClosingBalanceException ex)
            {
                Console.WriteLine("Caught InvalidClosingBalanceException: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions not explicitly caught above
                Console.WriteLine("Caught an exception: " + ex.Message);
            }
        }
    }
}
