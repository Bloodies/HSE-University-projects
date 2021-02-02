using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Data
{
    class MatrixData
    {
        private double angle;
        private double zc;

        public double Sin
        {
            get
            {
                return MakeSin(angle);
            }
            set { angle = value; }
        }

        public double Cos
        {
            get
            {
                return MakeCos(angle);
            }
            set { angle = value; }
        }

        public double Zc
        {
            get { return zc; }
            set { zc = value; }
        }

        public MatrixData(double angle, double zc)
        {
            this.angle = angle;
            this.zc = zc;
        }

        private double MakeSin(double angle)
        {
            var rad = angle * Math.PI / 180;
            return Math.Sin(rad);
        }

        private double MakeCos(double angle)
        {
            var rad = angle * Math.PI / 180;
            return Math.Cos(rad);
        }
    }
}
