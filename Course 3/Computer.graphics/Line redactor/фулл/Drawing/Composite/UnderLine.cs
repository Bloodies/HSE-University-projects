using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Drawing.Interactors;

namespace Drawing.Composite
{
    class UnderLine : Component
    {
        private double[,] realMatrix = new double[2, 4];
        private double[,] visibleMatrix = new double[2, 2];

        public void SetRealMatrix(double[,] matrix)
        {
            realMatrix = matrix;
        }

        public double[,] GetRealMatrix()
        {
            return realMatrix;
        }

        public void SetVisibleMatrix(double[,] matrix)
        {
            visibleMatrix = matrix;
        }

        public double[,] GetVisibleMatrix()
        {
            return visibleMatrix;
        }

        public void SetZ(double[] z)
        {
            realMatrix[0, 2] = z[0];
            realMatrix[1, 2] = z[1];
        }

        public double[] GetZ()
        {
            var z1 = realMatrix[0, 2];
            var z2 = realMatrix[1, 2];
            return new double[] { z1, z2 };
        }

        public UnderLine(Line line, CoordinateSystemInteractor coordinate) : base(line, coordinate)
        {
            var point1 = new Point(line.X1, line.Y1);
            var point2 = new Point(line.X2, line.Y2);

            var realMatrix = new double[,]
            {
                { point1.X, point1.Y, 0, 1 },
                { point2.X, point2.Y, 0, 1 }
            };
            SetRealMatrix(realMatrix);

            /*var visiblePoint1 = coordinate.GetPoint(point1);
            var visiblePoint2 = coordinate.GetPoint(point2);

            var visibleMatrix = new double[,]
            {
                { visiblePoint1[0], visiblePoint1[1], 0, 1 },
                { visiblePoint2[0], visiblePoint2[1], 0, 1 }
            };*/

            var visibleMatrix = new double[,]
            {
                { point1.X, point1.Y },
                { point2.X, point2.Y }
            };
            SetVisibleMatrix(visibleMatrix);
        }

        public override void Add(Component c)
        {
            throw new NotImplementedException();
        }

        public override void ClearColor()
        {
            shape.Stroke = Brushes.Black;
        }

        public override bool Contains(Shape shape)
        {
            return shape == this.shape;
        }

        public override Shape Display()
        {
            return shape;
        }

        public override double[] GetEquation()
        {
            return interactor.GetEquation(shape, coordinate);
        }

        public override void MoveShape(Point oldPos, Point newPos)
        {
            var point = newPos;
            var r = 20f;
            //var centerFirstEnd = new Point((shape as Line).X1, (shape as Line).Y1);
            var centerFirstEnd = new Point(realMatrix[0, 0], realMatrix[0, 1]);
            //var centerSecondEnd = new Point((shape as Line).X2, (shape as Line).Y2);
            var centerSecondEnd = new Point(realMatrix[1, 0], realMatrix[1, 1]);
            var checkHittingFirstEnd = interactor.CheckHittingPoint(point, centerFirstEnd, r);
            var checkHittingSecondEnd = interactor.CheckHittingPoint(point, centerSecondEnd, r);
            if (checkHittingFirstEnd)
            {
                interactor.MoveFirstPoint(shape as Line, point, oldPos);
            }
            else if (checkHittingSecondEnd)
            {
                interactor.MoveSecondPoint(shape as Line, point, oldPos);
            }
            else
            {
                var deltaX = point.X - oldPos.X;
                var deltaY = point.Y - oldPos.Y;
                shape = interactor.MoveShape(deltaX, deltaY, shape) as Line;
            }

            var x1 = (shape as Line).X1;
            var y1 = (shape as Line).Y1;

            var x2 = (shape as Line).X2;
            var y2 = (shape as Line).Y2;

            SetRealMatrix(MakeMatrix(x1, y1, x2, y2));

            var visiblePoint1 = coordinate.GetPoint(new Point(x1, y1));
            var visiblePoint2 = coordinate.GetPoint(new Point(x2, y2));

            SetVisibleMatrix(new double[,]
            {
                { visiblePoint1[0], visiblePoint1[1] },
                { visiblePoint2[0], visiblePoint2[1] }
            });
        }

        public override double[] GetCoordinates()
        {
            //var movedShape = shape;
            var first = new Point(realMatrix[0, 0], realMatrix[0, 1]);
            var second = new Point(realMatrix[1, 0], realMatrix[1, 1]);
            var firstEnd = coordinate.GetPoint(first).ToList();
            var secondEnd = coordinate.GetPoint(second).ToList();
            //var firstEnd = new List<double>(new double[] { visibleMatrix[0, 0], visibleMatrix[0, 1] });
            //var secondEnd = new List<double>(new double[] { visibleMatrix[1, 0], visibleMatrix[1, 1] });
            var z1 = realMatrix[0, 2];
            var z2 = realMatrix[1, 2];
            firstEnd.AddRange(secondEnd);
            firstEnd.AddRange(new double[] { z1, z2 });
            return firstEnd.ToArray();
        }

        public override void Remove()
        {
            interactor.DeleteShape(shape);
        }

        private double[,] MakeMatrix(double x1, double y1, double x2, double y2)
        {
            return new double[,]
            {
                { x1, y1, 0, 1 },
                { x2, y2, 0, 1 }
            };
        }
    }
}
