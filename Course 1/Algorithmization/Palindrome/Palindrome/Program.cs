using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    class Program
    {
        public static bool Palindrom(string s)
        {
            int j = s.Length - 1;
            for (int i = 0; i < j; i++, j--)
            {
                if (s[i] != s[j])
                {
                    return false;
                }
            }
            return true;
        }
        static void Main()
        {
            string s;
            Console.WriteLine("Введите текст:");
            s = Console.ReadLine();
            if (Palindrom(s))
            {
                Console.WriteLine("Этот текст - палиндром");
            }
            else
            {
                Console.WriteLine("Этот текст - не палиндром");
            }

            Console.WriteLine("Ещё текст? [да/нет]");
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
