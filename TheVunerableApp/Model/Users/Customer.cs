/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using TheVunerableApp.DataSource;

namespace TheVunerableApp.Model
{
    public class Customer: User
    {
        public string CustomerId { get; private set; }
        public Customer(string govId, string name, string sirName, string email, string password) : base(govId, name, sirName, email, password)
        {
            base.GovId = govId;
            base.Name = name;
            base.SirName = sirName;
            base.Email = email;
            base.Password = password;
            CustomerId = GenerateUserId(10);
        }

        public override string ToString()
        {
            return base.GovId + "-" + base.SirName + "," + base.Name;
        }


        /*
         * Two vulnerability identified in this method
         * 
         * 1.
         * Identified as CWE-306
         * 19/10/2023 - Identified by Thuan Pin Goh
         * 19/10/2023 - Exploited by Thuan Pin Goh
         * 19/10/2023 - Patched by Thuan Pin Goh
         * 
         * 2.
         * Identified as CWE-330
         * 19/10/2023 - Identified by Thuan Pin Goh
         * 19/10/2023 - Exploited by Thuan Pin Goh
         * 19/10/2023 - Patched by Thuan Pin Goh
         */

        private string GenerateUserId(int max)
        {
            //this code is to replace the original random, to ensure the account number has better randomness
            //Code with Vulnerabilities.
            //Random random = new Random(); 
            // Weakness Patched

            Random random = new Random(Guid.NewGuid().GetHashCode());
            string userId = "";

            //Code with Vulnerabilities.
            /*
            for (int i = 0; i < max; i++)
            {
                userId += random.Next(10).ToString();
            }
            */
            // Weakness Patched
            SQLiteDB db = new SQLiteDB();
            do
            {
                for (int i = 0; i < max; i++)
                {
                    userId += random.Next(10).ToString();
                }

            } while (!db.UserIDExists(userId)); //this function available in SQLiteDB

            return userId;
        }
    }
}
