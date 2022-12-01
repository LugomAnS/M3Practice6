using UserInterface;
using DBProvider;

namespace EmployeeApp
{
    /// <summary>
    /// Программа ведения справочника "сотрудников"
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа и работы
        /// </summary>
        static void Main()
        {
            while (true)
            {
                UI.MainMenuShow();

                int userInput = UI.MainMenuChoice();
                switch (userInput)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Записи справочника: ");
                        string dbRecords;
                        dbRecords = DB.ReadFromDB();
                        Console.WriteLine(dbRecords);
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.Clear();
                        string newEmployee;
                        newEmployee = UI.AddNewEmployee();
                        DB.WriteIntoDB(newEmployee);
                        Console.ReadKey(true);
                        break;
                    case 3:
                        UI.ApplicationExit();
                        break;
                    default:
                        Console.WriteLine("Что то пошло не так, обратитесь к разработчику");
                        break;
                }

            }
        }
    }
}