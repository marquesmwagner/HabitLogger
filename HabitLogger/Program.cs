namespace HabitLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database.Connection();
            Menu.ShowMenu();
        }
    }
}