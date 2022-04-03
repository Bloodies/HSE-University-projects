using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._3
{
    class Program
    {
        static void Main()
        {
            int n = 15;
            double a = 0.1, b = 1, k = 10;
            double periodx = 0.09;
            double e = 0.0001;
            double x = 0.1;

            Console.WriteLine("0,1 <= x <= 1   n = 15");
            Console.WriteLine("   ");
            while (x <= 0.99)
            {
                double sn, res, se;
                sn = Math.Pow(-1, 1) * (Math.Pow((2 * x), 2)) / 2;
                res = 1;
                se = Math.Pow(-1, 1) * (Math.Pow((2 * x), 2)) / 2;
                double y = 2 * (((Math.Pow(Math.Cos(x), 2)) - 1));
                for (int period1 = 2; period1 <= n; period1++)
                {
                    int fac = 1;
                    for (int i = 1; i <= period1 * 2; i++)
                    {
                        fac *= i;
                    }
                    res = ((Math.Pow(-1, period1)) * (Math.Pow((2 * x), (2 * period1)))) / (fac);
                    sn += res;
                }
                int period2 = 2;
                res = se;
                se = 0;
                while (Math.Abs(res) >= e)
                {
                    se += res;
                    var fac = 1;
                    for (int i = 1; i <= period2 * 2; ++i)
                    {
                        fac *= i;
                    }
                    res = (double)((Math.Pow(-1, period2)) * (Math.Pow((2 * x), (2 * period2)))) / (double)(fac);
                    period2++;
                }
                Console.WriteLine($"X={x}  SN={(double)sn}  SE={(double)se}  Y={y}");
                x += periodx;
            }
            Console.ReadKey();
        }
    }
}