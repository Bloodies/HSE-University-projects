using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._14
{
    class State : ICloneable
    {
        protected string countryname;
        public string[] city = null;
        public int counter1 = 0;

        public string CountryName
        {
            set { countryname = value; }
            get { return countryname; }
        }
        public State()
        {
            string[] city = null;
            countryname = "";
        }
        public State(string countryname, string[] city)
        {
            this.city = city;
            this.countryname = countryname;
        }
        public override string ToString()
        {
            return " страна: " + CountryName;
        }
        public virtual void Show()
        {
            Console.WriteLine(this.ToString());
        }
        public object ShallowCopy() //поверхностное копирование
        {
            return (State)this.MemberwiseClone();
        }
        public object Clone()
        {
            return new State("Клон" + this.countryname, this.city);
        }
        public virtual string ToCity(string[] cityB)
        {
            string s = string.Join(", ", cityB);
            return " города: " + s;
        }
        public virtual void ShowCity(string[] cityB)
        {
            Console.WriteLine(this.ToCity(cityB));
        }
    }
}