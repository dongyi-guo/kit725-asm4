/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TheVunerableApp.Model
{
    public class Admin : User
    {
        public DateTime StartDate { get; set; }
        public Position Position { get; set; }    
        public Role Role { get; set; }
        public string BranchName { get; set; }
        public string BranchId { get; set; }
        public string AdminId { get; private set; }

        public Admin(string govId, string name, string sirName, string email, string password, DateTime startDate, Position position, Role role, string branchName, string branchId):base(govId, name, sirName, email, password)
        {
            base.GovId = govId;
            base.Name = name;
            base.SirName = sirName;
            base.Email = email;
            base.Password = password;
          
            StartDate = startDate;
            Position = position;
            Role = role;
            BranchName = branchName;
            BranchId = branchId;
            AdminId = GenerateAdminId(8); // maximum length of an Id is 8 characters
        }


        public string GenerateAdminId(int max)
        {
            Random random = new Random();
            string id = "";

            for (int i = 0; i < max; i++)
            {
                id += random.Next(10).ToString();
            }

            return BranchId+"-"+id+"A";
        }
    }

    public enum Position
    {
        manager, specialist, attendent, representative, none
    }

    public enum Role
    {
        Admin, Teller, none
    }
}
