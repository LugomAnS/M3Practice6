using System.Text;
using System.Text.RegularExpressions;
using DBProvider;

namespace UserInterface
{
    public class UI
    {
        /// <summary>
        /// Отображает главное меню программы
        /// </summary>
        public static void MainMenuShow()
        {
            Console.Clear();
            Console.WriteLine("Приложение для ведения списка сотрудников");
            Console.WriteLine("Выберите режим работы: ");
            Console.WriteLine("1. Показать список сотрудников");
            Console.WriteLine("2. Внести нового сотрудника");
            Console.WriteLine("3. Выйти из программы");
        }

        /// <summary>
        /// Возвращает результат пользовательского выбора в главном меню
        /// </summary>
        /// <returns> 1 - Показать список, 2 - ввести нового, 3 - выход из программы </returns>
        public static int MainMenuChoice()
        {
            int userChoice;

            do
            {
                Console.WriteLine("Введите номер пункта меню для продолжения: ");
                string userInput = Console.ReadLine().ToString();
                int.TryParse(userInput, out userChoice);

            } while ( !(0 < userChoice && userChoice <= 3) );

            return userChoice;
        }


        /// <summary>
        /// Выход из приложения
        /// </summary>
        public static void ApplicationExit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Ввод информации о сотруднике
        /// </summary>
        /// <returns>Информация о сотруднике в формате записи в БД</returns>
        public static string AddNewEmployee()
        {
            int idEmployee = DB.GetNextEmployeeID();
            string employeeFullName = NewEmployeeFullName();
            int employeeAge = NewEmployeeAge();
            int employeeHeight = NewEmployeeHeight();
            string employeeBirthday = NewEmployeeBirthdayDate();
            string employeeBrthPlace = NewEmployeeBirthdayPlace();
            DateTime dateRecord = DateTime.Now;

            StringBuilder employeeInfo = new StringBuilder();
            employeeInfo.AppendJoin('#', idEmployee,
                                         dateRecord.ToString(),
                                         employeeFullName,
                                         employeeAge,
                                         employeeHeight,
                                         employeeBirthday,
                                         employeeBrthPlace
                                    );

            return employeeInfo.ToString();
            
        }

        /// <summary>
        /// Ввод ФИО нового сотрудника
        /// </summary>
        /// <returns>Возвращает ФИО сотрудника</returns>
        private static string NewEmployeeFullName()
        {
            string employeeFullName = string.Empty;
            bool isAllLetters = false;
            do
            {
                Console.WriteLine("Введите Ф.И.О. сотрудника:");
                employeeFullName = Console.ReadLine().ToString();

                for (int stringIndex = 0; stringIndex < employeeFullName.Length; stringIndex++)
                {
                    
                    isAllLetters  = Char.IsLetter(employeeFullName[stringIndex]);
                    if (!isAllLetters && (employeeFullName[stringIndex] != ' '))
                    {
                        break;
                    }
                }
              
            } while (!isAllLetters);

            return employeeFullName;
        }
        
        /// <summary>
        /// Ввод возраста сотрудника 
        /// </summary>
        /// <returns>Количество полных лет</returns>
        private static int NewEmployeeAge()
        {
            int employeeAge;

            do
            {
                Console.WriteLine("Введите количество полных лет сотрудника в диапазоне от 18 до 99");
                string userInput = Console.ReadLine().ToString();
                int.TryParse(userInput, out employeeAge);

            } while (!(17 < employeeAge && employeeAge <= 99));

            return employeeAge;

        }

        /// <summary>
        /// Ввод роста сотрудника
        /// </summary>
        /// <returns>Рост сотрудника в сантиметрах</returns>
        private static int NewEmployeeHeight()
        {
            int employeeHeight;

            do
            {
                Console.WriteLine("Введите рост сотрудника в сантиметрах от 50 до 250");
                string userInput = Console.ReadLine().ToString();
                int.TryParse(userInput, out employeeHeight);

            } while (!(50 <= employeeHeight && employeeHeight <= 250));

            return employeeHeight;

        }

        /// <summary>
        /// Ввод даты рождения сотрудника
        /// </summary>
        /// <returns>Возвращает день рождения в формате ДД.ММ.ГГГГ</returns>
        private static string NewEmployeeBirthdayDate()
        {
            string patternBirthday = @"^[0-3][0-9].[0-1][0-9].[1-2][0-9][0-9][0-9]$";
            bool userInputCheck = false;
            string userInput;
            do
            {
                Console.WriteLine("Введите дату рождения в формате ДД.ММ.ГГГГ");
                userInput = Console.ReadLine().ToString();

                userInputCheck = Regex.IsMatch(userInput, patternBirthday, RegexOptions.None, TimeSpan.FromMilliseconds(2000));

            } while (!userInputCheck);

            return userInput;
        }

        /// <summary>
        /// Ввод места рождения
        /// </summary>
        /// <returns></returns>
        private static string NewEmployeeBirthdayPlace()
        {
            string employeeBrthPlace = string.Empty;
            bool isAllLetters = false;
            do
            {
                Console.WriteLine("Введите место рождения сотрудника:");
                employeeBrthPlace = Console.ReadLine().ToString();

                for (int stringIndex = 0; stringIndex < employeeBrthPlace.Length; stringIndex++)
                {

                    isAllLetters = Char.IsLetter(employeeBrthPlace[stringIndex]);
                    if (!isAllLetters && (employeeBrthPlace[stringIndex] != ' ' ) )
                    {
                        break;
                    }
                }

            } while (!isAllLetters);

            return employeeBrthPlace;
        }
    }
}