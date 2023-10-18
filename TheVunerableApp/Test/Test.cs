/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TheVunerableApp.Controller;
using TheVunerableApp.DataSource;
using TheVunerableApp;
using TheVunerableApp.Model;
using System.Data.SQLite;

namespace TheVunerableApp.Test
{
    internal class Test
    {
        //Added over here

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
         */
        public static void CWE476_DongyiGuo()
        {
            SQLiteDB sql = new SQLiteDB();
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
        }

        /*
         * The following function exploits and tests CWE-427 for:
         * 
         * SQLiteDB.cs - GetCustomerDetailsFromDB(customerId)
         * OR
         * UseController.cs - DisplayUserDetails(customerId)
         */
        public static void CWE427_RonghuaYang()
        {
            // DB\Bank.sqlite created
            SQLiteDB db_tmp = new SQLiteDB();
            try
            {
                SQLiteConnection sql_conn = new SQLiteConnection("Data Source=" + db_tmp.Filepath);
                Console.WriteLine("CWE-427 Patched");
            }
            catch
            {
                Console.WriteLine("CWE-427 Exploited");
            }
        }


    }
}
