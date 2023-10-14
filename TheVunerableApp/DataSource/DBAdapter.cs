/*
* Disclaimer Ref#: 2023S2735-0-jR0L2p9QsVxGcY2uM5BfD8nHw
* This code is for assessment purposes only. 
* Any reuse of this code without permission is prohibited 
* and may result in academic integrity breach.
*/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheVunerableApp.DataSource
{
    internal class DBAdapter
    { 
        static void ConnectToRemoteDB()
        {
            string server = "Nebuchadnezzar";
            string database = "TheBankOfZion";
            string username = "Morpheus";
            string password = "iL00k4N30";

            string connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("Connected to MySQL server!");
                        // We arent using the Remote DB here; however, this is a valid code in the App
                        connection.Close();
                    }
                    else
                    {
                        Console.WriteLine("Failed to connect to MySQL server.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

}
