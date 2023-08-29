using DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    class Repository
    {

        #region Поля

        Workers[] workers;

        private string path;

        public int index;

        #endregion

        #region Конструкторы

        public Repository(string path)
        {
            this.path = path;
            this.index = 0;
            this.workers = new Workers[5];
        }

        #endregion

        #region Все методы

        /// <summary>
        /// Проверка на заполненности массива и при заполненности его - расширение в 2 раза
        /// </summary>
        /// <param name="flag">Условие проверки заполненности массива</param>
        public void Resize(bool flag)
        {
            if (flag)
            {
                Array.Resize(ref this.workers, this.workers.Length * 2);
            }
        }

        /// <summary>
        /// Заполняем и сохраняем нового работника в базу данных
        /// </summary>
        public void NewWorker()
        {
            Workers newWorker = new Workers(index);

            using (StreamWriter sw = new StreamWriter(this.path, true))
            {
                sw.WriteLine($"{Convert.ToString(newWorker.Id)}#{newWorker.TimeRecord}#{newWorker.FullName}#" +
                    $"{newWorker.Age}#{newWorker.Height}#{newWorker.Birthday.ToShortDateString()}#{newWorker.BornPlace}#");
                sw.Close();
            }
        }

        /// <summary>
        /// При загрузке базы данных проверяем массив на заполняемость, присваемаем записи работника индекс и инкремируем его
        /// </summary>
        /// <param name="workers"></param>
        public void AddWorker(Workers workers)
        {
            this.Resize(index >= this.workers.Length);
            this.workers[index] = workers;
            this.index++;
        }

        /// <summary>
        /// Обновление базы данных
        /// </summary>
        public Workers[] Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                index = 0;

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    AddWorker(new Workers(Convert.ToInt32(index), 
                        Convert.ToDateTime(args[1]), args[2], Convert.ToByte(args[3]), 
                        Convert.ToByte(args[4]), Convert.ToDateTime(args[5]), args[6]));
                }
                sr.Close();
            }

            return workers;
        }

        /// <summary>
        /// Показываем базу данных заранее загруженную
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < index; i++)
            {
                Console.Write(this.workers[i].Print());
                Console.WriteLine();
            }
        }   

        /// <summary>
        /// Ищем по ID
        /// </summary>
        /// <param name="id">Передаем ID по которому ищем запись</param>
        public void GetWorkerById (int id)
        {
            Load();

            Array.Resize(ref workers, index);

            if (id< index)
            {
                Console.Write(this.workers[id].Print());
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Такого ID не существует");
            }
            
        }

        /// <summary>
        /// Выводим диапазон дат, записанных в определенное время
        /// </summary>
        /// <param name="min">С этой даты</param>
        /// <param name="max">До этой</param>
        public void GetWorkerByTimeRecord(DateTime min, DateTime max)
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                bool print = false;

                Workers[] workersForTimeRecord = new Workers[this.workers.Length];

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    new Workers(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]));

                    if (min <= Convert.ToDateTime(args[1]) && max >= Convert.ToDateTime(args[1]))
                    {
                        Console.WriteLine(this.workers[Convert.ToInt32(args[0])].Print());
                        print = true;
                    }
                }

                if (!print)
                {
                    Console.WriteLine("Нет не одной записи удовлетворяющей запрашиваемому поиску!");
                }

                sr.Close();
            }

        }

        /// <summary>
        /// Удаляем работника по его id
        /// </summary>
        /// <param name="id">Передаем ID запись которую удаляем</param>
        public void DeleteWorkerById(int id)
        {

            if (id <= index)
            {

                using (StreamWriter sw = new StreamWriter(this.path))
                {
                    for (int i = 0; i < index; i++)
                    {
                        if (i == id)
                        {
                            continue;
                        }
                        else if (i < id)
                        {
                            sw.WriteLine(workers[i].WriteOrder(0));
                        }
                        else
                        {
                            sw.WriteLine(workers[i].WriteOrder(-1));
                        }
                    }

                    sw.Close();
                }

                Console.WriteLine("\nУдаление прошло успешно!!!");
            }
            else
            {
                Console.WriteLine("Такого ID не существует");
            }

        }

        #endregion

        #region Составные методы (инкапсуляция)

        /// <summary>
        /// Показывает все записи
        /// </summary>
        public void ShowAllRecords()
        {
            Load();
            Print();
        }

        /// <summary>
        /// Создаем новую запись
        /// </summary>
        public void CreateNewRecord()
        {
            Load();
            NewWorker();
        }

        /// <summary>
        /// Поиск по двум значениям, например дате создания
        /// </summary>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        public void SearchDataRecord(DateTime min, DateTime max)
        {
            Load();
            GetWorkerByTimeRecord(min, max);
        }

        /// <summary>
        /// Удаляем работника из БД
        /// </summary>
        /// <param name="id"></param>
        public void DeleteWorker(int id)
        {
            Load();

            DeleteWorkerById(id);
        }

        #endregion

    }
}