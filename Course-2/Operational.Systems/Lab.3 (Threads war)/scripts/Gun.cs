using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._3__Threads_war_.scripts
{
    class Gun
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Gun()
        {
            X = Console.BufferWidth / 2;
            Y = Console.BufferHeight - 3;
            Program.screenlock.WaitOne();
            Console.SetCursorPosition(X, Y);
            Console.Write('|');
            Program.screenlock.ReleaseMutex();
        }

        public void MoveLeft()
        {
            Program.screenlock.WaitOne();
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
            Program.screenlock.ReleaseMutex();
            X--;
            if (X == -1)
                X = Console.BufferWidth - 3;
            Program.screenlock.WaitOne();
            Console.SetCursorPosition(X, Y);
            Console.Write('|');
            Program.screenlock.ReleaseMutex();
        }
        public void MoveRight()
        {
            Program.screenlock.WaitOne();
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
            Program.screenlock.ReleaseMutex();
            X++;
            if (X > Console.BufferWidth - 3)
                X = 0;
            Program.screenlock.WaitOne();
            Console.SetCursorPosition(X, Y);
            Console.Write('|');
            Program.screenlock.ReleaseMutex();
        }
    }
}