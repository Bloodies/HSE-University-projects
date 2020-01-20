using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _3.Tree_height__Высота_дерева_
{
    class Tree_test
    {

        //public static int CalcHigh(ref int[,] roots, int i)
        //{
        //    if (roots[i, 1] == -1 && roots[i, 2] == -1)
        //        return 1;
        //    if (roots[i, 1] == -1)
        //        return 1 + CalcHigh(ref roots, roots[i, 2]);
        //    if (roots[i, 2] == -1)
        //        return 1 + CalcHigh(ref roots, roots[i, 1]);
        //    return 1 + Math.Max(CalcHigh(ref roots, roots[i, 1]), CalcHigh(ref roots, roots[i, 2]));
        //}

        static void Main()
        {
            StreamReader input = new StreamReader("input.txt");
            StreamWriter output = new StreamWriter("output.txt");
            int n = int.Parse(input.ReadLine());

            int[,] roots = new int[n, 4];
            //0 - h
            //1 - right
            //2 - left
            //3 - parent
            for (int i = 0; i < n; i++)
            {
                string[] line = input.ReadLine().Split(' ');
                roots[i, 0] = 0;
                roots[i, 1] = int.Parse(line[1]) - 1;
                roots[i, 2] = int.Parse(line[2]) - 1;
                roots[i, 3] = -1;
            }

            for (int i = 0; i < n; i++)
            {
                if (roots[i, 1] != -1)
                    roots[roots[i, 1], 3] = i;
                if (roots[i, 2] != -1)
                    roots[roots[i, 2], 3] = i;
            }
            int h = 0;
            /*for (int i = 0; i < n; i++)
            {
            if (roots[i, 3] == -1)
            {
            h = CalcHigh(ref roots, i);
            break;
            }
            }*/
            bool[] hcalc = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (roots[i, 3] == -1)
                {
                    hcalc[i] = true;
                    roots[i, 0] = 1;
                    break;
                }
            }

            for (int i = 0; i < Math.Log(n, 2) + 1; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (hcalc[j])
                    {
                        if (roots[j, 1] != -1)
                        {
                            roots[roots[j, 1], 0] = roots[j, 0] + 1;
                            hcalc[roots[j, 1]] = true;
                        }
                        if (roots[j, 2] != -1)
                        {
                            roots[roots[j, 2], 0] = roots[j, 0] + 1;
                            hcalc[roots[j, 2]] = true;
                        }
                    }
                }
            }
            h = 0;
            for (int i = 0; i < n; i++)
            {
                h = Math.Max(roots[i, 0], h);
            }
            output.Write(h);
            output.Close();
            input.Close();
        }
    }
}