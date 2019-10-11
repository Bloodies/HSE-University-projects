using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _1.Стек__Stack_
{
    class Stack
    {
        private static int[] array = new int[100000];
        private static int top = 0;

        private static bool IsEmpty()
        {
            if (top == 0) return true;
            else return false;
        }

        private static void Add(int[] array, int number)
        {
            if (top == array.Length)
            {
                throw new Exception("Overflow");
            }
            else
            {
                top++;
                array[top] = number;
            }
        }
        static void Remove(int[] array, PrintStream output)
        {
            if (IsEmpty())
            {
                throw new Exception("Underflow");
            }
            else
            {
                output.println(array[top]);
                top--;
            }
        }

        static void Main(string[] args)
        {
            var input = new Scanner<int>(new File("input.txt"));
            var fos = new FileStream("output.txt");
            var output = new StreamWriter(fos);

            int commandsCount = input.nextInt();

            for (int i = 0; i <= commandsCount; i++)
            {
                String command = new String();
                command = input.nextLine();
                if (command.startsWith("+"))
                {
                    command = command.Substring(2, command.Length());
                    int number = Integer.parseInt(command);
                    Add(array, number);
                }
                else if (command.StartsWith("-"))
                {
                    Remove(array, output);
                }
            }
            input.Close();
            output.Close();
        }
    }
}
