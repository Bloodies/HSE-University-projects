using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Bunch__Куча_
{
    class Program
    {
        public static bool Heap(int[] heap)
        {
            for (int i = 1; i <= heap.Length; i++)
            {
                if (2 * i <= heap.Length && heap[i - 1] > heap[2 * i - 1])
                    return false;
                if (2 * i + 1 <= heap.Length && heap[i - 1] > heap[2 * i])
                    return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            string input_f = "input.txt", output_f = "output.txt";
            using (FileStream sf = new FileStream(input_f, FileMode.OpenOrCreate)) { }
            using (FileStream sf = new FileStream(output_f, FileMode.OpenOrCreate)) { }
            using (StreamReader input = new StreamReader(input_f))
            {
                input.ReadLine();
                using (StreamWriter output = new StreamWriter(output_f))
                {
                    string[] strs = input.ReadLine().Trim().Split(new char[] { ' ' });
                    int[] arr = new int[strs.Length];
                    for (int i = 0; i < strs.Length; i++)
                        arr[i] = Convert.ToInt32(strs[i]);
                    if (Heap(arr))
                        output.WriteLine("YES");
                    else
                        output.WriteLine("NO");
                }
            }
        }
    }
}