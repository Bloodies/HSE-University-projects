using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/* Практическое задание №9 (21)
 * Напишите рекурсивный метод создания двунаправленного списка, 
 * в информационные поля элементов которого последовательно заносятся номера с 1 до N (N водится с клавиатуры). 
 * Первый включенный в список элемент, имеющий номер 1, оказывается в хвосте списка (последним). 
 * Разработайте рекурсивные методы поиска и удаления элементов списка.
 */

namespace Task_9
{
    class Search_for_Bugs
    {
        //---Проверка ввода-------------------------------------------
        #region input check
        public static int ProverkaVvoda() 
        {
            int number;
            bool res;
            do
            {
                res = int.TryParse(Console.ReadLine(), out number);

                if (res == false)
                {
                    Console.WriteLine("Некорректный ввод");
                }
            } while (!res);
            return number;
        }
        #endregion
        //------------------------------------------------------------
    }
    #region region point
    class Point
    {
        public int data;
        public Point next,
            pred;
        public Point()
        {
            data = 0;
            next = null;
            pred = null;
        }
        public Point(int d)
        {
            data = d;
            next = null;
            pred = null;
        }
        public override string ToString()
        {
            return data + " ";
        }
        public Point MakePoint1(int d)
        {
            Point p = new Point(d);
            return p;
        }
        public Point MakeList1(Point beg, int N, int size)
        {
            Point r = beg;
            N++;
            Point p = MakePoint1(N);
            r.next = p;
            p.pred = r;
            r = p;

            if (N != size) { MakeList1(r, N, size); }
            return beg;
        }
        public Point MakeList2(int size)
        {
            int N = 1;
            Point beg = MakePoint1(N);
            if (N != size) { beg = MakeList1(beg, N, size); }
            return beg;
        }
        public void ShowList1(Point beg)
        {
            Console.Write("Вывод списка: ");
            if (beg == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка, список пуст! Повторите ввод.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Point p = beg;
            while (p != null)
            {
                Console.Write(p);
                p = p.next;
            }
            Console.WriteLine();
        }
        public Point SwapBeg(Point beg)
        {
            Point p = beg;
            Point NewPoint = MakePoint1(1);
            while (p.next != null) { p = p.next; }
            p.next = NewPoint;
            beg = beg.next;
            return beg;
        }
        public void Search(Point beg, int N1, int number)
        {
            Point p = beg;
            N1++;
            if (N1 == number)
            {
                Console.WriteLine("Элемент с номеройм {0} найден: {1}", number, p);
                return;
            }
            if (p.next == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Элемент не найден");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Search(p.next, N1, number);
        }
        public Point Delete(Point beg, int N1, int number)
        {
            Point p = beg;
            if (p == null) return beg;
            if (number == 1) return p.next;
            N1++;
            if (N1 == number) p.next = p.next.next;
            else Delete(p.next, N1, number);
            return p;
        }
        public int CheckNumber()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                    return number;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка, введите еще раз");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
    #endregion
    class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Point p2 = new Point();
            int size = 0,
                a = 0,
                number = 0,
                N1 = 1;            
            Console.WriteLine("Практическое задание №9:");
            Console.WriteLine("Задача 21");
            Console.WriteLine(@"Напишите рекурсивный метод создания двунаправленного списка, 
в информационные поля элементов которого последовательно заносятся номера с 1 до N (N водится с клавиатуры). 
Первый включенный в список элемент, имеющий номер 1, оказывается в хвосте списка (последним). 
Разработайте рекурсивные методы поиска и удаления элементов списка.");
            Console.WriteLine(" ");
            do
            {
                Console.WriteLine("-------------Создание списка-----------");
                Console.WriteLine("Введите размер списка");
                while (a == 0)
                {
                    size = p2.CheckNumber();
                    if (size <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Ошибка, список не существует или пуст!\nПовторите ввод: ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        a++;
                    }
                }
                p2 = p2.MakeList2(size);
                p2.ShowList1(p2);
                Console.WriteLine("Первый включенный в список элемент, имеющий номер 1, оказывается в хвосте списка: ");
                p2 = p2.SwapBeg(p2);
                p2.ShowList1(p2);
                Console.WriteLine("-------------Поиск в списке-------------");
                p2.ShowList1(p2);
                Console.WriteLine(" ");
                Console.Write("Введите номер элемента который хотите найти: ");
                N1--;
                number = p2.CheckNumber();
                p2.Search(p2, N1, number);
                Console.WriteLine("-------------Удаление из списка-------------");
                p2.ShowList1(p2);
                Console.WriteLine(" ");
                Console.Write("Введите номер элемента который хотите удалить: ");
                N1++;
                number = p2.CheckNumber();
                if (number - 1 >= size)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Элемент не найден");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    p2 = p2.Delete(p2, N1, number);
                    if (size == 0)
                    {
                        p2.ShowList1(p2);
                    }
                    else
                    {
                        Console.Write("Вывод списка: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Список пуст!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                #region region menu
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Нажмите ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("любую клавишу");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" для повтора или ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Esc");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" для выхода...");
                int end = Console.ReadKey().KeyChar;
                Console.WriteLine(" ");
                switch (end)
                {
                    case 27:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.Write("Завершение работы.");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Environment.Exit(0);
                        break;
                    default:
                        Main();
                        continue;
                }
                #endregion
            } while (!true);
        }
    }
}