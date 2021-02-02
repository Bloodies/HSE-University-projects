using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

/* Практическое задание №6 (9)
 * Ввести а1, а2, а3, М, N, L. 
 * Построить последовательность чисел ак = (7/3* ак–1 + ак-2 ) /2 * ак–3. 
 * Построить N элементов последовательности,
 * либо найти первые M ее элементов, большие числа L (в зависимости от того, что выполнится раньше). 
 * Напечатать последовательность и причину остановки.
 */

namespace Task_6
{
    //---Проверка ввода-------------------------------------------
    #region region input check
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
                    Console.WriteLine("Некорректный ввод");
                }
            } while (!res);
            return number;
        }
        public static void InputDouble(ref double init, string splash)
        {
            bool ok = true;
            Console.Write(splash);
            do
            {
                string buf = Console.ReadLine();
                ok = double.TryParse(buf, out init);
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Неверный ввод!\nВведите вещественное число : ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!ok);
        }
        public static void InputInteger(ref int init, string splash)
        {
            bool ok = true;
            Console.Write(splash);
            do
            {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out init);
                if (init < 3) ok = false;
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Неверный ввод!\nВведите целое число > 2 : ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!ok);
        }
    }
    #endregion
    //------------------------------------------------------------
    class Program
    {
        private static Stopwatch First_speed = new Stopwatch();  // проверка на скорость для построения
        private static Stopwatch Second_speed = new Stopwatch(); // Проверка на скорость для нахождения
        private static double[] result; // итоговая последовательность
        private static int M, N;        // ограничители по длине последовательности
        private static double L;        // ограничитель для M

        private static void Input()
        {
            Search_for_Bugs.InputInteger(ref N, "Введите количество элементов N : ");
            result = new double[N];
            Search_for_Bugs.InputDouble(ref result[0], "Введите a1: ");
            Search_for_Bugs.InputDouble(ref result[1], "Введите a2: ");
            Search_for_Bugs.InputDouble(ref result[2], "Введите a3: ");
            Search_for_Bugs.InputInteger(ref M, "Введите количество элементов M : ");
            Search_for_Bugs.InputDouble(ref L, "Введите значение L (минимум для M): ");
        }
        private static bool Calc()
        {
            bool result = true;
            // подсчёт времени на N членов последовательности
            First_speed.Start();
            DateTime startTime_n = DateTime.Now;
            for (int i = 3; i < N; i++)
            {
                if (DateTime.Now.Subtract(startTime_n) >= new TimeSpan(0, 0, 0, 0, 2000))
                {
                    result = false;
                    break;
                }
                Program.result[i] = Math.Round(GetSequenceValue(Program.result, i), 2);
            }
            First_speed.Stop();
            // подсчёт времени на M членов последовательности
            Second_speed.Start();
            DateTime startTime_m = DateTime.Now;
            int c = 0;
            int d = 0;
            while (c != M)
            {
                if (DateTime.Now.Subtract(startTime_n) >= new TimeSpan(0, 0, 0, 0, 2000))
                {
                    result = false;
                    break;
                }
                if (GetSequenceValue(Program.result, d) > L)
                {
                    if (c <= M)
                        c++;
                    else
                        break;
                }
                d++;
            }
            Second_speed.Stop();
            if (result == true)
            {

                if (Second_speed.ElapsedMilliseconds < First_speed.ElapsedMilliseconds)
                {   // если поиск M членов последователности быстрее
                    Program.result = SearchSpeed();
                    Console.WriteLine("Поиск M элем. послед. быстрее на {0} миллисекунд(-у)", First_speed.ElapsedMilliseconds - Second_speed.ElapsedMilliseconds);
                }
                else
                {   // если подсчёт N членов последовательности быстрее
                    Console.WriteLine("Вычисление N элем. послед. быстрее на {0} миллисекунд(-у)", Second_speed.ElapsedMilliseconds - First_speed.ElapsedMilliseconds);
                }
                for (int i = 0; i < Program.result.Length; i++)
                    Console.WriteLine("{0} эл. послед. = {1}", i + 1, Program.result[i]);
            }
            return result;
        }
        private static double[] SearchSpeed()
        {
            double[] second = new double[3 + M];
            second[0] = result[0];
            second[1] = result[1];
            second[2] = result[2];
            int c = 0; // проверка для M
            int d = 0; // счётчик 
            while (c != M)
            {
                double temp = GetSequenceValue(result, d);
                if (temp > L)
                {
                    if (c <= M)
                    {
                        second[c + 3] = temp;
                        c++;
                    }
                    else break;
                }
                d++;
            }
            return second;
        }
        private static double GetSequenceValue(double[] seq, int input)
        {
            if (input == 0) { return seq[0]; }
            else if (input == 1) { return seq[1]; }
            else if (input == 2) { return seq[2]; }
            else { return ((7 / 3) * GetSequenceValue(seq, input - 1) + GetSequenceValue(seq, input - 2)) / (2 * GetSequenceValue(seq, input - 3)); }
        }        
        private static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Практическое задание №6:");
            Console.WriteLine("Задача 9");
            Console.WriteLine(@"Ввести а1, а2, а3, М, N, L. 
Построить последовательность чисел ак = (7/3* ак–1 + ак-2 ) /2 * ак–3. 
Построить N элементов последовательности, либо найти первые M ее элементов, большие числа L (в зависимости от того, что выполнится раньше). 
Напечатать последовательность и причину остановки.");
            Console.WriteLine(" ");
            Input();
            if (!Calc())
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Задайте не такие большие числа! (Лимит времени выполнения превышен)");
                Console.ForegroundColor = ConsoleColor.White;
                Input();
            }
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ");
                Console.WriteLine("------------------------------");
                Console.WriteLine("| Выберите действие:         |");
                Console.WriteLine("| 9) Продолжить              |");
                Console.WriteLine("| 0) Выход из консоли        |");
                Console.WriteLine("------------------------------");
                Console.Write("Действие: ");
                int check = Search_for_Bugs.ProverkaVvoda();
                switch (check)
                {
                    case 9:
                        Main();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нужно выбрать из списка!");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                }
            } while (true);
        }
    }
}