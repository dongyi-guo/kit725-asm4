/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
        private string GenerateAccountNumber()
        {
            Random random = new Random();
            string accountNumber = "";

            for (int i = 0; i < maxAccountNumberLength; i++)
            {
                accountNumber += random.Next(10).ToString();
            }

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
