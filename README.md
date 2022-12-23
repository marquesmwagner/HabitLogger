# HabitLogger
Habit Tracker console application, developed using C# and SQLite.

This is a aplication where you'll register your habits(ex. kms that you ran, number of times you drank a coffee or times you brushed your teeth), store and retrieve data from a database.

# Features

* When the application starts, it create a SQLite Database, if one doesn't exist. And create a table where the habits are logged.

```
      internal static void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS habit_table (
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

```

* The application has a menu of options.

  ![image](https://user-images.githubusercontent.com/38431500/208267761-fed17a55-a7e5-4e6f-9cc2-59949a3bb097.png)

* The user is able to view, insert, delete and update the records.

  ![image](https://user-images.githubusercontent.com/38431500/208434914-e32b6b25-998f-47f6-a690-b6c6a81c19c4.png)
