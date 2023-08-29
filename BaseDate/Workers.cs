using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    struct Workers
    {

        #region Поля по порядку записи
        private int id; //id

        private DateTime timeRecord; //время записи

        private string fullName; //ФИО

        private byte age; //возраст

        private byte height; //рост

        private DateTime birthday; //др

        private string bornPlace; //место рождения
        #endregion

        #region Свойства

        public int Id { 
            get { return this.id; } 
            set { this.id = value; }
        }

        public DateTime TimeRecord { get { return this.timeRecord; } }

        public string FullName 
        { 
            get { return this.fullName;}
            set { this.fullName = value; }
        }

        public byte Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        public byte Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public DateTime Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; }
        }

        public string BornPlace
        {
            get { return this.bornPlace; }
            set { this.bornPlace = value; }
        }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор целиком
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="timeRecord">Время записи</param>
        /// <param name="fullName">Ф.И.О.</param>
        /// <param name="age">Возраст</param>
        /// <param name="height">Рост</param>
        /// <param name="birthday">День рождения</param>
        /// <param name="bornPlace">Место рождения</param>
        public Workers(int id, DateTime timeRecord, string fullName, byte age, byte height, DateTime birthday, string bornPlace)
        {
            this.id = id;
            this.timeRecord = timeRecord;
            this.fullName = fullName;
            this.age = age;
            this.height = height;
            this.birthday = birthday;
            this.bornPlace = bornPlace;
        }

        /// <summary>
        /// Конструктор для заполнения нового сотрудника
        /// </summary>
        /// <param name="id"></param>
        public Workers(int id) : this (0, new DateTime(1900, 01, 01), String.Empty, 0, 0, new DateTime(1900,01,01), String.Empty)
        {
            this.id = id;

            Console.Write("Введите Ф.И.О.: ");
            this.fullName = Console.ReadLine();

            Console.Write("Введите возраст: ");
            this.age = Convert.ToByte(Console.ReadLine());

            Console.Write("Введите рост: ");
            this.height = Convert.ToByte(Console.ReadLine());

            Console.Write("Введите день рождения: ");
            this.birthday = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Введите место рождения: ");
            this.bornPlace = Console.ReadLine();

            this.timeRecord = DateTime.Now;
        }

        /// <summary>
        /// Конструктор для получения даты создания записи
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="timeRecord">Время записи</param>
        public Workers(int id, DateTime timeRecord) : this(id, timeRecord, String.Empty, 0, 0, new DateTime(1900, 01, 01), String.Empty)
        {
        }

        #endregion

        #region Методы

        /// <summary>
        /// Печатать только что введенную информацию
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{id} {timeRecord} {fullName} {age} {height} {birthday.ToShortDateString()} {bornPlace}";
        }

        /// <summary>
        /// При пересоздании таблицы после удаления файла из ID добавляем решетки, сделано для удобности
        /// </summary>
        /// <param name="plus">Указываем -1 в том случае, если после удаляемого ID нам надо пересчитать в таблице ID и пересохранить их, иначе 0</param>
        /// <returns></returns>
        public string WriteOrder(int plus)
        {
            return $"{id+plus}#{timeRecord}#{fullName}#{age}#{height}#{birthday.ToShortDateString()}#{bornPlace}";
        }

        /// <summary>
        /// Метод сортировки по возрасту
        /// </summary>
        /// <param name="workers1"></param>
        /// <param name="workers2"></param>
        /// <returns></returns>
        public static int CompareByAge(Workers workers1, Workers workers2)
        {
            return workers1.Age.CompareTo(workers2.Age);
        }

        /// <summary>
        /// Метод сортировки по дню рождению
        /// </summary>
        /// <param name="workers1"></param>
        /// <param name="workers2"></param>
        /// <returns></returns>
        public static int CompareByBirthday(Workers workers1, Workers workers2)
        {
            return workers1.Birthday.CompareTo(workers2.Birthday);
        }


        #endregion

    }
}