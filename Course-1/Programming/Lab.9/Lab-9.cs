using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

// Лабораторная работа учащегося ПИ-18-2
// Чепоков Елизар
// Вариант 23 (8)

/* Задание:
 * 
 * Часть 1:
 *   1.	Реализовать (в отдельном файле) определение нового класса (закрытые атрибуты, свойства, конструкторы, инициализация и вывод атрибутов).
 *   2.	Для демонстрации работы с объектами написать главную функцию, в которой создаются объекты класса и выводится информация, которая содержится в атрибутах.
 *   3.	Написать функцию, реализующую указанное в варианте действие и продемонстрировать работу функции. Рассмотреть два варианта:
 *       1) статическую функцию; 
 *       2) метод класса;
 *   4.	Используя статическую компоненту класса подсчитать количество созданных в программе объектов.
 *   
 * Часть 2:
 *   1.	Добавить к реализованному классу указанные в варианте перегруженные операции.
 *   2.	Написать демонстрационную программу, в которой создаются объекты пользовательских классов и выполняются указанные операции.
 *
 * Часть 3: 
 *   1.	Реализовать класс (в отдельном файле), полем которого является одномерный массив из элементов заданного в варианте типа. 
 *      Например, для класса Fraction нужно создать класс FractionArray следующим образом:
 *      class FractionArray
 *      {
 *          Fraction[] arr;
 *          int size;
 *           . . . .
 *      }
 *      В классе реализовать:
 *        •	конструктор без параметров;
 *        •	конструктор с параметрами, заполняющий элементы случайными значениями;
 *        •	конструктор с параметрами, позволяющий заполнить массив элементами, заданными пользователем с клавиатуры;
 *        •	индексатор (для доступа к элементам массива);
 *        •	метод для просмотра элементов массива;
 *   2.	Написать демонстрационную программу, позволяющую создать массив разными способами и распечатать элементы массива. 
 *      Подсчитать количество созданных объектов.
 *   3.	Выполнить указанное в варианте задание (если необходимо, перегрузить нужные для выполнения задачи операции или функции).
 */

/* Задание по варианту (8):
 * 
 * Часть 1:
 *   Класс:
 *     Money
 *     
 *   Атрибуты: 
 *     int rubles 
 *     int kopeks
 *     
 *   Методы:
 *     Вычитание  переменной типа Money (учесть, что рублей и копеек  не может быть меньше 0). Результат должен быть типа Money.
 * 
 * Часть 2:
 *   Название класса:
 *     Money
 *     
 *   Методы:  
 *     Унарные операции:
 *        ++  добавление копейки к объекту типа Money (учесть, что копеек не может быть больше 99).
 *        --  вычитание копейки из объекта типа Money (учесть, что копеек и рублей не может быть меньше 0).
 *     Операции приведения типа:
 *        int (неявная) – результатом является количество рублей (копейки отбрасываются);
 *        double (явная) – результатом является копейки, рубли отбрасываются, результат <1.
 *     Бинарные операции:
 *        - Money m, целое число (лево- и право- сторонние операции), результат должен быть типа Money.
 *        - Money m, Money m, результат должен быть типа Money.
 *        Результат не может быть отрицательным.
 *        
 * Часть 3:
 *   Название класса:
 *     Money
 *     
 *   Задание:  
 *     Найти максимальное значение
 */

namespace Lab._9
{
    class Search_for_Bugs
    {
        public static int ProverkaVvoda()
        {
            int number;
            bool res;
            do
            {
                res = int.TryParse(Console.ReadLine(), out number);

                if (res == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка, некорректный ввод");
                    Console.ResetColor();
                }
            } while (!res);
            return number;
        }
    }
    class Money
    {
        static int count = 0;
        int rubles;
        int kopeks;

        public int Rubles
        {
            set
            {
                if (value >= 0)
                {
                    rubles = value;
                }
                else
                {
                    rubles = 0;
                }
            }
            get { return rubles; }
        }
        public int Kopeks
        {
            set
            {
                if (value < 100)
                {
                    if (value < 0)
                    {
                        kopeks = 0;
                    }
                    else
                    {
                        kopeks = value;
                    }

                }
                else
                {
                    kopeks = value % 100;
                    rubles = rubles + ((value - kopeks) / 100);
                }
            }
            get { return kopeks; }
        }
        public Money(int irubles, int ikopeks)
        {
            Rubles = irubles;
            Kopeks = ikopeks;
            count++;
        }
        public Money()
        {
            Rubles = 0;
            Kopeks = 0;
            count++;
        }
        public void MoneyShow(/*Money m1*/)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(Rubles);
            Console.Write(".");
            if (Kopeks > 9)
            {
                Console.Write(Kopeks);
                Console.ResetColor();
            }
            else
            {
                Console.Write(0);
                Console.Write(Kopeks);
                Console.ResetColor();
            }
        }
        public void CountObjects()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Всего было создано ");
            Console.Write(count);
            Console.Write(" объектов класса Money");
            Console.ResetColor();
        }
        public Money Minus(/*Money m1, */Money m2)
        {
            Money m3 = new Money();
            int temp1, temp2, temp3;
            temp1 = this.Rubles * 100 + this.Kopeks;

            //temp1 = m1.Rubles * 100 + m1.Kopeks;
            temp2 = m2.Rubles * 100 + m2.Kopeks;

            temp3 = temp1 - temp2;

            m3.Kopeks = temp3 % 100;
            m3.Rubles = (temp3 - m3.kopeks) / 100;

            return m3;
        }
        public static Money operator ++(Money m1)
        {
            m1.Kopeks++;

            return m1;
        }
        public static Money operator --(Money m1)
        {
            //  Money m2 = new Money();

            if (m1.Kopeks == 0)
            {
                m1.Kopeks = 99;
                m1.Rubles--;
            }
            else
            {
                m1.Kopeks++;
            }
            return m1;
        }
        public int RublesInt(Money m1)
        {
            return m1.Rubles;
        }
        public double KopeksDouble(Money m1)
        {
            return ((Convert.ToDouble(m1.kopeks)) / 100);
        }
        public static Money operator +(Money m1, int irubles)
        {
            Money m2 = new Money();

            m1.Rubles = m1.Rubles + irubles;
            m2 = m1;

            return m2;
        }
        //public static Money operator +(int irubles, Money m1)
        //{
        //    Money m2 = new Money();
        //    m1.Rubles = m1.Rubles + irubles;
        //    m2 = m1;
        //    return m2;
        //}
        //public static Money operator -(Money m1, int irubles)
        //{
        //    Money m2 = new Money();
        //    m1.Rubles = m1.Rubles - irubles;
        //    m2 = m1;
        //    return m2;
        //}
        public static Money operator -(int irubles, Money m1)
        {
            Money m2 = new Money();
            int temp1, temp2, temp3;

            temp1 = irubles * 100;
            temp2 = m1.Rubles * 100 + m1.Kopeks;
            temp3 = temp1 - temp2;
            m2.Kopeks = temp3 % 100;
            m2.Rubles = (temp3 - m2.kopeks) / 100;
            return m2;
        }
        public static Money Minus(Money m1, Money m2)
        {
            Money m3 = new Money();
            int temp1, temp2, temp3;

            temp1 = m1.Rubles * 100 + m1.Kopeks;
            temp2 = m2.Rubles * 100 + m2.Kopeks;
            temp3 = temp1 - temp2;
            m3.Kopeks = temp3 % 100;
            m3.Rubles = (temp3 - m3.Kopeks) / 100;
            return m3;
        }
    }
    class MoneyArray
    {
        Money[] money;
        int size;
        public static int count = 0;

        public Money this[int i]
        {
            get { return money[i]; }
            set { money[i] = value; }
        }
        public MoneyArray()
        {
            Random random = new Random();

            size = random.Next(1, 10);
            money = new Money[size];

            for (int i = 0; i < size; i++)
            {
                money[i] = new Money();
                count++;
            }
        }
        public MoneyArray(int size)
        {
            Random random = new Random();

            int SwitchNumber = 0;
            bool ok = false;

            int rubles;
            int kopecks;

            money = new Money[size];

            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Как сформировать массив:   |");
            Console.WriteLine("| 1) Вручную                 |");
            Console.WriteLine("| 2) Рандомно                |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
            do
            {
                ok = Int32.TryParse(Console.ReadLine(), out SwitchNumber);
                if ((SwitchNumber < 1) || (SwitchNumber > 2) || (!ok))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Нужно выбрать из списка!");
                    Console.ResetColor();
                }
            } while ((SwitchNumber < 1) || (SwitchNumber > 2) || (!ok));

            for (int i = 0; i < size; i++)
            {
                switch (SwitchNumber)
                {
                    case 1:
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Введите число рублей");
                            Console.ResetColor();
                            do
                            {
                                ok = Int32.TryParse(Console.ReadLine(), out rubles);
                                if ((rubles < 0) || (!ok))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Пожалуйста, введите целое число рублей!");
                                    Console.ResetColor();
                                }
                            } while ((rubles < 0) || (!ok));

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Введите число копеек");
                            Console.ResetColor();
                            do
                            {
                                ok = Int32.TryParse(Console.ReadLine(), out kopecks);
                                if ((kopecks < 0) || (!ok))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Пожалуйста, введите целое число копеек!");
                                    Console.ResetColor();
                                }
                            } while ((kopecks < 0) || (!ok));
                            money[i] = new Money(rubles, kopecks);
                            count++;
                            break;
                        }
                    case 2:
                        rubles = random.Next(0, 99);
                        kopecks = random.Next(0, 99);
                        money[i] = new Money(rubles, kopecks);
                        count++;
                        break;
                }
            }
        }
        public int GreatestElementIndex()
        {
            double max = Convert.ToDouble(money[0].RublesInt(money[0])) + money[0].KopeksDouble(money[0]);
            double temp;
            int maxindex = 0;

            if (money.Length != 0)
            {
                for (int i = 1; i < money.Length; i++)
                {
                    temp = Convert.ToDouble(money[i].RublesInt(money[i])) + money[i].KopeksDouble(money[i]);
                    if (temp > max)
                    {
                        max = temp;
                        maxindex = i;
                    }
                }
            }
            return maxindex;
        }
    }
    class Program
    {
        static void Task_1_2()
        {
            Console.Clear();

            Money m1 = new Money(10, 0);
            Money m2 = new Money(5, 99);
            Money m3 = new Money();

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine("Для начала, создадим три объекта класса Money:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Money m1 = ");
            m1.MoneyShow();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Money m2 = ");
            m2.MoneyShow();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Money m3 = ");
            m3.MoneyShow();
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine("Присвоим m3 значение выражения m1 - m2");
            Console.WriteLine("Методом класса:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("m3 = ");
            m3 = m1.Minus(m2);
            // m3 = m3.Minus(m1, m2);
            m3.MoneyShow();
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine("Статической функцией:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("m3 = ");
            m3 = Money.Minus(m1, m2);
            m3.MoneyShow();
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine("Ничего не изменилось - метод класса и статическая функция работают одинаково.");

            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine("Теперь поработаем с унарными операциями:");
            Console.WriteLine("Вычтем 1 копейку из объекта m1:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("m1 = ");
            m1--;
            m1.MoneyShow();
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine("Прибавим 1 копейку к объекту m2:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("m2 = ");
            m2++;
            m2.MoneyShow();
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine("Операции приведения:");
            Console.WriteLine("Получим целое число рублей в m2:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("В m2 всего ");
            Console.Write(m2.RublesInt(m2));
            Console.WriteLine(" рублей.");
            Console.ResetColor();

            Console.WriteLine("Получим число копеек (рубли отбрасываются) в m1 в формате double:");
            Console.Write("В m1, если отбросить целые рубли, останется всего ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(m1.KopeksDouble(m1));
            Console.WriteLine(" рублей");
            Console.ResetColor();

            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine("Бинарные операции: Создадим еще два объекта класса Money - m4 и m5:");
            Money m4 = new Money();
            Money m5 = new Money();

            Console.WriteLine("Теперь присвоим m4 значение m1 + 5 (программа интерпретирует 5 как число рублей):");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("m4 = ");
            m4 = m1 + 5;
            m4.MoneyShow();
            Console.WriteLine();

            Console.WriteLine("И присвоим m5 значение 15 - m2 (программа интерпретирует 15 как число рублей):");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("m5 = ");
            m5 = 15 - m2;
            m5.MoneyShow();
            Console.WriteLine();

            Console.WriteLine("Если вычитать большее количество рублей чем есть в m2, то выводится 0.00 (Например 0 - m2)");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("m5 = ");
            m5 = 0 - m2;
            m5.MoneyShow();
            Console.WriteLine();

            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine("Теперь программа сосчитает, сколько объектов класса Money было создано за время ее работы.");
            m5.CountObjects();
            Console.WriteLine();
            Console.WriteLine("Пять из этих объектов были созданы в теле программы вручную, и еще по одному за каждую из четырех проделанных операций, не считая унарных. 5 + 4 = 9");
            Console.WriteLine();
            Console.Write("Нажмите любую клавишу чтобы выйти...");
            Console.ReadKey();
        }
        static void Task_3()
        {
            Console.ForegroundColor = ConsoleColor.White;

            bool ok = false;
            int size = 0;
            MoneyArray arr = null;

            Console.Clear();
            do
            {
                Console.WriteLine("\n----------------------------------------------------");
                Console.WriteLine("| Выберите следующее действие:                     |");
                Console.WriteLine("| 1) Создать массив Money                          |");
                Console.WriteLine("| 2) Вывести массив на экран                       |");
                Console.WriteLine("| 3) Найти максимальное значение массива           |");
                Console.WriteLine("| 4) Найти количество созданных элементов массива  |");
                Console.WriteLine("| 0) Выход из консоли                              |");
                Console.WriteLine("----------------------------------------------------");
                Console.Write("Действие: ");
                int SwitchNumber = Search_for_Bugs.ProverkaVvoda();
                switch (SwitchNumber)
                {
                    case 1:
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Введите размер массива");
                            Console.ResetColor();

                            do
                            {
                                ok = Int32.TryParse(Console.ReadLine(), out size);
                                if ((size < 1) || (size > 999999) || (!ok))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Введите целое число!");
                                    Console.ResetColor();
                                }
                            } while ((size < 1) || (size > 999999) || (!ok));

                            //class money array 
                            //

                            arr = new MoneyArray(size);
                            break;
                        }
                    case 2:
                        {
                            if (MoneyArray.count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("В массиве еще нет ни одного элемента!");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("------------------Массив Money-----------------");
                                for (int i = 0; i < size; i++)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine(Convert.ToDouble(arr[i].RublesInt(arr[i])) + arr[i].KopeksDouble(arr[i]));
                                    Console.ResetColor();
                                }
                                Console.WriteLine("-----------------------------------------------");
                            }
                            break;
                        }
                    case 3:
                        {
                            if (MoneyArray.count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("В массиве еще нет ни одного элемента!");
                                Console.ResetColor();
                            }
                            else
                            {
                                Money max = arr[0];

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("Максимальное значение массива = ");
                                Console.Write(Convert.ToDouble(arr[arr.GreatestElementIndex()].RublesInt(arr[arr.GreatestElementIndex()])) + arr[arr.GreatestElementIndex()].KopeksDouble(arr[arr.GreatestElementIndex()]));
                                Console.WriteLine(" руб.");
                                Console.Write("(находится под номером " + (arr.GreatestElementIndex() + 1) + ")");
                                Console.ResetColor();
                            }
                            break;
                        }
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Всего за время работы с программой было создано ");
                        Console.Write(MoneyArray.count);
                        Console.WriteLine(" элементов массива");
                        Console.ResetColor();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Thread.Sleep(900);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нужно выбрать из списка!");
                        Console.ResetColor();
                        continue;
                }
            } while (true);
        }
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            do
            {
                Console.WriteLine("\n----------------------------------------------------");
                Console.WriteLine("| Выберите следующее действие:                     |");
                Console.WriteLine("| 1) 1 и 2 задание                                 |");
                Console.WriteLine("| 2) 3 задание                                     |");
                Console.WriteLine("| 0) Выход из консоли                              |");
                Console.WriteLine("----------------------------------------------------");
                Console.Write("Действие: ");
                int SwitchNumber = Search_for_Bugs.ProverkaVvoda();
                switch (SwitchNumber)
                {
                    case 1:
                        Task_1_2();
                        break;                        
                    case 2:
                        Task_3();
                        break;                    
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Thread.Sleep(900);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нужно выбрать из списка!");
                        Console.ResetColor();
                        continue;
                }
            } while (true);
        }
    }
}