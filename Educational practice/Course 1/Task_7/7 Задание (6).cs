using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

/* Практическое задание №7 (6)
 * Выписать все булевы функции от 3 аргументов, которые не самодвойственны. 
 * Выписать их вектора в лексикографическом порядке.
 */

namespace Task_7
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Практическое задание №7:");
            Console.WriteLine("Задача 6");
            Console.WriteLine(@"Выписать все булевы функции от 3 аргументов, которые не самодвойственны.
Выписать их вектора в лексикографическом порядке.");
            Console.WriteLine(" ");
            ArrayList functions = new ArrayList();
            for (int i = 0; i < 256; i++)
            {
                string binary = Convert.ToString(i, 2);
                if (binary.Length < 8)
                {
                    int length = binary.Length;
                    for (int k = 0; k < 8 - length; k++)
                    {
                        binary = binary.Insert(0, "0");
                    }
                }
                string firstNabor = binary.Substring(0, 4);
                string secondNabor = binary.Substring(4, 4);
                if (firstNabor[0] == secondNabor[3] || firstNabor[1] == secondNabor[2] || firstNabor[2] == secondNabor[1] || firstNabor[3] == secondNabor[0])
                {
                    functions.Add(binary);
                }
            }
            foreach (string a in functions)
            {
                Console.WriteLine(a);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Всего не самодвойственных функций " + functions.Count);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ");
            Console.WriteLine("Для завершения программы нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(" ");
            Console.Write("Завершение работы.");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Environment.Exit(0);
        }
    }
}