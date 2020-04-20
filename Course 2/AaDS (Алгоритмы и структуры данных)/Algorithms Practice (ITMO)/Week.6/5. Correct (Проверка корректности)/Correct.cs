using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _5.Correct__Проверка_корректности_
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

            int[,] roots = new int[n, 6];
            //0 - key
            //1 - right 
            //2 - left 
            //3 - parent 
            //4 - min (левая граница)
            //5 - max (правая граница)

            //заполняем дерево
            for (int i = 0; i < n; i++)
            {
                //string[] line = input.ReadLine().Split(' ');
                string[] line = Console.ReadLine().Split(' ');
                roots[i, 0] = int.Parse(line[0]);
                roots[i, 1] = int.Parse(line[1]) - 1;
                roots[i, 2] = int.Parse(line[2]) - 1;
                roots[i, 3] = -1;
            }

            //заполняем родителей, опираясь на массив (корень дерева не будет иметь предков, т.е. parent = -1) 
            //это поможет нам найти корень, чтобы запустить алгоритм проверки
            for (int i = 0; i < n; i++)
            {
                if (roots[i, 1] != -1)
                    roots[roots[i, 1], 3] = i;
                if (roots[i, 2] != -1)
                    roots[roots[i, 2], 3] = i;
            }

            //будем отмечать пройденные вершины в алгоритме
            bool[] hcheck = new bool[n];

            //найдем корень, отметим его как пройденный, установим max и min значения для его потомков
            for (int i = 0; i < n; i++)
            {
                if (roots[i, 3] == -1)
                {
                    hcheck[i] = true;
                    roots[i, 4] = int.MinValue;
                    roots[i, 5] = int.MaxValue;
                }
            }

            bool correctree = true; //переменная, отвечающая за кореектность дерева

            /*Алгоритм
			 для корня(вершины) дерева установим нижнюю и верхнюю границу минус и плюс бесконечность соответственно
			 далее будем идти от корня дерева в листьям, выполняя следующее:
			 1. проверим, что ключ дерева принаждлежит интервалу границ
			 2. установим левую и правую границу для потомков:
					левый потомок наследует левую границу предка
					правая граница левого потомка равна корню предка
					правый потомок наследует правую границу 
					левая граница правого потомка равна корню предка
			*/


            //запускаем алгоритм обхода вершин (h <= log2(n), т.е. за log2(n) проходов по массиву мы гаранитровано проверим все дерево)
            for (int i = 0; i < Math.Log(n, 2) + 1; i++)
            {
                for (int j = 0; j < n; j++)
                {

                    if (hcheck[j]) //если нашли проверенное поддерево, проверяем его потомков
                    {
                        hcheck[j] = false; //чтобы не проверять одни корни несколько раз
                        if (roots[j, 1] != -1)
                        {
                            roots[roots[j, 1], 4] = roots[j, 4];
                            roots[roots[j, 1], 5] = roots[j, 0];
                            if (!(roots[roots[j, 1], 0] > roots[roots[j, 1], 4] && roots[roots[j, 1], 0] < roots[roots[j, 1], 5]))
                            {
                                correctree = false;
                                break;
                            }
                        }
                        if (roots[j, 2] != -1)
                        {
                            roots[roots[j, 2], 4] = roots[j, 0];
                            roots[roots[j, 2], 5] = roots[j, 5];
                            if (!(roots[roots[j, 2], 0] > roots[roots[j, 2], 4] && roots[roots[j, 2], 0] < roots[roots[j, 2], 5]))
                            {
                                correctree = false;
                                break;
                            }
                        }

                        if (roots[j, 1] != -1)
                            hcheck[roots[j, 1]] = true;
                        if (roots[j, 2] != -1)
                            hcheck[roots[j, 2]] = true;

                    }
                }
                if (correctree == false) break; //преждевременный выход из цикла
            }
            Console.WriteLine("\nВыходные данные:");
            if (correctree == true)
                //output.Write("YES");
                Console.Write("YES");
            else
                //output.Write("NO");
                Console.Write("NO");
            //output.Close();
            //input.Close();
            Menu.Repeat();
        }
    }
    public class Text
    {
        public static void Task()
        {
            Console.WriteLine(@"---------------------------------------------------------------------------------
|  Проверьте, выполняется ли для него свойство двоичного дерева поиска          |
|                                                                               |
|      Входные данные(input.txt)    ->     Выходные данные(output.txt)          |
|      ************                          ***********                        |
|      * 6        *                          * YES     *                        |
|      * -2 0 2   *                          *         *                        |
|      * 8 4 3    *                          *         *                        |
|      * 9 0 0    *                          *         *                        |
|      * 3 6 5    *                          *         *                        |
|      * 6 0 0    *                          *         *                        |
|      * 0 0 0    *                          *         *                        |
|      ************                          ***********                        |
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