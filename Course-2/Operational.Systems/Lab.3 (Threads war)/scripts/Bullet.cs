using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab._3__Threads_war_.scripts
{
    class Bullet
    {
        static public void CreateBullet(object o)
        {
            int x = (int)o;
            if (Program.Text.ReadPos(x, Console.BufferHeight - 2) == '*') return;
            if (!Program.bulletsem.WaitOne(0)) return;
            new Thread(new Bullet(x).MoveBullet).Start();
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Bullet(int x)
        {
            X = x;
            Y = Console.BufferHeight - 4;
        }
        public void MoveBullet()
        {
            for (; Y >= 0; Y--)
            {

                Program.screenlock.WaitOne();
                Console.SetCursorPosition(X, Y);
                Console.Write('*');
                Program.screenlock.ReleaseMutex();
                Thread.Sleep(100);
                Program.screenlock.WaitOne();
                Console.SetCursorPosition(X, Y);
                Console.Write(' ');
                Program.screenlock.ReleaseMutex();
            }
            Program.bulletsem.Release();
        }
    }
}