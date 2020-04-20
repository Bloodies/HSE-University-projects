using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _3.Quick_search__Быстрый_поиск_
{
    class Search_test
    {
        private static int[] BuildPrefix(string str)
        {
            int[] prefixes = new int[str.Length];
            int i = 1, j = 0;
            while (i < str.Length)
            {
                if (str[i] == str[j])
                {
                    prefixes[i] = j + 1;
                    ++i;
                    ++j;
                }
                else
                {
                    if (j > 0)
                    {
                        j = prefixes[j - 1];
                    }
                    else
                    {
                        prefixes[i] = 0;
                        ++i;
                    }
                }
            }
            return prefixes;
        }
        private static List<int> FindSubString(string subStr, string goalStr)
        {
            var listPositions = new List<int>();
            int i = 0, j = 0;
            int[] prefixes = BuildPrefix(subStr);
            while (i < goalStr.Length)
            {
                if (goalStr[i] == subStr[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    if (j > 0)
                    {
                        j = prefixes[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }

                if (j == subStr.Length)
                {
                    listPositions.Add(i - subStr.Length);
                    j = prefixes[j - 1];
                }
            }
            return listPositions;
        }
        static void Main()
        {
            StreamReader input = new StreamReader("input.txt");
            string subStr = input.ReadLine();
            string goalStr = input.ReadLine();
            input.Close();
            var listPositions = FindSubString(subStr, goalStr);
            StreamWriter output = new StreamWriter("output.txt");
            output.WriteLine(listPositions.Count);
            foreach (int position in listPositions)
            {
                output.Write(position + 1 + " ");
            }
            output.Close();
        }
    }
}