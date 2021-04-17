using Sap.Data.Hana;
using System;

namespace SapHanaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = "43875a52-8238-4efd-aaa1-19b0f9f714ee.hana.trial-us10.hanacloud.ondemand.com";
            string userName = "DBADMIN";
            string password = "ZAQ!2wsx";
            string port = "443";
            string connectionString = $"Server={hostName}:{port};UID={userName};PWD={password};encrypt=true;sslValidateCertificate=false";
            try
            {
                using (var conn = new HanaConnection(connectionString))
                {


                    conn.Open();
                    Console.WriteLine("Connected");
                    //Create table


                    //var createTableQuery = $"alter table todo alter (\"NAME\" NVARCHAR(500));";
                    //using (var cmd = new HanaCommand(createTableQuery, conn))
                    //{
                    //    int n = cmd.ExecuteNonQuery();
                    //}

                    // Querying table
                    var query = "SELECT ID, NAME From todo";
                    using (var cmd = new HanaCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Query result:");
                        var sbCol = new System.Text.StringBuilder();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            sbCol.Append(reader.GetName(i).PadRight(20));
                        }
                        Console.WriteLine(sbCol.ToString());
                        // Print rows
                        while (reader.Read())
                        {
                            var sbRow = new System.Text.StringBuilder();
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                sbRow.Append(reader[i].ToString().PadRight(20));
                            }
                            Console.WriteLine(sbRow.ToString());
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }
    }
}