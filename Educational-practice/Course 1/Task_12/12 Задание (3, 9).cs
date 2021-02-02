using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

/* Практическое задание №12 (3, 9)
 * Выполнить сравнение двух предложенных методов сортировки одномерных массивов,
 * содержащих n элементов, по количеству пересылок и сравнений. 
 * Для этого необходимо выполнить программную реализацию двух методов сортировки, включив в нее подсчет количества пересылок 
 * (т.е. перемещений элементов с одного места на другое) и сравнений. 
 * Провести анализ методов сортировки для трех массивов: упорядоченного по возрастанию, упорядоченного по убыванию и неупорядоченного. 
 * Все три массива следует отсортировать обоими методами сортировки. 
 * Найти в литературе теоретические оценки сложности каждого из методов и сравнить их с оценками, полученными на практике. 
 * Сделать выводы о том, насколько отличаются теоретические и практические оценки количества операций, объяснить почему это происходит.
 * 
 * Сравнить оценки сложности двух алгоритмов:
     3. Сортировка простыми вставками.
     9. Сортировка простым выбором.
 */

namespace Task_12
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.WriteLine("Практическое задание №12:");
                Console.WriteLine("Задача 3, 9");
                Console.WriteLine(@"Практическое задание №12 (3, 9)
Выполнить сравнение двух предложенных методов сортировки одномерных массивов, содержащих n элементов, по количеству пересылок и сравнений. 
Для этого необходимо выполнить программную реализацию двух методов сортировки, включив в нее подсчет количества пересылок 
(т.е. перемещений элементов с одного места на другое) и сравнений. 

Провести анализ методов сортировки для трех массивов: упорядоченного по возрастанию, упорядоченного по убыванию и неупорядоченного. 
Все три массива следует отсортировать обоими методами сортировки. 

Найти в литературе теоретические оценки сложности каждого из методов и сравнить их с оценками, полученными на практике. 
Сделать выводы о том, насколько отличаются теоретические и практические оценки количества операций, объяснить почему это происходит.

Сравнить оценки сложности двух алгоритмов:
     3. Сортировка простыми вставками.
     9. Сортировка простым выбором.");
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Введите размер массива: ");
                Console.ForegroundColor = ConsoleColor.White;
                int n = ReadAnswer();
                int[] arr = new int[n];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = rnd.Next(0, 100);
                }
                int[] arr2 = new int[n];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr2[i] = rnd.Next(0, 100);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                #region region first array
                Console.WriteLine($"---------------------Неупорядоченный массив-----------------------\n");
                Print(arr);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                int resultForChange = 0;
                int resultForCompare = 0;
                Console.WriteLine($"Сортировка простыми вставками");
                Console.WriteLine($"Ожидаемый результат: {(n * n) / 2}");
                InsertionSort(arr2);
                Console.WriteLine();

                resultForChange = 0;
                resultForCompare = 0;
                SelectionSort(arr, out resultForChange, out resultForCompare);
                Console.WriteLine($"Сортировка простым выбором");
                Console.WriteLine($"   Ожидаемый результат: {n * n - 1}");
                Console.WriteLine($"   Реальный результат: {resultForChange + resultForCompare}, число перестановок: {resultForChange}, число сравнений {resultForCompare}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                #endregion
                #region region second array
                Console.WriteLine($"---------------Массив упорядоченный по возрастанию----------------\n");
                Print(arr);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                resultForChange = 0;
                resultForCompare = 0;
                Console.WriteLine($"Сортировка простыми вставками");
                Console.WriteLine($"   Ожидаемый резуьтат: {(n * n) / 2}");
                InsertionSort(arr2);
                Console.WriteLine();

                resultForChange = 0;
                resultForCompare = 0;
                SelectionSort(arr, out resultForChange, out resultForCompare);
                Console.WriteLine($"Сортировка простым выбором");
                Console.WriteLine($"   Ожидаемый результат: {n * n - 1}");
                Console.WriteLine($"   Реальный результат: {resultForChange + resultForCompare}, число перестановок: {resultForChange}, число сравнений {resultForCompare}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                #endregion
                #region region third array
                Console.WriteLine($"-----------------Массив упорядоченный по убыванию-----------------\n");
                Array.Reverse(arr);
                Array.Reverse(arr2);
                Print(arr);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                resultForChange = 0;
                resultForCompare = 0;
                Console.WriteLine($"Сортировка простыми вставками");
                Console.WriteLine($"   Ожидаемый резуьтат: {(n * n) / 2}");
                InsertionSort(arr2);

                resultForChange = 0;
                resultForCompare = 0;
                SelectionSort(arr, out resultForChange, out resultForCompare);
                Console.WriteLine();
                Console.WriteLine($"Сортировка простым выбором");
                Console.WriteLine($"   Ожидаемый результат: {n * n - 1}");
                Console.WriteLine($"   Реальный результат: {resultForChange + resultForCompare}, число перестановок: {resultForChange}, число сравнений {resultForCompare}");
                #endregion
                #region region menu
                Console.WriteLine(" ");
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
                        Main(args);
                        continue;
                }
                #endregion
            } while (!true);
        }
        #region
        static void SelectionSort(int[] arr, out int countForChange, out int countForCompare) // метод простого выбора
        {
            countForChange = 0;
            countForCompare = 0;
            int min, n_min, j;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                min = arr[i]; n_min = i;
                for (j = i + 1; j < arr.Length; j++)
                {
                    countForCompare++;
                    if (arr[j] < min) // поиск минимального
                    {
                        countForCompare++;
                        min = arr[j];
                        n_min = j;
                    }
                }
                if (arr[i] != arr[n_min])
                {
                    arr[n_min] = arr[i]; //обмен
                    countForChange++;
                    arr[i] = min;
                }
            }
        }
        static void InsertionSort(int[] a)
        {
            long changeInsertionSort = 0,
                 compareInsertionSort = 0;
            InsertionSortMove(a, ref changeInsertionSort, ref compareInsertionSort);
            Console.WriteLine($"   Реальный результат: число перестановок: {changeInsertionSort}, число сравнений {compareInsertionSort}");
        }
        static void InsertionSortMove(int[] a, ref long change, ref long compare)
        {
            int n = a.Length;
            int x;

            for (int i = 1; i < n; i++)
            {
                x = a[i];
                int j = i;
                while (j > 0 && a[j - 1] > x)
                {
                    compare++;
                    a[j] = a[j - 1];
                    change++;
                    j--;
                }
                compare++;//последнее сравнение не засчитано в цикле
                a[j] = x;
            }
        }
        #endregion
        static void Print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
        static int ReadAnswer()
        {
            int a = 0;
            bool ok = false;
            do
            {
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                    if (a > 0)
                        ok = true;
                    else throw new Exception();
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Неверный ввод! \nВведите целое число от 0 до 100000: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    ok = false;
                }
            } while (!ok);
            return a;
        }
    }
}