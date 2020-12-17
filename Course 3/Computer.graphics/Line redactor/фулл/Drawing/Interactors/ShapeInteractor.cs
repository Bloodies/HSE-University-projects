using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Drawing.Interactors
{
    abstract class ShapeInteractor
    {
        public abstract void DeleteShape(Shape shape);

        public abstract Shape MoveShape(double x, double y, Shape shape);

        public abstract Shape PickShape(Shape shape);

        public abstract double[] GetEquation(Shape shape, CoordinateSystemInteractor coordinate);
    }
}
