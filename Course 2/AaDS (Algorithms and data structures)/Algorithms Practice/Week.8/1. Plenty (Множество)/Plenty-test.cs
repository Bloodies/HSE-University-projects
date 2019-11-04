using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1.Plenty__Множество_
{
    class Plenty_test
    {
        class Program
        {
            static void Main()
            {
                StreamReader input = new StreamReader("input.txt");
                int length = int.Parse(input.ReadLine());
                Dictionary<string, bool> dictionary = new Dictionary<string, bool>(length);
                StreamWriter output = new StreamWriter("output.txt");
                for (int i = 1; i <= length; i++)
                {
                    string[] tokens = input.ReadLine().Split(' ');
                    switch (tokens[0][0])
                    {
                        case 'A':
                            {
                                if (!dictionary.ContainsKey(tokens[1]))
                                {
                                    dictionary[tokens[1]] = true;
                                }

                                break;
                            }

                        case 'D':
                            {
                                if (dictionary.ContainsKey(tokens[1]))
                                {
                                    dictionary.Remove(tokens[1]);
                                }

                                break;
                            }

                        case '?':
                            {
                                if (dictionary.ContainsKey(tokens[1]))
                                {
                                    output.WriteLine('Y');
                                }
                                else
                                {
                                    output.WriteLine('N');
                                }

                                break;
                            }
                    }
                }

                input.Close();
                output.Close();
            }
        }
    }
}