using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] items = { "Один", "Два", "Три" };
            int[] position = new int[items.Length];
            int currentIndex = 0, previousIndex = 0;
            int positionX = 5, positionY = 2;
            bool itemSelected = false;

            //Начальный вывод пунктов меню.
            for (int i = 0; i < items.Length; i++)
            {
                if (!(i == 0))
                    positionX += items[i - 1].Length + 3;
                Console.CursorLeft = positionX;
                Console.CursorTop = positionY;
                Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(items[i]);
                position[i] = positionX;
            }
            do
            {
                // Вывод предыдущего активного пункта основным цветом.
                Console.CursorLeft = position[previousIndex];
                Console.CursorTop = positionY;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(items[previousIndex]);//Вывод активного пункта.

                Console.CursorLeft = position[currentIndex];
                Console.CursorTop = positionY;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(items[currentIndex]);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                previousIndex = currentIndex;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        currentIndex++;
                        break;
                    case ConsoleKey.LeftArrow:
                        currentIndex--;
                        break;
                    case ConsoleKey.Enter:
                        itemSelected = true;
                        break;
                }

                if (currentIndex == items.Length)
                    currentIndex = items.Length - 1;
                else if (currentIndex < 0)
                    currentIndex = 0;
            }
            while (!itemSelected);

            Console.CursorLeft = 40;
            Console.CursorTop = positionY;
            Console.WriteLine($"Выбран пункт: {currentIndex + 1}.");
            Console.ReadLine();
        }
    }
}
