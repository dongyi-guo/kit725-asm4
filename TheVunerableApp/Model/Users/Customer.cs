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
        private string GenerateUserId(int max)
        {
            Random random = new Random();
            string userId = "";

            for (int i = 0; i < max; i++)
            {
                userId += random.Next(10).ToString();
            }

            return userId;
        }
    }
}
