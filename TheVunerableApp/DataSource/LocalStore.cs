/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheVunerableApp.Model;

namespace TheVunerableApp.DataSource
{
    internal class LocalStore
    { 
        public LocalStore()
        {
            FilePath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["TRPath"];
        }

        public string FilePath { get; }

        /*
         *  Two vulnerabilities identified in this method
         *  
         *  1. 
         *  Identified as CWE-20
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         *  2.
         *  Identified as CWE-476
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         */
        // This function doesn't check if the transaction is not a null object.
        public bool StoreTransactions(Transaction transaction)
        {
            // Added null check
            if (null == transaction) return false;

            string transactionInJson = JsonSerializer.Serialize(transaction);
            string path = transaction.TransactionId + ".json";
            File.WriteAllText(Path.Combine(FilePath,path),transactionInJson);
            return true;
        }

        /*
         *  One vulnerability identified in this method
         *  
         *  1. 
         *  Identified as CWE-502
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         */
        // This function 
        public Transaction LoadTransaction(string path)
        {
           return JsonSerializer.Deserialize<Transaction>(path);
        }
    }
}
