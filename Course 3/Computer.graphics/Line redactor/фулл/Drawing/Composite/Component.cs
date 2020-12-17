using Drawing.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Drawing.Composite
{
    abstract class Component
    {
        protected Shape shape;
        protected LineInteractor interactor = new LineInteractor();
        protected CoordinateSystemInteractor coordinate;
            
        public Component(Shape shape, CoordinateSystemInteractor coordinate)
        {
            this.shape = shape;
            this.coordinate = coordinate;
        }

        public abstract Shape Display();
        public abstract void Add(Component c);
        public abstract void Remove();
        public abstract void ClearColor();
        public abstract void MoveShape(Point oldPos, Point newPos);
        public abstract bool Contains(Shape shape);
        public abstract double[] GetEquation();
        public abstract double[] GetCoordinates();
    }
}
