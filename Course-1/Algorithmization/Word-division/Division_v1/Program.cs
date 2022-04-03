﻿using System;

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
namespace Division_v1
{
    class Program
    {
        static bool Glassnii(char ch)
        {
            switch (ch)
            {
                case 'а':
                case 'я':
                case 'у':
                case 'ю':
                case 'е':
                case 'э':
                case 'о':
                case 'ё':
                case 'и':
                case 'ы':
                    return true;
                default:
                    return false;
            }
        }

        static void Main()
        {
            Console.Write("Введите слово: ");
            string slovo = Console.ReadLine();
            bool last = false;
            char[] word = new char[slovo.Length * 2];
            word = slovo.ToCharArray();
            int i = 0;
            int j, middle;
            string s1 = "";
            while (i < slovo.Length)
            {
                if (Glassnii(word[i]) == true)
                {
                    s1 = s1 + word[i];
                    if (i + 1 == slovo.Length) break;
                    if (Glassnii(word[i + 1]) == true)
                        if (last == false)
                        {
                            i++;
                            last = true;
                        }
                        else
                        {
                            s1 += '-';
                            i++;
                            last = false;
                        }
                    else
                    {
                        j = 1;
                        while ((i + j < slovo.Length) && (Glassnii(word[i + j]) != true))
                            j++;
                        if (i + j == slovo.Length)
                        {
                            for (int k = i + 1; k <= i + j - 1; k++)
                                s1 += word[k];
                            break;
                        }
                        j--;
                        middle = j / 2;
                        for (int k = i + 1; k <= i + middle; k++)
                            s1 += word[k];
                        s1 += '-';
                        for (int k = i + middle + 1; k <= i + j; k++)
                            s1 += word[k];
                        i = i + j + 1;
                    }
                }
                else
                {
                    s1 += word[i];
                    i++;
                }
            }
            s1 = s1.Replace("-ъ", "ъ-");
            s1 = s1.Replace("-ь", "ь-");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{s1}");
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