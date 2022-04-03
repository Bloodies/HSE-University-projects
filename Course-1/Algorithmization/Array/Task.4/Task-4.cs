using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Задание:
/// При выборе места строительства жилого комплекса при металлургическом комбинате необходимо учитывать "розу ветров" 
/// (следует расположить жилой комплекс так, чтобы частота ветра со стороны металлургического комбината была бы минимальной). 
/// Для этого в течение года проводилась регистрация направления ветра в районе строительства. 
/// Данные представлены  в виде массива, в котором направление ветра за каждый день кодируется следующим образом: 
///     1 — северный, 
///     2 — южный, 
///     3 — восточный, 
///     4 — западный,
///     5 — северо - западный, 
///     6 — северо - восточный,
///     7 — юго - западный,
///     8 — юго - восточный.
/// Определить, как должен быть расположен жилой комплекс по отношению к комбинату.
/// </summary>
namespace Task._4
{
    /// <summary>
    /// Класс вывода сообщений в консоль
    /// </summary>
    class Dialog
    {
        public static void Menu()
        {
            Console.WriteLine("\n-----------------------");
            Console.Write("Ещё Массив? [y/n]: ");
            if (Console.ReadLine() == "y")
                Program.Main();
            else
                Environment.Exit(0);
        }
        public static void MenuError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нужно выбрать из списка!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    /// <summary>
    /// Проверка ввода чисел в консоль
    /// </summary>
    class Input
    {
        public static int Int_menu()
        {
            int number;
            bool res;
            do
            {
                res = int.TryParse(Console.ReadLine(), out number);
                if (res == false) { Dialog.MenuError(); }
            } while (!res);
            return number;
        }
    }

    class Program
    {
        public static void Main()
        {
            Console.Clear();
            int[] wind = new int[8];
            bool ok;
            int tmp;
            do
            {
                Console.WriteLine("\n------------------------------");
                Console.WriteLine("| Как вы хотите вводить?     |");
                Console.WriteLine("| 1) Вручную                 |");
                Console.WriteLine("| 2) Рандомно                |");
                Console.WriteLine("| 0) Выход                   |");
                Console.WriteLine("------------------------------");
                Console.Write("Действие: ");
                int elementmenu = Input.Int_menu();
                switch (elementmenu)
                {
                    case 1:
                        for (int i = 0; i < 365; i++)
                        {
                            Console.Write($"Введите направление ветра в {i + 1} день: ");
                            do
                            {
                                ok = int.TryParse(Console.ReadLine(), out tmp);
                                if (tmp > 8 || tmp < 1)
                                {
                                    ok = false;
                                }
                                if (ok == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ошибка ввода, попробуйте еще раз");
                                    Console.ResetColor();
                                }
                            } while (!ok);
                            wind[tmp - 1]++;
                        }
                        break;
                    case 2:
                        Random rnd = new Random();
                        wind = new int[365];
                        for (int i = 0; i < 365; i++)
                        {
                            wind[i] = rnd.Next(1, 8);
                        }
                        break;
                    default:
                        Console.Clear();
                        Dialog.MenuError();
                        continue;
                }
                Console.WriteLine("--------------Направление всех ветров-------------\n");
                foreach (var i in wind) //Построение нового массива
                {
                    Console.Write(" " + i);
                }
                Console.WriteLine("\n---------------------Вывод------------------------\n");

                int min = wind[0];
                int direcrion = 1;
                for (int i = 1; i < 8; i++)
                {
                    if (wind[i] < min)
                        min = wind[i]; direcrion = i + 1;
                }
                switch (direcrion)
                {
                    case 1:
                        Console.WriteLine("Жилой комплекс должен быть расположен южнее завода");
                        Dialog.Menu();
                        break;
                    case 2:
                        Console.WriteLine("Жилой комплекс должен быть расположен северней  завода");
                        Dialog.Menu();
                        break;
                    case 3:
                        Console.WriteLine("Жилой комплекс должен быть расположен западней  завода");
                        Dialog.Menu();
                        break;
                    case 4:
                        Console.WriteLine("Жилой комплекс должен быть расположен восточней  завода");
                        Dialog.Menu();
                        break;
                    case 5:
                        Console.WriteLine("Жилой комплекс должен быть расположен юго-восточней завода");
                        Dialog.Menu();
                        break;
                    case 6:
                        Console.WriteLine("Жилой комплекс должен быть расположен юго-западней завода");
                        Dialog.Menu();
                        break;
                    case 7:
                        Console.WriteLine("Жилой комплекс должен быть расположен северо-восточней завода");
                        Dialog.Menu();
                        break;
                    case 8:
                        Console.WriteLine("Жилой комплекс должен быть расположен северо-западней завода");
                        Dialog.Menu();
                        break;
                }
            } while (true);
        }
    }
}