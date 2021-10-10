using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _1.Bunch__Куча_
{
    class Main_program
    {
        public static bool Heap(int[] heap)
        {
            for (int i = 1; i <= heap.Length; i++)
            {
                if (2 * i <= heap.Length && heap[i - 1] > heap[2 * i - 1])
                    return false;
                if (2 * i + 1 <= heap.Length && heap[i - 1] > heap[2 * i])
                    return false;
            }
            return true;
        }
        public static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Text.Task();
            //string input_f = "input.txt", output_f = "output.txt";
            //using (FileStream sf = new FileStream(input_f, FileMode.OpenOrCreate)) { }
            //using (FileStream sf = new FileStream(output_f, FileMode.OpenOrCreate)) { }
            //using (StreamReader input = new StreamReader(input_f))
            {
                //input.ReadLine();
                Console.WriteLine("Вводите данные как показано выше:");
                Console.WriteLine("\nВходные данные:");
                Console.ReadLine();
                //using (StreamWriter output = new StreamWriter(output_f))
                {
                    //string[] strs = input.ReadLine().Trim().Split(new char[] { ' ' });
                    string[] strs = Console.ReadLine().Trim().Split(new char[] { ' ' });
                    int[] arr = new int[strs.Length];
                    for (int i = 0; i < strs.Length; i++)
                        arr[i] = Convert.ToInt32(strs[i]);
                    Console.WriteLine("\nВыходные данные:");
                    if (Heap(arr))
                        //output.WriteLine("YES");
                        Console.WriteLine("YES");
                    else
                        //output.WriteLine("NO");
                        Console.WriteLine("NO");
                }
            }
            Menu.Repeat();
        }        
    }
    public class Text
        {
            public static void Task()
            {
                Console.WriteLine(@"----------------------------------------------------------------------------------------------------------------
|  Структуру данных «куча», или, более конкретно, «неубывающая пирамида», можно реализовать на основе массива. |
|  Определите, является ли он неубывающей пирамидой.                                                           |
|                                                                                                              |
|      Входные данные(input.txt)    ->     Выходные данные(output.txt)                                         |
|      *************                          ***********                                                      |
|      * 5         *                          * NO      *                                                      |
|      * 1 0 1 2 0 *                          * 3 5     *                                                      |
|      *************                          ***********                                                      |
|                                                                                                              |
|      В неубывающей пирамиде родительский элемент всегда должен быть меньше ребенка                           |
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