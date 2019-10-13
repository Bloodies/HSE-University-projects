using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _3.Sequence__Последовательность_
{
    class Program
    {
        static public void Push(int n)
        {

        }
        static void Main(string[] args)
        {
            StreamReader input = new StreamReader("input.txt");
            StreamWriter output = new StreamWriter("output.txt");
            int n = int.Parse(input.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                bool correct = true;
                string line = input.ReadLine();
                Stack<char> st = new Stack<char>();

                char ch;
                for (int j = 0; j < line.Length; j++)
                {
                    switch (line[j])
                    {
                        case '[':
                            st.Push('[');
                            break;

                        case '(':
                            st.Push('(');
                            break;

                        case ')':
                            if (st.Count == 0) correct = false;
                            else
                            {
                                ch = st.Pop();
                                if (ch != '(') correct = false;
                            }
                            break;

                        case ']':
                            if (st.Count == 0) correct = false;
                            else
                            {
                                ch = st.Pop();
                                if (ch != '[') correct = false;
                            }
                            break;
                    }
                    if (correct == false) break;
                }
                if (st.Count != 0) correct = false;
                if (correct == true) output.WriteLine("YES");
                else output.WriteLine("NO");
            }

            input.Close();
            output.Close();

        }
    }
}
