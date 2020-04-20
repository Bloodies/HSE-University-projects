using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1.Binary_search__Двоичный_поиск_
{
    class Search_test
    {
        private static Dictionary<int, (int, int)> _requestHash = new Dictionary<int, (int, int)>();

        private static (int, int) BinarySearch(int element, int leftBoard, int rightBoard, int[] array)
        {
            if (_requestHash.ContainsKey(element))
            {
                return _requestHash[element];
            }
            else
            {
                while (leftBoard < rightBoard)
                {
                    int middleIndex = (leftBoard + rightBoard) / 2;
                    if (array[middleIndex] < element)
                    {
                        leftBoard = middleIndex + 1;
                    }
                    else
                    {
                        if (array[middleIndex] > element)
                        {
                            rightBoard = middleIndex - 1;
                        }
                        else
                        {
                            int firstPosition = middleIndex, lastPosition = middleIndex;
                            while (firstPosition > 0 && array[firstPosition - 1] == array[middleIndex])
                            {
                                --firstPosition;
                            }

                            while (lastPosition < rightBoard && array[lastPosition + 1] == array[middleIndex])
                            {
                                ++lastPosition;
                            }

                            _requestHash[element] = (firstPosition + 1, lastPosition + 1);
                            return _requestHash[element];
                        }
                    }
                }

                if (array[leftBoard] == element)
                {
                    return (leftBoard + 1, leftBoard + 1);
                }
                else
                {
                    return (-1, -1);
                }
            }
        }

        static void Main()
        {
            StreamReader input = new StreamReader("input.txt");
            int length = int.Parse(input.ReadLine());
            int[] array = new int[length];
            int index = 0;
            foreach (string element in input.ReadLine().Split(' '))
            {
                array[index] = int.Parse(element);
                index++;
            }

            input.ReadLine();
            StreamWriter output = new StreamWriter("output.txt");

            foreach (string searchElement in input.ReadLine().Split(' '))
            {
                (int, int) result = BinarySearch(int.Parse(searchElement), 0, length - 1, array);
                output.WriteLine(result.Item1 + " " + result.Item2);
            }

            input.Close();
            output.Close();
        }
    }
}