using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._14
{
    class Republic : State
    {
        protected int population;
        public int Population
        {
            set { population = value; }
            get { return population; }
        }
        public Republic()
            : base()
        {
            population = 0;
        }
        public Republic(string countryname, string[] City2, int population)
            : base(countryname, City2)
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
    }
}