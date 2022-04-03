using System;
using System.Collections.Generic;

/// <summary>
/// Задание:
/// Напишите процедуру «Слог», разбивающую слово на слоги. 
/// Предложите свой алгоритм. 
/// За основу возьмите следующие правила: 
///     -Две подряд идущие гласные рассматриваются как одна гласная;
///     -Число слогов определяется числом гласных букв(с учетом предыдущегоправила);
///     -Если n – число согласных между двумя соседними гласными, то n/2 согласных
///         относятся к предыдущему слогу, а оставшиеся к следующему.
/// Примеры нескольких разбиений в соответствии с этим алгоритмом: 
///     «слог»
///     «сло - во»
///     «прог - ноз»
///     «транс - крип - ция»
///     «зоо - ма - га - зин».
/// </summary>
namespace Division_v2
{
    class Program
    {
        static void Main()
        {
            string[] Glassnie = { "а", "у", "е", "ё", "ы", "о", "я", "и", "э", "ю" };
            List<int> IndexGlassnie = new List<int>();

            Console.Write("Введите слово: ");
            string word = Console.ReadLine().ToLower();

            for (int i = 0; i < word.Length; i++)
            {
                string symbol = word.Substring(i, 1);
                for (int j = 0; j < Glassnie.Length; j++)
                {
                    if (symbol == Glassnie[j])
                    {
                        IndexGlassnie.Add(i);
                        break;
                    }
                }
            }
            string result = string.Empty;
            for (int i = IndexGlassnie.Count - 1; i > 0; i--)
            {
                string symbol = word.Substring(IndexGlassnie[i] - 1, 1);
                if (symbol == "ь" || symbol == "ъ")
                {
                    result = "-" + word.Substring(IndexGlassnie[i]) + result;
                    word = word.Remove(IndexGlassnie[i]);
                }
                else
                {
                    int schet = IndexGlassnie[i] - IndexGlassnie[i - 1] - 1;
                    int index = IndexGlassnie[i - 1] + 1 + schet / 2;
                    symbol = word.Substring(index, 1);
                    if (symbol == "ь" || symbol == "ъ")
                        index++;

                    result = "-" + word.Substring(index) + result;
                    word = word.Remove(index);
                }

            }
            result = word + result;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{result}");
            Console.ResetColor();

            Console.WriteLine("\n---------------------------");
            Console.Write("Хотите ввести ещё? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}
