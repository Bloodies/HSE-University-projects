using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab._1__Расписание_
{
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
    class Program
    {
        public static int VvodNonNegative()
        {
            Console.Write("Введите натуральное число: ");
            bool rightNonNegative;
            int numberZahlen;
            do
            {
                rightNonNegative = int.TryParse(Console.ReadLine(), out numberZahlen);
                if (numberZahlen < 1)
                {
                    rightNonNegative = false;
                }

                if (!rightNonNegative)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Можно вводить только целые положительные числа!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nПопробуйте ввести число ещё раз:");
                }
                else
                {
                    return numberZahlen;
                }
            }
            while (!rightNonNegative);
            return 0;
        }
        public static void PrintConditions(int workCount, int workerCount, double[] workTime)
        {
            Console.WriteLine("-------------------Условия задачи--------------------");
            for (int i = 0; i < workCount; i++)
            {
                Console.WriteLine("\n\t{0} - {1}h", (char)(65 + i), workTime[i]);
            }

            Console.WriteLine("\nКоличество работников: " + workerCount);
            Console.WriteLine("Максимальное время: {0}h", workTime.Max());
            Console.WriteLine("Среднее время: {0}h\n", (workTime.Sum() / workerCount));
        }
        static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            double maxWorkTime;
            int j = 0;

            Console.WriteLine("----------------Ввод количества работ----------------\n");
            int workCount = VvodNonNegative();
            double[] workTime = new double[workCount];
            for (int i = 0; i < workCount; i++)
            {
                Console.WriteLine("\nВведите время выполенния работы {0}", (char)(65 + i));
                workTime[i] = VvodNonNegative();
            }

            Console.WriteLine("\n-------------Ввод количества работников-------------");
            int workerCount = VvodNonNegative();
            if (workTime.Max() > (workTime.Sum() / workerCount)) { maxWorkTime = workTime.Max(); }
            else { maxWorkTime = (workTime.Sum() / workerCount); }

            Console.Clear();
            PrintConditions(workCount, workerCount, workTime);

            Console.WriteLine("----------------------Решение-----------------------");
            for (int i = 0; i < workerCount; i++)
            {
                Console.Write("Работник {0}:", i + 1);
                double deltaWorkTime = maxWorkTime;
                while (workTime.Sum() > 0 && deltaWorkTime > 0 && j < workCount)
                {
                    if (deltaWorkTime >= workTime[j])
                    {
                        Console.Write(" {0} {3}h({1} - {2});", (char)(65 + j), maxWorkTime - deltaWorkTime, maxWorkTime - deltaWorkTime + workTime[j], (maxWorkTime - deltaWorkTime + workTime[j]) - (maxWorkTime - deltaWorkTime));
                        deltaWorkTime -= workTime[j];
                        workTime[j] = 0;
                        j++;
                    }
                    else
                    {
                        Console.Write(" {0} {3}h({1} - {2});", (char)(65 + j), maxWorkTime - deltaWorkTime, maxWorkTime, (maxWorkTime) - (maxWorkTime - deltaWorkTime));
                        workTime[j] -= deltaWorkTime;
                        deltaWorkTime = 0;
                    }
                }
                Console.WriteLine("\n");
            }
            do
            {
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
                        Console.ResetColor();
                        continue;
                }
            } while (!true);
        }
    }
}