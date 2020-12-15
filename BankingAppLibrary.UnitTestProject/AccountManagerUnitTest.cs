using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BankingAppLibrary.UnitTestProject
{
    [TestClass]
    public class AccountManagerUnitTest
    {

        AccountManager target = null;
        Saving saving = null;
        List<Account> res = null;
        [TestInitialize]
        public void Initialize()
        {
            target = new AccountManager();
            saving = new Saving { Name = "Person", Pin = 1234, Balance = 999, Gender = "Male" };

        }

        [TestCleanup]
        public void CleanUp()
        {
            target = null;
            saving = null;
        }

        [TestMethod]
        public void Open_OnSuccess_SetOpenDate()
        {
            Account account = target.Open(saving, "Savings");
            Assert.AreEqual(DateTime.Now.Date, account.OpeningDate);
        }

        [TestMethod]
        public void Open_OnSuccess_SetStatusToActive()
        {


            Account account = target.Open(saving, "SAVINGS");

            Assert.AreEqual(true, account.IsActive);
        }


        [TestMethod]
        [ExpectedException(typeof(ExistingAccountException))]
        public void Open_WithActiveAcount_ShouldThrowExp()
        {
            saving.IsActive = true;

            Account account = target.Open(saving, "SAVINGS");
            Assert.IsTrue(account.IsActive);
        }

        [TestMethod]
        public void Close_OnSuccess_SetCloseDate()
        {
            AccountManager target = new AccountManager();
            Saving saving = new Saving { Name = "Person", Pin = 1234, Balance = 0, Gender = "Male", IsActive = true };
            bool res = target.Close(saving);
            Assert.AreEqual(DateTime.Now.Date, saving.ClosingDate);
        }

        [TestMethod]
        public void Close_Onsuccess_SetStatusToInactive()
        {
            AccountManager target = new AccountManager();

            Saving saving = new Saving
            {
                Name = "name",
                Pin = 1234,
                Balance = 0,
                IsActive = true
            };

            bool res = target.Close(saving);

            Assert.AreEqual(false, saving.IsActive);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountAlreadyClosedException))]
        public void Close_WithInactiveAccount_ShouldThrowException()
        {
            AccountManager target = new AccountManager();

            Saving saving = new Saving
            {
                Name = "name",
                Pin = 1234,
                Balance = 0,
                IsActive = false
            };

            bool res = target.Close(saving);
            Assert.IsFalse(saving.IsActive);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountHaveBalanceException))]
        public void Close_BalanceNotZero_ShouldThrowException()
        {
           Saving saving = new Saving
            {
                Name = "name",
                Pin = 1234,
                Balance = 500,
                IsActive = true
            };

            bool res = target.Close(saving);
            Assert.IsFalse(saving.IsActive);
        }


        //withdar test case
        [TestMethod]
        [ExpectedException(typeof(InvalidPinException))]
        public void Withdraw_WrongPin_GiveException()
        {
            saving.IsActive = true;
            target.Withdraw(saving, 2222, 200);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAmountEnterdException))]
        public void Withdraw_NegativeAmount_GiveException()
        {
            saving.IsActive = true;
            target.Withdraw(saving, 1234, -200);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientBalanceException))]
        public void Withdraw_AmountMoreThanBAlance_GiveException()
        {
            saving.IsActive = true;
            target.Withdraw(saving, 1234, 999999);
        }
        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]

        public void Withdraw_InvalidAccount_GiveException()
        {
            saving.IsActive = false;
            target.Withdraw(saving, 1234, 20);
        }

        [TestMethod]
        public void Withdraw_OnSuccess_BalaceMatch()
        {
            saving.IsActive = true;
            target.Withdraw(saving, 1234, 100);
            Assert.AreEqual(saving.Balance, 999 - 100);
        }

        //Deposite
        [TestMethod]
        [ExpectedException(typeof(PinNotMatchedException))]
        public void Deposit_WrongPin_GiveException()
        {
            saving.IsActive = true;
            target.Deposit(saving, 2222, 200);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAmountEnterdException))]
        public void Deposit_NegativeAmount_GiveException()
        {
            saving.IsActive = true;

            target.Deposit(saving, 1234, -200);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void Deposite_InvalidAccount_GiveException()
        {
            saving.IsActive = false;
            target.Deposit(saving, 1234, 20);
        }

        [TestMethod]
        public void Deposite_Onsuccess_BalanceMatch()
        {
            saving.IsActive = true;
            target.Deposit(saving, 1234, 300);
            Assert.AreEqual(saving.Balance, 999 + 300);
        }


        //tranfer
        [TestMethod]
        [ExpectedException(typeof(InvalidPinException))]
        public void Transfer_WrongPin_GiveException()
        {
            saving.IsActive = true;
            target.Withdraw(saving, 2222, 200);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAmountEnterdException))]
        public void Transfer_NegativeAmount_GiveException()
        {
            Saving tranferaccount = new Saving
            {
                AccountNumber = 2345,
                Balance = 500,
                IsActive = true
               

            };
            saving.IsActive = true;
            target.Transfer(saving, tranferaccount, 1234, -200);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientBalanceException))]
        public void Transfer_AmountMoreThanBAlance_GiveException()
        {
            Saving tranferaccount = new Saving
            {
                AccountNumber = 2345,
                Balance = 500,
                IsActive = true


            };
            saving.IsActive = true;
            target.Transfer(saving, tranferaccount, 1234, 90000000);
            
        }
        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void Transfer_InvalidAccountSender_GiveException()
        {
            Saving tranferaccount = new Saving
            {
                AccountNumber = 2345,
                Balance = 500,
                IsActive = true


            };
            
            
            saving.IsActive = false;
            target.Transfer(saving, tranferaccount, 1234, 60);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void Transfer_InvalidAccountReciver_GiveException()
        {
            Saving tranferaccount = new Saving
            {
                AccountNumber = 2345,
                Balance = 500,
                IsActive = false


            };

            saving.IsActive = true;
            target.Transfer(saving, tranferaccount, 1234, 60);
        }

        [TestMethod]
        public void Transfer_OnSuccess_BalaceMatchsender()
        {
            Saving tranferaccount = new Saving
            {
                AccountNumber = 2345,
                Balance = 500,
                IsActive = true

            };
            saving.IsActive = true;
            target.Transfer(saving,tranferaccount, 1234, 100);
            Assert.AreEqual(saving.Balance, 999 - 100);

        }
        [TestMethod]
        public void Transfer_OnSuccess_BalaceMatchsenderReceiver()
        {
            Saving tranferaccount = new Saving
            {
                AccountNumber = 2345,
                Balance = 500,
                IsActive = true


            };
            saving.IsActive = true;
            target.Transfer(saving, tranferaccount, 1234, 100);
            Assert.AreEqual(tranferaccount.Balance, 500 + 100);

        }
        //all saving test
        //all current test
        //all active test 
        //all balace test
        [TestMethod]
        public void AllSavingsAccount_OnSucess_ShouldGetOnlySavingsAccounts()
        {
            var actual = target.GetAllSavingsAccounts();
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(Saving));
        }

        [TestMethod]
        public void AllCurrentAccount_OnSucess_ShouldGetOnlyCurrentAccounts()
        {
            var actual = target.GetAllCurrentAccounts();
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(Current));
        }

        [TestMethod]
        public void AllActiveAccounts_OnSucess_ShouldGetOnlyActiveAccounts()
        {
            var actual = target.GetAllActiveAccounts();
            CollectionAssert.Contains(actual, target.accounts[0]);
        }

        [TestMethod]
        public void AccountsHavingBalance_OnSucess_ShouldGetAccountsHavingBalance()
        {
            var actual = target.GetAllAccountsHavingBalance();
            Assert.AreNotEqual(actual[0].Balance, 0);
        }

    }
}
