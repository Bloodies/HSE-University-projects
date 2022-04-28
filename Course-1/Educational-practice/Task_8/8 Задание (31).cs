using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task_8
{
    class Program
    {
        // Получилось составить только рандомную матрицу, так как не хватило мозгов
        // Если составлять матрицу полностью через рандомайзер, то матрица не будет симметричной
        #region
        static Random rnd = new Random();
        static int[,] RandomMatrix()
        {
            int choose = rnd.Next(1, 6);
            switch (choose)
            {
                case 1:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 0, 0, 0 },
                            { 1, 0, 0, 1, 0, 1 },
                            { 1, 0, 0, 1, 1, 0 },
                            { 0, 1, 1, 0, 1, 1 },
                            { 0, 0, 1, 1, 0, 1 },
                            { 0, 1, 0, 1, 1, 0 }
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
                case 2:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 1, 0, 0, 0, 0 },
                            { 1, 0, 1, 0, 1, 0, 0, 0 },
                            { 1, 1, 0, 1, 1, 1, 0, 0 },
                            { 1, 0, 1, 0, 0, 1, 1, 0 },
                            { 0, 1, 1, 0, 0, 1, 0, 1 },
                            { 0, 0, 1, 1, 1, 0, 1, 1 },
                            { 0, 0, 0, 1, 0, 1, 0, 1 },
                            { 0, 0, 0, 0, 1, 1, 1, 0 }
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
                case 3:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 0, 0, 0, 0 },
                            { 1, 0, 0, 1, 0, 1, 0 },
                            { 1, 0, 0, 0, 1, 1, 1 },
                            { 0, 1, 0, 0, 1, 1, 0 },
                            { 0, 0, 1, 1, 0, 0, 1 },
                            { 0, 1, 1, 1, 0, 0, 0 },
                            { 0, 0, 1, 0, 1, 0, 0,},
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
                case 4:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 0 },
                            { 1, 0, 0, 1 },
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0,}
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
                case 5:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 0, 0, 0, 0, 0 },
                            { 1, 0, 0, 1, 0, 1, 0, 0 },
                            { 1, 0, 0, 0, 1, 1, 1, 0 },
                            { 0, 1, 0, 0, 1, 1, 0, 0 },
                            { 0, 0, 1, 1, 0, 0, 1, 1 },
                            { 0, 1, 1, 1, 0, 0, 0, 1 },
                            { 0, 0, 1, 0, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, 1, 0, 0,},
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
                case 6:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 0, 0, 0, 0, 1, 1 },
                            { 1, 0, 0, 1, 0, 1, 0, 1, 1 },
                            { 1, 0, 0, 0, 1, 1, 1, 0, 0 },
                            { 0, 1, 0, 0, 1, 1, 0, 0, 0 },
                            { 0, 0, 1, 1, 0, 0, 1, 0, 1 },
                            { 0, 1, 1, 1, 0, 0, 0, 0, 1 },
                            { 0, 0, 1, 0, 1, 0, 0, 1, 0 },
                            { 1, 1, 0, 0, 0, 0, 1, 0, 1 },
                            { 1, 1, 0, 0, 1, 1, 0, 1, 0 },
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
            }
            return null;
        }
        #endregion
        #region
        static List<List<int>> FindMinColors(int[,] m)
        {
            int l = m.GetLength(0);
            bool[] check = new bool[m.GetLength(0)];
            List<List<int>> colors = new List<List<int>>();
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    List<int> NewColor = new List<int>();
                    if (i != j && m[i, j] == 0 && !check[i] && !check[j])
                    {
                        NewColor.Add(i);
                        NewColor.Add(j);
                        check[i] = true; check[j] = true;
                        colors.Add(NewColor);
                    }
                    if (j == m.GetLength(1) - 1 && !check[i] && !check[j])
                    {
                        NewColor.Add(i);
                        check[i] = true;
                        colors.Add(NewColor);
                    }
                }
            }
            return colors;
        }
        static List<List<int>> MakeMoreColors(List<List<int>> c, int v, int k)
        {
            int count = c.Count;
            List<List<int>> c1 = new List<List<int>>();
            foreach (List<int> a in c)
            {
                if (a.Count == 2 && count != k)
                {
                    List<int> NC1 = new List<int>();
                    NC1.Add(a[0]);
                    c1.Add(NC1);
                    List<int> NC2 = new List<int>();
                    NC2.Add(a[1]);
                    c1.Add(NC2);
                    count++;
                }
                else c1.Add(a);
            }
            return c1;
        }
        #endregion
        #region
        static void ShowList(List<List<int>> c)
        {
            Console.WriteLine("\nЦвета   Вершины");
            int i = 1;
            foreach (List<int> a in c)
            {
                Console.Write(i + " цвет: ");
                foreach (int b in a)
                {
                    Console.Write(b + " ");
                }
                i++;
                Console.WriteLine();
            }
        }
        static void ShowArray(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write(string.Format("{0,3}", arr[i, j]));
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion
        static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.WriteLine("Практическое задание №8:");
                Console.WriteLine("Задача 31");
                Console.WriteLine(@"Граф задан матрицей смежности. 
Найти в нем какую-либо правильную раскраску с помощью K красок.");
                Console.WriteLine(" ");
                bool ok = false;
                int k = 0;
                do
                {
                    try
                    {
                        Console.WriteLine("Введите k (кол-во цветов):");
                        k = Convert.ToInt32(Console.ReadLine());
                        ok = true;
                        if (k <= 0 || k > 10)
                        {
                            ok = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Введите число от 0 до 10!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    catch
                    {
                        ok = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ошибка!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                while (!ok);
                Console.WriteLine("---------Матрица составленная рандомайзером----------");
                int[,] matrix = RandomMatrix();
                if (k > matrix.GetLength(0))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Число вершин меньше K");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    List<List<int>> colors = FindMinColors(matrix);
                    if (k < colors.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Не достаточно цветов, чтобы программа могла правильно раскрасить граф");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (k == colors.Count) ShowList(colors);
                    if (k > colors.Count)
                    {
                        int vertex = matrix.GetLength(0);
                        colors = MakeMoreColors(colors, vertex, k);
                        ShowList(colors);
                    }
                }
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Нажмите ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("любую клавишу");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" для повтора или ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Esc");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" для выхода...");
                int end = Console.ReadKey().KeyChar;
                Console.WriteLine(" ");
                switch (end)
                {
                    case 27:
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
                        Main();
                        continue;
                }
            } while (!true);
        }
    }
}