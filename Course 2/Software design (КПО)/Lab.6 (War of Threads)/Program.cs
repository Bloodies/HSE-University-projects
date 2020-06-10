using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Lab._6__War_of_Threads_
{
    class Text
    {
        private struct COORD { public short X; public short Y; }
        static object gameover = new object();
        [DllImport("Kernel32", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("Kernel32", SetLastError = true)]
        static extern bool ReadConsoleOutputCharacter(IntPtr hConsoleOutput, [Out] char[] lpCharacter, uint nLength, COORD dwReadCoord, out int lpNumberOfCharsRead);
        public static void score()
        {
            Console.SetCursorPosition(0, 23);
            Console.Write("--------------------------------------------------------------------------------");
            Console.Write($"Война потоков - Попаданий: {Program.hit}, Промахов: {Program.miss}");
            if (Program.miss >= 4)
            {
                lock (gameover)
                {
                    Program.mainThread.Suspend();
                    MessageBox.Show($"Игра окончена!\nВаш счет: {Program.hit} попаданий.", "Thread War", MessageBoxButtons.OK);
                    Environment.Exit(0);
                }
            }
        }
        public static char ReadPos(int x, int y)
        {
            char[] ch = new char[1];
            int readCount;
            Program.screenlock.WaitOne();
            ReadConsoleOutputCharacter(GetStdHandle(-11), ch, 1, new COORD() { X = (short)x, Y = (short)y }, out readCount);
            Program.screenlock.ReleaseMutex();
            return ch[0];
        }
    }
    class Program
    {
        
        public static Mutex screenlock = new Mutex();
        public static AutoResetEvent startgame = new AutoResetEvent(false);
        public static Semaphore bulletsem = new Semaphore(3, 3);
        public static Thread mainThread = Thread.CurrentThread;
        public static int miss = 0;
        public static int hit = 0;
        
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);
            Console.CursorVisible = false;
            Text.score();
            Gun gun = new Gun();
            new Thread(Enemy.CreateEnemy).Start();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(false);
                switch (key.Key)
                {
                    case ConsoleKey.Spacebar:
                        new Thread(Bullet.CreateBullet).Start(gun.X);
                        Thread.Sleep(100);
                        break;
                    case ConsoleKey.LeftArrow:
                        startgame.Set();
                        gun.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        startgame.Set();
                        gun.MoveRight();
                        break;
                }
            }
        }
    }
}
