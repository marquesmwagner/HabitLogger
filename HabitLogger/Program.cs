namespace HabitLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HabitLoggerEngine.CreateDatabase();
            Menu.ShowMenu();
        }
    }
}