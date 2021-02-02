using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Задание:
 * Используя датчик случайных чисел, заполнить массив из двадцати элементов неповторяющимися числами.
 */

namespace Task._1
{
    class Program
    {
        static void Main()
        {
            int elem;
            bool ok;
            int[] array = new int[20];
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                do
                {
                    elem = rnd.Next(1, 100);
                    ok = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (elem == array[j])
                        {
                            ok = false;
                        }
                    }
                } while (!ok);
                array[i] = elem;
            }
            Console.WriteLine("------------------Массив-----------------\n");
            foreach (var i in array)
            {
                Console.Write(" " + i);
            }
            Console.WriteLine(" ");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Ещё Массив? [да/нет]");
            string rpt = Console.ReadLine();
            Console.WriteLine("");
            if (rpt == "да")
            {
                Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}