using System;

namespace Task_2
{
    class Program
    {
        static void Main()
        {
            string[] split;
            string s = Console.ReadLine();
            split = s.Split(new char[] {' '});
            int[] name = new int[14];
            for (int i = 0; i < 5; i++)
            {
                int.TryParse(split[i], out int n);
                name[n]++;
            }
            int[] col = new int[6];
            for (int i = 0; i < 14; i++)
                col[name[i]]++;
            bool flag = false;
            for (int j = 1; j <= 9; j++)
                if ((name[j] == 1) && (name[j] == name[j + 1]) && (name[j] == name[j + 2]) && (name[j] == name[j + 3]) && (name[j] == name[j + 4])) flag = true;
            if (col[5] == 1) Console.WriteLine("Impossible");
            else if (col[4] == 1) Console.WriteLine("Four of a Kind");
            else if ((col[3] == 1) && (col[2] == 1)) Console.WriteLine("Full House");
            else if (flag) Console.WriteLine("Straight");
            else if (col[3] == 1) Console.WriteLine("Three of a Kind");
            else if (col[2] == 2) Console.WriteLine("Two Pairs");
            else if (col[2] == 1) Console.WriteLine("One Pair");
            else Console.WriteLine("Nothing");
        }
    }
}
