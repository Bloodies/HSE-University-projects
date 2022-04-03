using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _1.Plenty__Множество_
{
    public class Main_program
    {
        public static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Text.Task();
            //StreamReader input = new StreamReader("input.txt");
            //StreamWriter output = new StreamWriter("output.txt");
            //int length = int.Parse(input.ReadLine());
            Console.WriteLine("Вводите данные как показано выше");
            Console.WriteLine("\nВходные данные:");
            Console.Write("Введите количество операций: ");
            int length = int.Parse(Console.ReadLine());
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>(length);
            for (int i = 1; i <= length; i++)
            {
                //string[] tokens = input.ReadLine().Split(' ');                
                string[] tokens = Console.ReadLine().Split(' ');
                switch (tokens[0][0])
                {
                    case 'A':
                        {
                            if (!dictionary.ContainsKey(tokens[1]))
                                dictionary[tokens[1]] = true;
                            break;
                        }
                    case 'D':
                        {
                            if (dictionary.ContainsKey(tokens[1]))
                                dictionary.Remove(tokens[1]);
                            break;
                        }
                    case '?':
                        {
                            Console.WriteLine("\nВыходные данные:");
                            if (dictionary.ContainsKey(tokens[1]))
                            {
                                //output.WriteLine('Y');
                                Console.WriteLine('Y');
                            }
                            else
                            {
                                //output.WriteLine('N');
                                Console.WriteLine('N');
                            }
                            break;
                        }
                }
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
            Console.WriteLine(@"------------------------------------------------------------------------------------------------------------
|  Реализуйте множество с операциями «добавление ключа», «удаление ключа», «проверка существования ключа». |
|                                                                                                          |
|      Входные данные(input.txt)    ->     Выходные данные(output.txt)                                     |
|      *********                           ********                                                        |
|      * 8     *                           * Y    *                                                        |
|      * A 2   *                           * N    *                                                        |
|      * A 5   *                           * N    *                                                        |
|      * A 3   *                           *      *                                                        |
|      * ? 2   *                           *      *                                                        |
|      * ? 4   *                           *      *                                                        |
|      * A 2   *                           *      *                                                        |
|      * D 2   *                           *      *                                                        |
|      * ? 2   *                           *      *                                                        |
|      *********                           ********                                                        |
|      8 - количество операций                                                                             |
|      A x - команда add - добавление x в множество                                                        |
|      D x - команда delete - удаление x из множества                                                      |
|      ? x - команда ask - есть ли x в множестве                                                           |
|                                                                                                          |
|      Y - элемент 2 (? 2) есть в множестве                                                                |
|      N - элемент 4 (? 4) отсутствует                                                                     |
------------------------------------------------------------------------------------------------------------
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
            do
            {
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
}