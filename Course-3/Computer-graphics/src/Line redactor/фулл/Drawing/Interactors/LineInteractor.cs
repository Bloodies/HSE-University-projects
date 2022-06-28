using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MathNet.Numerics.LinearAlgebra;

namespace Drawing.Interactors
{
    class LineInteractor : ShapeInteractor
    {
        private Random rnd = new Random();

        public Line CreateRandomLine(double width, double height, SolidColorBrush brush, int thickness, int id)
        {
            var firstPoint = GetPoint(width, height);
            var secondPoint = GetPoint(width, height);
            return new Line()
            {
                X1 = firstPoint.X,
                Y1 = firstPoint.Y,
                X2 = secondPoint.X,
                Y2 = secondPoint.Y,
                Stroke = brush,
                StrokeThickness = thickness,
                Tag = id
            };
        }

        public Line CreateMedian(Point point, Line line, int id)
        {
            var x1 = (line.X1 + line.X2) / 2;
            var y1 = (line.Y1 + line.Y2) / 2;
            return new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = point.X,
                Y2 = point.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 5,
                Tag = id
            };
        }

        public Line CreateHeight(Point point, Line line, int id)
        {
            var A1 = line.Y1 - line.Y2;
            var B1 = line.X2 - line.X1;
            var C1 = line.X1 * line.Y2 - line.X2 * line.Y1;

            var A2 = -B1;
            var B2 = A1;
            var C2 = B1 * point.X - A1 * point.Y;

            var x = (B1 * C2 - B2 * C1) / (A1 * B2 - A2 * B1);
            var y = (A2 * C1 - A1 * C2) / (A1 * B2 - A2 * B1);

            return new Line()
            {
                X1 = x,
                Y1 = y,
                X2 = point.X,
                Y2 = point.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 5,
                Tag = id
            };
        }

        public Line CreateBiss(Line line1, Line line2, int id)
        {
            var A1 = line1.Y1 - line1.Y2;
            var B1 = line1.X2 - line1.X1;
            var C1 = line1.X1 * line1.Y2 - line1.X2 * line1.Y1;

            var A2 = line2.Y1 - line2.Y2;
            var B2 = line2.X2 - line2.X1;
            var C2 = line2.X1 * line2.Y2 - line2.X2 * line2.Y1;

            var AB1 = Math.Sqrt(A1 * A1 + B1 * B1);
            var AB2 = Math.Sqrt(A2 * A2 + B2 * B2);

            var A = A1 * AB2 - A2 * AB1;
            var B = B1 * AB2 - B2 * AB1;
            var C = C1 * AB2 - C2 * AB1;

            var x1 = (B1 * C2 - B2 * C1) / (A1 * B2 - A2 * B1);
            var y1 = (A2 * C1 - A1 * C2) / (A1 * B2 - A2 * B1);

            var x2 = 500;
            var y2 = (-A * x2 - C) / B;

            return new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 5,
                Tag = id
            };
        }

        public override void DeleteShape(Shape line)
        {
            ((Canvas)line.Parent).Children.Remove(line);
        }

        public override Shape MoveShape(double x, double y, Shape line)
        {
            var changedLine = line as Line;
            double[,] matrixData = {
                { changedLine.X1, changedLine.Y1, 1 }, 
                { changedLine.X2, changedLine.Y2, 1 } 
            };
            double[,] matrixOp = { 
                { 1, 0, 0 }, 
                { 0, 1, 0 }, 
                { x, y, 1 } 
            };

            var D = Matrix<double>.Build.DenseOfArray(matrixData);
            var O = Matrix<double>.Build.DenseOfArray(matrixOp);

            var DS = D.Multiply(O);
            changedLine.X1 = DS[0, 0];
            changedLine.Y1 = DS[0, 1];
            changedLine.X2 = DS[1, 0];
            changedLine.Y2 = DS[1, 1];
            return changedLine;
        }

        public void MoveFirstPoint(Line line, Point point, Point oldPoint)
        {
            line.X1 += point.X - oldPoint.X;
            line.Y1 += point.Y - oldPoint.Y;
        }

        public void MoveSecondPoint(Line line, Point point, Point oldPoint)
        {
            line.X2 += point.X - oldPoint.X;
            line.Y2 += point.Y - oldPoint.Y;
        }

        public override Shape PickShape(Shape line)
        {
            line.Stroke = Brushes.Red;
            return line;
        }

        public bool CheckHittingPoint(Point point, Point center, double r)
        {
            double x = point.X - center.X;
            double y = point.Y - center.Y;
            return r * r >= x * x + y * y;
        }

        private Point GetPoint(double width, double height)
        {
            var x = rnd.NextDouble() * width;
            var y = rnd.NextDouble() * height;
            return new Point(x, y);
        }

        public override double[] GetEquation(Shape shape, CoordinateSystemInteractor coordinate)
        {
            var line = shape as Line;
            Point startPoint = new Point(line.X1, line.Y1);
            Point endPoint = new Point(line.X2, line.Y2);
            var firstPoint = coordinate.GetPoint(startPoint);
            var lastPoint = coordinate.GetPoint(endPoint);
            double A = firstPoint[1] - lastPoint[1];
            double B = lastPoint[0] - firstPoint[0];
            double C = firstPoint[0] * lastPoint[1] - lastPoint[0] * firstPoint[1];
            return new double[] { A, B, C };
        }
    }
}
