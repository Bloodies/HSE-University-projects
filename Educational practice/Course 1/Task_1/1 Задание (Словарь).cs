using System;


namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int output = 0;
            string check,
                   s;
            bool ok;
            s = Console.ReadLine();
            int.TryParse(s, out int n);
            string[] array = new string[n];
            for (int i = 0; i < n; i++)
                array[i] = Console.ReadLine();
            int[] dict = new int[26];
            int[] copy_dict = new int[26];
            check = Console.ReadLine();
            for (int i = 0; i < check.Length; i++)
                dict[(int)check[i] - 97]++;
            for (int i = 0; i < n; i++)
            {
                ok = true;
                for (int j = 0; j < 26; j++) copy_dict[j] = dict[j];
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (copy_dict[array[i][j] - 97] == 0) ok = false;
                    else copy_dict[array[i][j] - 97]--;
                }
                if (ok) output++;
            }
            Console.WriteLine(output);
        }
    }
}