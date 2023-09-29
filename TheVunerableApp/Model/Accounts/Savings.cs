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
    internal class Savings : Account
    {
        public Savings(double initialBalance, double interestRate, string customerId) : base(initialBalance, customerId)
        {
            base.Balance = initialBalance;
            base.AddCustomer(customerId);
            InterestRate = interestRate;
        }

        public double InterestRate { get; set; }

    }
}
