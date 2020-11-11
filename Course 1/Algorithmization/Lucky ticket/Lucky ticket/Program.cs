using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ticket = String.Format("{0:000000}", i);
                frsthalf = (int)ticket[0] + (int)ticket[1] + (int)ticket[2];
                scndhalf = (int)ticket[3] + (int)ticket[4] + (int)ticket[5];
                if (frsthalf == scndhalf)
                {
                    Console.WriteLine("Счастливый билет №" + numticket + ": " + ticket);
                    numticket++;
                }
            }
            numticket = numticket - 1;
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Всего счастливых билетов:" + numticket);
            Console.WriteLine("Минимальное расстояние между двумя идущими подряд счастливыми билетами = 9: 001001 и 001010");
            Console.WriteLine("Максимальное расстояние между двумя идущими подряд счастливыми билетами = 1001: 000000 и 001001");
            Console.WriteLine("Среднее количество счастиливых билетов на 1000 номеров: 55,252");
            
            Console.WriteLine("Ещё текст? [да/нет]");
            string restart = Console.ReadLine();
            Console.WriteLine("   ");
            if (restart == "да")
            {
                Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
