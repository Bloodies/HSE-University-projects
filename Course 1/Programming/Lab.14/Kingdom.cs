using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._14
{
    class Kingdom : Monarchy
    {
        protected string king;
        public string King
        {
            set { king = value; }
            get { return king; }
        }
        public Kingdom() : base()
        {
            king = "";
        }
        public Kingdom(string countryname, string[] City4, string king, int population)
            : base(countryname, City4, population)
        {
            this.king = king;
        }
        public override string ToString()
        {
            return base.ToString() + ", король: " + King;
        }
        public override void Show()
        {
            Console.WriteLine(this.ToString());
        }
    }
}