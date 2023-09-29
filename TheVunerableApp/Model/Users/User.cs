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
    public class User
    {
        public User(string govId, string name, string sirName, string email, string password)
        {
            GovId = govId;
            Name = name;
            SirName = sirName;
            Email = email;
            Password = password;
        }

        public string GovId { get; set; }
        public string Name { get; set; }
        public string SirName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
