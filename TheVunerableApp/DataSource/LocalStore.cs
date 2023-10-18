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

        public bool StoreTransactions(Transaction transaction)
        {
            string transactionInJson = JsonSerializer.Serialize(transaction);
            string path = transaction.TransactionId + ".json";
            File.WriteAllText(Path.Combine(FilePath,path),transactionInJson);
            return true;
        }

        public Transaction LoadTransaction(string path)
        {
           return JsonSerializer.Deserialize<Transaction>(path);
        }
    }
}
