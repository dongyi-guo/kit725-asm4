/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TheVunerableApp.DataSource;
using TheVunerableApp.Model;

namespace TheVunerableApp.Controller
{
    public static class UserController
    {
        public static string CreateUser(UserType type, string govId, string name, string sName, string email, string password, DateTime startDate, Position position, Role role, string branchName, string branchId)
        {
            SQLiteDB sql = new SQLiteDB(); 
            if (type == UserType.Admin) // Admin
            {
                Admin admin = new Admin(govId, name, sName, email, password, startDate, position, role, branchName, branchId);
                sql.CreateUserInDB(admin, 1);
                return admin.AdminId;
            }
            // else a Customer
            Customer customer = new Customer(govId, name, sName, email, password);
            sql.CreateUserInDB(customer, 0);
            return customer.CustomerId;
        }

        /*
         *  Two Weaknesses identified in this method
         *  
         *  1. 
         *  Identified as CWE-787
         *  1/9/2023 - Identified by Professor Amin 
         *  1/9/2023 - Exploited by Professor Amin
         *  21/9/2023 - Patched and tested by Professor Amin
         *  
         *  2. 
         *  Identified as CWE-125
         *  1/9/2023 - Identified by Professor Amin
         *  1/9/2023 - Exploited by Professor Amin
         *  21/9/2023 - Patched and tested by Professor Amin
         */
        public static List<string> ListOfAccountBalance(string customerId)
        {
            // get the number of account
            SQLiteDB db = new SQLiteDB();
            List<string> accountNumbers = db.GetAllAccountsFromDB(customerId);
            List<string> balances = new List<string>();

            //Code with Vulnerabilities.
            /*
            for (int i = 0; i<=accountNumbers.Count; i++)
            {
                balances[i] = accountNumbers[i] +"-"+ db.GetBalance(accountNumbers[i]);
            }
            */

            //Weakness resolved - fixed code
            for (int i = 0; i < accountNumbers.Count; i++)
            {
                balances.Add(accountNumbers[i] + "-" + db.GetBalance(accountNumbers[i]));
            }

            return balances;
        }
        /*
         * One vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-306
         * 19/10/2023 - Identified by Thuan Pin Goh
         * 19/10/2023 - Exploited by Thuan Pin Goh
         * 19/10/2023 - Patched by Thuan Pin Goh
         */
        public static string UpdateUser(string customerId, string name, string sName, string email, string govId)
        {
            /*
             * SQLiteDB sql = new SQLiteDB();
             * sql.UpdateCustomerDetailsInDB(customerId, name, sName, email, govId);
             */
            SQLiteDB sql = new SQLiteDB();
            if (!sql.UserIDExists(customerId))
            {
                sql.UpdateCustomerDetailsInDB(customerId, name, sName, email, govId);
                return "User details updated successfully.";
            }
            else
            {
                return "User not found. No updates were made.";
            }
        }


        /*
         * One vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-476
         * 18/10/2023 - Identified by Dongyi Guo
         * 18/10/2023 - Exploited by Dongyi Guo
         * 18/10/2023 - Patched by Dongyi Guo
         */
        public static Customer DisplayUserDetails(string customerId)
        {
            SQLiteDB sql = new SQLiteDB();

            // Code with Vulnerability
            // If the customer id is incorrect, null will be returned.
            // return sql.GetCustomerDetailsFromDB(customerId);
            
            // Weakness Patched
            Customer customer = sql.GetCustomerDetailsFromDB(customerId);
            if (null != customer)
            {
                return customer;
            }
            else
            {
                return new Customer("", "", "", "", "");
            }

            // This exploit can also be fixed in lower level class (SQLiteDB.cs) or where it is being called.
        }

        /*
         * One vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-306
         * 19/10/2023 - Identified by Thuan Pin Goh
         * 19/10/2023 - Exploited by Thuan Pin Goh
         * 19/10/2023 - Patched by Thuan Pin Goh
         */
        public static string RemoveCustomer(string customerId)
        {
            /*
             * SQLiteDB sql = new SQLiteDB();
             * Customer customer = sql.RemoveUser(customerId);
             */

            SQLiteDB sql = new SQLiteDB();
            if (!sql.UserIDExists(customerId))
            {
                Customer customer = sql.RemoveUser(customerId);
                return customer.CustomerId;// Return the customer id of the user removed from the database
            }
            else
            {
                return "This ID cannot be found, failed to remove customer";
            }
        }

        /*
         * One vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-125
         * 19/10/2023 - Identified by Thuan Pin Goh
         * 19/10/2023 - Exploited by Thuan Pin Goh
         * 19/10/2023 - Patched by Thuan Pin Goh
         */
        public static List<Customer> SearchCustomerByAccountNumber(string accountNumber)
        {
            SQLiteDB sql = new SQLiteDB();
            List<Customer> customerList = new List<Customer>();

            List<string> customerIds = sql.GetCustomerIdFromDB(accountNumber);
            //Code with Vulnerabilities.
            //for (int i = 0; i <= customerIds.Count; i++)
            // Weakness Patched
            for (int i = 0; i < customerIds.Count; i++)
            {
                customerList.Add(sql.GetCustomerDetailsFromDB(customerIds[i]));
            }
            return customerList;
        }
    }

    public enum UserType { Customer, Admin}
}
