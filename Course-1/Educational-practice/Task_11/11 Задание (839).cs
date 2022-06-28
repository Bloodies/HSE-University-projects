using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

/// <summary>
/// Практическое задание №11 (8)
/// Написать метод удаления из графа всех вершин с заданным значением информационного поля.
/// </summary>
namespace Task_11
{
    #region
    class Search_for_Bugs
    {
        //---Проверка ввода-------------------------------------------
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
        //------------------------------------------------------------
    }
    #endregion
    class Program
    {
        static double Сheck(bool mod = true, double inf = 1000)
        {
            double to = 0;
            bool flag = true;
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            while (flag)
            {
                try
                {
                    string from = Console.ReadLine();
                    to = Double.Parse(from.Replace(',', '.'), CultureInfo.InvariantCulture);
                    flag = false;
                    if (to < 0 && mod)
                    {
                        flag = true;
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ошибка ввода. введите число (цифру)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (to > inf)
                {
                    flag = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("число больше заданного диапазона");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return to;
        }
        static string Encryption(string text, int[] sequence, bool mod)
        {
            try
            {
                int n = text.Length,
                    k = sequence.Length,
                    d;

                if (n > k)
                {
                    d = k - n % k;
                }
                else
                {
                    d = k - n;
                }

                for (int i = 0; i < d; i++)
                    text += " ";
                n = text.Length;

                int c = 0;
                char[] result = new char[n];
                for (int i = 0; i < n; i++)
                {
                    if (i % k == 0)
                        c = i;
                    if (mod)
                        result[i] = text[sequence[i % k] + c];
                    else
                        result[sequence[i % k] + c] = text[i];
                }
                return new string(result);
            }
            catch (Exception e)
            {
                return "ошибка";
            }
        }
        static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                int k = 1;
                Console.WriteLine("Практическое задание №11:");
                Console.WriteLine("Задача 8");
                Console.WriteLine(@"Написать метод удаления из графа всех вершин с заданным значением информационного поля.");
                Console.WriteLine(" ");
                Console.Write("Введите длинну слова: ");
                int n = Search_for_Bugs.ProverkaVvoda();

                int[] sequence = new int[n];
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Введите цифры обязательно начиная с 0 до n-1 (пример: 1, 2, 4, 0, 3)");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < n; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Ввод {k} цифры: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    sequence[i] = (int)Сheck();
                    k++;
                }
                Console.WriteLine(" ");
                Console.Write("Введите слово необходимое для шифрования: ");
                var T = Encryption(Console.ReadLine(), sequence, true);
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Зашифрованное слово: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(T);
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Расшифровка: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Encryption(T, sequence, false));

                #region region menu
                Console.WriteLine(" ");
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