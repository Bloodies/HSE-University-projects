using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _1.Balance_check__Проверка_баланса_
{
    class Balance
    {
        private static (int, int, int)[] nodes;
        //private static StreamWriter output = new StreamWriter("output.txt");

        private static void BigStackMain()
        {
            Height_tree(0);
            for (int i = 0; i < nodes.Length; i++)
            {
                /*output.*/Console.WriteLine(NodeBalance(i));
            }
            Console.ReadKey();
            //output.Close();
        }
        private static int Height_tree(int numberNode)
        {
            int leftHeight = 0, rightHeight = 0;
            if (nodes[numberNode].Item1 != 0)
            {
                leftHeight = Height_tree(nodes[numberNode].Item1 - 1);
            }

            if (nodes[numberNode].Item2 != 0)
            {
                rightHeight = Height_tree(nodes[numberNode].Item2 - 1);
            }

            nodes[numberNode].Item3 = Math.Max(leftHeight, rightHeight) + 1;
            return Math.Max(leftHeight, rightHeight) + 1;
        }
        public static int NodeBalance(int numberNode)
        {
            int leftHeight = 0, 
                rightHeight = 0;
            if (nodes[numberNode].Item1 != 0)
            {
                leftHeight = nodes[nodes[numberNode].Item1 - 1].Item3;
            }
            if (nodes[numberNode].Item2 != 0)
            {
                rightHeight = nodes[nodes[numberNode].Item2 - 1].Item3;
            }
            return rightHeight - leftHeight;
        }
        static void Main()
        {
            //StreamReader input = new StreamReader("input.txt");
            int N_length = int.Parse(/*input.*/Console.ReadLine());
            nodes = new (int, int, int)[N_length];
            string[] tokens;
            for (int i = 0; i < N_length; i++)
            {
                tokens = /*input.*/Console.ReadLine().Split(' ');
                nodes[i] = (int.Parse(tokens[1]), int.Parse(tokens[2]), 0);
            }
            //input.Close();
            Thread newThread = new Thread(BigStackMain, 100000000);
            newThread.Start();
        }
    }
}