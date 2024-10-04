using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AutoMuhely
{
    internal class DatabaseHandler
    {
        static string firstConnectionCommand = "server=localhost;user=root;password='';";
        static string databaseName = "automuhely";

        static string connectionCommand = "server=localhost;database=automuhely;user=root;password=''";
        static string sqlPath = "automuhely.sql";

        public void DatabaseConnect()
        {
            try
            {
                using (var connection = new MySqlConnection(firstConnectionCommand))
                {
                    connection.Open();

                    string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {databaseName};";
                    using (var command = new MySqlCommand(createDatabaseQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.ChangeDatabase(databaseName);

                    string script = File.ReadAllText(sqlPath);

                    string[] sqlCommands = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var commandText in sqlCommands)
                    {
                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }

        public void Select(string selectQuery)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionCommand))
                {
                    connection.Open();

                    using (var command = new MySqlCommand(selectQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            // Lista a sorok tárolására
                            var rows = new List<Dictionary<string, object>>();

                            while (reader.Read())
                            {
                                // Szótár, ami az aktuális sort tárolja
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    // Minden oszlop nevét és értékét hozzáadjuk a szótárhoz
                                    row[reader.GetName(i)] = reader.GetValue(i);
                                }

                                // Sor hozzáadása a listához
                                rows.Add(row);
                            }

                            // Adatok kiírása a konzolra
                            /*foreach (var r in rows)
                            {
                                foreach (var kvp in r)
                                {
                                    Console.Write($"{kvp.Key}: {kvp.Value}\t");
                                }
                                Console.WriteLine();
                            }*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }

        string insertQuery = "INSERT INTO table_name (column1, column2) VALUES (@value1, @value2);";
        public void Insert(string insertQuery)
        {
            using (var connection = new MySqlConnection(connectionCommand))
            {
                connection.Open();
                using (var command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@value1", "Első érték");
                    command.Parameters.AddWithValue("@value2", "Második érték");

                    int rowsAffected = command.ExecuteNonQuery(); // Affected rows
                }
            }
        }
    }
}
