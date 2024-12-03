using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace AutoMuhely
{
    internal class DatabaseHandler
    {
        static string firstConnectionCommand = "server=localhost;user=root;password='';";
        static string databaseName = "automuhely";

        static string connectionCommand = "server=localhost;database=automuhely;user=root;password=''";
        static string sqlPath = "automuhely.sql";
        public string UserName {  get; set; }
        public string Role { get; set; }
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

        public (List<List<object>>, List<string>) Select(string selectQuery, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionCommand))
                {
                    connection.Open();

                    using (var command = new MySqlCommand(selectQuery, connection))
                    {
                        // Add parameters if provided
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            var rows = new List<List<object>>();
                            var columnNames = new List<string>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                columnNames.Add(reader.GetName(i));
                            }

                            while (reader.Read())
                            {
                                var row = new List<object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetValue(i));
                                }
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
        public void Insert(string insertQuery, Dictionary<string, object> parameters)
        {
                using (var connection = new MySqlConnection(connectionCommand))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(insertQuery, connection))
                    {
                        // Dynamically add parameters to the query
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        int rowsAffected = command.ExecuteNonQuery(); // Execute the insert statement
                    }
                }
        }
        public int LookupID(string query, Dictionary<string, object> parameters)
        {
            try
            {
                var result = Select(query, parameters);
                if (result.Item1.Count > 0)
                {
                    // Assuming the ID is in the first row and first column
                    return Convert.ToInt32(result.Item1[0][0]);
                }
                else
                {
                    return -1; // Return -1 if no match is found
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt az ID lekérdezése során: {ex.Message}", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
    }
}
