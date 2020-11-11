using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._14
{
    class MyDictionary : IEnumerable, IEnumerator
    {
        public int capacity;
        public IDictionary<string, State> Country3 = new Dictionary<string, State>();
        public bool tf = true;

        public object Current { get; }
        public MyDictionary()
        {
            this.capacity = 4;
        }
        public MyDictionary(int capacity)
        {
            this.capacity = capacity;
        }
        public MyDictionary(MyDictionary c)
        {
            this.capacity = c.capacity;
            this.Country3 = c.Country3;
        }
        public IDictionary<string, State> Add1(string a, State b)
        {
            if (Country3.Count == capacity) { Console.WriteLine("Превышение размера!"); tf = false; return Country3; }
            tf = true;
            try
            {
                Country3.Add(a, b);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Элемент с таким ключом уже добавлен");
                tf = false;
            }
            return Country3;
        }
        public int Count()
        {
            return Country3.Count;
        }
        public int Keys()
        {
            int a = 0;
            foreach (KeyValuePair<string, State> s in Country3)
            {
                if (s.Key != null) { a++; }
            }
            return a;
        }
        public int Values()
        {
            int a = 0;
            foreach (KeyValuePair<string, State> s in Country3)
            {
                if (s.Value != null) { a++; }
            }
            return a;
        }
        public bool ContainsKey(string key)
        {
            bool pr = false;
            foreach (KeyValuePair<string, State> s in Country3)
            {
                if (s.Key == key) { pr = true; }
            }
            return pr;
        }
        public MyDictionary Clear()
        {
            Country3 = null;
            capacity = 0;
            return this;
        }
        public MyDictionary Clone()
        {
            return new MyDictionary(this);
        }
        public void Show()
        {
            if (Country3 != null)
            {
                foreach (KeyValuePair<string, State> s in Country3)
                {
                    Console.WriteLine("Key = {0}", s.Key);
                    s.Value.Show();
                }
            }
            else
            {
                Console.WriteLine("Коллекция пуста");
            }
        }
        public IDictionary<string, State> Remove(object a)
        {
            foreach (KeyValuePair<string, State> s in Country3)
            {
                if (a == s.Value)
                {
                    Country3.Remove(s.Key);
                    return Country3;
                }
            }
            return Country3;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}