using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab._2
{
    //---Все менюшки----------------------------------------------
    class Text_Dialog
    {
        public static void PrintErrorMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нужно выбрать из списка!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Menu()
        {
            Console.WriteLine(" ");
            Console.WriteLine("------------------------------");
            Console.WriteLine("| Выберите действие:         |");
            Console.WriteLine("| 1) 1 Задание (6)           |");
            Console.WriteLine("| 2) 2 Задание (28)          |");
            Console.WriteLine("| 3) 3 Задание (57)          |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
        }
    }
    //------------------------------------------------------------
    //---Проверка ввода-------------------------------------------
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
                    Console.WriteLine("Некорректный ввод");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!res);
            return number;
        }
    }
    //------------------------------------------------------------
    class Program
    {
        public static void First_Task()
        {            
            double min = 0, 
                   a;
            int n;
            bool elem;
            Console.WriteLine("Введите длину последовательности");
            do
            {
                elem = int.TryParse(Console.ReadLine(), out n);
                if (n <= 0)
                    elem = false;
                if (elem == false)
                    Console.WriteLine("Длина не может быть меньше или равна 0 \nВведите длину последовательности");
            }
            while (!elem);
            elem = false;
            Console.WriteLine("Введите элемент последовательности");
            while (!elem)
            {
                elem = double.TryParse(Console.ReadLine(), out min);
                if (elem == false)
                    Console.WriteLine("Элемент должен быть числом");
            }
            for (int i = 1; i < n; i++)
            {
                Console.WriteLine("Введите элемент последовательности");
                do
                {
                    elem = double.TryParse(Console.ReadLine(), out a);
                    if (elem == false)
                        Console.WriteLine("Элемент должен быть числом");
                }
                while (!elem);
                if (a < min)
                    min = a;
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"Минимальный элемент последовательности равен {min}");

            Console.WriteLine("");
            Main();
        }
        public static void Second_Task()
        {
            int num1 = 0,
                min = 0,
                max = 0,
                i = 0;
            bool ok = false;
            while (!ok)
            {
                try
                {
                    Console.WriteLine("Введите 1 число");
                    num1 = Convert.ToInt32(Console.ReadLine());
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка, повторите ввод");
                    ok = false;
                }
            }
            min = num1;
            max = num1;
            while (num1 != 0)
            {
                try
                {
                    i++;
                    if (min > num1)
                        min = num1;
                    if (max < num1)
                        max = num1;
                    Console.WriteLine("Введите {0} число", i + 1);
                    num1 = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка, повторите ввод");
                    i--;
                }
            }
            int r = max - min;
            Console.WriteLine("    ");
            Console.WriteLine("    ");
            if (i == 0) Console.WriteLine("Последовательность пуста");
            else Console.WriteLine("Разность минимума и максимума = " + r);

            Console.WriteLine("");
            Main();
        }
        public static void Third_Task()
        {
            int n;
            double P = 1;
            bool ok;
            Console.WriteLine("Введите n:");
            do
            {
                ok = int.TryParse(Console.ReadLine(), out n);
                if (n <= 2)
                    ok = false;
                if (ok == false)
                    Console.WriteLine("Введите n заново:");
            }
            while (!ok);
            for (int i = 2; i <= n; i++)
            {
                i++;
                P *= 1 - (1 / (double)(i * i));

            }
            Console.WriteLine("    ");
            Console.WriteLine("    ");
            Console.WriteLine("P = " + P);

            Console.WriteLine("");
            Main();
        }
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Text_Dialog.Menu();
                int check = Search_for_Bugs.ProverkaVvoda();
                switch (check)
                {
                    case 1:
                        Console.Clear();
                        First_Task();
                        break;
                    case 2:
                        Console.Clear();
                        Second_Task();
                        break;
                    case 3:
                        Console.Clear();
                        Third_Task();
                        break;
                    case 0:
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
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
            } while (!true);
        }
    }
}