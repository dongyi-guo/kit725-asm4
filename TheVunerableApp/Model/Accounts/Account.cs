/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheVunerableApp.DataSource;

namespace TheVunerableApp.Model
{
    public class Account
    {
        private static int maxAccountNumberLength = 8;
        public string AccountNumber { get; }
        public double Balance { get;  set; }

        public List<String> customers { get; private set; }

        public Account(double initialBalance, string customerId)
        {
            AccountNumber = GenerateAccountNumber(); // Whenever an Account is created, Account number is auto generated
            Balance = initialBalance;
            customers = new List<String>();
            customers.Add(customerId);
        }

        public void AddCustomer(string customerId)
        {
            customers.Add(customerId);
        }

        /*
         * One vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-306 (potentially CWE-307, CWE-330)
         * 19/10/2023 - Identified by Thuan Pin Goh
         * 19/10/2023 - Exploited by Thuan Pin Goh
         * 19/10/2023 - Patched by Thuan Pin Goh
         */
        private string GenerateAccountNumber()
        {
            Random random = new Random();
            string accountNumber = "";
            //Code with Vulnerabilities.
            /*
            for (int i = 0; i < maxAccountNumberLength; i++)
            {
                accountNumber += random.Next(10).ToString();
            }
            */
            // Weakness Patched
            SQLiteDB db = new SQLiteDB();
            do
            {
                for (int i = 0; i < maxAccountNumberLength; i++)
                {
                   accountNumber += random.Next(10).ToString();
                }
           
             } while (!db.AccountNumberExists(accountNumber)); //this function available in SQLiteDB
            
            return accountNumber;
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Deposited {amount} to Account {AccountNumber}. New Balance: {Balance}");
            }
        }

        public void Withdraw(double amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn {amount} from Account {AccountNumber}. New Balance: {Balance}");
            }
            else
            {
                Console.WriteLine($"Insufficient balance in Account {AccountNumber}.");
            }
        }
    }
}
