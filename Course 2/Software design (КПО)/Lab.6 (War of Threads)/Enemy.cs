using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Lab._6__War_of_Threads_
{
    class Enemy
    {
        static public void CreateEnemy()
        {
            Random rnd = new Random();
            Program.startgame.WaitOne(15000, false);
            while (true)
            {
                if (rnd.Next(0, 101) < (Program.hit + Program.miss) / 25 + 20)
                    new Thread(new Enemy(rnd.Next(0, 11)).MoveEnemy).Start();
                Thread.Sleep(1000);
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        private readonly int dir;
        private static char[] badchar = { '-', '\\', '|', '/' };
        public Enemy(int y)
        {
            Y = y;
            X = y % 2 == 0 ? 0 : Console.BufferWidth - 1;
            dir = X == 0 ? 1 : -1;
        }
        public void MoveEnemy()
        {
            while ((dir == 1 && X != Console.BufferWidth) || (dir == -1 && X != 0))
            {
                bool hitme = false;
                Program.screenlock.WaitOne();
                Console.SetCursorPosition(X, Y);
                Console.Write(badchar[X % 4]);
                Program.screenlock.ReleaseMutex();
                for (int i = 0; i < 15; i++)
                {
                    Thread.Sleep(40);
                    if (Text.ReadPos(X, Y) == '*')
                    {
                        hitme = true;
                        break;
                    }
                }
                
                Program.screenlock.WaitOne();
                Console.SetCursorPosition(X, Y);
                Console.Write(' ');
                Program.screenlock.ReleaseMutex();
                if (hitme)
                {
                    Console.Beep();
                    Interlocked.Increment(ref Program.hit);
                    Console.SetCursorPosition(0, 24);
                    Text.score();
                    Thread.CurrentThread.Abort();
                }
                X += dir;
            }
            Interlocked.Increment(ref Program.miss);
            Console.SetCursorPosition(0, 24);
            Text.score();
        }
    }
}
