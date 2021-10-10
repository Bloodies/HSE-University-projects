using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _1.Balance_check__Проверка_баланса_
{
    public class main
    {
        public static void Main(string[] args)
        {
            using (var sw = new StreamWriter("output.txt"))
            {
                var stdin = File.ReadAllLines("input.txt");
                var n = long.Parse(stdin[0]);
                var tree = new Tree(n);
                tree.Parse(stdin);

                for (int i = 0; i < n; i++)
                    sw.WriteLine(TreeNode<long>.GetBalance(tree.Nodes[i]));

            }
        }
    }

    public class Tree
    {
        public TreeNode<long>[] Nodes { get; private set; }
        private long _nodesCount;
        List<TreeNode<long>> _leafs = new List<TreeNode<long>>();


        public Tree(long n)
        {
            _nodesCount = n;
            this.Nodes = new TreeNode<long>[n];
        }
        //Parsing
        public void Parse(string[] stdin)
        {
            for (int i = 1; i <= _nodesCount; i++)
            {
                var temp = stdin[i].Split(' ').Select(x => long.Parse(x)).ToArray();

                if (this.Nodes[i - 1] == null)
                    this.Nodes[i - 1] = new TreeNode<long>();
                this.Nodes[i - 1].Key = temp[0];
                //Left child
                if (temp[1] != 0)
                {
                    if (this.Nodes[temp[1] - 1] == null)
                        this.Nodes[temp[1] - 1] = new TreeNode<long>()
                        {
                            Parent = this.Nodes[i - 1]
                        };
                    this.Nodes[i - 1].Left = this.Nodes[temp[1] - 1];
                }
                //Right child
                if (temp[2] != 0)
                {
                    if (temp[2] != 0 && this.Nodes[temp[2] - 1] == null)
                        this.Nodes[temp[2] - 1] = new TreeNode<long>() { Parent = this.Nodes[i - 1] };
                    this.Nodes[i - 1].Right = this.Nodes[temp[2] - 1];
                }

                //Calc Height
                if (temp[1] == 0 & temp[2] == 0)
                {
                    TreeNode<long> leaf = this.Nodes[i - 1];
                    Stack<TreeNode<long>> curr = new Stack<TreeNode<long>>();
                    while (leaf != null)
                    {
                        curr.Push(leaf);
                        leaf = leaf.Parent;
                    }
                    while (curr.Count != 0)
                    {
                        leaf = curr.Pop();
                        if (leaf.Height < curr.Count)
                            leaf.Height = curr.Count;
                    }
                }
            }
        }
    }

    public class TreeNode<T> where T : IComparable<T>
    {
        public T Key { get; set; }
        public TreeNode<T> Parent { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public long Height { get; set; }

        public static long GetBalance(TreeNode<T> tree)
        {
            if (tree == null)
                throw new ArgumentNullException("Tree does not exist!");

            if (tree.Left != null && tree.Right != null)
                return tree.Right.Height - tree.Left.Height;
            if (tree.Left == null && tree.Right != null)
                return tree.Right.Height + 1;
            if (tree.Left != null && tree.Right == null)
                return -1 - tree.Left.Height;
            else
                return 0;
        }
    }
}