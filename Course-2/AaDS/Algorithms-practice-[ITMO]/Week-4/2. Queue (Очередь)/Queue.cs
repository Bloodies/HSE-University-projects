using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Queue__Очередь_
{
    class Queue
    {
        static void Main()
        {
            Queue<int> st = new Queue<int>();
            //StreamReader input = new StreamReader("input.txt");
            //StreamWriter output = new StreamWriter("output.txt");
            //int n = int.Parse(input.ReadLine());
            int n = int.Parse(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                //string currentline = input.ReadLine();
                string currentline = Console.ReadLine();
                if (currentline.Contains('+'))
                {
                    currentline = currentline.Replace("+", "").Replace(" ", "");
                    st.Enqueue(int.Parse(currentline));
                }
                else
                {
                    //output.WriteLine(st.Dequeue());
                    Console.WriteLine(st.Dequeue());
                }
            }
            //input.Close();
            //output.Close();
            Console.ReadKey();
        }
    }
}