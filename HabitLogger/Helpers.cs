using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    internal class Helpers
    {
        internal static string GetDateInput()
        {
            Console.WriteLine("\nPlease insert the date: (dd-mm-yy). Type 0 to return to menu.");
            var dateInput = Console.ReadLine().Trim();

            if (dateInput == "0") return "0";

            while (!DateTime.TryParseExact(dateInput,"dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\nInvalid date. (Format:dd-mm-yy). Type 0 to return to menu or try again.");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }
        internal static int GetNumberInput(string message) 
        {
            Console.WriteLine(message);

            var numberInput = Console.ReadLine();

            if (numberInput == "0") return 0;

            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0) 
            {
                Console.WriteLine("\nInvalid number. Try again.\n");
                numberInput = Console.ReadLine();
            }

            var finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }
        internal static string GetTextInput(string message) 
        {
            Console.WriteLine(message);

            var textInput = Console.ReadLine().Trim();

            while (string.IsNullOrEmpty(textInput))
            {
                Console.WriteLine("\nIt can't be empty. Please type a valid value.");
                textInput = Console.ReadLine().Trim();
            }

            return textInput;
        }
    }
}
