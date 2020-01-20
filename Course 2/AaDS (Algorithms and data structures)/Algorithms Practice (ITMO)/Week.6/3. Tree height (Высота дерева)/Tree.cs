using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _3.Tree_height__Высота_дерева_
{
    public class Main_program
    {
        public static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Text.Task();
            Console.WriteLine("Вводите данные как показано выше");
            Console.WriteLine("\nВходные данные:");
            //StreamReader input = new StreamReader("input.txt");
            //StreamWriter output = new StreamWriter("output.txt");
            //int n = int.Parse(input.ReadLine());
            int n = int.Parse(Console.ReadLine());
            
            int[,] root = new int[n, 4];
            //0 - h
            //1 - right
            //2 - left
            //3 - parent
            for (int i = 0; i < n; i++)
            {
                //string[] line = input.ReadLine().Split(' ');
                string[] line = Console.ReadLine().Split(' ');
                root[i, 0] = 0;
                root[i, 1] = int.Parse(line[1]) - 1;
                root[i, 2] = int.Parse(line[2]) - 1;
                root[i, 3] = -1;
            }

            for (int i = 0; i < n; i++)
            {
                if (root[i, 1] != -1)
                    root[root[i, 1], 3] = i;
                if (root[i, 2] != -1)
                    root[root[i, 2], 3] = i;
            }
            int h = 0;
            bool[] hcalc = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (root[i, 3] == -1)
                {
                    hcalc[i] = true;
                    root[i, 0] = 1;
                    break;
                }
            }

            for (int i = 0; i < Math.Log(n, 2) + 1; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (hcalc[j])
                    {
                        if (root[j, 1] != -1)
                        {
                            root[root[j, 1], 0] = root[j, 0] + 1;
                            hcalc[root[j, 1]] = true;
                        }
                        if (root[j, 2] != -1)
                        {
                            root[root[j, 2], 0] = root[j, 0] + 1;
                            hcalc[root[j, 2]] = true;
                        }
                    }
                }
            }
            h = 0;
            for (int i = 0; i < n; i++)
            {
                h = Math.Max(root[i, 0], h);
            }
            Console.WriteLine("\nВыходные данные:");
            Console.Write(h);
            //output.Write(h);
            //output.Close();
            //input.Close();
            Menu.Repeat();
        }
    }
    public class Text
    {
        public static void Task()
        {
            Console.WriteLine(@"----------------------------------------------------------------------------------------------------------------
|  Высотой дерева называется максимальное число вершин дерева в цепочке,                                       |
|  начинающейся в корне дерева, заканчивающейся в одном из его листьев, и не содержащей никакую вершину дважды.|
|                                                                                                              |
|  Так, высота дерева, состоящего из единственной вершины, равна единице.                                      | 
|  Высота пустого дерева равна нулю. Высота дерева, изображенного на рисунке, равна четырем.                   |
|                                                                                                              |
|                                  -2                                                                          |
|                                         8                                                                    |
|                                   3           9                                                              |
|                             0           6                                                                    |
|                                                                                                              |
|      Входные данные(input.txt)    ->     Выходные данные(output.txt)                                         |
|      **********                          *******                                                             |
|      * 6      *                          * 4   *                                                             |
|      * -2 0 2 *                          *     *                                                             |
|      * 8 4 3  *                          *     *                                                             |
|      * 9 0 0  *                          *     *                                                             |
|      * 3 6 5  *                          *     *                                                             |
|      * 6 0 0  *                          *     *                                                             |
|      * 0 0 0  *                          *     *                                                             |
|      **********                          *******                                                             |
|      6 - число вершин                                                                                        |
|      x1 x2 x3 - описание каждой вершины                                                                      |
|      x1 - корень                                                                                             |
|      x2 - левый лист                                                                                         |
|      x3 - правый лист                                                                                        |
|                                                                                                              |
|      4 - высота дерева                                                                                       |
----------------------------------------------------------------------------------------------------------------
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