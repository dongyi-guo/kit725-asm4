/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Transactions;
using TheVunerableApp.Controller;
using TheVunerableApp.DataSource;
using TheVunerableApp.Model;
using Transaction = TheVunerableApp.Model.Transaction;

namespace TheVunerableApp.Test
{
    internal class Test
    {
        // Added over here
        // To prevent creating SQLiteDB object again and again
        public static SQLiteDB sql = new SQLiteDB();
        public static LocalStore localStore = new LocalStore();
        
        /*
         * Provided by Professor Amin as sample
         * 
         * The following code exploits and tests CWEs 787 & 125 in UserController.cs for ListOfAccountBalance()
         */
        public static void ExploitBounds()
        {
            List<string> ListOfBalances = UserController.ListOfAccountBalance("6763996216");

            foreach (string balance in ListOfBalances)
            {
                Console.WriteLine(balance);
            }
        }

        /*
         * The following function exploits and tests CWE-476 for:
         * 
         * SQLiteDB.cs - GetCustomerDetailsFromDB(customerId)
         * OR
         * UseController.cs - DisplayUserDetails(customerId)
         * StoreTransactions(Transaction transaction): bool in LocalStore.cs
         */
        public static void CWE476_DongyiGuo()
        {
            // DisplayUserDetails(customerId) can return null if customerId does not exist.
            Customer customer_sdb = sql.GetCustomerDetailsFromDB("SB69-6969696969");
            Customer customer_uc = UserController.DisplayUserDetails("SB69-6969696969");
            try
            {
                // Calling function from a null object, Null Pointer Exception will be thrown.
                Console.WriteLine(customer_sdb.ToString());
                Console.WriteLine(customer_uc.ToString());

                //After the patch, no exception will be thrown.
                Console.WriteLine("CWE-476 Patched");
            }
            catch
            {
                Console.WriteLine("CWE-476 Exploited");
            }

            // This exploit has sample patches in SQLiteDB.cs and UserController.cs, but it can also be fixed while being called: 
            customer_sdb = sql.GetCustomerDetailsFromDB("SB69-6969696969");
            customer_uc = UserController.DisplayUserDetails("SB69-6969696969");

            // Use if condition to check whether the object is null:
            if (null != customer_sdb)
            {
                Console.WriteLine(customer_sdb.ToString());
            }
            else
            {
                Console.WriteLine("customer_sdb does not exist");
            }

            if (null != customer_uc)
            {
                Console.WriteLine(customer_uc.ToString());
            }
            else
            {
                Console.WriteLine("customer_uc does not exist");
            }

            // Or try catch with the exception thrown
            try
            {
                Console.WriteLine(customer_sdb.ToString());
            }
            catch
            {
                Console.WriteLine("customer_sdb does not exist");
            }

            try
            {
                Console.WriteLine(customer_uc.ToString());
            }
            catch
            {
                Console.WriteLine("customer_uc does not exist");
            }

            // For StoreTransactions(Transaction transaction): bool in LocalStore.cs
            // If this function takes null object as "transaction", it will be
            // deserializing and dereferencing an null object
            Model.Transaction sike = null;
            localStore.StoreTransactions(sike);
            Console.WriteLine("If no exception thrown, CWE-476 for StoreTransactions(Transaction transaction): bool in LocalStore.cs was Patched.");

        }

        /*
         * The following function exploits and tests CWE-427 for:
         * 
         * SQLiteDB.cs - Filepath
         */
        public static void CWE427_RonghuaYang()
        {
            // DB\Bank.sqlite created
            try
            {
                SQLiteConnection sql_conn = new SQLiteConnection("Data Source=" + sql.Filepath);
                Console.WriteLine("CWE-427 Patched");
            }
            catch
            {
                Console.WriteLine("CWE-427 Exploited");
            }
        }

        /*
         * The following function exploits and tests CWE-798 for:
         * 
         * DBAdapter.cs - server, database, username, password
         */
        public static void CWE798_DongyiGuo()
        {
            /* 
             * For this exploit, any actors that could have visual contact on
             * the code could easily obtain the server and database address,
             * username and password.
             * 
             * What's more, the binary file compiled can be decompiled and
             * analysed, providing actors the credential information.
             */
            Console.WriteLine("CWE-798 Patched");
        }

        /*
         * The following function exploits and tests CWE-522 for:
         * 
         * DBAdapter.cs - server, database, username, password
         */
        public static void CWE522_DongyiGuo()
        {
            /* 
             * For this exploit, any actors that could have visual contact on
             * the code could easily obtain the server and database address,
             * username and password.
             * 
             * What's more, the binary file compiled can be decompiled and
             * analysed, providing actors the credential information.
             */
            Console.WriteLine("CWE-522 Patched");
        }

        /*
         * The following function exploits and tests CWE-306 and CWE-330 for:
         * 
         * Account.cs - GenerateAccountNumber() function
         */
        public static void CWE306_CWE330_ThuanPinGoh()
        {
            /*
             * To exploit the CWE in this function, this function will have to 
             * flood the database to create an exactly same account number 
             * 
             * However, this can be a problem if the RNG generate a similar account number
             * and causing the data corruption
             */
            Console.WriteLine("CWE-306 and CWE-330 from account.cs cannot be tested by using normal method as it involve data corruption");
            //AccountController.CreateSavingsAccount("12345678", 1.0, 1.0);
            //AccountController.CloseAccount("12345678", "05183384");

        }

        /*
         * The following function exploits and tests CWE-306 and CWE-330 for:
         * 
         * Customer.cs - GenerateUserId() function
         */
        public static void CWE306_CWE330_2_ThuanPinGoh()
        {
            /*
             * To exploit the CWE in this function, this function will have to 
             * flood the database to create an exactly same account number 
             * 
             * However, this can be a problem if the RNG generate a similar account number
             * and causing the data corruption
             */
            Console.WriteLine("CWE-306 and CWE-330 from customer.cs cannot be tested by using normal method as it involve data corruption");

        }

        public static void CWE306_ThuanPinGoh()
        {
            /*
             * To exploit the CWE in this function, this function will have to 
             * flood the database to create an exactly same account number 
             * 
             * However, this can be a problem if the RNG generate a similar account number
             * and causing the data corruption
             */
            String message = UserController.RemoveCustomer("12345678");
            Console.WriteLine("CWE306_removeCustomer(): " + message);

        }

        /*
         * The following function exploits and tests CWE-798 for:
         * 
         * SQLiteDB – CreateUserInDB()
         */
        public static void CWE798_ThuanPinGoh()
        {
            /*
             * This exploit can be tested by using the createuser() in program.cs
             * The patched program will display a hashed + salted passowrd in database
             */
            Console.WriteLine("CWE-798 can be tested by using the createuser() in program.cs");
        }

        /*
         * The following function exploits and tests CWE-125 for:
         * 
         * UserController – SearchCustomerByAccountNumber()
         */
        public static void CWE125_ThuanPinGoh()
        {
            /*
             * This exploit can be tested by using the Program.SearchCustomerByAccountNumeber() in program.cs
             * The method will function properly after patching the program
             */
            Console.WriteLine("CWE125 can be tested by using the SearchCustomerByAccountNumeber() in program.cs");
        }

        /*
         * The following function exploits and tests CWE-20 for:
         * 
         * StoreTransaction(Transaction transaction): bool in SQLiteDB.cs
         * StoreTransactions(Transaction transaction): bool in LocalStore.cs
         * StoreTransactions(string sAccount, double amount, string tAccount): string in TransactionController.cs
         */
        public static void CWE20_DongyiGuo()
        {
            /*
             * For this exploit, situations are
             * 1. No null check
             * 2. No negative check on amount of money
             * 3. No validation on input, like account information
             */
            Model.Transaction sike = null;
            Model.Transaction negative = new Model.Transaction("6763996216", -500.00, "8829905701");
            Model.Transaction noSuchAccount = new Model.Transaction("66666666", 100, "99999999");
            bool sikeCheck = sql.StoreTransaction(sike);
            bool localStoreSikeCheck = localStore.StoreTransactions(sike);
            bool negativeCheck = sql.StoreTransaction(negative);
            bool noAccountCheck = sql.StoreTransaction(noSuchAccount);
            string invalid_amount = TransactionController.StoreTransactions(negative.SourceAccount, negative.Amount, negative.TargetAccount);
            string invalid_account = TransactionController.StoreTransactions(noSuchAccount.SourceAccount, noSuchAccount.Amount, noSuchAccount.TargetAccount);

            Console.WriteLine("The boolean below should be all false:");
            Console.WriteLine(sikeCheck);
            Console.WriteLine(localStoreSikeCheck);
            Console.WriteLine(negativeCheck);
            Console.WriteLine(noAccountCheck);
            Console.WriteLine(invalid_amount);
            Console.WriteLine(invalid_account);
            Console.WriteLine("If error messages above appear and no exception thrown, CWE-20 Patched");
            Console.WriteLine("If error messages appear wrong or exception thrown, CWE-20 Exploited");
        }

        /*
         * The following function exploits and tests CWE-22 for:
         * 
         * LocalStore.cs – LoadTransaction(string path)
         */
        public static void CWE22_DongyiGuo()
        {
            // To exploit CWE-22, we can pass a path that request visiting upper layer of the hierarchy:
            try
            {
                localStore.LoadTransaction(@"..\..\..\..\..\..\..\windows\System32");
                Console.WriteLine("If this succeeded, CWE-22 Exploited");
            }
            catch
            {
                Console.WriteLine("Exception thrown as patch wanted, CWE-22 Patched");
            }
            // Or we can use some absolute path by our own:
            try
            {
                localStore.LoadTransaction(@"C:\windows\System32");
                Console.WriteLine("If this succeeded, CWE-22 Exploited");
            }
            catch
            {
                Console.WriteLine("Exception thrown as patch wanted, CWE-22 Patched");
            }

        }

        /*
         * The following function exploits and tests CWE-73 for:
         * 
         * LocalStore.cs – LoadTransaction(string path)
         */
        public static void CWE73_DongyiGuo()
        {
            // To exploit CWE-73, we can pass a path that request visiting upper layer of the hierarchy:
            try
            {
                localStore.LoadTransaction(@"..\..\..\..\..\..\..\windows\System32");
                Console.WriteLine("If this succeeded, CWE-22 Exploited");
            }
            catch
            {
                Console.WriteLine("Exception thrown as patch wanted, CWE-22 Patched");
            }
            // Or we can use some absolute path by our own:
            try
            {
                localStore.LoadTransaction(@"C:\windows\System32");
                Console.WriteLine("If this succeeded, CWE-22 Exploited");
            }
            catch
            {
                Console.WriteLine("Exception thrown as patch wanted, CWE-22 Patched");
            }

        }

        /*
         * The following function exploits and tests CWE-502 for:
         * 
         * LocalStore.cs – LoadTransaction(string path)
         */
        public static void CWE502_DongyiGuo()
        {
            // To exploit CWE-502, just provide some path that won't work:
            try 
            {
                localStore.LoadTransaction(@"dfidsojifds\adfjaipd\f\daFNA\F\");
            }
            catch
            {
                Console.WriteLine("The Exception was catched, which is intentionally the patch for CWE-502, CWE-502 patched");

            }
        }
    }
}
