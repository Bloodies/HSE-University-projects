using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_numbers
{
    class Program
    {
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
            int schet = 0;
            Console.WriteLine("До какого числа искать?");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("-----------------------");
            for (int i = 0; i <= b; i++)
            {
                if (Number(i))
                {
                    schet++;
                    Console.Write(schet + ": " + i.ToString() + "\n");
                }
            }
            Console.WriteLine("Повторить? [да/нет]");
            string restart = Console.ReadLine();
            Console.WriteLine("   ");
            if (restart == "да")
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
