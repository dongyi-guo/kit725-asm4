﻿/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheVunerableApp.Model;
using TheVunerableApp.View;
using System.Security.Cryptography;
using System.Threading;

namespace TheVunerableApp.DataSource
{
    /*
     * For this class, every function that talks to the database could face a Race Condition while
     * multi-threading is applied, hence violating CWE362.
     */
    internal class SQLiteDB:DBAdapter
    {
        /*
         * One vulnerability identified in this variable
         * 
         * 1.
         * Identified as CWE-276
         * 18/10/2023 - Identified by Dongyi Guo
         * 18/10/2023 - Exploited by Dongyi Guo
         * 18/10/2023 - Patched by Dongyi Guo
         */
        public string ConnectionString = "Data Source=VulApp.db";

        // CWE-276 Dummy Patch - Weakness resolved
        // private string ConnectionString = "Data Source=VulApp.db";

        // To avoid Race condition, Mutex is introduced
        private static Mutex mutex = new Mutex();

        /*
         * Two vulnerabilities identified in this variable
         * 
         * 1.
         * Identified as CWE-427
         * 18/10/2023 - Identified by Ronghua Yang
         * 18/10/2023 - Exploited by Ronghua Yang
         * 18/10/2023 - Patched by Ronghua Yang
         * 2.
         * Identified as CWE-276
         * 18/10/2023 - Identified by Dongyi Guo
         * 18/10/2023 - Exploited by Dongyi Guo
         * 18/10/2023 - Patched by Dongyi Guo
         */

        // Code with vulnerability
        // This absolute path only works on certain machine as username of current user is part of it.
        // public string Filepath = @"C:\Users\mbamin\source\repos\TheVunerableApp\TheVunerableApp\DB\Bank.sqlite";

        // CWE-427: Weakness resolved - use relative path
        public string Filepath = @"DB\Bank.sqlite";

        // CWE-276 Dummy Patch - Weakness resolved
        // private string Filepath = @"DB\Bank.sqlite";

        /*
         * One vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-362
         * 18/10/2023 - Identified by Dongyi Guo
         * 18/10/2023 - Exploited by Dongyi Guo
         * 18/10/2023 - Patched by Dongyi Guo
         */
        public bool ConnectToDS()
        {
            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    conn.Close();
                    
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return true;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public double GetBalance(string accountNumber)
        {
            double balance = 0;
            string accountQuery = "SELECT Balance FROM AccountDetails WHERE AccountNumber = @accountNumber";
            // CWE-362 patch: To prevent race condition:
            try
            {
                mutex.WaitOne();
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(accountQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountNumber", accountNumber);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                balance = Double.Parse(reader["Balance"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return balance;
        }

        /*
         * One vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-476
         * 18/10/2023 - Identified by Dongyi Guo
         * 18/10/2023 - Exploited by Dongyi Guo
         * 18/10/2023 - Patched by Dongyi Guo
         *
         * 
         * 2.
         * Identified as CWE-362
         * 18/10/2023 - Identified by Dongyi Guo
         * 18/10/2023 - Exploited by Dongyi Guo
         * 18/10/2023 - Patched by Dongyi Guo
         */
        public Customer GetCustomerDetailsFromDB(string customerId)
        {
            string customerQuery = "SELECT Name, SirName, Email, GovId FROM User WHERE UserId = @userId";

            // If customer object not fetched from database, default customer value null will be returned.
            // Code with vulnerability
            // Customer customer = null;

            // Weakness Resolved
            Customer customer = new Customer("", "", "", "", "");

            try 
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                // This exploit can also be fixed in upper level class (UserController.cs).

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(customerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", customerId);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer = new Customer(reader["GovId"].ToString(), reader["Name"].ToString(),
                                    reader["SirName"].ToString(), reader["Email"].ToString(), "*");
                            }
                        }
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return customer;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public List<string> GetCustomerIdFromDB(string accountNo)
        {
            string customerQuery = "SELECT CustomerId FROM Account WHERE AccountNumber = @accountNo";
            List<string> customerIds;

            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(customerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountNo", accountNo);
                        customerIds = new List<string>();
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customerIds.Add(reader["CustomerId"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return customerIds;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public List<string> GetAllAccountsFromDB(string customerId)
        {
            string customerQuery = "SELECT AccountNumber FROM Account WHERE CustomerId = @customerId";
            List<string> accounts = new List<string>();

            try 
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(customerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(reader["AccountNumber"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return accounts;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public bool AddCustomerToAnAccount(string accountNumber,  string customerId)
        {
            string accountTypeQuery = "SELECT AccountType FROM Account WHERE AccountNumber = @accountNumber";
            string accountQuery = "INSERT INTO Account (AccountNumber, AccountType, CustomerId) VALUES (@accountNumber, @accountType, @customerId)";
            int rows = 0;
            string accountType = "";
            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(accountTypeQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountNumber", accountNumber);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accountType = reader["AccountType"].ToString();
                            }
                        }
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand(accountQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                        cmd.Parameters.AddWithValue("@accountType", accountType);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        rows = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return true;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public int UpdateCustomerDetailsInDB(string customerId, string name, string sName, string email, string govId )
        {
            string customerQuery = "UPDATE User SET Name = @name, SirName = @sName, Email = @email, GovId = @govId WHERE UserId = @userId";
            int rows = 0;

            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(customerQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", customerId);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@sName", sName);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@govId", govId);

                        rows = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return rows;
        }

        /*
         * This method is created to solve the CWE-306, CWE-307 in Customer.cs
         * To validate if there is any existing account having the same account number
         */
        public bool UserIDExists(string userID)
        {
            string accountQuery = "SELECT COUNT(*) FROM User WHERE UserId = @UserID";
            int count = 0;

            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand command = new SQLiteCommand(accountQuery, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            // If count is 0, the userID is unique; otherwise, it's not
            if (count == 0) return true; //no user has this ID yet
            return false; //existing user
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public bool CreateAccountInDB(Account accountObj, int flag)
        {
            string accountQuery = "INSERT INTO Account (AccountNumber, AccountType, CustomerId) VALUES (@accountNumber, @accountType, @customerId)";
            string accountDetailsQuery = "INSERT INTO AccountDetails (AccountNumber, Balance, MinBalance, MonthlyFee, InterestRate) VALUES (@accountNumber, @balance, @minBalance, @monthlyFee, @interestRate)";

            if (flag == 1) // Savings Account
            {
                Savings request = (Savings)accountObj;

                try
                {
                    // CWE-362 patch: To prevent race condition:
                    mutex.WaitOne();

                    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                    {
                        conn.Open();
                        int rows = 0;


                        using (SQLiteCommand cmd = new SQLiteCommand(accountQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@accountNumber", request.AccountNumber);
                            cmd.Parameters.AddWithValue("@accountType", "Savings");
                            cmd.Parameters.AddWithValue("@customerId", request.customers[0].ToString());
                            rows = cmd.ExecuteNonQuery();
                        }

                        using (SQLiteCommand cmd = new SQLiteCommand(accountDetailsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@accountNumber", request.AccountNumber);
                            cmd.Parameters.AddWithValue("@balance", request.Balance);
                            cmd.Parameters.AddWithValue("@minBalance", 0);
                            cmd.Parameters.AddWithValue("@monthlyFee", 0);
                            cmd.Parameters.AddWithValue("@interestRate", request.InterestRate);

                            rows = cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else if (flag == 0)
            {
                Current request = (Current)accountObj;

                try
                {
                    mutex.WaitOne();

                    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                    {
                        conn.Open();
                        int rows = 0;

                        using (SQLiteCommand cmd = new SQLiteCommand(accountQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@accountNumber", request.AccountNumber);
                            cmd.Parameters.AddWithValue("@accountType", "Current");
                            cmd.Parameters.AddWithValue("@customerId", request.customers[0].ToString());
                            rows = cmd.ExecuteNonQuery();
                        }

                        using (SQLiteCommand cmd = new SQLiteCommand(accountDetailsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@accountNumber", request.AccountNumber);
                            cmd.Parameters.AddWithValue("@balance", request.Balance);
                            cmd.Parameters.AddWithValue("@minBalance", request.MinimumBalance);
                            cmd.Parameters.AddWithValue("@monthlyFee", request.MonthlyFee);
                            cmd.Parameters.AddWithValue("@interestRate", 0);

                            rows = cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                Console.WriteLine("Unknown Account Type");
                System.Diagnostics.Debug.Assert(false, "Unknown Account Type");
            }
            return true;
        }

        /*
         * This method is created to solve the CWE-306, CWE-307 in Account.cs
         * To validate if there is any existing account having the same account number
         */
        public bool AccountNumberExists(string accountNumber)
        {
            string accountQuery = "SELECT COUNT(*) FROM Account WHERE AccountNumber = @AccountNumber";
            int count = 0;

            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand command = new SQLiteCommand(accountQuery, conn))
                    {
                        command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }

                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            // If count is 0, the account number is unique; otherwise, it's not
            if (count == 0)
            {
                return true;//no exisitng account number
            }
            else
            {
                return false;//existing account number
            }
        }

        /*
         * Two vulnerabilities identified in this method
         * 
         * 1.
         * Identified as CWE-798
         * 19/10/2023 - Identified by Thuan Pin Goh
         * 19/10/2023 - Exploited by Thuan Pin Goh
         * 19/10/2023 - Patched by Thuan Pin Goh
         * 2.
         * Identified as CWE-362
         * 18/10/2023 - Identified by Dongyi Guo
         * 18/10/2023 - Exploited by Dongyi Guo
         * 18/10/2023 - Patched by Dongyi Guo
         */
        public bool CreateUserInDB(User requestObj, int flag)
        {
            string userQuery = "INSERT INTO User (UserId, Name, SirName, Email, GovId) VALUES (@id, @name, @sName, @email, @govId)";
            string authQuery = "INSERT INTO Auth (UserId, Password, Role) VALUES (@id, @password, @role)";

            

            

            if (flag == 1) // true means create an Admin user
            {
                Admin request = (Admin)requestObj;

                string adminQuery = "INSERT INTO Admin (AdminId, StartDate, Position, BranchId, BranchName) VALUES (@AdminId, @StartDate, @Position, @BranchId, @BranchName)";

                try
                {
                    // CWE-362 patch: To prevent race condition:
                    mutex.WaitOne();

                    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                    {
                        conn.Open();
                        int rows = 0;

                        using (SQLiteCommand cmd = new SQLiteCommand(userQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", request.AdminId);
                            cmd.Parameters.AddWithValue("@name", request.Name);
                            cmd.Parameters.AddWithValue("@sName", request.SirName);
                            cmd.Parameters.AddWithValue("@email", request.Email);
                            cmd.Parameters.AddWithValue("@govId", request.GovId);
                            rows = cmd.ExecuteNonQuery();
                        }

                        using (SQLiteCommand cmd = new SQLiteCommand(authQuery, conn))
                        {

                            byte[] salt = new byte[16]; // Generate a random salt for each user
                            new RNGCryptoServiceProvider().GetBytes(salt);

                            int iterations = 10000;

                            string hashedPassword = Convert.ToBase64String(new Rfc2898DeriveBytes(request.Password, salt, iterations).GetBytes(32));

                            cmd.Parameters.AddWithValue("@id", request.AdminId);

                            // Original Code with Vulnerabilities.
                            // cmd.Parameters.AddWithValue("@password", request.Password);

                            // Weakness Patched
                            cmd.Parameters.AddWithValue("@password", hashedPassword);
                            cmd.Parameters.AddWithValue("@role", "Admin");
                            rows = cmd.ExecuteNonQuery();
                        }

                        using (SQLiteCommand cmd = new SQLiteCommand(adminQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@AdminId", request.AdminId);
                            cmd.Parameters.AddWithValue("@StartDate", request.StartDate.ToString());
                            cmd.Parameters.AddWithValue("@Position", request.Position.ToString());
                            cmd.Parameters.AddWithValue("@BranchId", request.BranchId);
                            cmd.Parameters.AddWithValue("@BranchName", request.BranchName);

                            rows = cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else if (flag == 0)
            {
                Customer request = (Customer)requestObj;
                try
                {
                    // CWE-362 patch: To prevent race condition:
                    mutex.WaitOne();

                    using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                    {
                        conn.Open();
                        int rows = 0;

                        using (SQLiteCommand cmd = new SQLiteCommand(userQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", request.CustomerId);
                            cmd.Parameters.AddWithValue("@name", request.Name);
                            cmd.Parameters.AddWithValue("@sName", request.SirName);
                            cmd.Parameters.AddWithValue("@email", request.Email);
                            cmd.Parameters.AddWithValue("@govId", request.GovId);
                            rows = cmd.ExecuteNonQuery();
                        }

                        using (SQLiteCommand cmd = new SQLiteCommand(authQuery, conn))
                        {

                            byte[] salt = new byte[16]; // Generate a random salt for each user
                            new RNGCryptoServiceProvider().GetBytes(salt);

                            int iterations = 10000;

                            string hashedPassword = Convert.ToBase64String(new Rfc2898DeriveBytes(request.Password, salt, iterations).GetBytes(32));

                            cmd.Parameters.AddWithValue("@id", request.CustomerId);

                            // Code with Vulnerabilities.
                            // cmd.Parameters.AddWithValue("@password", request.Password);

                            // Weakness Patched
                            cmd.Parameters.AddWithValue("@password", hashedPassword);

                            cmd.Parameters.AddWithValue("@role", "none");
                            rows = cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                Console.WriteLine("Unknow user type");
                System.Diagnostics.Debug.Assert(false, "Unknown User Type");
            }
            return true;
        }

        /*
         *  One vulnerability identified in this method
         *  
         *  1. 
         *  Identified as CWE-20
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         *  2.
         *  Identified as CWE-362
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched by Dongyi Guo
         */

        // First, there is no null check.
        // Second, the transaction could have: negative amount of money, or invalid account inputs
        public bool StoreTransaction(Transaction transaction)
        {
            
            

            // Start of Patch
            // Added null check
            if (null == transaction) return false;

            // Added transaction sanity check
            if (transaction.Amount < 0) return false;

            // Check if the account existed in the database before for sourceAccount
            List<string> res = new List<string>();
            string vQuery = "SELECT * FROM Account WHERE AccountNumber = @account";
            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(vQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@account", transaction.SourceAccount);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Add(reader["AccountNumber"].ToString());
                            }
                        }
                    }
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            // Check SourceAccount exist, if not return false
            if (0 == res.Count) return false;
            // Do the same to TargetAccount
            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();
                
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(vQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@account", transaction.TargetAccount);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                res.Add(reader["AccountNumber"].ToString());
                            }
                        }
                    }
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            
            // Check TargetAccount exist, if not return false
            if (0 == res.Count) return false;
            //End of Patch

            string tQuery = "INSERT INTO TRecord (Id, Source, Target) VALUES (@id, @sAccountNumber, @tAccountNumber)";

            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    int rows = 0;

                    using (SQLiteCommand cmd = new SQLiteCommand(tQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", transaction.TransactionId);
                        cmd.Parameters.AddWithValue("@sAccountNumber", transaction.SourceAccount);
                        cmd.Parameters.AddWithValue("@tAccountNumber", transaction.TargetAccount);

                        rows = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return true;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public bool CloseAccount(string accountNumber)
        {
            string accountQuery = "DELETE FROM Account WHERE AccountNumber = @accountNumber";
            string accountDetailsQuery = "DELETE FROM AccountDetails WHERE AccountNumber = @accountNumber";
            int rows = 0;
            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(accountQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                        rows = cmd.ExecuteNonQuery();

                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(accountDetailsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                        rows = cmd.ExecuteNonQuery();

                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return true;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public Customer RemoveUser(string customerId) 
        {
            string accountQuery = "DELETE FROM Account WHERE CustomerId = @customerId";
            string userQuery = "DELETE FROM User WHERE UserId = @@customerId";
            int rows = 0;

            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();

                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(accountQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        rows = cmd.ExecuteNonQuery();

                    }

                    using (SQLiteCommand cmd = new SQLiteCommand(userQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        rows = cmd.ExecuteNonQuery();

                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            
            return null;
        }

        /*
        * One vulnerability identified in this method
        * 
        * 1.
        * Identified as CWE-362
        * 18/10/2023 - Identified by Dongyi Guo
        * 18/10/2023 - Exploited by Dongyi Guo
        * 18/10/2023 - Patched by Dongyi Guo
        */
        public bool getAuthForTest()
        {
            string query = "Select * FROM AUTH";
            try
            {
                // CWE-362 patch: To prevent race condition:
                mutex.WaitOne();
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader.GetString(0));
                            }
                        }
                    }
                    conn.Close();
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            
            return false; 
        }
    }
}
