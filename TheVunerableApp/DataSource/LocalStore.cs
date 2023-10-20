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
         *  Three vulnerabilities identified in this method
         *  
         *  1. 
         *  Identified as CWE-22
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         *  2. 
         *  Identified as CWE-73
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         *  3. 
         *  Identified as CWE-502
         *  18/10/2023 - Identified by Dongyi Guo
         *  18/10/2023 - Exploited by Dongyi Guo
         *  18/10/2023 - Patched and tested by Dongyi Guo
         */
        // This function reflects on three major vulnerabilites related to file path issues.
        public Transaction LoadTransaction(string path)
        {
            // CWE-22 & 73: Without any limitation on the file path, user can pass
            // files whereever on the system, even some places the user does not
            // even have access to but this program has.
            //
            // To fix this, use Filepath declared before to limit the upper level
            // of the path, and reject path if "Up one directory level" is in it.
            //
            // Hence @param string path now only takes relative path under root
            // of Filepath location.
            if (path.Contains(".."))
            {
                throw new ArgumentException("No visit upper layer folder");
            }
            path = Path.Combine(FilePath, path);

            // CWE-502: The json file is provided freely from the user, the sanity of it should be checked,
            // JsonSerializer.Deserialize<>() provides Exceptions to be thrown while there are something wrong.
            // While calling this function exception handler should always be kept in mind.

            return JsonSerializer.Deserialize<Transaction>(path);
        }
    }
}
