using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab._7
{
    //---Все менюшки----------------------------------------------
    class Text_Dialog
    {
        public static void MainError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нужно выбрать из списка!");
            Console.ResetColor();
        }
        public static void ErrorMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка, некорректный ввод");
            Console.ResetColor();
        }
        public static void FormList()
        {
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Как сформировать список:   |");
            Console.WriteLine("| 1) Рандомно                |");
            Console.WriteLine("| 2) Вручную                 |");
            Console.WriteLine("| 9) Вернуться в начало      |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
        }
    }
    //------------------------------------------------------------
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
                    Text_Dialog.ErrorMenu();
                }
            } while (!res);
            return number;
        }

    }
    //---Выполнение задач--------------------------------------------------------------------
    class Point
    {

        public double data;      //информационное поле
        public Point next;       //адресное поле

        public Point(double element)  //конструктор с параметрами
        {
            data = element;
            next = null;
        }

        public override string ToString()
        {
            return data.ToString() + " ";
        }

    }

    class BiList
    {
        public int data;
        public BiList next,        //адрес следующего элемента
        last;                      //адрес предыдущего элемента

        public BiList(int d)       // Конструктор с параметрами 
        {
            data = d;
            next = null;
            last = null;
        }

        public override string ToString()
        {
            return data.ToString() + " ";  //Возвращаем data через пробел
        }
    }

    class Tree
    {
        public string data;
        public Tree left,        //адрес левого поддерева
        right;                     //адрес правого поддерева
        public Tree(string d)
        {
            data = d;
            left = null;
            right = null;
        }
        public override string ToString()
        {
            return data.ToString() + " ";
        }
    }
    //---------------------------------------------------------------------------------------
    //---Вход в программу--------------------------------------------------------------------
    class Program
    {
        const int MinNumber = -100;
        const int MaxNumber = 100;
        const int NumberOfElemenets = 5;
        static Random rnd = new Random();

        static string RemoveSpaces()
        {
            string symbol = Console.ReadLine();

            symbol = symbol.Replace("  ", "");
            symbol = symbol.Trim().Replace(" ", "");
            symbol = symbol.TrimEnd().Replace(" ", "");

            return symbol;
        }
        static Point MakePoint(double d)
        {
            Point p = new Point(d);
            return p;
        }
        static BiList MakePoint2(int d)
        {
            BiList p = new BiList(d);
            return p;
        }
        static Tree MakePoint3(string d)
        {
            Tree p = new Tree(d);
            return p;
        }
        public static int IntRND(int LowerLimit, int HigherLimit, string Error)
        {
            int count;
            bool confirmed;

            do
            {
                confirmed = Int32.TryParse(Console.ReadLine(), out count);
                if ((count < LowerLimit) || (count > HigherLimit) || (confirmed != true))
                {
                    Console.WriteLine(Error);
                }
            } while ((count < LowerLimit) || (count > HigherLimit) || (confirmed != true));
            return count;
        }
        public static int Int()
        {
            bool ok;
            int n;
            do
            {
                ok = Int32.TryParse(Console.ReadLine(), out n);
                if (!ok) Console.WriteLine("Вы ввели не целое число. Пожалуйста, введите другое.");
            } while (!ok);
            return n;
        }
        public static double Double()
        {
            bool ok;
            double n;
            do
            {
                ok = double.TryParse(Console.ReadLine(), out n);
                if (!ok) Console.WriteLine("Вы ввели не вещественное число. Пожалуйста, введите символ.");
            } while (!ok);
            return n;
        }
        //---Однонаправленный список---------------------------------------------------------------------------------
        #region
        // 4. Первое меню (однонаправленный список)
        static void Point(string[] args)
        {
            int SwitchNumber;
            Point List1 = null;
            do
            {
                Console.WriteLine("\n-----------------------------------------------------------------------------");
                Console.WriteLine("| Выберите следующее действие:                                              |");
                Console.WriteLine("| 1) Сформировать список                                                    |");
                Console.WriteLine("| 2) Вставить после каждого отрицательного элемента списка нулевой элемент  |");
                Console.WriteLine("| 3) Вывести текущий список на экран                                        |");
                Console.WriteLine("| 9) В начало                                                               |");
                Console.WriteLine("| 0) Выход из консоли                                                       |");
                Console.WriteLine("-----------------------------------------------------------------------------");
                Console.Write("Действие: ");
                SwitchNumber = Search_for_Bugs.ProverkaVvoda();
                switch (SwitchNumber)
                {
                    case 1:
                        Console.Clear();
                        List1 = FormNewList1(args);
                        Console.Clear();
                        Console.WriteLine("----------Однонаправленный список--------------\n");
                        ShowList1(List1);
                        Console.WriteLine("-----------------------------------------------");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("----------Однонаправленный список--------------\n");
                        List1 = WorkList1(List1);
                        ShowList1(List1);
                        Console.WriteLine("-----------------------------------------------");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("----------Однонаправленный список--------------\n");
                        ShowList1(List1);
                        Console.WriteLine("-----------------------------------------------");
                        break;
                    case 9:
                        Console.Clear();
                        Main(args);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Thread.Sleep(900);
                        Environment.Exit(0);
                        break;
                    default:
                        Text_Dialog.MainError();
                        continue;
                }
            } while (true);
        }
        // 4.1 - Формирование нового списка
        static Point FormNewList1(string[] args)
        {
            Console.WriteLine("----------Формирование однонапраленного списка----------------");
            Console.WriteLine("Введите число элементов в списке");
            int size = IntRND(1, 999999, "Введенное вами число не может являться числом элементов в списке");
            double info = 0, LowLimit = 0, HighLimit = 0;
            Point beg = null;
            Point r = null;
            Point p = null;
            do
            {
                Text_Dialog.FormList();
                int how = Search_for_Bugs.ProverkaVvoda();
                switch (how)
                {
                    case 1:
                        Console.Write("Введите нижнюю границу:");
                        LowLimit = Double();
                        do
                        {
                            Console.Write("Введите верхнюю границу:");
                            HighLimit = Double();
                            if (HighLimit < LowLimit)
                            {
                                Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                            }
                        } while (HighLimit < LowLimit);
                        info = rnd.Next(Convert.ToInt32(LowLimit), Convert.ToInt32(HighLimit));
                        beg = MakePoint(info);
                        r = beg;
                        for (int i = 1; i < size; i++)
                        {
                            info = rnd.Next(Convert.ToInt32(LowLimit), Convert.ToInt32(HighLimit));
                            p = MakePoint(info);
                            r.next = p;
                            r = p;
                        }
                        Console.WriteLine("Список успешно сформирован");
                        break;
                    case 2:
                        Console.WriteLine("Введите элемент вещественного типа под номером 1");
                        info = Double();
                        beg = MakePoint(info);
                        r = beg;
                        for (int i = 1; i < size; i++)
                        {
                            Console.WriteLine("Введите элемент вещественного типа под номером {0}", i + 1);
                            info = Double();
                            p = MakePoint(info);
                            r.next = p;
                            r = p;
                        }
                        Console.WriteLine("Список успешно сформирован");
                        break;
                    case 9:
                        Console.Clear();
                        Main(args);
                        break;
                    case 0:
                        Console.Clear();
                        Thread.Sleep(900);
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Environment.Exit(0);
                        break;
                    default:
                        Text_Dialog.MainError();
                        continue;
                }
            } while (!true);
            return beg;
        }
        // 4.2 - Вставка нулевых элементов после отрицательных
        static Point WorkList1(Point List1)
        {
            bool found = false;
            if (List1 == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("");
                Console.ResetColor();
                return List1;
            }
            Point p = List1;
            while (p != null)
            {
                if (p.data < 0)
                {
                    found = true;
                    Point d = MakePoint(0);
                    d.next = p.next;
                    p.next = d;
                }
                p = p.next;
            }
            if (found == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("В списке нет отрицательных элементов\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("В список добавлены нулевые элементы после каждого отрицательного элемента\n");
                Console.ResetColor();
            }
            return List1;
        }
        // 4.3 - Вывод списка на экран.
        static void ShowList1(Point List1)
        {
            if (List1 == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Список пуст");
                Console.ResetColor();
                return;
            }
            Point p = List1;
            while (p != null)
            {
                Console.Write(p);
                p = p.next;
            }
            Console.WriteLine();
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------
        //---Двунаправленный список----------------------------------------------------------------------------------
        #region
        static void PointTwo(string[] args)
        {
            int SwitchNumber;
            BiList List2 = null;
            do
            {
                Console.WriteLine("\n--------------------------------------------------------");
                Console.WriteLine("| Выберите следующее действие:                         |");
                Console.WriteLine("| 1) Сформировать список                               |");
                Console.WriteLine("| 2) Удалить из текущего списка первый четный элемент  |");
                Console.WriteLine("| 3) Вывести текущий список на экран                   |");
                Console.WriteLine("| 9) В начало                                          |");
                Console.WriteLine("| 0) Выход из консоли                                  |");
                Console.WriteLine("--------------------------------------------------------");
                Console.Write("Действие: ");
                SwitchNumber = Search_for_Bugs.ProverkaVvoda();
                switch (SwitchNumber)
                {
                    case 1:
                        Console.Clear();
                        List2 = FormNewList2(args);
                        Console.Clear();
                        Console.WriteLine("----------Двунапраленный список----------------\n");
                        ShowList2(List2);
                        Console.WriteLine("-----------------------------------------------");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("----------Двунапраленный список----------------\n");
                        List2 = WorkList2(List2);
                        ShowList2(List2);
                        Console.WriteLine("-----------------------------------------------");
                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nУдаление уже произведено!!!");
                            Console.ResetColor();
                            Console.WriteLine("\n--------------------------------------------------------");
                            Console.WriteLine("| Выберите следующее действие:                         |");
                            Console.WriteLine("| 1) Сформировать новый список                         |");
                            Console.WriteLine("| 2) Вывести текущий список на экран                   |");
                            Console.WriteLine("| 9) В начало                                          |");
                            Console.WriteLine("| 0) Выход из консоли                                  |");
                            Console.WriteLine("--------------------------------------------------------");
                            Console.Write("Действие: ");
                            SwitchNumber = Search_for_Bugs.ProverkaVvoda();
                            switch (SwitchNumber)
                            {
                                case 1:
                                    Console.Clear();
                                    List2 = FormNewList2(args);
                                    Console.Clear();
                                    Console.WriteLine("----------Двунапраленный список----------------\n");
                                    ShowList2(List2);
                                    Console.WriteLine("-----------------------------------------------");
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("----------Двунапраленный список----------------\n");
                                    ShowList2(List2);
                                    Console.WriteLine("-----------------------------------------------");
                                    break;
                                case 9:
                                    Console.Clear();
                                    Main(args);
                                    break;
                                case 0:
                                    Console.Clear();
                                    Console.WriteLine(" ");
                                    Console.WriteLine("Завершение работы...");
                                    Thread.Sleep(900);
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Text_Dialog.MainError();
                                    continue;
                            }
                        } while (!true);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("----------Двунапраленный список----------------\n");
                        ShowList2(List2);
                        Console.WriteLine("-----------------------------------------------");
                        break;
                    case 9:
                        Console.Clear();
                        Main(args);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Thread.Sleep(900);
                        Environment.Exit(0);
                        break;
                    default:
                        Text_Dialog.MainError();
                        continue;
                }
            } while (true);
        }
        // 5.1 - Формирование нового списка
        static BiList FormNewList2(string[] args)
        {
            Console.WriteLine("----------Формирование двунапраленного списка----------------");
            Console.Write("\nВведите число элементов в списке:");
            int size = IntRND(1, 999999, "Ошибка, при вводе, повторите!");
            int info = 0, LowLimit = 0, HighLimit = 0;
            BiList beg = null;
            BiList r = null;
            BiList p = null;
            do
            {
                Text_Dialog.FormList();
                int how = Search_for_Bugs.ProverkaVvoda();
                switch (how)
                {
                    case 1:
                        Console.Write("Введите нижнюю границу:");
                        LowLimit = Int();
                        do
                        {
                            Console.Write("Введите верхнюю границу:");
                            HighLimit = Int();
                            if (HighLimit < LowLimit)
                            {
                                Console.WriteLine("Верхняя граница не может быть меньше нижней!!!");
                            }
                        } while (HighLimit < LowLimit);
                        info = rnd.Next(LowLimit, HighLimit);
                        beg = MakePoint2(info);
                        r = beg;
                        for (int i = 1; i < size; i++)
                        {
                            info = rnd.Next(LowLimit, HighLimit);
                            p = MakePoint2(info);
                            r.next = p;
                            p.last = r;
                            r = p;
                        }
                        Console.WriteLine("Список сформирован");
                        break;
                    case 2:
                        Console.WriteLine("Введите элемент строкового типа под номером 1");
                        info = Int();
                        beg = MakePoint2(info);
                        r = beg;
                        for (int i = 1; i < size; i++)
                        {
                            Console.WriteLine("Введите элемент строкового типа под номером {0}", i + 1);
                            info = Int();
                            p = MakePoint2(info);
                            r.next = p;
                            p.last = r;
                            r = p;
                        }
                        Console.WriteLine("Список сформирован");
                        break;
                    case 9:
                        Console.Clear();
                        Main(args);
                        break;
                    case 0:
                        Console.Clear();
                        Thread.Sleep(900);
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Environment.Exit(0);
                        break;
                    default:
                        Text_Dialog.MainError();
                        continue;
                }
            } while (!true);
            return beg;
        }
        // 5.2 - Обработка списка
        static BiList WorkList2(BiList List2)
        {
            bool found = false;
            if (List2 == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Список пуст");
                Console.ResetColor();
                return List2;
            }
            BiList p = List2;

            while ((p != null))
            {
                if (p.data % 2 == 0)
                {
                    found = true;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Первый четный элемент удален из списка\n");
                    Console.ResetColor();
                    if (p.last != null)
                    {
                        if (p.next != null)
                        {
                            p.last.next = p.next;
                            p.next.last = p.last;
                            break;
                        }
                        else
                        {
                            p.last.next = null;
                            break;
                        }
                    }
                    else
                    {
                        if (p.next != null)
                        {
                            List2 = p.next;
                            break;
                        }
                        else
                        {
                            List2 = null;
                            break;
                        }
                    }
                }
                p = p.next;
            }
            if (found == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("В списке нет ни одного четного элемента\n");
                Console.ResetColor();
            }
            return List2;
        }
        // 5.3 - Вывод списка на экран
        static void ShowList2(BiList List2)
        {
            if (List2 == null)
            {
                Console.WriteLine("Список пуст");
                Console.ResetColor();
                return;
            }
            BiList p = List2;
            while (p != null)
            {
                Console.WriteLine(p.data);
                p = p.next;
            }
            Console.WriteLine();
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------
        //---Бинарное дерево-----------------------------------------------------------------------------------------
        #region
        static string RandomStr()
        {
            string[] randStr = new string[] { "illa", "llls", "*?", "если", "честно", "я", "не", "понял", "как", "делать", "дерево", "поиска", "sdfscv", "01110", "0111011", "1111000", "sdvxc", "11111111111111", "sdfsvxcv", "sd", "d", "фыячся", "сколько", "же", "можно", "меня", "пытать", "43555", "12345567", "012345697", "4234" };
            int num = rnd.Next(1, 31);
            string str = randStr[num];
            Console.WriteLine("Добавляется элемент {0}", str);
            return str;
        }
        static void Tree(string[] args)
        {
            Tree tree = null;
            string[] arr = new string[999999];

            int pos = 0;
            int size = 0;
            do
            {
                Console.WriteLine("\n-----------------------------------------------------------");
                Console.WriteLine("| Выберите следующее действие:                            |");
                Console.WriteLine("| 1) Сформировать идеально сбалансированное дерево        |");
                Console.WriteLine("| 2) Найти количество листьев в дереве                    |");
                Console.WriteLine("| 3) Преобразовать в дерево поиска                        |");
                Console.WriteLine("| 4) Вывести дерево на экран                              |");
                Console.WriteLine("| 9) В главное меню                                       |");
                Console.WriteLine("| 0) Выход                                                |");
                Console.WriteLine("-----------------------------------------------------------");
                Console.Write("Действие: ");
                int Menu = Search_for_Bugs.ProverkaVvoda();
                switch (Menu)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("---------------Формирование дерева----------------");
                            Console.WriteLine("Введите количество элементов в дереве:");
                            size = IntRND(1, 999999, "Ошибка, некорректный ввод!");
                            tree = CreateTree(tree, size, args);
                            Console.Clear();
                            Console.WriteLine("-------------------Дерево-------------------");
                            ShowTree(tree);
                            Console.WriteLine("--------------------------------------------");
                            break;
                        }
                    case 2:
                        if (tree == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Дерево пустое!!!");
                            Console.ResetColor();
                            continue;
                        }
                        BeginSearchIdealTree(tree);
                        break;
                    case 3:
                        if (tree == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Дерево пустое!!!");
                            Console.ResetColor();
                            continue;
                        }
                        else
                        {
                            CompileTree(ref arr, ref pos, tree);
                            tree = TreeTransform(tree, null);
                            Console.Clear();
                            Console.WriteLine("-------------------Дерево-------------------");
                            ShowTree(tree);
                            Console.WriteLine("--------------------------------------------");
                            break;
                        }
                    case 4:
                        if (tree == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Дерево пустое!!!");
                            Console.ResetColor();
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("-------------------Дерево-------------------");
                            ShowTree(tree);
                            Console.WriteLine("--------------------------------------------");
                            break;
                        }
                    case 9:
                        Console.Clear();
                        Main(args);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Thread.Sleep(900);
                        Environment.Exit(0);
                        break;
                    default:
                        Text_Dialog.MainError();
                        continue;
                }
            } while (true);
        }
        static Tree CreateTree(Tree tree, int size, string[] args)
        {
            do
            {
                Text_Dialog.FormList();
                int how = Search_for_Bugs.ProverkaVvoda();
                switch (how)
                {
                    case 1:
                        tree = IdealTreeRnd(size, tree);
                        break;
                    case 2:
                        tree = IdealTree(size);
                        break;
                    case 9:
                        Console.Clear();
                        Main(args);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Thread.Sleep(900);
                        Environment.Exit(0);
                        break;
                    default:
                        Text_Dialog.MainError();
                        continue;
                }
            } while (!true);
            return tree;
        }
        static Tree IdealTreeRnd(int size, Tree p)
        {
            Tree root;
            int nl, nr;
            if (size == 0)
            {
                p = null;
                return p;
            }
            nl = size / 2;
            nr = size - nl - 1;
            string temp = RandomStr();
            root = new Tree(temp);
            root.left = IdealTreeRnd(nl, root.left);
            root.right = IdealTreeRnd(nr, root.right);
            p = root;
            return p;
        }
        static Tree IdealTree(int size)
        {
            Tree List3 = null;
            List3 = FormIdealTree(size, List3);
            return List3;
        }
        static Tree FormIdealTree(int size, Tree p)
        {
            string symbol = "";
            Tree r = null;
            int nl, nr;
            if (size == 0)
            {
                p = null;
                return p;
            }
            nl = size / 2;
            nr = size - nl - 1;
            Console.WriteLine("Введите элемент дерева:");
            do
            {
                symbol = RemoveSpaces();
                if (symbol == "")
                {
                    Console.WriteLine("Вы ничего не ввели, попробуйте снова:");
                }

            } while (symbol == "");
            r = new Tree(symbol);
            r.left = FormIdealTree(nl, r.left);
            r.right = FormIdealTree(nr, r.right);
            return r;
        }
        static int BeginSearchIdealTree(Tree p)
        {
            int count = 0;
            SearchIdealTree(p, ref count);
            Console.ForegroundColor = ConsoleColor.Cyan;
            string[] Padej = { "листьев", "листа", "лист" };
            if ((count % 100 != 11) && (count % 10 == 1))
            {
                Console.WriteLine($"В дереве всего {count} {Padej[2]}");
            }
            else if (((count % 10 >= 2) && (count % 10 <= 4)) && (count % 100 != 12) && (count % 100 != 13) && (count % 100 != 14))
            {
                Console.WriteLine($"В дереве всего {count} {Padej[1]}");
            }
            else
            {
                Console.WriteLine($"В дереве всего {count} {Padej[0]}");
            }
            Console.ResetColor();
            return count;
        }
        static void SearchIdealTree(Tree p, ref int count)
        {
            if ((p.left == null) && (p.right == null))
            {
                count++;
            }
            else
            {
                if (p.left != null)
                {
                    SearchIdealTree(p.left, ref count);
                }
                if (p.right != null)
                {
                    SearchIdealTree(p.right, ref count);
                }
            }
        }
        static void CompileTree(ref string[] arr, ref int pos, Tree p)
        {
            if (p != null)
            {
                arr[pos] = p.data;
                pos++;
                CompileTree(ref arr, ref pos, p.left);
                CompileTree(ref arr, ref pos, p.right);
            }
        }
        static Tree CompileSearchTree(string[] arr, int pos)
        {
            Tree root = MakePoint3(arr[0]);
            Tree beg = root;
            for (int i = 1; i < pos; i++)
            {
                beg = CompileSearchTreeStep(root, arr[i]);
            }

            return beg;
        }
        static Tree CompileSearchTreeStep(Tree root, string data)
        {
            if (root == null)
            {
                root = MakePoint3(data);
                return root;
            }
            else
            {
                if (data.Length < root.data.Length)
                {
                    root.left = CompileSearchTreeStep(root.left, data);
                }
                if (data.Length > root.data.Length)
                {
                    root.right = CompileSearchTreeStep(root.right, data);
                }
            }
            return root;
        }
        static Tree AddKey(Tree tree, string data)
        {
            if (tree == null)
            {
                tree = new Tree(data);
                return tree;
            }
            if (data.Length < tree.data.Length) tree.left = AddKey(tree.left, data);
            if (data.Length > tree.data.Length) tree.right = AddKey(tree.right, data);
            return tree;
        }

        static Tree TreeTransform(Tree tree, Tree newtree)
        {
            if (tree != null)
            {
                newtree = AddKey(newtree, tree.data);
                newtree = TreeTransform(tree.left, newtree);
                newtree = TreeTransform(tree.right, newtree);
                return newtree;
            }
            return newtree;

        }
        static void ShowTree(Tree p, int l = 0)
        {
            if (p != null)
            {
                ShowTree(p.left, l + 3);
                for (int i = 0; i < l; i++) Console.Write(" ");
                Console.WriteLine(p.data);
                ShowTree(p.right, l + 3);
            }
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            Console.Clear();
            do
            {
                Console.WriteLine("\n----------------------------------");
                Console.WriteLine("| Выберите следующее действие:   |");
                Console.WriteLine("| 1) Однонаправленные списки     |");
                Console.WriteLine("| 2) Двунаправленные списки      |");
                Console.WriteLine("| 3) Бинарное дерево             |");
                Console.WriteLine("| 0) Выход из консоли            |");
                Console.WriteLine("----------------------------------");
                Console.Write("Действие: ");
                int MainMenu = Search_for_Bugs.ProverkaVvoda();
                switch (MainMenu)
                {
                    case 1:
                        Console.Clear();
                        Point(args);
                        break;
                    case 2:
                        Console.Clear();
                        PointTwo(args);
                        break;
                    case 3:
                        Console.Clear();
                        Tree(args);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.WriteLine("Завершение работы...");
                        Thread.Sleep(900);
                        Environment.Exit(0);
                        break;
                    default:
                        Text_Dialog.MainError();
                        continue;
                }
            } while (true);
        }
    }
    //---------------------------------------------------------------------------------------
}