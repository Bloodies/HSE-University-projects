using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Задание:
/// Используя датчик случайных чисел, заполнить массив из двадцати элементов неповторяющимися числами.
/// </summary>
namespace Task_1
{    
    class Program
    {
        /// <summary>
        /// Создание и заполнение массива рандомными числами в диапазоне 1-100
        /// </summary>
        /// 
        static void Main()
        {
            Random rnd = new Random();
            int[] array = new int[20];
            int elem;
            bool ok;

            for (int i = 0; i < 20; i++)
            {
                do
                {
                    elem = rnd.Next(1, 100);
                    ok = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (elem == array[j])
                            ok = false;
                    }
                } while (!ok);
                array[i] = elem;
            }

            Console.WriteLine("------------------Массив-----------------");

            foreach (var i in array)
                Console.Write($" {i}");

            Console.WriteLine("\n-----------------------------------------");

            Console.Write("Ещё Массив? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}