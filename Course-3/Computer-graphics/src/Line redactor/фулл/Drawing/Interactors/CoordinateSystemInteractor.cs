using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Drawing.Interactors
{
    public abstract class CoordinateSystemInteractor
    {
        protected double[] vectorOffset;
        protected double[] vectorCenter;
        private int scale = 1;

        public int Scale { get => scale; set => scale = value; }

        public abstract void SetOffsetVector(double[] offsetVector);

        public abstract double[] GetPoint(Point mousePoint);

        public abstract void SetScale(int scale);

        public abstract List<UIElement> CreateAxes(double width, double heigth, double stepAxis);

        public abstract double[] ToNormalCoordinates(Point point);
    }
}
