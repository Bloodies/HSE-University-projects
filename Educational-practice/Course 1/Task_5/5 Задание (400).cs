using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Task_5
{
    //---Все менюшки----------------------------------------------
    #region region menu
    class Text_Dialog
    {
        public static void PrintErrorMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нужно выбрать из списка!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void RestartMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n---!!!!!------Матрица пуста-----!!!!!!-----");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Выберите действие:         |");
            Console.WriteLine("| 9) Построить новую матрицу |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
            int check = Search_for_Bugs.ProverkaVvoda();
            switch (check)
            {
                case 9:
                    Program.Main();
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
                    Text_Dialog.PrintErrorMenu();
                    break;
            }
        }
        public static void EndMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Выберите действие:         |");
            Console.WriteLine("| 9) Построить новую матрицу |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
            int check = Search_for_Bugs.ProverkaVvoda();
            switch (check)
            {
                case 9:
                    Program.Main();
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
                    Text_Dialog.PrintErrorMenu();
                    break;
            }
        }
    }
    #endregion
    //------------------------------------------------------------
    //---Проверка ввода-------------------------------------------
    #region region input check
    class Search_for_Bugs
    {
        public static int ProverkaVvoda() 
        {
            int number;
            bool res;
            do
            {
                res = int.TryParse(Console.ReadLine(), out number);

                if (res == false)
                {
                    Console.WriteLine("Некорректный ввод");
                }
            } while (!res);
            return number;
        }
        public static double GetDouble()
        {
            double a = 0;
            bool protect = true;
            do
            {
                try
                {
                    a = Convert.ToDouble(Console.ReadLine());
                    protect = true;
                }
                catch
                {
                    protect = false;
                    Console.WriteLine("Неверный ввод элемента матрицы!");
                }
            } while (!protect);
            return a;
        }
        public static int GetMatrixSize()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int size = 0;
            bool access;
            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    size = Convert.ToInt32(Console.ReadLine());
                    if (size < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Размер матрицы не может быть меньше 0");
                        Console.Write("Повторите ввод: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        access = false;
                    }
                    else
                    {
                        access = true;
                    }
                }
                catch
                {
                    access = false;
                    Console.WriteLine("Неверно задан размер матрицы");
                }
            } while (!access);
            return size;
        }
    }
    #endregion
    //------------------------------------------------------------
    class Program
    {        
        public static void Main()
        {
            Console.Clear();
            int Size = 0,
                ArrayMin = 0,
                ArrayMax = 100;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Практическое задание №5:");
            Console.WriteLine("Задача 400");
            Console.WriteLine(@"Дана действительная квадратная матрица порядка n.\nПолучить x1*xn + x2*xn-1 + ... + xn*x1
где xk-наибольшее значение элементов k-й строки данной матрицы.");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.White;
            do
            {                
                Console.WriteLine("------------------------------");
                Console.WriteLine("| Выберите вид матрицы:      |");
                Console.WriteLine("| 1) Рандомная матрица       |");
                Console.WriteLine("| 2) Матрица с вводом        |");
                Console.WriteLine("| 0) Выход из консоли        |");
                Console.WriteLine("------------------------------");
                Console.Write("Действие: ");
                int check = Search_for_Bugs.ProverkaVvoda();
                switch (check)
                {
                    case 1:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("------------Формирование матрицы--------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Введите размерность матрицы: ");
                        Size = Search_for_Bugs.GetMatrixSize();
                        Console.Write("\nВведите нижнюю границу матрицы:");
                        ArrayMin = Search_for_Bugs.ProverkaVvoda();
                        do
                        {
                            Console.Write("Введите верхнюю границу матрицы:");
                            ArrayMax = Search_for_Bugs.ProverkaVvoda();
                            if (ArrayMax < ArrayMin)
                            {
                                Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                            }
                        } while (ArrayMax < ArrayMin);
                        Random rand = new Random();
                        if (Size > 0)
                        {
                            double[,] matrix = new double[Size, Size];
                            for (int i = 0; i < Size; i++)
                            {
                                for (int j = 0; j < Size; j++)
                                {
                                    matrix[i, j] = rand.Next(ArrayMin, ArrayMax);
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("------------Матрица--------------");
                            Console.ForegroundColor = ConsoleColor.White;
                            for (int i = 0; i < Size; i++)
                            {
                                for (int j = 0; j < Size; j++)
                                {
                                    Console.Write(matrix[i, j] + " ");
                                }
                                Console.WriteLine();
                            }
                            double[] maxStringValues = new double[Size];
                            for (int i = 0; i < Size; i++)
                            {
                                double max = matrix[i, 0];
                                for (int j = 0; j < Size; j++)
                                {

                                    if (max < matrix[i, j])
                                    {
                                        max = matrix[i, j];
                                    }
                                }
                                maxStringValues[i] = max;
                            }
                            Console.WriteLine(" ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Максимумы в строчках:");
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (double numbers in maxStringValues)
                            {
                                Console.Write(numbers + " ");
                            }
                            double sum = 0;
                            for (int a = 0; a < Size; a++)
                            {
                                sum += maxStringValues[a] * maxStringValues[(Size - 1) - a];
                            }
                            Console.WriteLine(" ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nСумма:");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("S = " + sum);
                            Console.WriteLine(" ");
                            Text_Dialog.EndMenu();
                        }
                        else
                        {
                            Text_Dialog.RestartMenu();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("------------Формирование матрицы--------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Введите размерность матрицы: ");
                        Size = Search_for_Bugs.GetMatrixSize();
                        if (Size > 0)
                        {
                            double[,] matrix = new double[Size, Size];
                            for (int i = 0; i < Size; i++)
                            {
                                for (int j = 0; j < Size; j++)
                                {
                                    Console.Write("введите [{0},{1}] элемент матрицы: ", i, j);
                                    matrix[i, j] = Search_for_Bugs.GetDouble();
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("------------Матрица--------------");
                            Console.ForegroundColor = ConsoleColor.White;
                            for (int i = 0; i < Size; i++)
                            {
                                for (int j = 0; j < Size; j++)
                                {
                                    Console.Write(matrix[i, j] + " ");
                                }
                                Console.WriteLine();
                            }
                            double[] maxStringValues = new double[Size];
                            for (int i = 0; i < Size; i++)
                            {
                                double max = matrix[i, 0];
                                for (int j = 0; j < Size; j++)
                                {

                                    if (max < matrix[i, j])
                                    {
                                        max = matrix[i, j];
                                    }
                                }
                                maxStringValues[i] = max;
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Максимумы в строчках:");
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (double numbers in maxStringValues)
                            {
                                Console.Write(numbers + " ");
                            }
                            double sum = 0;
                            for (int a = 0; a < Size; a++)
                            {
                                sum += maxStringValues[a] * maxStringValues[(Size - 1) - a];
                            }
                            Console.WriteLine(" ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nСумма:");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("S = " + sum);
                            Console.WriteLine(" ");
                            Text_Dialog.EndMenu();
                        }
                        else
                        {
                            Text_Dialog.RestartMenu();
                        }
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
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
            } while (true);
        }
    }
}