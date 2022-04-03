using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Sequence__Последовательность_
{
    class Sequence_test
    {
        static void Main()
        {
            StreamReader input = new StreamReader("input.txt");
            StreamWriter output = new StreamWriter("output.txt");
            int n = int.Parse(input.ReadLine());
            //int n = int.Parse(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                bool correct = true;
                string line = input.ReadLine();
                //string line = Console.ReadLine();
                Stack<char> str = new Stack<char>();

                char ch;
                for (int j = 0; j < line.Length; j++)
                {
                    switch (line[j])
                    {
                        case '[':
                            str.Push('[');
                            break;
                        case '(':
                            str.Push('(');
                            break;
                        case ')':
                            if (str.Count == 0) correct = false;
                            else
                            {
                                ch = str.Pop();
                                if (ch != '(') correct = false;
                            }
                            break;
                        case ']':
                            if (str.Count == 0) correct = false;
                            else
                            {
                                ch = str.Pop();
                                if (ch != '[') correct = false;
                            }
                            break;
                    }
                    if (correct == false) break;
                }
                if (str.Count != 0) correct = false;
                if (correct == true)
                {
                    output.WriteLine("YES");
                    //Console.WriteLine("YES");
                }
                else
                {
                    output.WriteLine("NO");
                    //Console.WriteLine("NO");
                }
            }
            input.Close();
            output.Close();
            //Console.ReadKey();
        }
    }
}