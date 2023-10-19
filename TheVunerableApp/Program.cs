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
using TheVunerableApp.Model;
using TheVunerableApp.View;

namespace TheVunerableApp
{
    /*
     * This is a driver file that tests the code
     * For the assessment this class is not 
     * required to be analysed.
     * */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Test.Test.ExploitBounds(); // Professor Amin's Sample CWE-787 & 125
            Test.Test.CWE476_DongyiGuo();
            Test.Test.CWE427_RonghuaYang();
            Test.Test.CWE798_DongyiGuo();
            Test.Test.CWE306_ThuanPinGoh();
            Test.Test.CWE798_ThuanPinGoh();
            Test.Test.CWE125_ThuanPinGoh();
            Test.Test.CWE522_DongyiGuo();

            // Program.DbSetUpForTesting(); // For setup only Do not use it unless the db is in inconsistence state.


            /* Following are test methods that you can use individually to run some test drivers for the code.
             * However, you should be writing your test code in Test.cs */

            // Program.UpdateCustomerDetails();
            // Program.DisplayUserDetails();
            Program.SearchCustomerByAccountNumeber();
            // Program.getAllUserAccounts();
            // Program.AddCustomerToAccount();
            // Program.getAccountBalance();
            // Program.PrintFilePathsFromAppSettings();

        }

        private static void PrintFilePathsFromAppSettings()
        {
            Console.WriteLine(TransactionController.getTRPath());
        }        
        private static void SearchCustomerByAccountNumeber() 
        {
            List<Customer> customers = UserController.SearchCustomerByAccountNumber("31146289");
            Console.WriteLine(customers[0]);
        }
        private static void CreateUser() 
        {
            AdminView.CreateUser();
        }
        private static void UpdateCustomerDetails() 
        {
            UserController.UpdateUser("6763996216", "Jean", "Grey", "j.grey@xmen.com", "321-456-9876");
        }

        private static void DisplayUserDetails() 
        {
            Customer customer = UserController.DisplayUserDetails("SB19-87204084A");
            Console.WriteLine(customer);
        }
        private static void AddCustomerToAccount() 
        {
            AccountController.AddCustomerToAnAccount("89048295", "6763996216");
        }
        private static void getAllUserAccounts() 
        {
            List<string> accounts = AccountController.SearchAccountByCustomer("6763996216");
        }
        private static void getAccountBalance()
        {
           Console.WriteLine(AccountController.GetAccountBalance("89048295"));
        }
   
        private static void DbSetUpForTesting()
        {
            //This is just a driver method to create baseline dataset for testing of the system
            //1. Create 1 Admin
            //2. Create 4 Users
            //3. Create 3 Current Accounts
            //4. Create 3 Savings Accounts
            //5. Create 10 Transactions between these accounts

            // Creating RequestObjects
            //1.
            // string admin = UserController.CreateUser(UserType.Admin, "408-332-8739", "Charles", "Xavier", "c.xavier@xmen.com", "iCanLocate1111", DateTime.Parse("01/04/1956"), Model.Position.manager, Model.Role.Admin, "Sandy Bay", "SB19");

            //2.
            /*  string user1 = UserController.CreateUser(UserType.Customer, "321-456-9876", "Jean", "Grey", "j.grey@xmen.com", "iReadMind1978", DateTime.Parse("27/07/2019"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
              string user2 = UserController.CreateUser(UserType.Customer, "456-335-1135", "Henry", "McCoy", "h.mcoy@xmen.com", "iAmTheBeast1968", DateTime.Parse("26/06/2018"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
              string user3 = UserController.CreateUser(UserType.Customer, "789-336-1698", "Scott", "Summers", "s.summers@xmen.com", "theThirdEye200", DateTime.Parse("23/11/2011"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
              string user4 = UserController.CreateUser(UserType.Customer, "870-667-0773", "Bobby", "Drake", "b.drake@xmen.com", "zeroC00l11", DateTime.Parse("17/03/2022"), Model.Position.none, Model.Role.none, "Sandy Bay", "SB19");
            */
            //3.
            /*  AccountController.CreateCurrentAccount(user1, 100, 50, 10);
              AccountController.CreateCurrentAccount(user2, 7500, 50, 10);
              AccountController.CreateCurrentAccount(user4, 9500, 50, 10);
            */
            //4.
            /*  AccountController.CreateSavingsAccount(user1, 2.4, 10000);
              AccountController.CreateSavingsAccount(user2, 4.2, 7500);
              AccountController.CreateSavingsAccount(user3, 1.9, 19807);
            */

            //TransactionController.StoreTransactions("46101163", 675.00, "52805275");
            //Console.WriteLine("Setup complete..");

            //SQLiteDB sQLiteDB = new SQLiteDB();
            //sQLiteDB.ConnectToDS();
            // sQLiteDB.getAuthForTest();


        }
    }
}
