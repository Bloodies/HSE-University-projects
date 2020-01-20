using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _1.Binary_search__Двоичный_поиск_
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
            //int length = int.Parse(input.ReadLine());
            int length = int.Parse(Console.ReadLine());
            int[] array = new int[length];
            int index = 0;
            foreach (string element in /*input.ReadLine().Split(' ')*/Console.ReadLine().Split(' '))
            {
                array[index] = int.Parse(element);
                index++;
            }

            //input.ReadLine(); 
            Console.ReadLine();
            Console.WriteLine("\nВыходные данные:");
            foreach (string searchElement in /*input.ReadLine().Split(' ')*/Console.ReadLine().Split(' '))
            {
                (int, int) result = Search.BinarySearch(int.Parse(searchElement), 0, length - 1, array);
                //output.WriteLine(result.Item1 + " " + result.Item2);
                Console.WriteLine(result.Item1 + " " + result.Item2);
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
|  Дан массив из n элементов, упорядоченный в порядке неубывания, и m запросов: |
|  найти первое и последнее вхождение некоторого числа в массив.                |
|                                                                               |
|      Входные данные(input.txt)    ->     Выходные данные(output.txt)          |
|      *************                          ***********                       |
|      * 5         *                          * 1 2     *                       |
|      * 1 1 2 2 2 *                          * 3 5     *                       |
|      * 3         *                          * -1 -1   *                       |
|      * 1 2 3     *                          *         *                       |
|      *************                          ***********                       |
|      5 - размер массива                                                       |
|      1 1 2 2 2 - n чисел - элементы массива в порядке неубывания              |
|      3 - число запросов                                                       |
|      1 2 3 - запросы на нахождение этих чисел                                 |
|                                                                               |
|      1 2 - число '1' находится на позициях с 1 по 2                           |
|      3 5 - число '2' находится на позициях с 3 по 5                           |
|      -1 -1 - число '3' числа в массиве нет                                    |
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
    class Search
    {
        public static Dictionary<int, (int, int)> _requestHash = new Dictionary<int, (int, int)>();

        public static (int, int) BinarySearch(int element, int leftBoard, int rightBoard, int[] array)
        {
            if (_requestHash.ContainsKey(element))
            {
                return _requestHash[element];
            }
            else
            {
                while (leftBoard < rightBoard)
                {
                    int middleIndex = (leftBoard + rightBoard) / 2;
                    if (array[middleIndex] < element)
                    {
                        leftBoard = middleIndex + 1;
                    }
                    else
                    {
                        if (array[middleIndex] > element)
                        {
                            rightBoard = middleIndex - 1;
                        }
                        else
                        {
                            int firstPosition = middleIndex, lastPosition = middleIndex;
                            while (firstPosition > 0 && array[firstPosition - 1] == array[middleIndex])
                            {
                                --firstPosition;
                            }

                            while (lastPosition < rightBoard && array[lastPosition + 1] == array[middleIndex])
                            {
                                ++lastPosition;
                            }

                            _requestHash[element] = (firstPosition + 1, lastPosition + 1);
                            return _requestHash[element];
                        }
                    }
                }

                if (array[leftBoard] == element)
                {
                    return (leftBoard + 1, leftBoard + 1);
                }
                else
                {
                    return (-1, -1);
                }
            }
        }
    }
}
