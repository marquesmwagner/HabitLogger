using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitLogger.Models;

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
                        Quantity INTEGER
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
                                Quantity = reader.GetInt32(2)
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
                    Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-MM-yy")} - Quantity: {dw.Quantity}");
                }
                Console.WriteLine("\n-----------------------------------\n");
            }
        }
        internal static void Insert()
        {
            var date = Helpers.GetDateInput();

            var quantity = Helpers.GetNumberInput("\nPlease insert number of glasses or other measure of your choice (no decimals allowed)\n");

            using (var connection = new SQLiteConnection(connectionString)) 
            {
                connection.Open();
                
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        internal static void Delete()
        {
            GetAllRecords();

            if (listIsEmpty)
            {
                Console.WriteLine("\nThe list is empty. You can't delete any record.");
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
    }
}
