using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Падежи
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите количество коров");
            string encows = Console.ReadLine();
            long cows;
            while (!long.TryParse(encows, out cows))
            {
                Console.WriteLine("В нашем члучае не может быть не целых коров /nВведите целое число");
                Main();
            }
            CountCows(cows);
        }
        static void CountCows(long cows)
        {
            string[] cowname = { "Коров", "Коровы", "Корова" };
            if ((cows % 100 != 11) && (cows % 10 == 1))
            {
                Console.WriteLine($"{cows} {cowname[2]}");
            }
            else if (((cows % 10 >= 2) && (cows % 10 <= 4)) && (cows % 100 != 12) && (cows % 100 != 13) && (cows % 100 != 14))
            {
                Console.WriteLine($"{cows} {cowname[1]}");
            }
            else
            {
                Console.WriteLine($"{cows} {cowname[0]}");
            }
            Console.ReadKey();
        }
    }
}
