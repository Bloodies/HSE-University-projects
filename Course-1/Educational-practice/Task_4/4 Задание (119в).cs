using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/// <summary>
/// Практическое задание №4 (119в)
/// Вычислить бесконечную сумму с заданной точностью е (е > 0).
/// Считать, что требуемая точность достигнута,
/// если вычислена сумма нескольких первых слагаемых и очередное слагаемое оказалось по модулю меньше,
/// чем е,- это и все последующие слагаемые можно уже не учитывать.
/// </summary>
namespace Task_4
{
    class Program
    {
        //реализация факториала
        public static int Factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * Factorial(x - 1);
            }
        }
        public static double Epsilon()
        {
            double eps = 0;
            bool ok = true;
            Console.WriteLine("Введите точность для вычисления суммы ряда:");
            do
            {
                try
                {
                    eps = Convert.ToDouble(Console.ReadLine());

                    if (eps < Math.Pow(10, -9))
                    {
                        ok = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Точность слишком мала (переполнение стека)");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Попробуйте еще раз...");
                    }
                    else ok = true;
                }
                catch
                {
                    ok = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Неверный ввод числа Epsilon");
                }
            } while (!ok);
            return eps;
        }
        public static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Практическое задание №3:");
            Console.WriteLine("Задача 119в");
            Console.WriteLine("Вычислить бесконечную сумму с заданной точностью е (е > 0).");
            Console.WriteLine(" ");
            double eps = Epsilon(); //объявление числа эпсилон
            double sum = -1;
            double an = -1;
            int nextMember = 2;
            int k = 1;
            do
            {
                Console.OutputEncoding = Encoding.Unicode;
                while (Math.Abs(an) > eps)
                {
                    long factorial = Factorial(nextMember);
                    an = (double)k / factorial;
                    sum += an;
                    Console.WriteLine("Сумма первых " + nextMember + " членов ряда = " + sum);
                    nextMember++;
                    k *= -1;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Полученная сумма с заданной точностью: " + sum);
                #region menu
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