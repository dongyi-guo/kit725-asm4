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
using TheVunerableApp.DataSource;
using TheVunerableApp.Model;

namespace TheVunerableApp.Controller
{
    public static class TransactionController
    {
        public static string getTRPath()
        {
            LocalStore store = new LocalStore();
            return store.FilePath;
        }

        /*
         *  One vulnerability identified in this method
         *  
         *  1. 
         *  Identified as CWE-20
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         */
        // This function does not verify the amount in this transaction is less than 0 or not. When the amount of money in a transaction is negative, it either does not make sense, or the transaction should be recorded with reversed starting account and targeting account.
        // What's more, this function does not care whether the accounts passed in as parameter exist or not. The lower layer implementation of this function can throw exception because of this.
        public static string StoreTransactions(string sAccount, double amount, string tAccount)
        {
            // Added negative amount detection
            if (0 > amount)
            {
                return "Invalid amount of money";
            }

            LocalStore store = new LocalStore();
            Transaction transaction = new Transaction(sAccount, amount, tAccount);

            // Code with issues
            //store.StoreTransactions(transaction);

            //SQLiteDB db = new SQLiteDB();
            //db.StoreTransaction(transaction);

            // We pass the transaction to DB function first, as if there is no such account on sAccount or tAccount, the newly patched StoreTransaction(transaction) in SQLiteDB.cs will return false
            SQLiteDB db = new SQLiteDB();
            Boolean isSucceeded = db.StoreTransaction(transaction);

            if (isSucceeded)
            {
                return transaction.TransactionId;
            }
            else
            {
                return "Account doesn't exist";
            }
        }

        public static void LoadTransaction(string path)
        {
            LocalStore store = new LocalStore();
        }
    }
}
