using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._13
{
    class MyCollection : ICollection<Time>
    {
        public string name;
        public List<Time> arr = new List<Time>();

        public MyCollection(string Name)
        {
            name = Name;
        }

        public bool IsReadOnly => false;

        int ICollection<Time>.Count => arr.Count;

        public virtual void Add(Time item) => arr.Add(item);

        public virtual void Clear() => arr.Clear();

        public bool Contains(Time item) => arr.Contains(item);

        public void CopyTo(Time[] array, int arrayIndex) => arr.CopyTo(array, arrayIndex);

        public IEnumerator<Time> GetEnumerator() => arr.GetEnumerator();

        public virtual bool Remove(Time item) => arr.Remove(item);

        public virtual bool RemoveByNumber(int num)
        {
            if ((num >= 0) && (num < this.Count()))
            {
                Remove(arr[num]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual void Fill(int num)
        {
            Random rnd = new Random();
            arr.Clear();
            for (int i = 0; i < num; i++)
            {
                Time item = new Time(rnd.Next(23), rnd.Next(59));
                arr.Add(item);
            }
        }
        public virtual void SortHours()
        {
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int j = i; j < arr.Count(); j++)
                {
                    if (arr[i].Hours > arr[j].Hours)
                    {
                        Time temp = arr[j];
                        arr[j] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
        }
        public virtual void SortMinutes()
        {
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int j = i; j < arr.Count(); j++)
                {
                    if (arr[i].Minutes > arr[j].Minutes)
                    {
                        Time temp = arr[j];
                        arr[j] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
        }
        public virtual void Display()
        {
            for (int i = 0; i < arr.Count(); i++)
            {
                Console.WriteLine(arr[i].ToString());
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => arr.GetEnumerator();
    }
}