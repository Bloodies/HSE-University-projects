using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._14
{
    class Monarchy : State
    {
        protected int population;
        public int Population
        {
            set { population = value; }
            get { return population; }
        }
        public Monarchy()
            : base()
        {
            population = 0;
        }
        public Monarchy(string countryname, string[] City3, int population)
            : base(countryname, City3)
        {
            this.population = population;
        }
        public override string ToString()
        {
            return base.ToString() + ", популяция: " + Population;
        }
        public override void Show()
        {
            Console.WriteLine(this.ToString());
        }
        public object ShallowCopy() //поверхностное копирование
        {
            return (Monarchy)this.MemberwiseClone();
        }
        public object Clone()
        {
            return new Monarchy("Клон" + this.countryname, this.city, this.population);
        }
    }
}