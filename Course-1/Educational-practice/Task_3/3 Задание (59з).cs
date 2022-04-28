using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/* Практическое задание №3 (59з)
 * Даны действительные числа х, у.
 * Определить, принадлежит ли точка с координатами (х, у) заштрихованной части плоскости. 
 */

namespace Task_3
{
    class Program
    {
        #region input number
        public static double GetDouble(string s)
        {
            Console.Write(s);
            double number;
            bool ok = false;
            do
            {
                ok = Double.TryParse(Console.ReadLine(), out number);
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Некорретный формат входной строки. Повторите ввод вещественного числа.");
                    Console.ResetColor();
                }
            } while (!ok);
            return number;
        }
        #endregion
        public static bool Rectangle(double x, double y)
        {
            //проверка точки на принадлежность прямоугольной области
            //область от -2 до 1 по x
            //и от -1 до 1 по y
            bool accept = (y >= -2 && y <= 1) && (x >= -1 && x <= 1);
            return accept;
        }
        public static bool Triangle(double x, double y)
        {
            //проверка точки на принадлежность треугольным областям
            bool accept = (y > x) && (y > -x);
            return accept;
        }
        static void Main()
        {
            Console.Clear();
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Практическое задание №3:");
                Console.WriteLine("Задача 59з");
                Console.WriteLine("Даны действительные числа х, у.");
                Console.WriteLine("Определить, принадлежит ли точка с координатами (х, у) заштрихованной части плоскости:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"                            ");
                Console.WriteLine(@"                  ^y        ");
                Console.WriteLine(@"             ║\  1╬   /║    ");
                Console.WriteLine(@"             ║░\  ║  /░║    ");
                Console.WriteLine(@"             ║░░\ ║ /░░║    ");
                Console.WriteLine(@"           -1║░░░\║/░░░║1   ");
                Console.WriteLine(@"          ───╬────╬────╬──>x");
                Console.WriteLine(@"             ║░░░░║░░░░║    ");
                Console.WriteLine(@"             ║░░░░║░░░░║    ");
                Console.WriteLine(@"             ║░░░░║░░░░║    ");
                Console.WriteLine(@"             ║░░░░║░░░░║    ");
                Console.WriteLine(@"             ║░░░░║░░░░║    ");
                Console.WriteLine(@"             ╚──-2╬────╝    ");
                Console.WriteLine(@"                            ");
                Console.ForegroundColor = ConsoleColor.White;
                double x = GetDouble("Введите х: ");
                double y = GetDouble("Введите y: ");
                //Если принадлежит прямоугольнику и не вринадлежит закрашенным треугольником
                //то accept
                bool accepted = Rectangle(x, y) && !Triangle(x, y);
                if (accepted)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Точка (" + x + ";" + y + ") принадлежит заданной области");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Точка (" + x + ";" + y + ") не принадлежит заданной области");
                    Console.ResetColor();
                }
                #region menu
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ");
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