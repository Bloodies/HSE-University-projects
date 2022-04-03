using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palindrome
{
    class Program
    {
        /// <summary>
        /// Сравнение символов слова для нахождения палиндрома
        /// </summary>
        /// <param name="word"> Введенное слово </param>
        /// <returns>
        /// Возвращает true/false
        /// true - если палиндром
        /// false - если нет
        /// </returns>
        public static bool Palindrom(string word)
        {
            int j = word.Length - 1;
            for (int i = 0; i < j; i++, j--)
                if (word[i] != word[j])
                    return false;
            return true;
        }

        static void Main()
        {
            Console.Write("Введите текст: ");
            if (Palindrom(Console.ReadLine()))
                Console.WriteLine("Этот текст - палиндром");
            else
                Console.WriteLine("Этот текст - не палиндром");

            Console.WriteLine("\n-----------------------");
            Console.Write("Ещё текст? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}
