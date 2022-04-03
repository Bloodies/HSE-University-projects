using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Задание:
/// Дан двумерный массив из двух строк и двадцати двух столбцов. 
/// В его первой строке записано количество мячей, забитых футбольной командой в той или иной игре, 
/// во второй — количество пропущенных мячей в этой же игре.
///
/// Для каждой проведенной игры напечатать словесный результат: "выигрыш", "ничья" или "проигрыш".
/// a.  Определить количество выигрышей данной команды.
/// b.  Определить количество выигрышей и количество проигрышей данной  команды.
/// c.  Определить количество выигрышей, количество ничьих и количество проигрышей данной команды.
/// d.  Определить, в скольких играх разность забитых и пропущенных мячей была большей или равной трем.
/// e.  Определить общее число очков, набранных командой (за выигрыш даются 3 очка, за ничью — 1, за проигрыш — 0)
/// </summary>
namespace Task_2
{
    class Program
    {
        /// <summary> Проверка на ввод целого числа </summary>
        /// <returns> Возвращает целое число при успехе </returns>
        public static int Int()
        {
            int number;
            bool res;
            do
            {
                res = int.TryParse(Console.ReadLine(), out number);
                if (res == false)
                    Console.WriteLine("Некорректный ввод");

            } while (!res);
            return number;
        }

        static void Main()
        {
            Console.Clear();
            int win = 0,
                draw = 0,
                lose = 0,
                dif = 0;
            bool ok;
            int[,] array = new int[2, 22];
            Console.WriteLine("Введите массив");
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Как вы хотите вводить?     |");
            Console.WriteLine("| 1) Вручную                 |");
            Console.WriteLine("| 2) Рандомно                |");
            Console.WriteLine("------------------------------");
            Console.Write("Ввод: ");
            int elementmenu = Int();
            switch (elementmenu)
            {
                case 1:
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 22; j++)
                        {
                            Console.Write($"Введите элемет строки {i + 1} и игры {j + 1}:  ");
                            do
                            {
                                ok = int.TryParse(Console.ReadLine(), out array[i, j]);
                                if (ok == false)
                                    Console.WriteLine("Ошибка ввода, попробуйте еще раз");
                            }while (!ok);
                        }
                    }
                    break;
                case 2:
                    Random rnd = new Random();
                    array = new int[2, 22];
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 22; j++)
                            array[i, j] = rnd.Next(1, 8);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Нужно выбрать из списка!");
                    Console.ResetColor();
                    return;
            }
            Console.WriteLine("\n------------------Статистика по игре--------------");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    Console.Write($"{array[i, j]} ");
                Console.WriteLine();
            }

            Console.WriteLine("\n---------------------Вывод------------------------");
            for (int j = 0; j < 22; j++)
            {
                if (array[0, j] > array[1, j])
                    win++;
                if (array[0, j] < array[1, j])
                    lose++;
                if (array[0, j] == array[1, j])
                    draw++;
                if (Math.Abs(array[0, j] - array[1, j]) >= 3)
                    dif++;
            }

            Console.WriteLine($"Количество выйгрышей равно {win}");
            Console.WriteLine($"Количество пройгрышей равно {lose}");
            Console.WriteLine($"Количество ничьих равно {draw}");
            Console.WriteLine($"Количество игр, где разность забитых и пропущенных мячей была большей или равной трем равна {dif}");
            Console.WriteLine($"Количество заработанных очков {win * 3 + draw}");

            Console.WriteLine("\n-----------------------");
            Console.Write("Ещё Игру? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}