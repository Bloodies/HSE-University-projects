using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab._6
{
    class Text_Dialog
    {
        //---Все менюшки----------------------------------------------     
        public static void PrintErrorMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нужно выбрать из списка!");
            Console.ResetColor();
        }
        //------------------------------------------------------------
    }
    class Search_for_Bugs
    {
        //---Проверка ввода-------------------------------------------
        public static int ProverkaVvoda() //Проверка ввода в массив
                                          //Тут больше и нечего добавлять
                                          //Просто небольшая функция на проверку
        {
            int number;
            bool res;
            do
            {
                res = int.TryParse(Console.ReadLine(), out number);

                if (res == false)
                {
                    Console.WriteLine("Некорректный ввод");
                }
            } while (!res);
            return number;
        }
        public static char Trying()
        {
            Console.WriteLine("Введите букву или цифру");
            while (true)
            {

                string str = Console.ReadLine();
                if (str.Length == 1)
                    return str[0];
                else
                    Console.WriteLine("Неверный ввод\nПопробуйте снова");
            }
        }
        //------------------------------------------------------------
    }
    class Program
    {
        //---Первое задание------------------------------------------------------------------------------------------
        #region
        static string symbols = "1234567890abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        static Random r = new Random();
        static char GetRandomChar()
        {
            var index = r.Next(symbols.Length);
            return symbols[index];
        }
        static void RestartMenu()
        {
            Console.Clear();
            Console.WriteLine("\n---!!!!!------Массив пуст-----!!!!!!-----");
            Console.WriteLine("\n------------------------------");
            Console.WriteLine("| Выберите действие:         |");
            Console.WriteLine("| 9) Вернуться в начало      |");
            Console.WriteLine("| 0) Выход из консоли        |");
            Console.WriteLine("------------------------------");
            Console.Write("Действие: ");
            int check = Search_for_Bugs.ProverkaVvoda();
            switch (check)
            {
                case 9:
                    Main();
                    break;
                case 0:
                    Environment.Exit(0);  //Выход из консоли
                    break;
                default:
                    Text_Dialog.PrintErrorMenu();
                    break;
            }
        }
        static void FirstTask(ref int stringSize, ref int columnSize, ref char[,] dvumernii_array)
        {
            Console.Clear();
            dvumernii_array = null;
            do
            {
                Console.WriteLine("\n------------------------------");
                Console.WriteLine("| Выберите вид массива:      |");
                Console.WriteLine("| 1) Рандомный массив        |");
                Console.WriteLine("| 2) Массив с вводом         |");
                Console.WriteLine("| 9) Вернуться в начало      |");
                Console.WriteLine("| 0) Выход из консоли        |");
                Console.WriteLine("------------------------------");
                Console.Write("Действие: ");
                int check = Search_for_Bugs.ProverkaVvoda();
                switch (check)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("----------Формирование массива----------------");
                        Console.Write("Ввдеите количество строк: ");
                        stringSize = Search_for_Bugs.ProverkaVvoda();
                        if (stringSize > 0)
                        {
                            RanomDvumerniiArray(ref stringSize, ref columnSize, ref dvumernii_array);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("----------Формирование массива----------------");
                        Console.Write("Ввдеите количество строк: ");
                        stringSize = Search_for_Bugs.ProverkaVvoda();
                        if (stringSize > 0)
                        {
                            VvodDvumerniiArray(ref stringSize, ref columnSize, ref dvumernii_array);
                        }
                        else
                        {
                            RestartMenu();
                        }
                        break;
                    case 9:
                        Main();
                        break;
                    case 0:
                        Environment.Exit(0);  //Выход из консоли
                        break;
                    default:
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
            } while (!true);
        }
        static void RanomDvumerniiArray(ref int stringSize, ref int columnSize, ref char[,] dvumernii_array)
        {
            Console.Write("\nВведите количество столбцов: ");
            columnSize = Search_for_Bugs.ProverkaVvoda();
            dvumernii_array = new char[stringSize, columnSize];
            Random rand = new Random();
            for (int i = 0; i < stringSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    dvumernii_array[i, j] = GetRandomChar();
                }
            }
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < dvumernii_array.GetLength(0); i++)
            {
                for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                {
                    Console.Write("{0}\t", dvumernii_array[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n-----------------------------------------");
            Dictionary<int, char[]> tempDict = new Dictionary<int, char[]>();
            char[] tempArr = new char[columnSize];
            for (int i = 0; i < stringSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    tempArr[j] = dvumernii_array[i, j];
                }
                if (!tempArr.All(char.IsLetter))
                {
                    tempDict.Add(i, tempArr);
                    tempArr = new char[columnSize];
                }
            }

            if (tempDict.Count > 0)
            {
                Console.WriteLine("---------------Новый массив--------------\n");
                if (dvumernii_array.GetLength(0) == tempArr.Length)
                {
                    Console.WriteLine(" Нет строк без цифр ");
                }
                else
                {
                    foreach (var item in tempDict)
                    {
                        foreach (var ar in item.Value)
                        {
                            Console.Write("{0}\t", ar);
                        }
                        Console.WriteLine();
                    }
                }
                do
                {
                    Console.WriteLine("\n------------------------------");
                    Console.WriteLine("| Выберите вид массива:      |");
                    Console.WriteLine("| 1) Повторить удаление      |");
                    Console.WriteLine("| 9) Вернуться в начало      |");
                    Console.WriteLine("| 0) Выход из консоли        |");
                    Console.WriteLine("------------------------------");
                    Console.Write("Действие: ");
                    int check = Search_for_Bugs.ProverkaVvoda();
                    switch (check)
                    {
                        case 1:
                            Console.WriteLine(" ");
                            Console.WriteLine("Больше нет строк без цифр");
                            Console.WriteLine(" ");
                            break;
                        case 9:
                            Main();
                            break;
                        case 0:
                            Environment.Exit(0);  //Выход из консоли
                            break;
                        default:
                            Text_Dialog.PrintErrorMenu();
                            continue;
                    }
                } while (!true);
            }
            else
            {
                Console.WriteLine("Массив пуст");
            }
        }
        static void VvodDvumerniiArray(ref int stringSize, ref int columnSize, ref char[,] dvumernii_array)
        {
            Console.Write("\nВведите количество столбцов: ");
            columnSize = Search_for_Bugs.ProverkaVvoda();
            dvumernii_array = new char[stringSize, columnSize];
            Random rand = new Random();
            for (int i = 0; i < stringSize; i++)
            {
                Console.WriteLine("Вводдля строки №" + i);
                for (int j = 0; j < columnSize; j++)
                {
                    dvumernii_array[i, j] = Search_for_Bugs.Trying();
                }
            }
            Dictionary<int, char[]> tempDict = new Dictionary<int, char[]>();
            char[] tempArr = new char[columnSize];
            for (int i = 0; i < stringSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    tempArr[j] = dvumernii_array[i, j];
                }
                if (!tempArr.All(char.IsLetter))
                {
                    tempDict.Add(i, tempArr);
                    tempArr = new char[columnSize];
                }
            }
            Console.WriteLine("------------------Массив-----------------\n");
            for (int i = 0; i < dvumernii_array.GetLength(0); i++)
            {
                for (int j = 0; j < dvumernii_array.GetLength(1); j++)
                {
                    Console.Write("{0}\t", dvumernii_array[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n-----------------------------------------");
            if (tempDict.Count > 0)
            {
                Console.WriteLine("---------------Новый массив--------------\n");
                if (dvumernii_array.GetLength(0) == tempArr.Length)
                {
                    Console.WriteLine(" Нет строк без цифр ");
                }
                else
                {
                    foreach (var item in tempDict)
                    {
                        foreach (var ar in item.Value)
                        {
                            Console.Write("{0}\t", ar);
                        }
                        Console.WriteLine();
                    }
                }
                do
                {
                    Console.WriteLine("\n------------------------------");
                    Console.WriteLine("| Выберите вид массива:      |");
                    Console.WriteLine("| 1) Повторить удаление      |");
                    Console.WriteLine("| 9) Вернуться в начало      |");
                    Console.WriteLine("| 0) Выход из консоли        |");
                    Console.WriteLine("------------------------------");
                    Console.Write("Действие: ");
                    int check = Search_for_Bugs.ProverkaVvoda();
                    switch (check)
                    {
                        case 1:
                            Console.WriteLine(" ");
                            Console.WriteLine("Больше нет строк без цифр");
                            Console.WriteLine(" ");
                            break;
                        case 9:
                            Main();
                            break;
                        case 0:
                            Environment.Exit(0);  //Выход из консоли
                            break;
                        default:
                            Text_Dialog.PrintErrorMenu();
                            continue;
                    }
                } while (!true);
            }
            else
            {
                Console.WriteLine("Массив пуст");
            }
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------
        //---Второе задание------------------------------------------------------------------------------------------
        #region        
        static string SecondTask()
        {
            int pos = 0;                          // Позиция начала слова
            string changedString = string.Empty;  // Возвращаемая строка
            string tempWord;                      // Массив символов
            string tempSentence;                  // Массив предложений
            string endSentence;                   // Знак, заканчивающий предложение

            Console.WriteLine(" ");
            Console.Write("Введите строку: ");
            string str = Console.ReadLine();
            str = str.ToLower();
            str = str.Replace(",", "");
            str = " " + str;
            if ((str[str.Length - 1] != '.') && (str[str.Length - 1] != '!') && (str[str.Length - 1] != '?'))
            {
                str = str + ".";
            }
            if (str.Length != 0)
            {
                StringBuilder newString = new StringBuilder(str);
                Regex regexWord = new Regex(@"\w+[!|.|?|,]{0}");          // Выражение для выборки слова
                Regex regexSentence = new Regex(@"[\w|\s]*[!|.|?]");      // Выражение для выборки предложения
                Match word = regexWord.Match(str);                        // Выбираем только слова из строки

                while (word.Success)
                {
                    pos = word.Index;                         // Запоминаем позицию слова в строке 
                    tempWord = new string(word.ToString().Reverse().ToArray());
                    // Заменяем слово в строке на новое
                    newString.Remove(pos, tempWord.Length);   // Удаляем исходное слово из строки
                    newString.Insert(pos, tempWord);          // Вставляем новое слово
                    word = word.NextMatch();                  // Ищем следующее слово в строке
                }

                str = newString.ToString();
                Match sentence = regexSentence.Match(str);    // Выбираем только предложения из строки
                while (sentence.Success)
                {
                    pos = sentence.Index;
                    tempSentence = new string(sentence.ToString().ToArray());          // Запоминаем позицию предложения в строке
                    endSentence = tempSentence.Substring(tempSentence.Length - 1, 1);  // Запоминаем последний символ предложения
                    tempSentence = tempSentence.Substring(0, tempSentence.Length - 1); // Обрезаем последний символ предложения
                    tempSentence = string.Join(" ", tempSentence.Split(' ', ',', ';', ';').OrderByDescending(x => x.Length));
                    // Сортируем слова в предложении по убыванию длины слова
                    newString.Remove(pos, tempSentence.Length + 1);                    // Удаляем исходное предложение из строки
                    newString.Insert(pos, tempSentence);                               // Вставляем новое предложение
                    newString.Insert(pos + tempSentence.Length, endSentence);          // Вставляем последний символ предложения
                    sentence = sentence.NextMatch();                                   // Ищем следующее предложение в строке
                }

                str = newString.ToString();
                str = str.Replace(" .", ". ");
                str = str.Replace(" !", "! ");
                str = str.Replace(" ?", "? ");

                Console.WriteLine("-------------Новая строка-------------");
                Console.WriteLine(str);
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                Console.WriteLine("Похоже что вы ничего не ввели");
                SecondTask();
            }
            return changedString;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------
        static void Main()
        {
            Console.Clear(); //Очищение консоли
            int stringSize = 0,
                columnSize = 0;
            char[,] dvumernii_array = null;
            do
            {
                Console.WriteLine("\n-----------------------------------------");
                Console.WriteLine("| Выберите следующее действие:          |");
                Console.WriteLine("| 1) 1 задание                          |");
                Console.WriteLine("| 2) 2 задание                          |");
                Console.WriteLine("| 0) Выход                              |");
                Console.WriteLine("-----------------------------------------");
                Console.Write("Действие: ");
                int elementmenu = Search_for_Bugs.ProverkaVvoda();
                switch (elementmenu) //Выбор действия из меню
                {
                    case 1:
                        FirstTask(ref stringSize, ref columnSize, ref dvumernii_array);
                        break;
                    case 2:
                        Console.Clear();
                        SecondTask();
                        break;
                    case 0:
                        Environment.Exit(0);  //Выход из консоли
                        break;
                    default:
                        Text_Dialog.PrintErrorMenu();
                        continue;
                }
            } while (true);
        }
    }
}