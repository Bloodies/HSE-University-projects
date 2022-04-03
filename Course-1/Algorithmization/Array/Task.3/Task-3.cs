using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Задание:
/// Дан массив ненулевых целых чисел. 
/// Определить, сколько раз элементы массива при просмотре от его начала меняют знак. 
/// Например, в массиве 10, –4, 12, 56, –4, –89 знак меняется 3 раза.
/// </summary>
namespace Task._3
{
    class Program
    {
        static void Main()
        {
            bool ok;
            int kol = 0,
                FirstElem,
                AllElem,
                ArraySize;

            Console.Write("Введите количество элементов: ");
            do
            {
                ok = int.TryParse(Console.ReadLine(), out ArraySize);
                if (ArraySize < 1)
                    ok = false;
                if (ok == false)
                    Console.WriteLine("Ошибка ввода, попробуйте еще раз");
            } while (!ok);

            Console.Write("Введите элемент 1: ");
            do
            {
                ok = int.TryParse(Console.ReadLine(), out FirstElem);
                if (ok == false)
                    Console.WriteLine("Ошибка ввода, попробуйте еще раз");
            } while (!ok);

            for (int i = 1; i < ArraySize; i++)
            {
                Console.Write($"Введите элемент {i + 1}: ");
                do
                {
                    ok = int.TryParse(Console.ReadLine(), out AllElem);
                    if (ok == false)
                        Console.WriteLine("Ошибка ввода, попробуйте еще раз");
                } while (!ok);
                if (FirstElem * AllElem < 0)
                    kol++;
                FirstElem = AllElem;
            }
            Console.WriteLine($"\nЗнак меняется {kol} раз");

            Console.WriteLine("\n-----------------------");
            Console.Write("Ещё Массив? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}