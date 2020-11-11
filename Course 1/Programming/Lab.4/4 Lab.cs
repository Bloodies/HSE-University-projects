using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._4
{
    class Program
    {
        /*Программа строит массив с возможностью выбора (ДСЧ или вручную)
         *После выводит массив и показывает меню взаимодействия
         *Proverka, ProverkaMassiva, ProverkaVvoda отвечает за проверку правильности
         *Poisk1 и Poisk2 для поиска в массиве, в неотсортированном и отсортированном
         *Massiv, MassivRandom, MassivVvod отвечает за построение массива
         *Udalenie отвечает за удаление из массива
         *Dobavlenie добавление чисел в массив
         *Perestanovka переставляет элементы на указанное число
         *Sortirovka Сортирует от меньшего к большему
         *Main вход в программу        */

        const int massivmin = 1;      //Минимальное количество элементов в массиве
        const int massivmax = 100;    //Максимальное количество элементов в массиве
        const int elementmin = -100;  //нижняя граница
        const int elementmax = 100;   //верхняя граница
        static bool Proverka(int[] massiv, ref int elementi) //Проверка правильности ввода переменных и выбора меню
                                                             //Тут больше и нечего добавлять
                                                             //Просто небольшая функция на проверку
        {
            if (massiv == null)
            {
                if (elementi != 0) elementi = 0;
                return false;
            }
            return true;
        }
        static int ProverkaMassiva(string title, int left, int right)  //Проверка на ввод массива и его элементов
                                                                       //Тут больше и нечего добавлять
                                                                       //Просто небольшая функция на проверку
        {
            bool ok = false;
            int number = elementmin;
            do
            {
                Console.WriteLine(title);
                try
                {
                    string buf = Console.ReadLine();
                    number = Convert.ToInt32(buf);
                    if (number >= left && number < right) ok = true;
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
        static int ProverkaVvoda() //Проверка ввода в массив
                                   //Тут больше и нечего добавлять
                                   //Просто небольшая функция на проверку
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
        static void MenuVihod()
        {
            Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("\n-----------------------------------------");
            Console.WriteLine("| Выберите следующее действие:          |");
            Console.WriteLine("| 9) Построить новый массив             |");
            Console.WriteLine("| 0) Выход                              |");
            Console.WriteLine("-----------------------------------------");
            Console.Write("Действие: ");
            int PodMenu = ProverkaVvoda();
            switch (PodMenu)
            {
                case 9:
                    Main();
                    break;
                case 0:
                    Environment.Exit(0);  //Выход из консоли
                    break;
                default:
                    Console.WriteLine("Нужно выбрать из списка!");
                    break;
            }
        }
        static int Poisk1(int[] massiv, int elementi, int number, out int count) //Поиск по неотсортированному массиву
        {
            int polojenie = -1;
            count = 0;
            for (int i = 0; i < elementi; i++)
            {
                count++;
                if (massiv[i] < 0)
                {
                    polojenie = i; break;
                }
            }
            Console.WriteLine(" ");
            return polojenie;
        }
        static int Poisk2(int[] massiv, int elementi, int number, out int count) //Поиск по отсортированному массиву
        {
            int left = 0,
                right = elementi - 1,
                middle = 0;
            count = 0;
            do
            {
                middle = (left + right) / 2;
                if (massiv[middle] < number)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle;
                    count++;
                }
            }
            while (left != right);
            Console.WriteLine(" ");
            if (number == massiv[left])
            {
                return left;
            }
            else
            {
                return -1;
            }
        }
        static int[] Massiv(int elementi) //Меню с массивами
        {
            int[] massiv = null;
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Выберите вид массива:      |");
            Console.WriteLine("| 1) Рандомный массив        |");
            Console.WriteLine("| 2) Массив с вводом         |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
            int check = ProverkaVvoda();
            switch (check)
            {
                case 1:
                    massiv = MassivRandom(elementi);
                    if (Proverka(massiv, ref elementi))
                    {
                        Console.Clear();
                        Console.WriteLine("------------------Массив-----------------\n");
                        foreach (var i in massiv) //Построение нового массива
                        {
                            Console.Write(" " + i);
                        }
                    }
                    else
                        Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
                    break;
                case 2:
                    massiv = MassivVvod(elementi);
                    if (Proverka(massiv, ref elementi))
                    {
                        Console.Clear();
                        Console.WriteLine("------------------Массив-----------------\n");
                        foreach (var i in massiv) //Построение нового массива
                        {
                            Console.Write(" " + i);
                        }
                    }
                    else
                        Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
                    break;
            }
            return massiv;
        }
        static int[] MassivRandom(int elementi)
        {
            Random rnd = new Random();
            int[] massiv = new int[elementi];
            for (int i = 0; i < elementi; i++)
            {
                massiv[i] = rnd.Next(elementmin, elementmax);
            }
            return massiv;
        }
        static int[] MassivVvod(int elementi)
        {
            int[] massiv = new int[elementi];
            for (int i = 0; i < elementi; i++)
            {
                massiv[i] = ProverkaMassiva("Введите элемент массива из диапазона от -100 до 100", elementmin, elementmax);
            }
            return massiv;
        }
        static void Udalenie(ref int[] massiv, ref int elementi, int NechetnieElem, int ChetnieElem) //Удаление из массива
        {
            Console.Clear(); //Очищение консоли
            Console.WriteLine("------------Исходный массив--------------\n");
            foreach (var i in massiv)//Построение старого массива
            {
                Console.Write(" " + i);
            }
            int j = 0;
            if (elementi != 0) //Построение нового массива
            {
                for (int i = 0; i < elementi; i++) //выборка и создание нового массива
                {
                    if (massiv[i] % 2 != 0) NechetnieElem++;
                    else ChetnieElem += 1;
                }
                if (ChetnieElem > 0)
                {
                    int[] b = new int[NechetnieElem];
                    for (int i = 0; i < elementi; i++) //выборка и создание нового массива
                    {
                        if (massiv[i] % 2 != 0)
                        {
                            b[j] = massiv[i];
                            j++;
                        }
                    }
                    elementi = NechetnieElem;
                    massiv = b;
                    if (elementi != 0) //выборка и создание нового массива
                    {
                        Console.WriteLine("\n-----------Удаляю все чётные-------------\n");
                        foreach (int i in massiv) //выборка и создание нового массива
                        {
                            Console.Write(" " + i); //Построение нового массива
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
                    }
                }
                else
                {
                    Console.WriteLine("\n-----В массиве нет чётных элементов------");
                }
                NechetnieElem = 0;
                ChetnieElem = 0;
                j = 0;
            }
            else
            {
                Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
            }
        }
        static void Dobavlenie(ref int[] massiv, ref int elementi) //Добавление в массив
        {
            Console.Clear(); //Очищение консоли
            Console.WriteLine("------------------Массив-----------------\n");
            foreach (var i in massiv) //Построение старого массива
            {
                Console.Write(" " + i);
            }
            Console.WriteLine("\n----------------Добавление---------------\n");
            int K = ProverkaMassiva("Введите номер, с которого нужно добавлять элементы в массив:", 0, massivmax);
            if (K <= 0 || K > elementi + 1) //выборка и создание нового массива
            {
                Console.WriteLine("Не правильно задан номер, с которого нужно добавлять элементы массива!");
                return;
            }
            int N = ProverkaMassiva("Введите сколько элементов нужно добавлять в массив:", 0, massivmax);
            if (elementi + N > massivmax) //выборка и создание нового массива
            {
                Console.WriteLine("Не правильно задано количество добавляемых элементов массива!");
                return;
            }
            int[] temp;
            if (Proverka(massiv, ref elementi)) //выборка и создание нового массива
                temp = new int[elementi + N];
            else
                temp = new int[N];
            int[] newmassiv = Massiv(N);
            if (!Proverka(newmassiv, ref N))
            {
                Console.WriteLine("Элементы для добавления не сформированы!");
                return;
            }
            int j = 0;
            for (int i = 0; i < K - 1; i++) //выборка и создание нового массива
            {
                temp[j] = massiv[i];
                j++;
            }
            for (int i = 0; i < N; i++)//выборка и создание нового массива
            {
                temp[j] = newmassiv[i];
                j++;
            }
            for (int i = K - 1; i < elementi; i++) //выборка и создание нового массива
            {
                temp[j] = massiv[i];
                j++;
            }
            Console.WriteLine("\n-------------Изменённый Массив------------\n");
            foreach (var i in temp) //Построение нового массива
            {
                Console.Write(" " + i);
            }
            elementi = elementi + N;
            massiv = temp;
        }
        static void Perestanovka(int[] massiv, int elementi) //Перестановка в массиве
        {
            Console.Clear();
            if (Proverka(massiv, ref elementi) && elementi != 0)
            {
                //Очищение консоли
                int[] perestanovkamassiva = massiv;
                Console.Write("\n------------Исходный массив--------------\n");
                foreach (var i in massiv)
                {
                    Console.Write(" " + i);
                }
                Console.Write("\n----------Переставляем массив------------\n");

                Console.Write("На сколько элементов сдвигать (M): ");
                int M = ProverkaVvoda();
                for (int i = 0; i < M; ++i) //выборка и создание нового массива
                {
                    int aLast = perestanovkamassiva[elementi - 1];
                    for (int j = elementi - 1; j > 0; j--) //выборка и создание нового массива
                        perestanovkamassiva[j] = perestanovkamassiva[j - 1];
                    perestanovkamassiva[0] = aLast;
                }
                Console.Write("\n--------------Новый массив---------------\n");
                for (int i = 0; i < elementi; ++i) //выборка и создание нового массива
                {
                    Console.Write(" " + perestanovkamassiva[i]);
                }
            }
            else
            {
                MenuVihod();
            }

        }
        static void Sortirovka(int[] massiv, int elementi) //Сортировка массива
        {
            Console.Clear(); //Очищение консоли
            if (Proverka(massiv, ref elementi) && elementi != 0)
            {
                for (int i = 0; i < elementi - 1; i++) //выборка и создание нового массива
                {
                    int min = massiv[i];
                    int n_min = i;
                    for (int j = i + 1; j < elementi; j++)
                        if (massiv[j] < min)
                        {
                            min = massiv[j];
                            n_min = j;
                        }
                    massiv[n_min] = massiv[i];
                    massiv[i] = min;
                }

                Console.WriteLine("------------------Массив-----------------\n");
                foreach (var i in massiv) //Построение нового массива
                {
                    Console.Write(" " + i);
                }
            }
            else
            {
                MenuVihod();
            }
        }
        static void Main() //Главный вход в программу
        {
            Console.Clear(); //Очищение консоли
            int elementi = 0,      //Элементы массива
                NechetnieElem = 0, //Нечетные элементы
                ChetnieElem = 0;   //Четные элементы
            int[] massiv = null;
            bool ok = false;
            Console.WriteLine("----------Формирование массива----------------");
            Console.WriteLine("Введите количество элементов в массиве:");
            elementi = ProverkaMassiva("", massivmin, massivmax);
            massiv = Massiv(elementi);
            ok = false;
            do
            {
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("\n-----------------------------------------");
                Console.WriteLine("| Выберите следующее действие:          |");
                Console.WriteLine("| 1) Удалить все чётные элементы        |");
                Console.WriteLine("| 2) Добавить N элементов начиная с K   |");
                Console.WriteLine("| 3) Сдвинуть массив                    |");
                Console.WriteLine("| 4) Найти элемент с заданным ключом    |");
                Console.WriteLine("| 5) Отсортировать простым выбором      |");
                Console.WriteLine("| 9) Построить новый массив             |");
                Console.WriteLine("| 0) Выход                              |");
                Console.WriteLine("-----------------------------------------");
                Console.Write("Действие: ");
                int elementmenu = ProverkaVvoda();
                switch (elementmenu) //Выбор действия из меню
                {
                    case 1:
                        Udalenie(ref massiv, ref elementi, NechetnieElem, ChetnieElem);
                        ok = false;
                        break;
                    case 2:
                        Dobavlenie(ref massiv, ref elementi);
                        ok = false;
                        break;
                    case 3:
                        Perestanovka(massiv, elementi);
                        ok = false;
                        break;
                    case 4:
                        int count = 0,
                        polojenie = -1;
                        if (Proverka(massiv, ref elementi))
                        {
                            Console.Clear(); //Очищение консоли
                            if (Proverka(massiv, ref elementi) && elementi != 0)
                            {
                                Console.WriteLine("------------------Массив-----------------\n");
                                foreach (var i in massiv)
                                {
                                    Console.Write(" " + i);
                                }
                                int number = ProverkaMassiva("\nВведите число для поиска:", elementmin, elementmax);
                                if (!ok)
                                {
                                    polojenie = Poisk1(massiv, elementi, number, out count) + 1;
                                    if (polojenie < 0) //выборка и создание нового массива
                                    {
                                        Console.WriteLine("Элемент {0} в массиве не найден, \nКоличество сравнений: {1}", number, count);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Элемент {0} находится на позиции {1}, \nКоличество сравнений: {2}", number, polojenie, count);
                                    }
                                }
                                else
                                {
                                    polojenie = Poisk2(massiv, elementi, number, out count) + 1;
                                    if (polojenie < 0) //выборка и создание нового массива
                                    {
                                        Console.WriteLine("Элемент {0} в массиве не найден. \nКоличество сравнений: {1}", number, count);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Элемент {0} находится на позиции {1}. \nКоличество сравнений: {2}", number, polojenie, count);
                                    }
                                }
                            }
                            else
                            {
                                MenuVihod();
                            }
                        }
                        else
                            Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
                        break;
                    case 5:
                        if (!ok)
                        {
                            Sortirovka(massiv, elementi);
                            ok = true;
                        }
                        break;
                    case 9:
                        Main();
                        ok = true;
                        break;
                    case 0:
                        Environment.Exit(0);  //Выход из консоли
                        break;
                    default:
                        Console.WriteLine("Нужно выбрать из списка!");
                        break;
                }
            } while (true);
        }
    }
}
