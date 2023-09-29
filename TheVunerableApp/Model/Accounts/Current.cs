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
    internal class Current : Account
    {
        public Current(double initialBalance, double minimumBalance, double monthlyFee, string customerId) : base(initialBalance, customerId)
        {
            base.Balance = initialBalance;
            base.AddCustomer(customerId);
            MinimumBalance = minimumBalance;
            MonthlyFee = monthlyFee;
        }

        public double MinimumBalance { get; set; }

        public double MonthlyFee { get; set; }

    }
}
