using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._14
{
    class Program
    {
        public static void GetData1(List<State> input, string country) // Выборка данных с использованием LINQ-запросов
        {
            var subset = from c in input
                         where c.CountryName == country
                         orderby c
                         select c;
            foreach (State s in subset)
            {
                for (int i = 0; i < s.city.Length; i++)
                {
                    Console.WriteLine(s.city[i]);
                }
            }
        }

        public static void GetData2(List<State> input, string country) // Выборка данных с использованием методов расширения
        {
            var subset = input.Where(c => c.CountryName == country).OrderBy(c => c).Select(c => c);
            foreach (State s in subset)
            {
                for (int i = 0; i < s.city.Length; i++)
                {
                    Console.WriteLine(s.city[i]);
                }
            }
        }

        public static void Count1(List<State> input) // Получение счетчика с использованием LINQ-запросов
        {
            int numb = (from c in input where (c is Kingdom == true) select c).Count<State>();
            Console.WriteLine("Число королевств - " + numb);

        }

        public static void Count2(List<State> input) // Получение счетчика с использованием методов расширения
        {
            int numb = input.Where(c => (c is Kingdom == true)).OrderBy(c => c).Select(c => c).Count<State>();
            Console.WriteLine("Число королевств - " + numb);
        }

        public static void Intersection1(List<State> input1, List<State> input2) // Поиск пересечения множеств с использованием LINQ-запросов
        {
            var Inter = (from c in input1 select c).Intersect(from c2 in input2 select c2);
            Console.WriteLine("Пересечение множеств:");
            foreach (var c in Inter)
                Console.WriteLine(c);
        }

        public static void Intersection2(List<State> input1, List<State> input2) // Поиск пересечения множеств с использованием методов расширения
        {
            var Inter = (input1.Select(c => c)).Intersect(input2.Select(c => c));
            Console.WriteLine("Пересечение множеств:");
            foreach (var c in Inter)
                Console.WriteLine(c);
        }

        public static void Aggregate1(List<State> input) // Агрегирование данных с использованием LINQ-запросов
        {
            Console.WriteLine("Наибольшее число городов = {0}", (from t in input select t.city.Length).Max());
            Console.WriteLine("Наименьшее число городов = {0}", (from t in input select t.city.Length).Min());

        }

        public static void Aggregate2(List<State> input) // Агрегирование данных с использованием методов расширения
        {
            Console.WriteLine("Наибольшее число городов = {0}", (input.Select(c => c.city.Length)).Max());
            Console.WriteLine("Наименьшее число городов = {0}", (input.Select(c => c.city.Length)).Min());
        }

        static void Main(string[] args)
        {
            int j = 0;
            string a, b;
            int g;
            MyDictionary C3 = new MyDictionary(6);
            string[] City1 = new string[4] { "Perm", "Moscow", "Kaliningrad", "Vladivostok" };
            string[] City2 = new string[2] { "Kiev", "Lugansk" };
            string[] City3 = new string[1] { "Tokyo" };
            string[] City4 = new string[3] { "London", "Oxford", "Kambridge" };
            string[] City5 = new string[2] { "Pattya", "Bankok" };

            List<State> Country = new List<State>();
            a = "Russia"; State h3 = new State(a, City1); Country.Add(h3);
            a = "Ukraine"; g = 55; Republic h1 = new Republic(a, City2, g); Country.Add(h1);
            a = "Japan"; g = 66; Monarchy h2 = new Monarchy(a, City3, g); Country.Add(h2);
            a = "United Kingdom"; g = 33; b = "Elizabeth II"; Kingdom h4 = new Kingdom(a, City4, b, g); Country.Add(h4);

            List<State> Country2 = new List<State>();
            Country2.Add(h3);
            Country2.Add(h4);


            Console.WriteLine("Запрос на выборку данных (все города России):");
            Console.WriteLine("");

            GetData1(Country, "Russia");
            Console.WriteLine("");
            GetData2(Country, "Russia");

            Console.WriteLine("");
            Console.WriteLine("Запрос на получение счетчика (число королевств)");
            Console.WriteLine("");

            Count1(Country);
            Console.WriteLine("");
            Count2(Country);

            Console.WriteLine("");
            Console.WriteLine("Запрос на пересечение множеств");
            Console.WriteLine("");

            Intersection1(Country, Country2);
            Console.WriteLine("");
            Intersection2(Country, Country2);

            Console.WriteLine("");
            Console.WriteLine("Запрос на агрегирование данных (страны с наибольшим и наименьшим числом городов):");
            Console.WriteLine("");

            Aggregate1(Country);
            Console.WriteLine("");
            Aggregate2(Country);
        }
    }
}