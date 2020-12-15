using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingAppLibrary
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public int Pin { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }

    public class Saving : Account
    {
        public string Gender { get; set; }
    }

    public class Current : Account
    {
        public string CompanyName { get; set; }
    }

    public class AccountManager
    {
        public List<Account> accounts = new List<Account>
        {
            new Saving{Name="acc1", Balance = 0 ,IsActive=true},
            new Saving{Name="acc2", Balance = 13000 ,IsActive=true},
            new Saving{Name="acc2", Balance = 0 ,IsActive=true},
            new Current{Name="acc3", Balance = 0 ,IsActive=false},
            new Saving{Name="acc4", Balance = 1000 ,IsActive=false},
        };

        public List<Saving> GetAllSavingsAccounts()
        {
            //List<Account> savingAccount = new List<Account>();
            //foreach(var item in accounts)
            //{
            //    if(item is Saving)
            //    {
            //        savingAccount.Add(item);
            //    }
            //}
            //return savingAccount;

            return accounts.OfType<Saving>().ToList();
        }
        public List<Current> GetAllCurrentAccounts()
        {
            /*
            List<Account> currentAccount = new List<Account>();
            foreach (var item in accounts)
            {
                if (item is Current )
                {
                    currentAccount.Add(item);
                }
            }

            return currentAccount;*/
            return accounts.OfType<Current>().ToList();
        }

        public List<Account> GetAllActiveAccounts()
        {
            /*List<Account> activeAccount = new List<Account>();
            foreach (var item in accounts)
            {
                if (item.IsActive == true )
                {
                    activeAccount.Add(item);
                }
            }
            return activeAccount;*/
            return accounts.Where(a => a.IsActive = true).ToList();
        }

        public List<Account> GetAllAccountsHavingBalance()
        {
            /*List<Account> balanceAccount = new List<Account>();
            foreach (var item in accounts)
            {
                if (item.Balance>=0)
                {
                    balanceAccount.Add(item);
                }
            }
            return balanceAccount;*/
            return accounts.Where(a => a.Balance > 0).ToList();


        }
        public Account Open(Account account, string accType)
        {

            if (account.IsActive == true)
            {
                throw new ExistingAccountException("Account Already Exists!");
            }
            if (String.IsNullOrEmpty(account.Name))
            {
                throw new InvalidAccountTypeException("Invalid Account Type");
            }
            if (account.Balance <= 0)
            {
                throw new InvalidAmountException("Invalid Amount");
            }
            if (account.Pin <= 999 && account.Pin >= 10000)
            {
                throw new InvalidPinException("Invalid Pin Exception");
            }

            /*if(accType.ToLower().Equals("Savings".ToLower()))
            {
                account = new Saving();
                ((Saving)account).Gender = ((Saving)account).Gender;
            }*/
            /*else
            {
                account = new Current();
                ((Current)account).CompanyName = CompanyName;
            }*/
            account.OpeningDate = DateTime.Now.Date;
            account.IsActive = true;
            return account;
        }

        public bool Close(Account ExistingAccount)
        {
            bool IsClosed = false;

            if (!ExistingAccount.IsActive)
                throw new AccountAlreadyClosedException();

            if (ExistingAccount.Balance > 0)
                throw new AccountHaveBalanceException();

            ExistingAccount.IsActive = false;
            ExistingAccount.ClosingDate = DateTime.Now.Date;
            IsClosed = true;

            return IsClosed;
        }

        public void Withdraw(Account ExistingAccount, int pin, int withdrawamount)
        {
            if (ExistingAccount.IsActive == false) throw new AccountNotFoundException();
            if (ExistingAccount.Pin != pin) throw new InvalidPinException();
            if (withdrawamount <= 0) throw new InvalidAmountEnterdException();
            if (ExistingAccount.Balance - withdrawamount < 0)
                throw new InsufficientBalanceException();
            ExistingAccount.Balance -= withdrawamount;
        }

        public void Transfer(Account a1, Account a2, int pin, int transferAmount)
        {
            if (a1.IsActive == false) throw new AccountNotFoundException();
            if (a2.IsActive == false) throw new AccountNotFoundException();
            if (pin != a1.Pin) throw new PinNotMatchedException();
            if (transferAmount <= 0) throw new InvalidAmountEnterdException();
            if (a1.Balance - transferAmount < 0)
                throw new InsufficientBalanceException();
            a1.Balance -= transferAmount;
            a2.Balance += transferAmount;

        }
        public void Deposit(Account account, int pin, int DepositAmount)
        {
            if (account.IsActive == false) throw new AccountNotFoundException();
            if (pin != account.Pin) throw new PinNotMatchedException();
            if (DepositAmount <= 0) throw new InvalidAmountEnterdException();
            account.Balance += DepositAmount;
        }

    }

    public class InvalidAccountTypeException : ApplicationException
    {
        public InvalidAccountTypeException(string message = null, Exception inner = null) : base(message, inner)
        {

        }
    }
    public class InvalidAmountException : ApplicationException
    {
        public InvalidAmountException(string message = null, Exception inner = null) : base(message, inner)
        {

        }
    }
    public class InvalidPinException : ApplicationException
    {
        public InvalidPinException(string message = null, Exception inner = null) : base(message, inner)
        {

        }
    }
    public class ExistingAccountException : ApplicationException
    {
        public ExistingAccountException(string message = null, Exception inner = null) : base(message, inner)
        {

        }
    }
    public class AccountAlreadyClosedException : ApplicationException
    {
        public AccountAlreadyClosedException(string msg = null, Exception inner = null) : base(msg, inner)
        {

        }
    }
    public class AccountHaveBalanceException : ApplicationException
    {
        public AccountHaveBalanceException(string msg = null, Exception inner = null) : base(msg, inner)
        {

        }
    }
    public class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException(string msg = null, Exception inner = null) : base(msg, inner)
        {

        }
    }
    public class InvalidAmountEnterdException : ApplicationException
    {
        public InvalidAmountEnterdException(string msg = null, Exception inner = null) : base(msg, inner)
        {

        }
    }
    public class PinNotMatchedException : ApplicationException
    {
        public PinNotMatchedException(string msg = null, Exception inner = null) : base(msg, inner)
        {

        }
    }
    public class AccountNotFoundException : ApplicationException
    {
        public AccountNotFoundException(string msg = null, Exception inner = null) : base(msg, inner)
        {

        }
    }
    public class InactiveWithBalance : ApplicationException
    { 
        public InactiveWithBalance(string msg = null, Exception inner = null): base(msg, inner)
        {

        }
    }

}




