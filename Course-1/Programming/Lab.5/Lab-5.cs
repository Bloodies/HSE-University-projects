using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._5
{
    class Text_Dialog
    {
        //---Все менюшки----------------------------------------------
        public static void PrintMenuONEmer()
        {
            Console.WriteLine("\n----------------------------------------------");
            Console.WriteLine("| Выберите следующее действие:               |");
            Console.WriteLine("| 1) Удалить элементы с чётными индексами    |");
            Console.WriteLine("| 2) Построить новый одномерный массив       |");
            Console.WriteLine("| 9) Вернуться в начало                      |");
            Console.WriteLine("| 0) Выход                                   |");
            Console.WriteLine("----------------------------------------------");
            Console.Write("Действие: ");
        }
        public static void PrintMenuTWOmer()
        {
            Console.WriteLine("\n----------------------------------------------------------------");
            Console.WriteLine("| Выберите следующее действие:                                 |");
            Console.WriteLine("| 1) Добавить K столбцов начиная со столбца под номером N      |");
            Console.WriteLine("| 2) Построить новый двумерный массив                          |");
            Console.WriteLine("| 9) Вернуться в начало                                        |");
            Console.WriteLine("| 0) Выход                                                     |");
            Console.WriteLine("----------------------------------------------------------------");
            Console.Write("Действие: ");
        }
        public static void PrintPodMenuTWOmer()
        {
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Как вы хотите добавлять?   |");
            Console.WriteLine("| 1) Рандомно                |");
            Console.WriteLine("| 2) Вручную                 |");
            Console.WriteLine("| 9) Вернуться в начало      |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
        }
        public static void PrintMenuRvanM()
        {
            Console.WriteLine("\n----------------------------------------------");
            Console.WriteLine("| Выберите следующее действие:               |");
            Console.WriteLine("| 1) Добавить строку с заданным номером      |");
            Console.WriteLine("| 2) Построить новый рваный массив           |");
            Console.WriteLine("| 9) Вернуться в начало                      |");
            Console.WriteLine("| 0) Выход                                   |");
            Console.WriteLine("----------------------------------------------");
            Console.Write("Действие: ");
        }
        public static void PrintPodMenuRvanM()
        {
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Как вы хотите добавлять?   |");
            Console.WriteLine("| 1) Рандомно                |");
            Console.WriteLine("| 2) Вручную                 |");
            Console.WriteLine("| 9) Вернуться в начало      |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
        }
        public static void PrintErrorMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нужно выбрать из списка!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        //------------------------------------------------------------
    }
    class Search_for_Bugs
    {
        //---Проверка ввода-------------------------------------------
        public static int ProverkaMassiva(string title, int ArrayMin, int right)  
        {
            bool ok = false;
            int number = ArrayMin;
            do
            {
                Console.WriteLine(title);
                try
                {
                    int buf = int.Parse(Console.ReadLine());
                    number = Convert.ToInt32(buf);
                    if (number >= ArrayMin && number < right)
                        ok = true;
                    else
                    {
                        Console.WriteLine("Неверный ввод\nПопробуйте снова");
                        ok = false;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверный ввод\nПопробуйте снова");
                    ok = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Неверный ввод\nПопробуйте снова");
                    ok = false;
                }
            } while (!ok);
            return number;
        }
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
        public static int InputNumber(string Text, int minSize, int maxSize)
        {
            int number = 0;
            bool ok = false;
            do
            {
                try
                {
                    Console.WriteLine(Text);
                    number = Convert.ToInt32(Console.ReadLine());
                    if (number >= minSize && number < maxSize) ok = true;
                    else ok = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
                }

            } while (!ok);
            return number;

        }
        //------------------------------------------------------------
    }
    class Program
    {
        const int MinSize = 1;
        const int MaxSize = 100;
        const int MinNumber = -100;
        const int MaxNumber = 100;

        static void RestartMenu()
        {
            Console.Clear();
            Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Выберите действие:         |");
            Console.WriteLine("| 9) Вернуться в начало      |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
            int check = Search_for_Bugs.ProverkaVvoda();
            switch (check)
            {
                case 9:
                    Main();
                    break;
                case 0:
                    Environment.Exit(0);  //Выход из консоли
                    break;
                default:
                    Text_Dialog.PrintErrorMenu();
                    break;
            }
        }
        //---ОДНОМЕРНЫЙ МАССИВ----------------------------------------------------------------------------------------------------
        #region
        static void Massiv(ref int elementi, ref int ArrayMin, ref int ArrayMax, ref int[] massiv) //Меню с массивами
        {
            do
            {
                Console.Clear();
                massiv = null;
                Console.WriteLine("\n------------------------------");
                Console.WriteLine("| Выберите вид массива:      |");
                Console.WriteLine("| 1) Рандомный массив        |");
                Console.WriteLine("| 2) Массив с вводом         |");
                Console.WriteLine("| 9) Вернуться в начало      |");
                Console.WriteLine("| 0) Выход из консоли        |");
                Console.WriteLine("------------------------------");
                Console.Write("Действие: ");
                int check = Search_for_Bugs.ProverkaVvoda();
                switch (check)
                {
                    case 1:
                        Console.WriteLine("----------Формирование массива----------------");
                        Console.Write("Введите нижнюю границу массива:");
                        ArrayMin = Search_for_Bugs.ProverkaVvoda();
                        do
                        {
                            Console.Write("Введите верхнюю границу массива:");
                            ArrayMax = Search_for_Bugs.ProverkaVvoda();
                            if (ArrayMax < ArrayMin)
                            {
                                Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                            }
                        } while (ArrayMax < ArrayMin);
                        Console.Write("Введите количество элементов в массиве:");
                        elementi = Search_for_Bugs.ProverkaMassiva("", MinSize, MaxSize);
                        massiv = MassivRandom(elementi, ref ArrayMin, ref ArrayMax);
                        if (elementi > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("------------------Массив-----------------\n");
                            foreach (var i in massiv) //Построение нового массива
                            {
                                Console.Write(" " + i);
                            }
                            Console.WriteLine("\n-----------------------------------------");
                            do
                            {
                                Text_Dialog.PrintMenuONEmer();
                                int ElementPodmenu = Search_for_Bugs.ProverkaVvoda();
                                switch (ElementPodmenu) //Выбор действия из меню
                                {
                                    case 1:
                                        Udalenie(ref massiv, ref elementi, ref ArrayMin, ref ArrayMax);
                                        break;
                                    case 2:
                                        Massiv(ref elementi, ref ArrayMin, ref ArrayMax, ref massiv);
                                        break;
                                    case 9:
                                        Main();
                                        break;
                                    case 0:
                                        Environment.Exit(0);  //Выход из консоли
                                        break;
                                    default:
                                        Text_Dialog.PrintErrorMenu();
                                        continue;
                                }
                            } while (true);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 2:
                        Console.WriteLine("----------Формирование массива----------------");
                        Console.Write("Введите количество элементов в массиве:");
                        elementi = Search_for_Bugs.ProverkaMassiva("", MinSize, MaxSize);
                        if (elementi > 0)
                        {
                            massiv = MassivVvod(elementi);
                            Console.Clear();
                            Console.WriteLine("------------------Массив-----------------\n");
                            foreach (var i in massiv) //Построение нового массива
                            {
                                Console.Write(" " + i);
                            }
                            Console.WriteLine("\n-----------------------------------------");
                            do
                            {
                                Text_Dialog.PrintMenuONEmer();
                                int ElementPodmenu = Search_for_Bugs.ProverkaVvoda();

                                switch (ElementPodmenu) //Выбор действия из меню
                                {
                                    case 1:
                                        Udalenie(ref massiv, ref elementi, ref ArrayMin, ref ArrayMax);
                                        break;
                                    case 2:
                                        Massiv(ref elementi, ref ArrayMin, ref ArrayMax, ref massiv);
                                        break;
                                    case 9:
                                        Main();
                                        break;
                                    case 0:
                                        Environment.Exit(0);  //Выход из консоли
                                        break;
                                    default:
                                        Text_Dialog.PrintErrorMenu();
                                        continue;
                                }
                            } while (true);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 9:
                        Main();
                        break;
                    case 0:
                        Environment.Exit(0);  //Выход из консоли
                        break;
                    default:
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
            } while (!true);
        }
        static int[] MassivRandom(int elementi, ref int ArrayMin, ref int ArrayMax)
        {
            Random rnd = new Random();
            int[] massiv = new int[elementi];
            for (int i = 0; i < elementi; i++)
            {
                massiv[i] = rnd.Next(ArrayMin, ArrayMax);
            }
            return massiv;
        }
        static int[] MassivVvod(int elementi)
        {
            int[] massiv = new int[elementi];
            for (int i = 0; i < elementi; i++)
            {
                massiv[i] = Search_for_Bugs.ProverkaMassiva("Введите элемент массива из диапазона от -100 до 100", MinNumber, MaxNumber);
            }
            return massiv;
        }
        static void Udalenie(ref int[] massiv, ref int elementi, ref int ArrayMin, ref int ArrayMax)
        {
            Console.Clear();
            Console.WriteLine("------------------Массив-----------------\n");
            foreach (var i in massiv) //Построение нового массива
            {
                Console.Write(" " + i);
            }
            Console.WriteLine("\n-------------Новый массив----------------");
            int[] NewMassiv = new int[massiv.Length / 2];
            if (massiv.Length % 2 == 0)
            {
                NewMassiv = new int[massiv.Length / 2];
            }
            else
            {
                NewMassiv = new int[(massiv.Length / 2) + 1];
            }
            int v = 0;
            if (elementi > 1)
            {
                for (int i = 0; i < massiv.Length; i++)
                    if (i % 2 == 0)
                    {
                        NewMassiv[v] = massiv[i];
                        v++;
                    }
            }
            else
            {
                Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
            }
            foreach (var i in NewMassiv) //Построение нового массива
            {
                Console.Write(" " + i);
            }
            Console.WriteLine("\n-----------------------------------------");
            do
            {
                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("| Выберите следующее действие:               |");
                Console.WriteLine("| 1) Построить новый одномерный массив       |");
                Console.WriteLine("| 9) Вернуться в начало                      |");
                Console.WriteLine("| 0) Выход                                   |");
                Console.WriteLine("----------------------------------------------");
                Console.Write("Действие: ");
                int ElementPodmenu = Search_for_Bugs.ProverkaVvoda();

                switch (ElementPodmenu) //Выбор действия из меню
                {
                    case 1:
                        Massiv(ref elementi, ref ArrayMin, ref ArrayMax, ref massiv);
                        break;
                    case 9:
                        Main();
                        break;
                    case 0:
                        Environment.Exit(0);  //Выход из консоли
                        break;
                    default:
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
            } while (true);
        }
        #endregion
        //------------------------------------------------------------------------------------------------------------------------
        //---ДВУМЕРНЫЙ МАССИВ-----------------------------------------------------------------------------------------------------
        #region
        static int[,] DvumerniiArray(int ArrayMin, int ArrayMax, ref int stringSize, ref int columnSize, ref int[,] dvumernii_array)
        {
            do
            {
                Console.Clear();
                dvumernii_array = null;
                Console.WriteLine("\n------------------------------");
                Console.WriteLine("| Выберите вид массива:      |");
                Console.WriteLine("| 1) Рандомный массив        |");
                Console.WriteLine("| 2) Массив с вводом         |");
                Console.WriteLine("| 9) Вернуться в начало      |");
                Console.WriteLine("| 0) Выход из консоли        |");
                Console.WriteLine("------------------------------");
                Console.Write("Действие: ");
                int check = Search_for_Bugs.ProverkaVvoda();
                switch (check)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("----------Формирование массива----------------");
                        Console.Write("Ввдеите количество строк: ");
                        stringSize = Search_for_Bugs.ProverkaVvoda();

                        if (stringSize > 0)
                        {
                            RanomDvumerniiArray(ArrayMin, ArrayMax, ref stringSize, ref columnSize, ref dvumernii_array);
                            do
                            {
                                Text_Dialog.PrintMenuTWOmer();
                                int ElementPodmenu = Search_for_Bugs.ProverkaVvoda();
                                switch (ElementPodmenu) //Выбор действия из меню
                                {
                                    case 1:
                                        Text_Dialog.PrintPodMenuTWOmer();
                                        int ElementPodPodmenu = Search_for_Bugs.ProverkaVvoda();
                                        switch (ElementPodPodmenu)
                                        {
                                            case 1:
                                                Console.Clear();
                                                RandomVvod(ArrayMin, ArrayMax, ref stringSize, ref columnSize, ref dvumernii_array);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                RuchnoiVvod(ref stringSize, ref columnSize, ref dvumernii_array);
                                                break;
                                            case 9:
                                                Main();
                                                break;
                                            case 0:
                                                Environment.Exit(0);  //Выход из консоли
                                                break;
                                            default:
                                                Text_Dialog.PrintErrorMenu();
                                                continue;
                                        }
                                        break;
                                    case 2:
                                        DvumerniiArray(ArrayMin, ArrayMax, ref stringSize, ref columnSize, ref dvumernii_array);
                                        break;
                                    case 9:
                                        Main();
                                        Console.Clear();
                                        break;
                                    case 0:
                                        Environment.Exit(0);  //Выход из консоли
                                        break;
                                    default:
                                        Text_Dialog.PrintErrorMenu();
                                        continue;
                                }
                            } while (true);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("----------Формирование массива----------------");
                        Console.Write("Ввдеите количество строк: ");
                        stringSize = Search_for_Bugs.ProverkaVvoda();

                        if (stringSize > 0)
                        {
                            VvodDvumerniiArray(ref stringSize, ref columnSize, ref dvumernii_array);
                            do
                            {
                                Text_Dialog.PrintMenuTWOmer();
                                int ElementPodmenu = Search_for_Bugs.ProverkaVvoda();
                                switch (ElementPodmenu) //Выбор действия из меню
                                {
                                    case 1:
                                        Text_Dialog.PrintPodMenuTWOmer();
                                        int ElementPodPodmenu = Search_for_Bugs.ProverkaVvoda();
                                        switch (ElementPodPodmenu)
                                        {
                                            case 1:
                                                Console.Clear();
                                                RandomVvod(ArrayMin, ArrayMax, ref stringSize, ref columnSize, ref dvumernii_array);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                RuchnoiVvod(ref stringSize, ref columnSize, ref dvumernii_array);
                                                break;
                                            case 9:
                                                Main();
                                                break;
                                            case 0:
                                                Environment.Exit(0);  //Выход из консоли
                                                break;
                                            default:
                                                Text_Dialog.PrintErrorMenu();
                                                continue;
                                        }
                                        break;
                                    case 2:
                                        DvumerniiArray(ArrayMin, ArrayMax, ref stringSize, ref columnSize, ref dvumernii_array);
                                        break;
                                    case 9:
                                        Main();
                                        break;
                                    case 0:
                                        Environment.Exit(0);  //Выход из консоли
                                        break;
                                    default:
                                        Text_Dialog.PrintErrorMenu();
                                        continue;
                                }
                            } while (true);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 9:
                        Main();
                        break;
                    case 0:
                        Environment.Exit(0);  //Выход из консоли
                        break;
                    default:
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
                return dvumernii_array;
            } while (true);
        }
        static int[,] RanomDvumerniiArray(int ArrayMin, int ArrayMax, ref int stringSize, ref int columnSize, ref int[,] dvumernii_array)
        {

            Console.Write("\nВведите количество столбцов: ");
            columnSize = Search_for_Bugs.ProverkaVvoda();
            Console.Write("\nВведите нижнюю границу массива:");
            ArrayMin = Search_for_Bugs.ProverkaVvoda();
            do
            {
                Console.Write("Введите верхнюю границу массива:");
                ArrayMax = Search_for_Bugs.ProverkaVvoda();
                if (ArrayMax < ArrayMin)
                {
                    Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                }
            } while (ArrayMax < ArrayMin);
            dvumernii_array = new int[stringSize, columnSize];
            Random rand = new Random();
            for (int i = 0; i < stringSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    dvumernii_array[i, j] = rand.Next(ArrayMin, ArrayMax);
                }
            }
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < dvumernii_array.GetLength(0); i++)
            {
                for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                {
                    Console.Write(dvumernii_array[i, j] + "\t ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n-----------------------------------------");
            return dvumernii_array;
        }
        static int[,] VvodDvumerniiArray(ref int stringSize, ref int columnSize, ref int[,] dvumernii_array)
        {
            Console.WriteLine("----------Формирование массива----------------");

            Console.Write("\nВведите количество столбцов: ");
            columnSize = Search_for_Bugs.ProverkaVvoda();
            dvumernii_array = new int[stringSize, columnSize];
            Random rand = new Random();
            for (int i = 0; i < stringSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    dvumernii_array[i, j] = Search_for_Bugs.InputNumber("Введите элемент матрицы", MinNumber, MaxNumber);
                }
            }
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < dvumernii_array.GetLength(0); i++)
            {
                for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                {
                    Console.Write(dvumernii_array[i, j] + "\t ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n-----------------------------------------");
            return dvumernii_array;
        }
        static int[,] RandomVvod(int ArrayMin, int ArrayMax, ref int stringSize, ref int columnSize, ref int[,] dvumernii_array)
        {
            {
                Console.Clear(); //Очищение консоли
                int N = 1, //номер с которого добавляем
                    K = 1; //Количество добавляемых столбцов
                bool ok_K = true, //bool на проверку
                     ok_N = true;
                Console.WriteLine("------------------Массив-----------------\n");
                for (int i = 0; i < dvumernii_array.GetLength(0); i++)
                {
                    for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                    {
                        Console.Write(dvumernii_array[i, j] + "\t ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n-----------------------------------------");
                do
                {
                    ok_N = true;
                    N = Search_for_Bugs.InputNumber("Введите номер столбца (N), с которого нужно добавлять:", MinSize, MaxSize); //проверка на правильность
                    if (N <= 0 || N > columnSize + 1)
                    {
                        ok_N = false;
                        Console.WriteLine("Не правильно задан номер, с которого нужно добавлять!");
                    }
                } while (!ok_N);
                do
                {
                    ok_K = true;
                    K = Search_for_Bugs.InputNumber("Введите количество столбцов (K) для добавления:", MinSize, MaxSize); //проверка на правильность
                    if (columnSize + K > MaxSize)
                    {
                        ok_K = false;
                        Console.WriteLine("Не правильно задано количество столбцов!");
                    }
                } while (!ok_K);
                Console.Write("\nВведите нижнюю границу массива:");
                ArrayMin = Search_for_Bugs.ProverkaVvoda(); //проверка на правильность
                do
                {
                    Console.Write("Введите верхнюю границу массива:");
                    ArrayMax = Search_for_Bugs.ProverkaVvoda(); //проверка на правильность
                    if (ArrayMax < ArrayMin)
                    {
                        Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                    }
                } while (ArrayMax < ArrayMin);
                int[,] temp = new int[stringSize, K];
                Random rand = new Random();
                for (int i = 0; i < stringSize; i++)
                {
                    for (int j = 0; j < K; j++)
                    {
                        temp[i, j] = rand.Next(ArrayMin, ArrayMax);
                    }
                }
                columnSize += K;  // необходимо при следующем добавлении новых столбцов
                int[,] New_Array = new int[stringSize, columnSize]; //создание нового двумерного массива
                for (int i = 0; i < dvumernii_array.GetLength(0); i++)
                {
                    for (int j = 0, Index = 0; j < dvumernii_array.GetLength(1); j++, Index++)
                    {
                        //Напоминалка
                        //при смене N на (N-1) столбцы будут добавляться начиная с 0                        
                        New_Array[i, Index] = dvumernii_array[i, j];
                        if (N > dvumernii_array.GetLength(0))
                        {
                            if (j >= (N - 2))
                            {
                                for (int y = 0; y < temp.GetLength(1); y++)
                                {
                                    Index++;
                                    New_Array[i, Index] = temp[i, y];

                                }
                            }
                        }
                        else
                        {
                            if (j == (N - 1))
                            {
                                for (int y = 0; y < temp.GetLength(1); y++)
                                {
                                    New_Array[i, Index] = temp[i, y];
                                    Index++;
                                }
                            }
                            New_Array[i, Index] = dvumernii_array[i, j];
                        }

                    }
                }
                dvumernii_array = New_Array;
                Console.WriteLine("\n-------------Изменённый Массив------------\n");
                for (int i = 0; i < dvumernii_array.GetLength(0); i++)
                {
                    for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                    {
                        Console.Write(dvumernii_array[i, j] + "\t ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n-----------------------------------------");
            }
            return dvumernii_array;
        }
        static int[,] RuchnoiVvod(ref int stringSize, ref int columnSize, ref int[,] dvumernii_array)
        {
            {
                Console.Clear(); //Очищение консоли
                int N = 1,
                    K = 1;
                bool ok_K = true,
                     ok_N = true;
                Console.WriteLine("------------------Массив-----------------\n");
                for (int i = 0; i < dvumernii_array.GetLength(0); i++)
                {
                    for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                    {
                        Console.Write(dvumernii_array[i, j] + "\t ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n-----------------------------------------");
                do
                {
                    ok_N = true;
                    N = Search_for_Bugs.InputNumber("Введите номер столбца (N), с которого нужно добавлять:", MinSize, MaxSize);
                    if (N <= 0 || N > columnSize + 1)
                    {
                        ok_N = false;
                        Console.WriteLine("Не правильно задан номер, с которого нужно добавлять!");
                    }
                } while (!ok_N);
                do
                {
                    ok_K = true;
                    K = Search_for_Bugs.InputNumber("Введите количество столбцов (K) для добавления:", MinSize, MaxSize);
                    if (columnSize + K > MaxSize)
                    {
                        ok_K = false;
                        Console.WriteLine("Не правильно задано количество столбцов!");
                    }
                } while (!ok_K);
                int[,] temp = new int[stringSize, K];
                Random rand = new Random();
                for (int i = 0; i < stringSize; i++)
                {
                    for (int j = 0; j < K; j++)
                    {
                        temp[i, j] = Search_for_Bugs.InputNumber("Введите элемент матрицы", MinNumber, MaxNumber);
                    }
                }
                columnSize += K;  // необходимо при следующем добавлении новых столбцов
                int[,] New_Array = new int[stringSize, columnSize];
                for (int i = 0; i < dvumernii_array.GetLength(0); i++)
                {
                    for (int j = 0, Index = 0; j < dvumernii_array.GetLength(1); j++, Index++)
                    {
                        //Напоминалка
                        //при смене N на (N-1) столбцы будут добавляться начиная с 0
                        New_Array[i, Index] = dvumernii_array[i, j];
                        if (N > dvumernii_array.GetLength(0))
                        {
                            if (j >= (N - 2))
                            {
                                for (int y = 0; y < temp.GetLength(1); y++)
                                {
                                    Index++;
                                    New_Array[i, Index] = temp[i, y];

                                }
                            }
                        }
                        else
                        {
                            if (j == (N - 1))
                            {
                                for (int y = 0; y < temp.GetLength(1); y++)
                                {
                                    New_Array[i, Index] = temp[i, y];
                                    Index++;
                                }
                            }
                            New_Array[i, Index] = dvumernii_array[i, j];
                        }

                    }
                }
                dvumernii_array = New_Array;
                Console.WriteLine("\n-------------Изменённый Массив------------\n");
                for (int i = 0; i < dvumernii_array.GetLength(0); i++)
                {
                    for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                    {
                        Console.Write(dvumernii_array[i, j] + "\t ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n-----------------------------------------");
            }
            return dvumernii_array;
        }
        #endregion
        //------------------------------------------------------------------------------------------------------------------------
        //---РВАНЫЙ МАССИВ--------------------------------------------------------------------------------------------------------
        #region
        static int[][] RvaniiArray(int ArrayMin, int ArrayMax, ref int NumberOfStrings)
        {
            do
            {
                Console.Clear();
                int[][] rvanii_array = null;
                Console.WriteLine("\n------------------------------");
                Console.WriteLine("| Выберите вид массива:      |");
                Console.WriteLine("| 1) Рандомный массив        |");
                Console.WriteLine("| 2) Массив с вводом         |");
                Console.WriteLine("| 9) Вернуться в начало      |");
                Console.WriteLine("| 0) Выход из консоли        |");
                Console.WriteLine("------------------------------");
                Console.Write("Действие: ");
                int check = Search_for_Bugs.ProverkaVvoda();
                switch (check)
                {
                    case 1:
                        Console.Clear();
                        RvaniiArrayRandom(ArrayMin, ArrayMax, ref rvanii_array, ref NumberOfStrings);
                        if (NumberOfStrings > 0)
                        {
                            do
                            {
                                Text_Dialog.PrintMenuRvanM();
                                int ElementPodmenu = Search_for_Bugs.ProverkaVvoda();
                                switch (ElementPodmenu) //Выбор действия из меню
                                {
                                    case 1:
                                        Text_Dialog.PrintPodMenuRvanM();
                                        int ElementPodPodmenu = Search_for_Bugs.ProverkaVvoda();
                                        switch (ElementPodPodmenu)
                                        {
                                            case 1:
                                                Console.Clear();
                                                RandomDobavlenie(ref rvanii_array, ref NumberOfStrings, ArrayMin, ArrayMax);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                VvodDobavlenie(ref rvanii_array, ref NumberOfStrings);
                                                break;
                                            case 9:
                                                Main();
                                                break;
                                            case 0:
                                                Environment.Exit(0);  //Выход из консоли
                                                break;
                                            default:
                                                Text_Dialog.PrintErrorMenu();
                                                continue;
                                        }
                                        break;
                                    case 2:
                                        RvaniiArray(ArrayMin, ArrayMax, ref NumberOfStrings);
                                        break;
                                    case 9:
                                        Main();
                                        Console.Clear();
                                        break;
                                    case 0:
                                        Environment.Exit(0);  //Выход из консоли
                                        break;
                                    default:
                                        Text_Dialog.PrintErrorMenu();
                                        continue;
                                }
                            } while (true);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        RvaniiArrayVvod(ref rvanii_array, ref NumberOfStrings);
                        if (NumberOfStrings > 0)
                        {
                            do
                            {
                                Text_Dialog.PrintMenuRvanM();
                                int ElementPodmenu = Search_for_Bugs.ProverkaVvoda();
                                switch (ElementPodmenu) //Выбор действия из меню
                                {
                                    case 1:
                                        Text_Dialog.PrintPodMenuRvanM();
                                        int ElementPodPodmenu = Search_for_Bugs.ProverkaVvoda();
                                        switch (ElementPodPodmenu)
                                        {
                                            case 1:
                                                Console.Clear();
                                                RandomDobavlenie(ref rvanii_array, ref NumberOfStrings, ArrayMin, ArrayMax);
                                                break;
                                            case 2:
                                                Console.Clear();
                                                VvodDobavlenie(ref rvanii_array, ref NumberOfStrings);
                                                break;
                                            case 9:
                                                Main();
                                                break;
                                            case 0:
                                                Environment.Exit(0);  //Выход из консоли
                                                break;
                                            default:
                                                Text_Dialog.PrintErrorMenu();
                                                continue;
                                        }
                                        break;
                                    case 2:
                                        RvaniiArray(ArrayMin, ArrayMax, ref NumberOfStrings);
                                        break;
                                    case 9:
                                        Main();
                                        break;
                                    case 0:
                                        Environment.Exit(0);  //Выход из консоли
                                        break;
                                    default:
                                        Text_Dialog.PrintErrorMenu();
                                        continue;
                                }
                            } while (true);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 9:
                        Main();
                        break;
                    default:
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
                return rvanii_array;
            } while (true);
        }
        static int[][] RvaniiArrayRandom(int ArrayMin, int ArrayMax, ref int[][] rvanii_array, ref int NumberOfStrings)
        {
            int ColumnSize,
                Roll = 1;
            NumberOfStrings = 1;
            Random rnd = new Random();
            Console.WriteLine("----------Формирование массива----------------\n");
            do
            {
                Console.Write("Введите количество строк: ");
                NumberOfStrings = Search_for_Bugs.ProverkaVvoda();
                if (NumberOfStrings <= 0)
                {
                    Console.WriteLine("Количество строк не может быть меньше или равно 0\n");
                }
            }
            while (NumberOfStrings <= 0);
            Console.Write("Введите нижнюю границу массива:");
            ArrayMin = Search_for_Bugs.ProverkaVvoda();
            do
            {
                Console.Write("Введите верхнюю границу массива:");
                ArrayMax = Search_for_Bugs.ProverkaVvoda();
                if (ArrayMax < ArrayMin)
                {
                    Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                }
            } while (ArrayMax < ArrayMin);
            rvanii_array = new int[NumberOfStrings][];
            for (int i = 0; i < NumberOfStrings; i++)
            {
                Console.Write("Введите количество Элементов в " + Roll + " строке: ");
                do
                {
                    ColumnSize = Search_for_Bugs.ProverkaVvoda();
                    if (ColumnSize <= 0)
                    {
                        Console.WriteLine("Я не хочу чтобы Массив пустовал!!!");
                    }
                } while (ColumnSize <= 0);
                rvanii_array[i] = new int[ColumnSize];
                for (int j = 0; j < ColumnSize; j++)
                {
                    rvanii_array[i][j] = rnd.Next(ArrayMin, ArrayMax);
                }
                Roll++;
            }
            Console.Clear();
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < NumberOfStrings; i++)
            {
                for (int j = 0; j < rvanii_array[i].Length; j++)
                    Console.Write(rvanii_array[i][j].ToString() + " ");
                Console.WriteLine();
            }
            Console.WriteLine("\n-----------------------------------------");
            return rvanii_array;
        }
        static int[][] RvaniiArrayVvod(ref int[][] rvanii_array, ref int NumberOfStrings)
        {
            int ColumnSize,
                Roll = 1;
            NumberOfStrings = 1;
            Random rnd = new Random();
            Console.WriteLine("----------Формирование массива----------------\n");
            do
            {
                Console.Write("Введите количество строк: ");
                NumberOfStrings = Search_for_Bugs.ProverkaVvoda();
                if (NumberOfStrings <= 0)
                {
                    Console.WriteLine("Количество строк не может быть меньше или равно 0\n");
                }
            }
            while (NumberOfStrings <= 0);
            rvanii_array = new int[NumberOfStrings][];
            for (int i = 0; i < NumberOfStrings; i++)
            {
                Console.Write("Введите количество Элементов в " + Roll + " строке: ");
                do
                {
                    ColumnSize = Search_for_Bugs.ProverkaVvoda();
                    if (ColumnSize <= 0)
                    {
                        Console.WriteLine("Я не хочу чтобы Массив пустовал!!!");
                    }
                } while (ColumnSize <= 0);
                rvanii_array[i] = new int[ColumnSize];
                for (int j = 0; j < ColumnSize; j++)
                {
                    rvanii_array[i][j] = Search_for_Bugs.InputNumber("Введите элемент:", MinNumber, MaxNumber);
                }
                Roll++;
            }
            Console.Clear();
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < NumberOfStrings; i++)
            {
                for (int j = 0; j < rvanii_array[i].Length; j++)
                    Console.Write(rvanii_array[i][j].ToString() + " ");
                Console.WriteLine();
            }
            Console.WriteLine("\n-----------------------------------------");
            return rvanii_array;
        }
        static int[][] RandomDobavlenie(ref int[][] rvanii_array, ref int NumberOfStrings, int ArrayMin, int ArrayMax)
        {
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < rvanii_array.Length; i++)
            {
                if (rvanii_array[i].Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("пустая строка"); ;
                    Console.ResetColor();
                }
                for (int j = 0; j < rvanii_array[i].Length; j++)
                    Console.Write(rvanii_array[i][j].ToString() + " ");
                Console.WriteLine();

            }
            Console.WriteLine("\n-----------------------------------------\n");
            Random rnd = new Random();
            int ColumnSize,
                plus_string;
            Console.Write("Введите номер строки: ");
            do
            {
                plus_string = Search_for_Bugs.ProverkaVvoda();
                if ((plus_string > rvanii_array.Length + 1) || (plus_string <= 0))
                {
                    Console.WriteLine("Строка не должна выходить за пределы массива!!!");
                }
            }
            while ((plus_string > rvanii_array.Length + 1) || (plus_string <= 0));
            plus_string--;
            Console.Write("Введите нижнюю границу массива:");
            ArrayMin = Search_for_Bugs.ProverkaVvoda();
            do
            {
                Console.Write("Введите верхнюю границу массива:");
                ArrayMax = Search_for_Bugs.ProverkaVvoda();
                if (ArrayMax < ArrayMin)
                {
                    Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                }
            } while (ArrayMax < ArrayMin);
            int[][] new_dobavlenie = new int[rvanii_array.Length + 1][];
            for (int i = 0; i < new_dobavlenie.Length; i++)
            {
                if (i < plus_string)
                {
                    new_dobavlenie[i] = new int[rvanii_array[i].Length];
                    for (int j = 0; j < rvanii_array[i].Length; j++)
                        new_dobavlenie[i][j] = rvanii_array[i][j];
                }
                if ((i >= plus_string) && (i != rvanii_array.Length))
                {
                    new_dobavlenie[i + 1] = new int[rvanii_array[i].Length];
                    for (int j = 0; j < rvanii_array[i].Length; j++)
                        new_dobavlenie[i + 1][j] = rvanii_array[i][j];
                }
                if (i == plus_string)
                {
                    Console.Write("Введите количество Элементов в задаваемой строке: ");
                    ColumnSize = Search_for_Bugs.ProverkaVvoda();
                    new_dobavlenie[i] = new int[ColumnSize];
                    Random randm = new Random();
                    for (int j = 0; j < new_dobavlenie[i].Length; j++)
                    {
                        new_dobavlenie[i][j] = randm.Next(ArrayMin, ArrayMax);
                    }
                }
            }
            rvanii_array = new int[rvanii_array.Length + 1][];
            for (int i = 0; i < rvanii_array.Length; i++)
            {
                rvanii_array[i] = new int[new_dobavlenie[i].Length];
                for (int j = 0; j < rvanii_array[i].Length; j++)
                    rvanii_array[i][j] = new_dobavlenie[i][j];
            }
            Console.WriteLine("\n-------------Изменённый Массив------------\n");
            for (int i = 0; i < rvanii_array.Length; i++)
            {
                if (rvanii_array[i].Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("пустая строка"); ;
                    Console.ResetColor();
                }
                for (int j = 0; j < rvanii_array[i].Length; j++)
                {
                    Console.Write(rvanii_array[i][j].ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n-----------------------------------------");
            rvanii_array = new_dobavlenie;
            return rvanii_array;
        }
        static int[][] VvodDobavlenie(ref int[][] rvanii_array, ref int NumberOfStrings)
        {
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < rvanii_array.Length; i++)
            {
                if (rvanii_array[i].Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("пустая строка"); ;
                    Console.ResetColor();
                }
                for (int j = 0; j < rvanii_array[i].Length; j++)
                    Console.Write(rvanii_array[i][j].ToString() + " ");
                Console.WriteLine();

            }
            Console.WriteLine("\n-----------------------------------------\n");
            Random rnd = new Random();
            int ColumnSize,
                plus_string;
            Console.Write("Введите номер строки: ");
            do
            {
                plus_string = Search_for_Bugs.ProverkaVvoda();
                if ((plus_string > rvanii_array.Length + 1) || (plus_string <= 0))
                {
                    Console.WriteLine("Строка не должна выходить за пределы массива!!!");
                }
            }
            while ((plus_string > rvanii_array.Length + 1) || (plus_string <= 0));
            plus_string--;

            int[][] new_dobavlenie = new int[rvanii_array.Length + 1][];
            for (int i = 0; i < new_dobavlenie.Length; i++)
            {
                if (i < plus_string)
                {
                    new_dobavlenie[i] = new int[rvanii_array[i].Length];
                    for (int j = 0; j < rvanii_array[i].Length; j++)
                        new_dobavlenie[i][j] = rvanii_array[i][j];
                }
                if ((i >= plus_string) && (i != rvanii_array.Length))
                {
                    new_dobavlenie[i + 1] = new int[rvanii_array[i].Length];
                    for (int j = 0; j < rvanii_array[i].Length; j++)
                        new_dobavlenie[i + 1][j] = rvanii_array[i][j];
                }
                if (i == plus_string)
                {
                    Console.Write("Введите количество Элементов в задаваемой строке: ");
                    ColumnSize = Search_for_Bugs.ProverkaVvoda();
                    new_dobavlenie[i] = new int[ColumnSize];

                    for (int j = 0; j < new_dobavlenie[i].Length; j++)
                    {
                        bool okey = false;
                        do
                        {
                            Console.WriteLine("Введите {0} элемент", j + 1);
                            try
                            {
                                new_dobavlenie[i][j] = Search_for_Bugs.ProverkaVvoda();
                                okey = true;
                            }
                            catch
                            {
                                okey = false;
                                Console.WriteLine("Неверный ввод");
                            }
                        } while (!okey);
                    }

                }
            }
            rvanii_array = new int[rvanii_array.Length + 1][];
            for (int i = 0; i < rvanii_array.Length; i++)
            {
                rvanii_array[i] = new int[new_dobavlenie[i].Length];
                for (int j = 0; j < rvanii_array[i].Length; j++)
                    rvanii_array[i][j] = new_dobavlenie[i][j];
            }
            Console.WriteLine("\n-------------Изменённый Массив------------\n");
            for (int i = 0; i < rvanii_array.Length; i++)
            {
                if (rvanii_array[i].Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("пустая строка"); ;
                    Console.ResetColor();
                }
                for (int j = 0; j < rvanii_array[i].Length; j++)
                {
                    Console.Write(rvanii_array[i][j].ToString() + " ");
                }
                Console.WriteLine();

            }
            Console.WriteLine("\n-----------------------------------------");
            rvanii_array = new_dobavlenie;
            return rvanii_array;
        }
        #endregion
        //------------------------------------------------------------------------------------------------------------------------
        static void Main()
        {
            Console.Clear(); //Очищение консоли
            int elementi = 0,
                NumberOfStrings = 0,
                stringSize = 0,
                columnSize = 0,
                ArrayMin = 0,
                ArrayMax = 100;
            int[] massiv = null;
            int[,] dvumernii_array = null;
            do
            {
                Console.WriteLine("\n-----------------------------------------");
                Console.WriteLine("| Выберите следующее действие:          |");
                Console.WriteLine("| 1) Построить одномерный массив        |");
                Console.WriteLine("| 2) Построить двумерный массив         |");
                Console.WriteLine("| 3) Построить рваный массив            |");
                Console.WriteLine("| 0) Выход                              |");
                Console.WriteLine("-----------------------------------------");
                Console.Write("Действие: ");
                int elementmenu = Search_for_Bugs.ProverkaVvoda();
                switch (elementmenu) //Выбор действия из меню
                {
                    case 1:
                        {
                            Massiv(ref elementi, ref ArrayMin, ref ArrayMax, ref massiv);
                        }
                        break;
                    case 2:
                        {
                            DvumerniiArray(ArrayMin, ArrayMax, ref stringSize, ref columnSize, ref dvumernii_array);
                        }
                        break;
                    case 3:
                        {
                            RvaniiArray(ArrayMin, ArrayMax, ref NumberOfStrings);
                        }
                        break;
                    case 0:
                        Environment.Exit(0);  //Выход из консоли
                        break;
                    default:
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
            } while (true);
        }
    }
}