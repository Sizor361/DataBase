using DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class Program
    {
        /// <summary>
        /// Проверка на существованме фалйа и создание если нету
        /// </summary>
        /// <param name="path">Путь файла</param>
        static void CheckOnExist(string path)
        {

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        /// <summary>
        /// Сортировка по возрасту
        /// </summary>
        /// <param name="repository">Передаем сюда экземпляр репозитория</param>
        /// <param name="inputSymbol">Символ при выборе меню</param>
        static void SortByAge(Repository repository, char inputSymbol)
        {
            Console.Clear();

            repository.Load();

            Workers[] workers = repository.Load();

            Array.Resize(ref workers, repository.index);
            Array.Sort(workers, Workers.CompareByAge);

            for (int i = 0; i < repository.index; i++)
            {
                Console.WriteLine(workers[i].Print());
            }

            Console.WriteLine("\nНажмите 1 чтобы выйти назад!");

            do
            {
                inputSymbol = Convert.ToChar(Console.ReadLine());

            } while (inputSymbol != '1');

        }

        /// <summary>
        /// Сортировка по дате рождения
        /// </summary>
        /// <param name="repository">Передаем сюда экземпляр репозитория</param>
        /// <param name="inputSymbol">Символ при выборе меню</param>
        static void SortByBirthday(Repository repository, char inputSymbol)
        {
            Console.Clear();

            repository.Load();

            Workers[] workers = repository.Load();

            Array.Resize(ref workers, repository.index);
            Array.Sort(workers, Workers.CompareByBirthday);

            for (int i = 0; i < repository.index; i++)
            {
                Console.WriteLine(workers[i].Print());
            }

            Console.WriteLine("\nНажмите 1 чтобы выйти назад!");

            do
            {
                inputSymbol = Convert.ToChar(Console.ReadLine());

            } while (inputSymbol != '1');

        }

        /// <summary>
        /// Всё меню (очень большое, знаю)
        /// </summary>
        /// <param name="repository">Ссылка на экземпляр репозитория</param>
        static void Menu(Repository repository)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ В ГЛАВНОЕ МЕНЮ!!!\n" +
                    "1 - Прочитать всю базу данных\n" +
                    "2 - Сделать новую запись\n" +
                    "3 - Поиск записей в базе данных\n" +
                    "4 - Удаление записей из базы данных\n" +
                    "5 - Сортировка записей в базе данных");

                char inputSymbol = Convert.ToChar(Console.ReadLine());

                switch (inputSymbol)
                {
                    case '1':
                        Console.Clear();

                        Console.WriteLine("ID   Дата создания записи   Ф.И.О.   Возраст   Вес   День рождение   Место рождения\n");

                        repository.ShowAllRecords();

                        Console.WriteLine("\n1 - выйти в главное меню");

                        do
                        {
                            inputSymbol = Convert.ToChar(Console.ReadLine());

                        } while (inputSymbol != '1');

                        break;

                    case '2':

                        do
                        {
                            Console.Clear();

                            repository.CreateNewRecord();

                            Console.WriteLine("\nЗапись успешно создана!!!\n" +
                                "1 - Выйти в главное меню\n" +
                                "2 - Сделать ещё одну запись");

                            inputSymbol = Convert.ToChar(Console.ReadLine());

                            if (inputSymbol == '1')
                            {
                                break;
                            }

                            else if (inputSymbol == '2')
                            {
                                continue;
                            }


                        } while (true);

                        break;

                    case '3':
                        bool exit = false;
                        do
                        {
                            Console.Clear();

                            Console.WriteLine("По какому атрибуту ищем?\n" +
                                "1 - ID\n" +
                                "2 - Датам создания записи\n" +
                                "3 - Выйти в главное меню");

                            inputSymbol = Convert.ToChar(Console.ReadLine());

                            switch (inputSymbol)
                            {
                                case '1':

                                    Console.Clear();

                                    Console.Write("Введите ID по которому вы ищите запись - ");

                                    int id = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine();

                                    repository.GetWorkerById(id);

                                    Console.WriteLine("\n1 - назад");

                                    do
                                    {
                                        inputSymbol = Convert.ToChar(Console.ReadLine());

                                    } while (inputSymbol != '1');

                                    break;

                                case '2':

                                    Console.Clear();

                                    Console.Write("Введите начало даты по которой ищите (дд.мм.гггг. чч.мм.сс) - ");

                                    DateTime min = Convert.ToDateTime(Console.ReadLine());

                                    Console.Write("Введите конец даты по которой ищите (дд.мм.гггг. чч.мм.сс) - ");

                                    DateTime max = Convert.ToDateTime(Console.ReadLine());

                                    if (min == max)
                                    {
                                        max = max.AddDays(1);
                                    }

                                    Console.WriteLine();

                                    repository.SearchDataRecord(min, max);

                                    Console.WriteLine("\n1 - назад");

                                    do
                                    {
                                        inputSymbol = Convert.ToChar(Console.ReadLine());

                                    } while (inputSymbol != '1');

                                    break;


                                case '3':
                                    exit = true;
                                    break;
                            }

                        } while (!exit);


                        break;

                    case '4':
                        Console.Clear();

                        Console.Write($"Введите ID запись которую хотите удалить - ");

                        int idForDelete = Convert.ToInt32(Console.ReadLine());

                        repository.DeleteWorker(idForDelete);

                        Console.WriteLine("1 - вернуться назад");

                        do
                        {
                            inputSymbol = Convert.ToChar(Console.ReadLine());

                        } while (inputSymbol != '1');


                        break;

                    case '5':

                        bool switcher = false;

                        do
                        {

                            Console.Clear();

                            Console.WriteLine("По какому полю отсортировать?\n" +
                                   "1 - Возрасту \n" +
                                   "2 - Дате рождения\n" +
                                   "3 - Выйти в главное меню");

                            inputSymbol = Convert.ToChar(Console.ReadLine());

                            switch (inputSymbol)
                            {
                                case '1':

                                    SortByAge(repository, inputSymbol);

                                    break;

                                case '2':

                                    SortByBirthday(repository, inputSymbol);

                                    break;

                                case '3':

                                    switcher = true;

                                    break;
                            }

                        } while (!switcher);
                        break;
                }

            }

        }

        static void Main(string[] args)
        {
            string path = "DataBase.txt";

            CheckOnExist(path);

            Repository repository = new Repository(path);

            Menu(repository);
        }
    }
}