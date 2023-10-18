/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/


using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheVunerableApp.DataSource;
using TheVunerableApp.Model;

namespace TheVunerableApp.Controller
{
    public static class AccountController
    {
        public static Account CreateSavingsAccount(String customerId, double interestRate, double balance)
        {
            Savings savings = new Savings(balance, interestRate, customerId);
            SQLiteDB db = new SQLiteDB();
            db.CreateAccountInDB(savings, 1);
            return null;
        }

        public static Account CreateCurrentAccount(string customerId, double initBalance, double minBalance, double fee)
        {
            Current current = new Current(initBalance,minBalance,fee,customerId);
            SQLiteDB db = new SQLiteDB();
            db.CreateAccountInDB(current, 0);
            return null;
        }

        public static string CloseAccount(string customerId, string accountNumber) 
        { 
            SQLiteDB db = new SQLiteDB();
            db.CloseAccount(accountNumber);
            accountNumber = null; 
            return accountNumber; 
        }
        public static List<string> SearchAccountByCustomer(string customerId) 
        {
            SQLiteDB db = new SQLiteDB();
           return db.GetAllAccountsFromDB(customerId);
        }
        public static void AddCustomerToAnAccount(string accountNumber, string customerId) 
        { 
            SQLiteDB db = new SQLiteDB();
            db.AddCustomerToAnAccount(accountNumber, customerId);
        }

        public static double GetAccountBalance(string accountNumber)
        {
            SQLiteDB dB = new SQLiteDB();
            return dB.GetBalance(accountNumber);
        }
    }
}
