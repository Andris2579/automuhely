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

        public string ConnectionCommand
        {
            get { return connectionCommand; }
        }

        public void DatabaseConnect()
        {
            try
            {
                using (var connection = new MySqlConnection(firstConnectionCommand))
                {
                    connection.Open();

                    string checkDatabaseQuery = $"SHOW DATABASES LIKE '{databaseName}';";
                    using (var command = new MySqlCommand(checkDatabaseQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if(result == null)
                        {
                            string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {databaseName};";
                            using (var createCommand = new MySqlCommand(createDatabaseQuery, connection))
                            {
                                createCommand.ExecuteNonQuery();
                            }

                            connection.ChangeDatabase(databaseName);

                            string script = File.ReadAllText(sqlPath);

                            string[] sqlCommands = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var commandText in sqlCommands)
                            {
                                using (var commandDatabaseInsert = new MySqlCommand(commandText, connection))
                                {
                                    commandDatabaseInsert.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
            }
        }

        public (List<List<object>>, List<string>) Select(string selectQuery)
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
                            var rows = new List<List<object>>();

                            var columnNames = new List<string>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                columnNames.Add(reader.GetName(i));
                            }

                            while (reader.Read())
                            {
                                // Szótár, ami az aktuális sort tárolja
                                var row = new List<object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    // Minden oszlop nevét és értékét hozzáadjuk a szótárhoz
                                    row.Add(reader.GetValue(i));
                                }

                                // Sor hozzáadása a listához
                                rows.Add(row);
                            }

                            return (rows, columnNames);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt: {ex.Message}");
                return (null, null);
            }
        }

        public void Update(string updateQuery, Dictionary<string, object> parameters)
        {
            using (var connection = new MySqlConnection(connectionCommand))
            {
                connection.Open();
                using (var command = new MySqlCommand(updateQuery, connection))
                {
                    // Paraméterek dinamikus hozzáadása
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    int rowsAffected = command.ExecuteNonQuery(); // Érintett sorok
                    Console.WriteLine($"{rowsAffected} sor frissítve."); // Opció: érintett sorok kiírása
                }
            }
        }

        public void Delete(string deleteQuery, Dictionary<string, object> parameters)
        {
            using (var connection = new MySqlConnection(connectionCommand))
            {
                connection.Open();
                using (var command = new MySqlCommand(deleteQuery, connection))
                {
                    // Paraméterek dinamikus hozzáadása
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    int rowsAffected = command.ExecuteNonQuery(); // Érintett sorok
                    Console.WriteLine($"{rowsAffected} sor törölve."); // Opció: érintett sorok kiírása
                }
            }
        }
    }
}
