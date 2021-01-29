using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drawing
{
    class CustomLine1488 : Shape
    {
        LineGeometry line = new LineGeometry();

        private double x1;
        private double y1;
        private double x2;
        private double y2;

        public double X1
        {
            get { return x1; }
            set
            {
                //x1 = value;
                x1 += value - x1;
                //VisualTransform = new TranslateTransform(x1, 0);
                //line.Transform = new TranslateTransform(x1, 0);
            }
        }
        public double Y1
        {
            get { return y1; }
            set
            {
                //y1 = value;
                y1 += value - y1;
                //VisualTransform = new TranslateTransform(0, y1);
                //line.Transform = new TranslateTransform(0, y1);
            }
        }

        public double X2
        {
            get { return x2; }
            set
            {
                //x2 = value;
                x2 += value - x2;
                //VisualTransform = new TranslateTransform(x2, 0);
                //line.Transform = new TranslateTransform(x2, 0);
            }
        }
        public double Y2
        {
            get { return y2; }
            set
            {
                //y2 = value;
                y2 += value - y2;
                //VisualTransform = new TranslateTransform(0, y2);
                //line.Transform = new TranslateTransform(0, y2);
            }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                line = new LineGeometry(new System.Windows.Point(X1, Y1), 
                    new System.Windows.Point(X2, Y2));
                return line;
            }
        }
    }
}
