using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    internal class Menu
    {
        internal static void ShowMenu()
        {
            Console.Clear();
            var closeApp = false;

            while (!closeApp)
            {
                Console.WriteLine("\nHabit Logger - Menu");
                Console.WriteLine("\nWhat do you like to do?");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("0 - Close the application");
                Console.WriteLine("1 - View all records");
                Console.WriteLine("2 - Insert record");
                Console.WriteLine("3 - Delete record");
                Console.WriteLine("4 - Update record");
                Console.WriteLine("----------------------------------\n");

                string commandInput = Console.ReadLine();

                switch (commandInput)
                {
                    case "0":
                        Console.WriteLine("\nClose the application");
                        closeApp = true;
                        break;
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please type a number from 0 to 4.");
                        break;
                }
            }
        }
    }
}
