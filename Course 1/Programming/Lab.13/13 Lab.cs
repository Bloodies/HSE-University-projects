using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._13
{
    class Program
    {
        public static int ReadInteger(int LowerLimit, int HigherLimit, string ErrorMessage)
        {
            int count;
            bool confirmed;

            do
            {
                confirmed = Int32.TryParse(Console.ReadLine(), out count);
                if ((count < LowerLimit) || (count > HigherLimit) || (confirmed != true))
                {
                    Console.WriteLine(ErrorMessage);
                }
            } while ((count < LowerLimit) || (count > HigherLimit) || (confirmed != true));
            return count;
        }
        static void WorkCollection(MyNewCollection c, Journal j)
        {
            int s = 0;

            do
            {
                Console.WriteLine("Что бы вы хотели сделать далее?");
                Console.WriteLine("1 - Заполнить коллекцию случайными элементами");
                Console.WriteLine("2 - Добавить элемент в коллекцию");
                Console.WriteLine("3 - Удалить элемент из коллекции");
                Console.WriteLine("4 - Подсчитать число элементов в коллекции");
                Console.WriteLine("5 - Отсортировать коллекцию по числу часов");
                Console.WriteLine("6 - Отсортировать коллекцию по числу минут");
                Console.WriteLine("7 - Очистить коллекцию");
                Console.WriteLine("8 - Просмотреть коллекцию");
                Console.WriteLine("9 - Просмотреть журнал событий для коллекции");
                Console.WriteLine("10 - Завершить работу с коллекцией");

                s = ReadInteger(1, 10, "Вы ввели неправильное число. Введите число заново.");

                switch (s)
                {
                    case 1:
                        {
                            Console.WriteLine("Введите число элементов, которые вы хотите добавить в коллекцию");
                            c.Fill(ReadInteger(1, 100, "Вы ввели неправильное число. Введите число заново."));
                            Console.WriteLine("Коллекция успешно заполнена.");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Введите число часов в добавляемом элементе");
                            int h = ReadInteger(0, 23, "Вы ввели неправильное число. Введите число заново.");
                            Console.WriteLine("Введите число минут в добавляемом элементе");
                            int m = ReadInteger(0, 59, "Вы ввели неправильное число. Введите число заново.");
                            c.Add(new Time(h, m));
                            Console.WriteLine("Элемент успешно добавлен");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Введите номер удаляемого элемента");
                            if (c.RemoveByNumber(ReadInteger(0, 999999, "")) == true)
                            {
                                Console.WriteLine("Элемент успешно удален");
                            }
                            else
                            {
                                Console.WriteLine("Элемента с таким номером нет в коллекции");
                            };


                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine($"В коллекции всего {c.Count()} элементов");
                            break;
                        }
                    case 5:
                        {
                            c.SortHours();
                            Console.WriteLine("Коллекция успешно отсортирована");
                            break;
                        }
                    case 6:
                        {
                            c.SortMinutes();
                            Console.WriteLine("Коллекция успешно отсортирована");
                            break;
                        }
                    case 7:
                        {
                            c.Clear();
                            Console.WriteLine("Коллекция успешно очищена");
                            break;
                        }
                    case 8:
                        {
                            c.Display();
                            break;
                        }
                    case 9:
                        {
                            j.Display();
                            break;
                        }
                    case 10:
                        {

                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Вы ввели неправильное число. Введите число заново.");
                            break;
                        }
                }
            } while (s != 10);
        }
        static void Main(string[] args)
        {
            MyNewCollection c1 = new MyNewCollection("Коллекция 1");
            MyNewCollection c2 = new MyNewCollection("Коллекция 2");

            Journal j1 = new Journal();
            c1.CollectionCountChanged += new CollectionHandler(j1.CollectionCountChanged);
            c1.CollectionReferenceChanged += new CollectionHandler(j1.CollectionReferenceChanged);

            Journal j2 = new Journal();
            c1.CollectionReferenceChanged += new CollectionHandler(j2.CollectionReferenceChanged);
            c2.CollectionReferenceChanged += new CollectionHandler(j2.CollectionReferenceChanged);

            Console.WriteLine("Здравствуйте. Вас приветствует программа, демонстрирующая работу обработчиков событий.");
            Console.WriteLine("Сейчас в программе созданы две коллекции типа <Time>, с которыми вы можете работать.");
            Console.WriteLine("Также в ней созданы по одному журналу событий для каждой из этих коллекций, которые вы в любой момент можете просмотреть. Первый журнал подписан на изменения числа элементов и изменения ссылок на элементы из первой коллекции; второй журнал подписан на события изменения числа элементов в обеих коллекциях");

            int s = 0;

            do
            {
                Console.WriteLine("Что бы вы хотели сделать далее?");
                Console.WriteLine("1 - Работать с первой коллекцией");
                Console.WriteLine("2 - Работать со второй коллекцией");
                Console.WriteLine("3 - Завершить работу с программой");

                s = ReadInteger(1, 3, "Вы ввели неправильное число. Введите число заново.");

                switch (s)
                {
                    case 1:
                        {
                            WorkCollection(c1, j1);
                            break;
                        }
                    case 2:
                        {
                            WorkCollection(c2, j2);
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Вы ввели неправильное число. Введите число заново.");
                            break;
                        }
                }
            } while (s != 3);
        }
    }
}