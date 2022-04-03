using System;

namespace Word_cases
{
    class Program
    {
        /// <summary>
        /// Выбор падежа
        /// </summary>
        /// <param name="cows"> Ввод количества коров </param>
        static void CountCows(long cows)
        {
            string[] cowname = { "Коров", "Коровы", "Корова" };

            if ((cows % 100 != 11) && (cows % 10 == 1))
                Console.WriteLine($"{cows} {cowname[2]}");
            else if (((cows % 10 >= 2) && (cows % 10 <= 4)) && (cows % 100 != 12) && (cows % 100 != 13) && (cows % 100 != 14))
                Console.WriteLine($"{cows} {cowname[1]}");
            else
                Console.WriteLine($"{cows} {cowname[0]}");
        }

        static void Main()
        {
            long cows;

            Console.Write("\nВведите количество коров: ");
            while (!long.TryParse(Console.ReadLine(), out cows))
            {
                Console.WriteLine("В нашем cлучае не может быть не целых коров \nВведите целое число!");
                Main();
            }
            CountCows(cows);

            Console.WriteLine("\n--------------------");
            Console.Write("Повторить? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}
