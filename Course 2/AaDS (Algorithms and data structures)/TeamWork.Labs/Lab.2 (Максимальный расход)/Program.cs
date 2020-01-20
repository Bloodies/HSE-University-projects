using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab._2__Максимальный_расход_
{
    public class Main_program
    {
        private static int _int_input(string message)
        {
            var digit = -1;
            while (true)
            {
                Console.Write(message);
                var ok = int.TryParse(Console.ReadLine(), out digit);
                if (ok) return digit;
            }
        }
        private static int _check_zero_one_value()
        {
            var Size = _int_input("Write size of the graph: ");
            while (true)
            {
                if (Size == 0 || Size == 1)
                {
                    Console.WriteLine("Wrong input! Size cannot be a zero|one value... Repeat please");
                    Size = _int_input("Write size of the graph: ");
                    continue;
                }

                return Size;
            }
        }
        public static void Main()
        {
            int size = _check_zero_one_value();
            var _generator = new GraphGenerator(size, 0);
            byte[,] graph = _generator.GetMatrix;
            int[,] ribs_capacity = _generator.GetRibsCapacity;
            GraphAction action = new GraphAction(graph, ribs_capacity);
            action.Output();
            Console.WriteLine("Generator answer: {1}\nMax flow in the graph: {0}",
                action.GetMaxFlow,
                _generator.GetSum);
            Console.ReadKey(true);
        }
    }
    public class GraphGenerator
    {
        private byte[,] graph;
        private int[,] rib_capacity;
        private int Size;
        private int Sum;
        public GraphGenerator() : base() { }
        public GraphGenerator(int _Size, int type) : base()
        {
            Size = _Size;
            if (type == 0)
            {
                Generate();
            }
            else
            {
                Input();
            }
        }
        public byte[,] GetMatrix => graph;
        public int[,] GetRibsCapacity => rib_capacity;
        public int GetSum => Sum;
        private void Generate()
        {
            Random rnd = new Random();
            int[,] matrix = new int[Size, Size];
            byte[,] smezh = new byte[Size, Size];
            int sum = 0;
            if (Size == 0) Size = rnd.Next(3, 100);
            //Заполнение A и нулей
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (j <= i)
                    {
                        matrix[i, j] = 0;
                        smezh[i, j] = 0;
                    }
                    else
                    {
                        if (j * 2 > Size - 1 && i * 2 <= Size - 1)
                        {
                            matrix[i, j] = rnd.Next(0, 100);
                            if (matrix[i, j] == 0) smezh[i, j] = 0;
                            else smezh[i, j] = 1;
                            sum += matrix[i, j];
                        }
                    }
                }
            }

            Sum = sum;

            //Заполнение B и C
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((j > i) && ((j * 2 <= Size - 1 && i * 2 <= Size - 1) || (j * 2 > Size - 1 && i * 2 > Size - 1)))
                    {
                        do
                        {
                            matrix[i, j] = rnd.Next(0, 10000);
                        } while (matrix[i, j] < sum);
                        if (matrix[i, j] == 0) smezh[i, j] = 0;
                        else smezh[i, j] = 1;
                    }
                }
            }
            rib_capacity = matrix;
            graph = smezh;
        }
        /// <summary>
        /// Input graph from the keyboard
        /// </summary>
        private void Input()
        {
            StreamReader st = new StreamReader("input.txt");
            Size = Int32.Parse(st.ReadLine());
            int[,] matrix = new int[Size, Size];
            byte[,] smezh = new byte[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                string tmp = st.ReadLine();
                string[] arr = tmp.Split(' ');
                for (int j = 0; j < Size; j++)
                {
                    matrix[i, j] = Int32.Parse(arr[j]);
                    if (matrix[i, j] == 0) smezh[i, j] = 0; else smezh[i, j] = 1;
                }
            }
            rib_capacity = matrix;
            graph = smezh;
        }
    }
    public class GraphAction
    {
        private readonly byte[,] graph;
        private readonly int[,] rib_capacity;
        private int[,] curr_flow;
        private int size;
        private int max_flow;
        private bool[] used;
        private int[] flow;
        private int[] parent;
        private int[] dist;
        public GraphAction(byte[,] _graph, int[,] _rib_capacity) : base()
        {
            graph = _graph;
            rib_capacity = _rib_capacity;
            max_flow = 0;
            size = graph.GetLength(0);

            used = new bool[size];
            flow = new int[size];
            parent = new int[size];
            dist = new int[size];
            curr_flow = new int[size, size];

            find_max_flow();
        }
        public int GetMaxFlow => max_flow;
        public void Output()
        {
            for (int row = 0; row < size; ++row)
            {
                for (int column = 0; column < size; ++column)
                {
                    Console.Write("{0}({1}) \t", graph[row, column], rib_capacity[row, column]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            for (int row = 0; row < size; ++row)
            {
                for (int column = 0; column < size; ++column)
                {
                    if (curr_flow[row, column] < 0)
                    {
                        Console.Write("{0} \t", 0);
                    }
                    else
                    {
                        Console.Write("{0} \t", curr_flow[row, column]);
                    }
                }

                Console.WriteLine();
            }
        }
        private void find_max_flow()
        {
            int u, v;
            int s = 0, t = size - 1;
            max_flow = 0;
            while (bfs(s, t))
            {
                int rib_add = flow[t];
                v = t;
                u = parent[v];
                while (v != s)
                {
                    curr_flow[u, v] += rib_add;
                    curr_flow[v, u] -= rib_add;
                    v = u;
                    u = parent[v];
                }

                max_flow += rib_add;
            }
        }
        private void init()
        {
            for (int i = 1; i < size; ++i)
            {
                parent[i] = 0;
                used[i] = false;
                flow[i] = 0;
                dist[i] = Int32.MaxValue;
            }
        }
        private bool bfs(int s, int t)
        {
            init();
            Queue<int> Q = new Queue<int>();
            used[s] = true;
            parent[s] = s;
            flow[s] = Int32.MaxValue;

            Q.Enqueue(s);
            while (!used[t] && Q.Count != 0)
            {
                int u = Q.Dequeue();
                for (int node = 1; node < size; ++node)
                {
                    if (!used[node] && (rib_capacity[u, node] - curr_flow[u, node] > 0))
                    {
                        flow[node] = Math.Min(flow[u], rib_capacity[u, node] - curr_flow[u, node]);
                        used[node] = true;
                        parent[node] = u;
                        Q.Enqueue(node);
                    }
                }
            }

            return used[t];
        }
    }
}
