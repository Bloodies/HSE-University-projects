using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab._1
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
            Console.WriteLine("| 1) 1 Задание               |");
            Console.WriteLine("| 2) 2 Задание               |");
            Console.WriteLine("| 3) 3 Задание               |");
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
            int m, n, x;
            bool ok;

            Console.WriteLine("Введите m и n:");            
            do    // Это чтоб не вводились литеры
            {
                Console.Write("m= ");
                string buf = Console.ReadLine(); // Присваивание значения переменной m
                ok = int.TryParse(buf, out m);
            }
            while (!ok);
            do    // Это чтоб не вводились литеры
            {
                Console.Write("n= ");
                string buf = Console.ReadLine(); // Присваивание значения переменной n
                ok = int.TryParse(buf, out n);
            }
            while (!ok);
            double a = m - 1;
            double c = n++;
            double d = n--;
            double e = n + 1;
            a = m - n;
            double b = (m - 1) - (n);
            {
                Console.WriteLine($"1) m={(m - 1)}  n={(n + 1)}  --m-n++ = {b}"); // Вывод первого ответа в консоле
                if ((m * m) < n)
                    Console.WriteLine($"2) m={(m + 1)}  n={(n - 1)}  m++>--n = true");  // Вывод второго ответа в консоле
                else
                    Console.WriteLine($"2) m={(m + 1)}  n={(n - 1)}  m++>--n = false"); // Вывод второго ответа в консоле
                if (n > (m + 1))
                    Console.WriteLine($"3) m={(m - 1)}  n={(n + 1)}  m--<++n = true");  // Вывод третьего ответа в консоле
                else
                    Console.WriteLine($"3) m={(m - 1)}  n={(n + 1)} m--<++n = false"); // Вывод третьего ответа в консоле
            }
            Console.WriteLine("  ");
            Console.WriteLine("Введите x:");
            do    // Это чтоб не вводились литеры
            {
                Console.Write("x= ");
                string buf = Console.ReadLine();  // Присваивание значения переменной x
                ok = int.TryParse(buf, out x);
            }
            while (!ok);
            if (x == 0) // ОДЗ для x
            {
                Console.WriteLine("4) x не может быть равен нулю");  // Программа выдает ошибку если x = 0
            }
            else
            {
                x = 1 + (1 / x) + (1 / (x * x)); // Присваивание решения иксу
                Console.WriteLine($"4) {x}"); // Вывод четвертого ответа
            }

            Console.WriteLine("");
            Main();
        }
        public static void Second_Task()
        {
            double x, y;
            bool ok;
            do    // Это чтоб не вводились литеры и еще что то
            {
                Console.WriteLine("Введите значение x");
                string buf = Console.ReadLine(); // Присваивание значения переменной x
                ok = Double.TryParse(buf, out x);
            }
            while (ok != true);
            do    // Это чтоб не вводились литеры и еще что то
            {
                Console.WriteLine("Введите значение y");
                string buf = Console.ReadLine(); // Присваивание значения переменной y
                ok = Double.TryParse(buf, out y);
            }
            while (ok != true);
            bool ok1 = x * x + y * y <= 25;  // если все выражение меньше или равно 25 то оно входит в закр область
            bool ok2 = (x + 5) * (x + 5) + y * y <= 25;  //аналогично
            ok = ok1 || ok2; // оба значения сравниваются
            Console.WriteLine(ok);

            Console.WriteLine("");
            Main();
        }
        public static void Third_Task()
        {
            double a = 100, b = 0.001;
            double ans = ((((a - b) * (a - b) * (a - b)) - ((a * a * a) - (3 * (a * a) * b))) / ((3 * a * (b * b)) - (b * b * b)));
            /* сначала я упрощал
             * но потом я подумал что будет здорово оставить как пример
             * так что я оставил
             * + с упращениями все не влезло бы в блок-схему*/
            Console.WriteLine($"Ответ в double= {ans}");

            float a1 = 100f, b1 = 0.001f;
            float c = (float)Math.Pow((a1 - b1), 3);    // упрощение выражения заменой (a - b)*(a - b)*(a - b) переменной с
            float d = (float)Math.Pow(a1, 3);           // упрощение выражения заменой a*a*a переменной d
            float e = (float)Math.Pow(a1, 2);           // упрощение выражения заменой a*a переменной e
            float f = (float)Math.Pow((3 * (e) * b1), 1);  // упрощение выражения заменой (3*(a*a)*b) переменной f
            float g = (float)Math.Pow(b1, 2);           // упрощение выражения заменой b*b переменной g
            float h = (float)Math.Pow((3 * a1 * (g)), 1);   // упрощение выражения заменой (3*a*(b*b) переменной h
            float i = (float)Math.Pow(b1, 3);           // ОСТАВЛЮ ЭТО БЕЗ КОММЕНТАРИЕВ
            float ans1 = ((c - (d - f)) / (h - i));
            Console.WriteLine($"Ответ в float= {ans1}");

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