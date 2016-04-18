using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoLotConnectedLayer;
using System.Configuration;
using System.Data;

namespace AutoLotCUIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Консольный пользовательский интерфейс AutoLot ****\n");

            //Получение строки подключения из App.config.
            string cnStr = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;
            bool userDone = false;
            string userCommand = "";

            //Создание объекта InventoryDAL().
            InventoryDAL invDAL = new InventoryDAL();
            invDAL.OpenConnection(cnStr);

            //Запросы пользовательских команд, пока не будет нажата клавиша <Q>.
            try
            {
                Showlnstructions();
                do
                {
                    Console.Write("\nВведите команду: ");
                    userCommand = Console.ReadLine();
                    Console.WriteLine();
                    switch (userCommand.ToUpper())
                    {
                        case "I":
                            InsertNewCar(invDAL);
                            break;
                        case "U":
                            UpdateCarPetName(invDAL);
                            break;
                        case "D":
                            DeleteCar(invDAL);
                            break;
                        case "L":
                            Listlnventory(invDAL); // Список  наличия автомобилей
                            break;
                        case "S":
                            Showlnstructions();
                            break;
                        case "P":
                            LookUpPetName(invDAL);
                            break;
                        case "Q":
                            userDone = true;
                            break;
                        default:
                            Console.WriteLine("Неверно! Попробуйте еще раз");
                            break;
                    }
                }
                while (!userDone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                invDAL.CloseConnection();
            }
        }

        private static void Showlnstructions()
        {
            Console.WriteLine("I: Добавление нового автомобиля.");
            Console.WriteLine("U: Изменение существующего автомобиля.");
            Console.WriteLine("D: Удаление существующего автомобиля.");
            Console.WriteLine("L: Вывод автомобилей в наличии.");
            Console.WriteLine("S: Вывод этих инструкций.");
            Console.WriteLine("Р: Вывод дружественного имени автомобиля.");
            Console.WriteLine("Q: Завершение работы.");
        }

        private static void Listlnventory(InventoryDAL invDAL)
        {
            // Вывод автомобилей в наличии.
            DataTable dt = invDAL.GetAllInventoryAsDataTable();

            // Передача DataTable вспомогательной функции для вывода. 
            DisplayTable(dt);
        }

        private static void DisplayTable(DataTable dt)
        {
            // Вывод имен столбцов.
            for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
            {
                Console.Write(dt.Columns[curCol].ColumnName.Trim() + "\t");
            }
            Console.WriteLine("\n--------------------------------------");

            // Вывод содержимого DataTable.
            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                {
                    Console.Write(dt.Rows[curRow][curCol].ToString().Trim() + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void DeleteCar(InventoryDAL invDAL)
        {
            // Получение идентификатора удаляемого автомобиля.
            Console.Write("Введите ID удаляемого автомобиля: " );
            int id = int.Parse(Console.ReadLine());
            
            //На случай нарушения ограничений первичного ключа!
            try
            {
                invDAL.DeleteCar(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }

        private static void InsertNewCar(InventoryDAL invDAL)
        {
            // Получение данных от пользователя. 
            int newCarlD;
            string newCarColor, newCarMake, newCarPetName;
            Console.Write("Введите ID автомобиля: ");
            newCarlD = int.Parse(Console.ReadLine());
            Console.Write("Введите цвет автомобиля: ");
            newCarColor = Console.ReadLine();
            Console.Write("Введите модели автомобиля: ");
            newCarMake = Console.ReadLine();
            Console.Write("Введите дружественного имени: ");
            newCarPetName = Console.ReadLine();

            // Передача информации в библиотеку доступа к данным. 
            invDAL.InsertAuto(newCarlD, newCarColor, newCarMake, newCarPetName);
        }

        private static void UpdateCarPetName(InventoryDAL invDAL)
        {
            // Получение данных от пользователя.
            int carID;
            string newCarPetName;
            Console.Write("Введите ID автомобиля: ");
            carID = int.Parse(Console.ReadLine());
            Console.Write("Введите новое дружественное имя: ");
            newCarPetName = Console.ReadLine();

            // Передача информации в библиотеку доступа к данным.
            invDAL.UpdateCarPetName(carID, newCarPetName);
        }

        private static void LookUpPetName(InventoryDAL invDAL)
        {
            // Получение идентификатора автомобиля. 
            Console.Write("Введите ID автомобиля: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Имя {0} - {1}.", id, invDAL.LookUpPetName(id));
        } 
    }
}
