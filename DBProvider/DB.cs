using System.Text;
using System.Text.RegularExpressions;

namespace DBProvider
{
    public class DB
    {
        /* Получаем путь к нашей "БД"
         * каким либо образом, через конфиг файл или
         * соединение с СУБД, считаем что получили и сохранили значение
         */
        private const string DB_PATH = @"EmployeeDB.txt";

        /// <summary>
        /// Получить следующее значение Id сотрудника 
        /// </summary>
        /// <returns>Id для записи в БД, значение, "-1" сообщает что БД не существует</returns>
        public static int GetNextEmployeeID()
        {
            int employeeNextID = -1;
            if (CheckDBExist())
            {
                using (StreamReader employeeDB = new StreamReader(DB_PATH))
                {
                    StringBuilder employeeID = new StringBuilder();
                    
                    string id;
                    while ((id = employeeDB.ReadLine()) != null)
                    {   
                        employeeID.Clear();
                        for (int indexString = 0; indexString < id.Length; indexString++)
                        {
                            if (id[indexString] != '#')
                            {
                                employeeID.Append(id[indexString]);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    int.TryParse(employeeID.ToString(), out employeeNextID);
                }
            }
            else
            {
                return employeeNextID;
            }

            return employeeNextID + 1;

        }

        /// <summary>
        /// Считывает данные из файла
        /// </summary>
        /// <returns>Сведения о сотрудниках что есть в файле</returns>
        public static string ReadFromDB()
        {
            StringBuilder employeesData = new StringBuilder();
            if (CheckDBExist())
            {
                using (StreamReader employeeDB = new StreamReader(DB_PATH))
                {
                    string employeeRead;
                    while ( (employeeRead = employeeDB.ReadLine()) != null)
                    {   
                        string[] employees = Regex.Split(employeeRead, @"#");
                        employeesData.AppendJoin(' ', employees);
                        employeesData.Append('\n');
                    }
                }
            }
            else
            {
                CreateDB();
                return "Создан файл для наполнения, сведения о сотрудниках отсутствуют";
            }

            return employeesData.ToString();

        }

        /// <summary>
        /// Запись в БД
        /// </summary>
        /// <param name="valueToWrite">Строка которая будет записана в БД</param>
        public static void WriteIntoDB(string valueToWrite)
        {
            if (CheckDBExist())
            {
                using (StreamWriter swIntoDB = new StreamWriter(DB_PATH, true))
                {
                    swIntoDB.WriteLine(valueToWrite);
                }
                Console.WriteLine("Сведения о сотруднике записаны в БД");

            }
            else
            {
                Console.WriteLine("БД не существует, запись не возможна");
            }

        }

        /// <summary>
        /// Проверяет существование БД
        /// </summary>
        /// <returns>Возвращает истину есть БД есть, и ложно если БД отсутсвует</returns>
        private static bool CheckDBExist()
        {
            return File.Exists(DB_PATH);
        }

        /// <summary>
        /// Создает файл БД в каталоге определенном настройками
        /// </summary>
        private static void CreateDB()
        {
            FileStream newDb = File.Create(DB_PATH);
            newDb.Close();      
        }
    }
}