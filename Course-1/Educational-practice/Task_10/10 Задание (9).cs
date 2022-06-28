using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;

/// <summary>
/// Практическое задание №10 (9)
/// Написать метод «стягивания» в одну вершину всех вершин, информационное поле которых содержит заданное значение. 
/// При «стягивании» в графе остается только одна вершина, содержащая заданное значение,
/// остальные вершины удаляются, но все исходящие из них и входящие в них дуги «передаются» оставшейся вершине. 
/// При этом петли, связывающие вершину с собой, не создаются. 
/// Не создаются также и параллельные дуги
/// </summary>
namespace Task_10
{
    #region
    class MyList
    {
        public Vertex[] v;
        public int Count;
        public MyList()
        {
            Count = 0;
        }
        public Vertex this[int i]
        {
            get { return v[i]; }
            set { v[i] = value; }
        }
        public void Add(Vertex a)
        {
            Vertex[] buff = new Vertex[Count + 1];
            buff[Count] = a;
            for (int i = 0; i < Count; i++)
            {
                buff[i] = v[i];
            }
            Count++;
            v = buff;
        }
        public bool Remove(Vertex a)
        {
            bool flag = true;
            MyList buff = new MyList();
            for (int i = 0; i < Count; i++)
            {
                if (flag && v[i] == a)
                {
                    flag = false;
                }
                else
                {
                    buff.Add(v[i]);
                }
            }
            v = buff.v;
            if (!flag)
            { Count--; return true; }
            else
                return false;
        }
        public bool Contains(Vertex a)
        {
            for (int i = 0; i < Count; i++)
            {
                if (v[i] == a)
                {
                    return true;
                }
            }
            return false;
        }
        public void Concat(MyList a, int name)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (!Contains(a[i]) && a[i].name != name)
                {
                    Add(a[i]);
                }
            }
        }
    }
    #endregion

    #region
    class Vertex
    {
        public int name;
        public double value;
        public MyList linked;
        public Vertex()
        {
            name = 0;
            value = 0;
            linked = new MyList();
        }
        static public bool operator !=(Vertex a, Vertex b)
        {
            return (a.name != b.name && a.value != b.value);
        }
        static public bool operator ==(Vertex a, Vertex b)
        {
            return (a.name == b.name &&
                a.value == b.value);
        }
        public Vertex(int n, double v)
        {
            name = n;
            value = v;
            linked = new MyList();
        }
        public void Read()
        {
            Console.WriteLine("Enter name :");
            name = int.Parse(Console.ReadLine());

            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            Console.WriteLine("Enter value :");
            value = double.Parse(Console.ReadLine(), formatter);
        }
        public void Read_linked()
        {
            Console.WriteLine("Enter number of linked vertexes :");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var buff = new Vertex();
                buff.Read();
                linked.Add(buff);
            }
        }
        public void Print()
        {
            Console.Write($"{ name }({value}) :");
            for (int i = 0; i < linked.Count; i++)
                Console.Write($"{ linked[i].name }({linked[i].value}),");
            Console.WriteLine();
        }
    }
    #endregion

    class Program
    {
        static MyList Connect(MyList v)
        {
            Vertex dell = null, add = null;
            //проверяем есть ли соседи с одинаковыми значениями
            //если таких нет - алгоритм заканчивает работу
            while (true)
            {
                //поиск соседних верщин с одинаковыми значениями
                bool flag = true;
                for (int i = 0; i < v.Count; i++)
                {
                    for (int j = 0; j < v[i].linked.Count; j++)
                    {
                        if (v[i].value == v[i].linked[j].value)
                        {
                            flag = false;
                            add = v[i];
                            dell = v[i].linked[j];
                            break;

                        }
                    }
                    if (!flag)
                        break;
                }
                if (flag)
                    return v;
                add.linked.Concat(dell.linked, add.name);
                //удаляем из всех вершин смежность с вершиной Jname и добавляем I
                for (int i = 0; i < v.Count; i++)
                {
                    if (v[i].name != add.name)
                    {
                        if (v[i].linked.Remove(dell))
                        {
                            if (v[i].name != add.name)
                            {
                                v[i].linked.Add(add);
                            }

                        }
                    }
                    else  //теперь удаляем вершину с дублирующимся значением из исходного массива
                        v[i].linked.Remove(dell);
                }
                v.Remove(dell);
            }
        }
        static void Main()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            do
            {
                Console.WriteLine("Практическое задание №11:");
                Console.WriteLine("Задача 9");
                Console.WriteLine(@"Написать метод «стягивания» в одну вершину всех вершин, информационное поле которых содержит заданное значение. 
При «стягивании» в графе остается только одна вершина, содержащая заданное значение, 
остальные вершины удаляются, но все исходящие из них и входящие в них дуги «передаются» оставшейся вершине. 
При этом петли, связывающие вершину с собой, не создаются.
Не создаются также и параллельные дуги.");
                Console.WriteLine(" ");
                Console.WriteLine("ПРОГРАММА УЖЕ ИМЕЕТ ЗАПОЛНЕНЫЙ ГРАФ");
                MyList v = new MyList();
                v.Add(new Vertex(0, 0));
                v.Add(new Vertex(1, 6));
                v.Add(new Vertex(2, 7));
                v.Add(new Vertex(3, 7));
                v.Add(new Vertex(4, 7));

                v[0].linked.Add(v[1]);
                v[0].linked.Add(v[3]);
                v[1].linked.Add(v[4]);
                v[2].linked.Add(v[0]);
                v[3].linked.Add(v[2]);
                v[3].linked.Add(v[4]);
                v[4].linked.Add(v[2]);

                for (int i = 0; i < v.Count; i++)
                    v[i].Print();
                Connect(v);
                Console.WriteLine("ИТОГОВЫЙ СЖАТЫЙ ГРАФ");
                for (int i = 0; i < v.Count; i++)
                    v[i].Print();

                #region region menu
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Нажмите ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("любую клавишу");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" для повтора или ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Esc");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" для выхода...");
                int end = Console.ReadKey().KeyChar;
                Console.WriteLine(" ");
                switch (end)
                {
                    case 27:
                        Console.Clear();
                        Console.WriteLine(" ");
                        Console.Write("Завершение работы.");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Environment.Exit(0);
                        break;
                    default:
                        Main();
                        continue;
                }
                #endregion

            } while (!true);
        }
    }
}