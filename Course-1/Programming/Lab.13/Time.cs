using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._13
{
    public class Time
    {
        int minutes;
        int hours;
        public static int count;

        public int Hours
        {
            get { return hours; }
            set
            {
                if (value >= 0) hours = value;
                else
                {
                    Console.WriteLine("часы не могут быть отрицательными!");
                    hours = 0;
                }
            }
        }

        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value < 0)
                {
                    minutes = 0;
                    Console.WriteLine("минуты не могут быть отрицательными!");
                }
                else
                if (value < 60) minutes = value;
                else
                {
                    hours += value / 60;
                    minutes = value % 60;
                }
            }
        }

        public Time()
        {
            Hours = 0;
            Minutes = 0;
            count++;
        }

        public Time(int h, int m)
        {
            Hours = h;
            Minutes = m;
            count++;
        }

        public override string ToString()
        {
            if (Hours > 9)
            {
                if (Minutes > 9)
                {
                    return Hours.ToString() + ":" + Minutes;
                }
                else
                {
                    return Hours.ToString() + ":0" + Minutes;
                }
            }
            else
            {
                if (Minutes > 9)
                {
                    return "0" + Hours.ToString() + ":" + Minutes;
                }
                else
                {
                    return "0" + Hours.ToString() + ":0" + Minutes;
                }
            }
        }

        public void Minus(int minutes)
        {
            Minutes -= minutes;
        }

        public static Time Minus(Time t, int minutes)
        {
            t.Minutes -= minutes;
            return t;
        }

        public override bool Equals(object obj)
        {
            Time t = (Time)obj;
            return Minutes == t.Minutes && Hours == t.Hours;
        }

        public static Time operator --(Time t)
        {
            if (t.Minutes == 0)
            {
                if (t.Hours != 0) { t.Hours -= 1; t.Minutes += 59; }
                else { Console.WriteLine("Нельзя вычесть!"); }
            }
            else { t.Minutes -= 1; }
            return t;
        }

        public static Time operator -(Time t)
        {
            t.Hours = 0;
            t.Minutes = 0;
            return t;
        }

        public static bool operator ==(Time t1, Time t2)
        {
            return t1.Equals(t2);
        }

        public static bool operator !=(Time t1, Time t2)
        {
            bool proverka = t1.Equals(t2);
            if (proverka == true) { proverka = false; }
            else { proverka = true; }
            return proverka;
        }

        public void Show()
        {
            if (Hours > 9)
            {
                if (Minutes > 9)
                {
                    Console.WriteLine(Hours.ToString() + ":" + Minutes.ToString());
                }
                else
                {
                    Console.WriteLine(Hours.ToString() + ":0" + Minutes.ToString());
                }
            }
            else
            {
                if (Minutes > 9)
                {
                    Console.WriteLine("0" + Hours.ToString() + ":" + Minutes.ToString());
                }
                else
                {
                    Console.WriteLine("0" + Hours.ToString() + ":0" + Minutes.ToString());
                }
            }
        }

        public static implicit operator int(Time t)
        {
            return t.Hours * 60 + t.Minutes;
        }

        public static explicit operator double(Time t)
        {
            return t.Hours;
        }
    }
}