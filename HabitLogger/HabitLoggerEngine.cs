using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    internal class HabitLoggerEngine
    {
        static string connectionString = @"Data Source=habit-logger.db";
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
        
    }
}
