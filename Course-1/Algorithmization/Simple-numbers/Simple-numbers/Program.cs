using System;

namespace Simple_numbers
{
    class Program
    {
        /// <summary>
        /// Поиск простых чисел
        /// </summary>
        /// <param name="N"> Введенное число </param>
        /// <returns> true - если простое </returns>
        private static bool Number(int N)
        {
            for (int i = 2; i <= (int)(N / 2); i++)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }

        static void Main()
        {
            int score = 0;
            Console.WriteLine("До какого числа искать?");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("-----------------------");
            for (int i = 0; i <= b; i++)
            {
                if (Number(i))
                {
                    score++;
                    Console.Write($"{score}: {i}\n");
                }
            }

            Console.WriteLine("\n-----------------");
            Console.Write("Повторить? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}
