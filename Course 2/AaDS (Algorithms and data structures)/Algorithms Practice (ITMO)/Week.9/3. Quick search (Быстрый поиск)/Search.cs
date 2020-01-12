using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _3.Quick_search__Быстрый_поиск_
{
    public class Main_program
    {
        public static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            //StreamReader input = new StreamReader("input.txt");
            //StreamWriter output = new StreamWriter("output.txt");
            //string subStr = input.ReadLine();
            //string goalStr = input.ReadLine();
            Text.Task();
            Console.WriteLine("Вводите данные как показано выше");
            Console.WriteLine("\nВходные данные:");
            string subStr = Console.ReadLine();
            string goalStr = Console.ReadLine();
            var listPositions = Program.FindSubString(subStr, goalStr);
            //output.WriteLine(listPositions.Count);
            Console.WriteLine("\nВыходные данные:");
            Console.WriteLine(listPositions.Count);
            foreach (int position in listPositions)
            {
                //output.Write(position + 1 + " ");
                Console.Write(position + 1 + " ");
            }
            //input.Close();
            //output.Close();
            Menu.Repeat();
        }
    }
    public class Text
    {
        public static void Task()
        {
            Console.WriteLine(@"---------------------------------------------------------------------------------
|  Даны строки 'p' и 't'.                                                       |
|  Требуется найти все вхождения строки 'p' в строку 't' в качестве подстроки.  |
|                                                                               |
|      Входные данные(input.txt)    ->     Выходные данные(output.txt)          |
|      ***********                          ***********                         |
|      * aba     *                          * 2       *                         |
|      * abaCaba *                          * 1 5     *                         |
|      ***********                          ***********                         |
|      2 - число, сколько раз 'aba' встречается в 'abaCaba'                     |
|                                                                               |
|      1 - 'aba' встречается с первого символа 'aba'Caba                        |
|      5 - 'aba' встречается с пятого символа abaC'aba'                         |
---------------------------------------------------------------------------------
");
        }
        public static void Error()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нужно выбрать из списка!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Input
    {
        public static int Menu()
        {
            int number;
            bool res;
            do {
                res = int.TryParse(Console.ReadLine(), out number);
                if (res == false)
                    Text.Error();
            } while (!res);
            return number;
        }
    }
    public class Menu
    {
        public static void Repeat()
        {
            do
            {
                Console.WriteLine("\n-----------------------");
                Console.WriteLine("| Выберите действие:  |");
                Console.WriteLine("| 1) Повторить ввод   |");
                Console.WriteLine("| 0) Выход из консоли |");
                Console.WriteLine("-----------------------");
                Console.Write("Действие: ");
                int input = Input.Menu();
                switch (input)
                {
                    case 1:
                        Main_program.Main();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.Write("Завершение работы.");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Environment.Exit(0);
                        break;
                    default:
                        Text.Error();
                        continue;
                }
            } while (!true);
        }
    }        
    public class Program
    {
        public static int[] BuildPrefix(string str)
        {
            int[] prefixes = new int[str.Length];
            int i = 1, j = 0;
            while (i < str.Length)
            {
                if (str[i] == str[j])
                {
                    prefixes[i] = j + 1;
                    ++i;
                    ++j;
                }
                else
                {
                    if (j > 0)
                    {
                        j = prefixes[j - 1];
                    }
                    else
                    {
                        prefixes[i] = 0;
                        ++i;
                    }
                }
            }
            return prefixes;
        }
        public static List<int> FindSubString(string subStr, string goalStr)
        {
            var listPositions = new List<int>();
            int i = 0, j = 0;
            int[] prefixes = BuildPrefix(subStr);
            while (i < goalStr.Length)
            {
                if (goalStr[i] == subStr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    if (j > 0)
                    {
                        j = prefixes[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }

                if (j == subStr.Length)
                {
                    listPositions.Add(i - subStr.Length);
                    j = prefixes[j - 1];
                }
            }
            return listPositions;
        }
    }
}