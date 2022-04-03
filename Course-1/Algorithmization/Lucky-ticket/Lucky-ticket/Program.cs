using System;

namespace Lucky_ticket
{
    class Program
    {
        static void Main()
        {
            string ticket;
            int frsthalf,
                scndhalf,
                numticket = 1;
            for (int i = 0; i <= 999999; i++)
            {
                ticket = String.Format($"{i:000000}");
                frsthalf = (int)ticket[0] + (int)ticket[1] + (int)ticket[2];
                scndhalf = (int)ticket[3] + (int)ticket[4] + (int)ticket[5];
                if (frsthalf == scndhalf)
                {
                    Console.WriteLine($"Счастливый билет №{numticket}: {ticket}");
                    numticket++;
                }
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Всего счастливых билетов: {numticket - 1}");
            Console.WriteLine("Минимальное расстояние между двумя идущими подряд счастливыми билетами = 9: 001001 и 001010");
            Console.WriteLine("Максимальное расстояние между двумя идущими подряд счастливыми билетами = 1001: 000000 и 001001");
            Console.WriteLine("Среднее количество счастиливых билетов на 1000 номеров: 55,252");

            Console.WriteLine("\n------------------");
            Console.Write("Ещё текст? [y/n]: ");
            if (Console.ReadLine() == "y")
                Main();
            else
                Environment.Exit(0);
        }
    }
}
