using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Moq;

namespace BankApp.Business.UnitTest
{
    [TestClass]
    public class AccountManagerUnitTest
    {
        //Account a1 = null;
        //Account a2 = null;
        //[TestInitialize]
        //public void Init()
        //{
        //    a1 = new Account();
        //    a2 = new Account();
        //}
        //[TestCleanup]
        //public void Cleanup()
        //{
        //    a1 = null;
        //    a2 = null;
        //}

        [TestMethod]
        public void OpenAccountTest_ValidInput_ShouldOpenAccount()
        {
            Account a1 = new Account { AccNo = 101, Balance = 10000, IsActive = false };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\banktransactions.txt", true);
            mockStreamWriter.Setup(m => m.WriteLine($"{a1.AccNo} account opened on {a1.OpeningDate}"));


            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.OpenAccount(a1);
            mockStreamWriter.Verify(m => m.WriteLine($"{a1.AccNo} account opened on {a1.OpeningDate}"), Times.AtLeastOnce);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void OpenAccountTest_InvalidInput_ShouldThrowException()
        {
            Account a1 = new Account { AccNo = 209, Balance = 10000, IsActive = true };
            //Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\banktransactions.txt", true);
            var mockStreamWriter = new Mock<StreamWriter>(MockBehavior.Strict, "dummyPath");
            var accountManager = new AccountManager(mockStreamWriter.Object);            
            accountManager.OpenAccount(a1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CloseAccountTest_InvalidInput_ShouldThrowException()
        {
            Account a1 = new Account { AccNo = 210, Balance = 100, IsActive = true };
            //Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\banktransactions.txt", true);
            var mockStreamWriter = new Mock<StreamWriter>(MockBehavior.Strict, "somePath");
            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.CloseAccount(a1);
        }
        [TestMethod]
        public void CloseAccountTest_ValidInput_ShouldCloseAccount()
        {
            Account a1 = new Account { AccNo = 111, Balance = 0, IsActive = true };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\somebanktransactions.txt", true);
            mockStreamWriter.Setup(m => m.WriteLine($"{a1.AccNo} account closed on {a1.ClosingDate}"));


            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.CloseAccount(a1);
            mockStreamWriter.Verify(m => m.WriteLine($"{a1.AccNo} account closed on {a1.ClosingDate}"), Times.AtLeastOnce);
        }
        [TestMethod]
        public void DepositTest_ValidInput_ShouldDeposit()
        {
            Account a1 = new Account { AccNo = 111, Balance = 1000, IsActive = true };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\somebanktransactions1.txt", true);
            mockStreamWriter.Setup(m => m.WriteLine($"Rs.{99} has been deposited to {a1.AccNo}"));

            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.Deposit(a1, 99);
            mockStreamWriter.Verify(m => m.WriteLine($"Rs.{99} has been deposited to {a1.AccNo}"), Times.AtLeastOnce);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DepositTest_InvalidInput_ShouldThrowException()
        {
            Account a1 = new Account { AccNo = 111, Balance = 1000, IsActive = false };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\somebanktransactions2.txt", true);

            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.Deposit(a1, 99);
        }
        //


        [TestMethod]
        public void WithdrawTest_ValidInput_ShouldWithdraw()
        {
            Account a1 = new Account { AccNo = 111, Balance = 1000, IsActive = true, PIN = 1234 };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\somebanktransactions3.txt", true);
            mockStreamWriter.Setup(m => m.WriteLine($"Rs.{99} has been withdrawn from {a1.AccNo}"));

            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.Withdraw(a1, 99, 1234);
            mockStreamWriter.Verify(m => m.WriteLine($"Rs.{99} has been withdrawn from {a1.AccNo}"), Times.AtLeastOnce);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void WithdrawTest_InvalidInput_ShouldThrowException()
        {
            Account a1 = new Account { AccNo = 111, Balance = 1000, IsActive = false, PIN = 5678 };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\somebanktransactions4.txt", true);

            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.Withdraw(a1, 99, 1234);
        }

        [TestMethod]
        public void TransferTest_ValidInput_ShouldTransfer()
        {
            Account a1 = new Account { AccNo = 111, Balance = 1000, IsActive = true, PIN = 1234 };
            Account a2 = new Account { AccNo = 112, Balance = 500, IsActive = true, PIN = 0909 };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\somebanktransactions333.txt", true);
            mockStreamWriter.Setup(m => m.WriteLine($"Rs.{100} has been transfered from {a1.AccNo} to {a2.AccNo}"));

            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.Transfer(a1, a2, 100, 1234);
            mockStreamWriter.Verify(m => m.WriteLine($"Rs.{100} has been transfered from {a1.AccNo} to {a2.AccNo}"), Times.AtLeastOnce);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TransferTest_InvalidInput_ShouldThrowException()
        {
            Account a1 = new Account { AccNo = 111, Balance = 1000, IsActive = true, PIN = 1234 };
            Account a2 = new Account { AccNo = 112, Balance = 500, IsActive = true, PIN = 0909 };
            Mock<StreamWriter> mockStreamWriter = new Mock<StreamWriter>("C:\\Users\\MOHAMMAD SALMAN\\Documents\\somebanktransactions444.txt", true);

            var accountManager = new AccountManager(mockStreamWriter.Object);
            accountManager.Transfer(a1, a2, 100, 5555);
        }
    }
}
