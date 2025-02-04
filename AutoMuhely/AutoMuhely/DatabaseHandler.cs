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
        static string firstConnectionCommand = "server=localhost;user=root;password='';Allow Zero Datetime=True;Convert Zero Datetime=True;";
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
                                    try
                                    {
                                        if (reader.GetFieldType(i) == typeof(DateTime))
                                        {
                                            var value = reader.IsDBNull(i) ? (DateTime?)null : reader.GetDateTime(i);
                                            row.Add(value?.ToString("yyyy-MM-dd HH:mm:ss") ?? "NULL");
                                        }
                                        else
                                        {
                                            row.Add(reader.IsDBNull(i) ? null : reader.GetValue(i));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // Log the error and skip the field
                                        Console.WriteLine($"Error processing field {reader.GetName(i)}: {ex.Message}");
                                        row.Add($"Error: {ex.Message}");
                                    }
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
                Console.WriteLine($"Critical Error: {ex.Message}");
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
