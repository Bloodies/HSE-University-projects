using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HSE.ComputerGraphics.Paint.UI
{
    public struct LineConstants
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public LineConstants(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public override string ToString()
        {
            return $"{A:#;-#}x{B:+0;-#}y{C:+0;-#}=0";
        }
    }
    public static class LineExtension
    {
        public static LineConstants GetLineConstants(this Line line)
        {
            double A = line.Y2 - line.Y1;
            double B = -(line.X2 - line.X1);
            double C = A * (-line.X1) + B * (-line.Y1);
            return new LineConstants(A, B, C);
        }

        public static Line GetMedian(this Line line, Point begin)
        {
            Point middle = new Point((line.X1 + line.X2) / 2, (line.Y1 + line.Y2) / 2);

            Line median = new Line
            {
                X1 = begin.X,
                Y1 = begin.Y,
                X2 = middle.X,
                Y2 = middle.Y,
                Stroke = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = 2,
                Cursor = Cursors.SizeAll
            };

            return median;
        }

        public static Line GetHeight(this Line line, Point begin)
        {
            //https://stackoverflow.com/questions/36483776/line-and-perpendicular-segment-from-a-isolated-point-get-all-coordinates
            Vector p1 = new Vector(line.X1, line.Y1);
            Vector p2 = new Vector(line.X2, line.Y2);
            Vector p0 = new Vector(begin.X, begin.Y);

            Vector s = p2 - p1;
            Vector v = p0 - p1;

            double len = Vector.Multiply(v, s) / Vector.Multiply(s, s);
            Vector projection = len * s;

            Point pointIntersection = new Point(line.X1 + projection.X, line.Y1 + projection.Y);
            return new Line
            {
                X1 = begin.X,
                Y1 = begin.Y,
                X2 = pointIntersection.X,
                Y2 = pointIntersection.Y,
                Stroke = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = 2,
                Cursor = Cursors.SizeAll
            };
        }

        public static Line GetBisector(Line line1, Line line2)
        {
            Line tempLine = line2;
            tempLine.X2 = line1.X1 - line2.X1;
            tempLine.Y2 = line1.Y1 - line2.Y1;
            tempLine.X1 = line1.X1;
            tempLine.Y1 = line1.Y1;

            Vector vec1 = line1.GetVector();
            Vector vec2 = tempLine.GetVector();
            vec1.Normalize();
            vec2.Normalize();
            Vector sum = Vector.Add(vec1, vec2);
            sum.Normalize();

            int length = 10;
            Vector endVec = Vector.Multiply(sum, length);

            Point end = new Point(endVec.X, endVec.Y);

            MessageBox.Show($"{Vector.AngleBetween(vec1, sum)}, {Vector.AngleBetween(vec2, sum)} {Vector.AngleBetween(vec1, vec2)}");

            return new Line
            {
                X1 = line1.X1,
                Y1 = line1.Y1,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = 2,
                Cursor = Cursors.SizeAll
            };
        }

        public static Vector GetVector(this Line line)
        {
            return new Vector(line.X2 - line.X1, line.Y2 - line.Y1);
        }
    }
}
