using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitLogger.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HabitLogger
{
    internal class HabitLoggerEngine
    {
        static string connectionString = @"Data Source=habit-logger.db";
        static bool listIsEmpty = true;
        internal static void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS drinking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Type TEXT,
                        Quantity INTEGER,
                        Unit TEXT
                        )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        internal static void GetAllRecords() 
        {
            Console.Clear();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    "SELECT * FROM drinking_water";

                List<Models.HabbitLogger> tableData = new();

                SQLiteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows) 
                {
                    while (reader.Read()) 
                    {
                        tableData.Add(
                            new Models.HabbitLogger
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                                Type = reader.GetString(2),
                                Quantity = reader.GetInt32(3),
                                Unit = reader.GetString(4)
                            });
                    }
                    listIsEmpty = false;
                }
                else
                {
                    Console.WriteLine("No rows found");
                    listIsEmpty = true;
                }

                connection.Close();

                Console.WriteLine("-----------------------------------\n");
                foreach (var dw in tableData)
                {
                    Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-MM-yy")} Habit: {dw.Type} - Quantity: {dw.Quantity} {dw.Unit}");
                }
                Console.WriteLine("\n-----------------------------------\n");
            }
        }
        internal static void GetRecordsByYear()
        {
            Console.Clear();

            var year = Helpers.GetTextInput("\nPlease insert the year that you want list");

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"SELECT * FROM drinking_water WHERE substr(date, -2) = '{year}' ORDER BY date";

                List<Models.HabbitLogger> tableData = new();

                SQLiteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new Models.HabbitLogger
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                                Type = reader.GetString(2),
                                Quantity = reader.GetInt32(3),
                                Unit = reader.GetString(4)
                            });
                    }
                    listIsEmpty = false;
                }
                else
                {
                    Console.WriteLine("\nYear not found");
                    listIsEmpty = true;
                    return;
                }

                connection.Close();

                Console.WriteLine("-----------------------------------\n");
                foreach (var dw in tableData)
                {
                    Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-MM-yy")} Habit: {dw.Type} - Quantity: {dw.Quantity} {dw.Unit}");
                }
                Console.WriteLine("\n-----------------------------------\n");
            }
        }
        internal static void GetRecordsBySpecificHabit()
        {
            Console.Clear();

            var habit = Helpers.GetTextInput("\nPlease insert the habit that you want to list");

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"SELECT * FROM drinking_water WHERE type = '{habit}' ORDER BY date";

                List<Models.HabbitLogger> tableData = new();

                SQLiteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                            new Models.HabbitLogger
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                                Type = reader.GetString(2),
                                Quantity = reader.GetInt32(3),
                                Unit = reader.GetString(4)
                            });
                    }
                    listIsEmpty = false;
                }
                else
                {
                    Console.WriteLine("\nHabit not found");
                    listIsEmpty = true;
                    return;
                }

                connection.Close();

                Console.WriteLine("-----------------------------------\n");
                foreach (var dw in tableData)
                {
                    Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-MM-yy")} Habit: {dw.Type} - Quantity: {dw.Quantity} {dw.Unit}");
                }
                Console.WriteLine("\n-----------------------------------\n");
            }
        }
        internal static void Insert()
        {
            Console.Clear();

            var date = Helpers.GetDateInput();

            if (date == "0") return;

            var type = Helpers.GetTextInput("\nPlease insert the type of habit");

            var unit = Helpers.GetTextInput($"\nPlease insert the unit of {type}").ToLower();

            var quantity = Helpers.GetNumberInput($"\nPlease insert number of {type} in {unit} (no decimals allowed)");

            using (var connection = new SQLiteConnection(connectionString)) 
            {
                connection.Open();
                
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO drinking_water(date, type, quantity, unit) VALUES('{date}', '{type}', {quantity}, '{unit}')";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            Console.Clear();
        }
        internal static void Delete()
        {
            GetAllRecords();

            if (listIsEmpty)
            {
                Console.WriteLine("\nThe list is empty. You can't exclude any record.");
                return;
            }
            
            var recordId = Helpers.GetNumberInput("\nPlease type the Id of the record you want to exclude or type 0 to go back to menu.\n");

            using (var connection = new SQLiteConnection(connectionString)) 
            {
                connection.Open();
                
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"DELETE FROM drinking_water WHERE Id = '{recordId}'";

                var rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"\nRecord with Id {recordId} doesn't exist.");
                } else
                {
                    Console.WriteLine($"\nRecord with Id {recordId} was excluded.\n");
                }

                connection.Close();
            }    
        }
        internal static void Update()
        {
            GetAllRecords();

            if (listIsEmpty)
            {
                Console.WriteLine("\nThe list is empty. You can't update any record.");
                return;
            }

            var recordId = Helpers.GetNumberInput("\nPlease type the Id of the record you want to update or type 0 to go back to menu.\n");

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText =
                    $"SELECT EXISTS(SELECT 1 FROM drinking_water WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\nRecord with Id {recordId} doesn't exist.");
                    connection.Close();
                    return;
                }

                string date = Helpers.GetDateInput();

                var type = Helpers.GetTextInput($"\nPlease insert the type of habit");

                var unit = Helpers.GetTextInput($"\nPlease insert the unit of {type}");

                int quantity = Helpers.GetNumberInput($"\nPlease insert number of {type} in {unit} (no decimals allowed)");

                var tableCmd = connection.CreateCommand() ;
                tableCmd.CommandText =
                    $"UPDATE drinking_water SET date = '{date}', type = '{type}', quantity = {quantity}, unit = '{unit}' WHERE Id = {recordId}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
