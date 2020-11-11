using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._10
{
    class person
    {
        protected int BirthYear;
        protected int BirthMonth;
        protected int BirthDay;
        protected string name;
        protected string surname;

        public person()
        {
            name = "Не указано";
            surname = "Не указано";
            BirthYear = 1950;
            BirthMonth = 1;
            BirthDay = 1;
        }
        public person(string Name, string Surname, int birthYear, int birthMonth, int birthDay)
        {
            name = Name;
            surname = Surname;
            BirthYear = birthYear;
            BirthMonth = birthMonth;
            BirthDay = birthDay;
        }
        virtual public void Display()
        {
            Console.Write("Человек по имени ");
            Console.Write(name);
            Console.Write(" ");
            Console.Write(surname);
            Console.Write(", род. ");
            Console.Write(BirthDay);
            Console.Write(".");
            Console.Write(BirthMonth);
            Console.Write(".");
            Console.WriteLine(BirthYear);
            Console.WriteLine("");
        }
        public int GetBirthYear()
        {
            return BirthYear;
        }
        public string GetName()
        {
            return name;
        }
        public string GetSurname()
        {
            return surname;
        }
    }
    class student : person
    {
        private double AverageMark;
        private int CodeNumber;
        private string speciality;
        private int year;

        public student() : base()
        {
            AverageMark = 0;
            CodeNumber = 0;
            Speciality = "Не указана";
            year = 1;
        }
        public student(string Name, string Surname, int birthYear, int birthMonth, int birthDay, double averageMark, int codeNumber, string Speciality, int Year) : base(Name, Surname, birthYear, birthMonth, birthDay)
        {
            AverageMark = averageMark;
            CodeNumber = codeNumber;
            speciality = Speciality;
            year = Year;
        }
        override public void Display()
        {
            Console.Write("Студент ");
            Console.Write(name);
            Console.Write(" ");
            Console.Write(surname);
            Console.Write(", род. ");
            Console.Write(BirthDay);
            Console.Write(".");
            Console.Write(BirthMonth);
            Console.Write(".");
            Console.Write(BirthYear);
            Console.Write(", студент ");
            Console.Write(year);
            Console.Write(" курса специальности ''");
            Console.Write(speciality);
            Console.Write("'', средний балл - ");
            Console.Write(AverageMark);
            Console.Write(", личный код - ");
            Console.WriteLine(CodeNumber);
            Console.WriteLine("");
        }
        public int GetYear()
        {
            return year;
        }
        public string GetSpeciality()
        {
            return speciality;
        }
        public string Speciality
        {
            get
            {
                return speciality;
            }
            set
            {

            }
        }
    }
    class staff : person
    {
        protected int CodeNumber;
        protected string occupation;
        protected double salary;

        public staff() : base()
        {
            CodeNumber = 0;
            occupation = "Не указана";
            salary = 0;
        }
        public staff(string Name, string Surname, int birthYear, int birthMonth, int birthDay, int codeNumber, string Occupation, int Salary) : base(Name, Surname, birthYear, birthMonth, birthDay)
        {
            CodeNumber = codeNumber;
            occupation = Occupation;
            salary = Salary;
        }
        override public void Display()
        {
            Console.Write("Сотрудник ");
            Console.Write(name);
            Console.Write(" ");
            Console.Write(surname);
            Console.Write(", род. ");
            Console.Write(BirthDay);
            Console.Write(".");
            Console.Write(BirthMonth);
            Console.Write(".");
            Console.Write(BirthYear);
            Console.Write(", должность - ");
            Console.Write(occupation);
            Console.Write(", зарплата - ");
            Console.Write(salary);
            Console.Write(", личный код - ");
            Console.WriteLine(CodeNumber);
            Console.WriteLine("");
        }
    }
    class teacher : staff
    {
        protected string Speciality;
        public teacher() : base()

        {
            Speciality = "Не указана";
        }
        public teacher(string Name, string Surname, int birthYear, int birthMonth, int birthDay, int codeNumber, string Occupation, int Salary, string speciality) : base(Name, Surname, birthYear, birthMonth, birthDay, codeNumber, Occupation, Salary)

        {
            Speciality = speciality;
        }
        override public void Display()
        {
            Console.Write("Преподаватель ");
            Console.Write(name);
            Console.Write(" ");
            Console.Write(surname);
            Console.Write(", род. ");
            Console.Write(BirthDay);
            Console.Write(".");
            Console.Write(BirthMonth);
            Console.Write(".");
            Console.Write(BirthYear);
            Console.Write(" по предмету ''");
            Console.Write(Speciality);
            Console.Write("'', зарплата - ");
            Console.Write(salary);
            Console.Write(", личный код - ");
            Console.WriteLine(CodeNumber);
            Console.WriteLine("");
        }
    }
    class Program
    {
        static void Search1(person[] Collection)  //Поиск имен студентов с выбранного курса
        {
            int Year;
            bool ok;
            bool found = false;
            student Student = new student();

            Console.WriteLine("Введите номер курса, имена студентов с которого вы хотите найти");
            do
            {
                ok = Int32.TryParse(Console.ReadLine(), out Year);
                if (!ok)
                {
                    Console.WriteLine("Введенное вами число не является целым. Пожалуйста, введите другое");
                }
            } while (!ok);

            foreach (person Person in Collection)

            {
                Student = Person as student;

                if (Student != null)
                {
                    if (Year == Student.GetYear())
                    {
                        Console.WriteLine(Student.GetName());
                        found = true;
                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine("На выбранном курсе нет ни одного студента");
            }
        }
        static void Search2(person[] Collection)  //Поиск фамилий студентов с выбранной специальности
        {
            string Speciality;
            bool ok;
            bool found = false;
            student Student = new student();

            Console.WriteLine("Введите специальность, фамилии студентов с которой вы хотите найти");

            Speciality = Console.ReadLine();


            foreach (person Person in Collection)

            {
                Student = Person as student;

                if (Student != null)
                {
                    if (Speciality == Student.GetSpeciality())
                    {
                        Console.WriteLine(Student.GetSurname());
                        found = true;
                    }
                }
            }
            if (found == false)
            {
                Console.WriteLine("Выбранная специальность не существует");
            }
        }
        static void Search3(person[] Collection)  //Поиск имен и фамилий людей выбранного года рождения
        {
            int Year;
            bool ok;
            bool found = false;
            student Student = new student();

            Console.WriteLine("Введите год рождения, имена и фамилии людей которого вы хотите найти");
            do
            {
                ok = Int32.TryParse(Console.ReadLine(), out Year);
                if (!ok)
                {
                    Console.WriteLine("Введенное вами число не является целым. Пожалуйста, введите другое");
                }

            } while (!ok);

            foreach (person Person in Collection)

            {
                if (Year == Person.GetBirthYear())
                {
                    Console.Write(Person.GetName());
                    Console.Write(" ");
                    Console.WriteLine(Person.GetSurname());
                    found = true;
                }
            }
            if (found == false)
            {
                Console.WriteLine("На выбранном курсе нет ни одного студента");
            }
        }
        static void Main(string[] args)
        {
            person[] Collection = new person[13];

            Collection[0] = new student("Иван", "Иванов", 1997, 12, 27, 4.33, 123907, "Программная инженерия", 3);
            Collection[1] = new staff("Елена", "Кузнецова", 1990, 12, 27, 765234, "Уборщица", 31500);
            Collection[2] = new staff("Александр", "Александров", 1985, 1, 9, 452433, "Системный администратор", 55000);
            Collection[3] = new student("Петр", "Петров", 1999, 6, 11, 2.5, 123255, "Бизнес-информатика", 1);
            Collection[4] = new teacher("Ирина", "Калашникова", 1978, 3, 14, 887431, "Преподаватель", 75000, "Программирование");
            Collection[5] = new student("Сергей", "Сергеев", 1995, 11, 11, 3.31, 123552, "История", 5);
            Collection[6] = new teacher("Алексей", "Алексеев", 1969, 7, 22, 439017, "Преподаватель", 95000, "Компьютерный практикум");
            Collection[7] = new student("Макар", "Макаров", 1995, 11, 11, 3.31, 123552, "История", 5);
            Collection[8] = new student("Абдул", "Абдулов", 1995, 7, 23, 1.81, 123552, "История", 5);
            Collection[9] = new student("Денис", "Денисов", 1997, 8, 13, 5, 123553, "Программная инженерия", 3);
            Collection[10] = new student("Максим", "Максимов", 1996, 5, 30, 2.92, 123554, "Бизнес-информатика", 4);
            Collection[11] = new student("Матвей", "Матвеев", 1999, 6, 22, 3.8, 123890, "История", 1);
            Collection[12] = new student("Никита", "Никитин", 1999, 2, 19, 4.1, 123567, "Программная инженерия", 1);

            for (int i = 0; i < Collection.Length; i++)
            {
                Collection[i].Display();
            }

            int SwitchNumber = 0;
            bool ok;
            do
            {
                Console.WriteLine("Что вы хотите сделать с данной коллекцией?");
                Console.WriteLine("1 - Найти имена всех студентов на выбранном курсе");
                Console.WriteLine("2 - Найти фамилии всех студентов с выбранной специальности");
                Console.WriteLine("3 - Найти имена и фамилии всех людей выбранного года рождения");
                Console.WriteLine("4 - Завершить работу с программой");
                do
                {
                    ok = Int32.TryParse(Console.ReadLine(), out SwitchNumber);
                    if ((!ok) || (SwitchNumber < 1) || (SwitchNumber > 4))
                    {
                        Console.WriteLine("Введенное вами число не соответствует ни одному из предложенных вариантов. Пожалуйста, введите другое");
                    }

                } while ((!ok) || (SwitchNumber < 1) || (SwitchNumber > 4));

                switch (SwitchNumber)
                {
                    case 1:
                        {
                            Search1(Collection);
                            break;
                        }

                    case 2:
                        {
                            Search2(Collection);
                            break;
                        }

                    case 3:
                        {
                            Search3(Collection);
                            break;
                        }

                    case 4:
                        {
                            break;
                        }
                }
            } while (SwitchNumber != 4);
        }
    }
}