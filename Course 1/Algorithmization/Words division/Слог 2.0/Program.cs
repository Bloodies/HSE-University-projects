using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Слог_2._0
{
    class Program
    {
        /*Задание:
         * Напишите процедуру «Слог», разбивающую слово на слоги.Предложите свой
         * алгоритм. За основу возьмите следующие правила: 
         * - две подряд идущие гласные рассматриваются как одна гласная; 
         * - число слогов определяется числом гласных букв(с учетом предыдущегоправила); 
         * - Если n – число согласных между двумя соседними гласными, то n/2 согласных
         * относятся к предыдущему слогу, а оставшиеся к следующему.Вот примеры
         * нескольких разбиений в соответствии с этим алгоритмом: «слог», «сло - во»,
         * «прог - ноз», «транс – крип - ция», «зоо – ма – га – зин». */
        static void Main()
        {
            Console.Write("Введите слово: ");
            string Slovo = Console.ReadLine();
            string[] Glassnie =
              { "а",
                "у",
                "е",
                "ё",
                "ы",
                "о",
                "я",
                "и",
                "э",
                "ю" };
            Slovo = Slovo.ToLower();
            List<int> IndexGlassnie = new List<int>();
            for (int i = 0; i < Slovo.Length; i++)
            {
                string symbol = Slovo.Substring(i, 1);
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
                string symbol = Slovo.Substring(IndexGlassnie[i] - 1, 1);
                if (symbol == "ь" || symbol == "ъ")
                {
                    result = "-" + Slovo.Substring(IndexGlassnie[i]) + result;
                    Slovo = Slovo.Remove(IndexGlassnie[i]);
                }
                else
                {
                    int schet = IndexGlassnie[i] - IndexGlassnie[i - 1] - 1;
                    int index = IndexGlassnie[i - 1] + 1 + schet / 2;
                    symbol = Slovo.Substring(index, 1);
                    if (symbol == "ь" || symbol == "ъ")
                        index++;

                    result = "-" + Slovo.Substring(index) + result;
                    Slovo = Slovo.Remove(index);
                }

            }
            result = Slovo + result;
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(result);
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine("---------------------------");
            Console.WriteLine("Хотите ввести ещё? [да/нет]");
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
