using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _1.Stack__Стек_
{
    class Stack_test
    {
        static void Main()
        {
            Stack<int> st = new Stack<int>();
            StreamReader input = new StreamReader("input.txt");
            StreamWriter output = new StreamWriter("output.txt");
            int n = int.Parse(input.ReadLine());
            //int n = int.Parse(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                string currentline = input.ReadLine();
                //string currentline = Console.ReadLine();
                if (currentline.Contains('+'))
                {
                    currentline = currentline.Replace("+", "").Replace(" ", "");
                    st.Push(int.Parse(currentline));
                }
                else
                {
                    output.WriteLine(st.Pop());
                    //Console.WriteLine(st.Pop());
                }
            }
            input.Close();
            output.Close();
            //Console.ReadKey();
        }
    }
}