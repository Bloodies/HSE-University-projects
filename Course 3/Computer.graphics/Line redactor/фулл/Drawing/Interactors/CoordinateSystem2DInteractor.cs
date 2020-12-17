using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drawing.Interactors
{
    public class CoordinateSystem2DInteractor : CoordinateSystemInteractor
    {
        public CoordinateSystem2DInteractor(double width, double heigth)
        {
            vectorCenter = new double[2] { width / 2, heigth / 2 };
        }

        public override List<UIElement> CreateAxes(double width, double heigth, double step = 10)
        {
            List<UIElement> lines = new List<UIElement>();
            
            var centerWidth = vectorCenter[0];
            var centerHeigth = vectorCenter[1];
            var startPointX = centerWidth % step;
            var x = -centerWidth + startPointX;
            var startPointY = centerHeigth % step;
            var y = centerHeigth - startPointY;

            for (var i = startPointX; i < width; i += step)
            {
                var mark = CreateLine(i, centerHeigth - 5, i, centerHeigth);

                var label = CreateLabel(i, centerHeigth, x);
                lines.Add(label);

                x += step;
                lines.Add(mark);
            }

            for (var i = startPointY; i < heigth; i += step)
            {
                var mark = CreateLine(centerWidth - 5, i, centerWidth + 5, i);

                var label = CreateLabel(centerWidth, i, y);
                lines.Add(label);

                y -= step;
                lines.Add(mark);
            }

            var lineX = CreateLine(centerWidth, 0, centerWidth, heigth);
            lines.Add(lineX);

            var lineY = CreateLine(0, centerHeigth, width, centerHeigth);
            lines.Add(lineY);

            return lines;
        }

        private Line CreateLine(double x1, double y1, double x2, double y2)
        {
            return new Line()
            {
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                Stroke = Brushes.Gray,
                Focusable = false,
                IsEnabled = false,
                Name = "Axis"
            };
        }

        private Label CreateLabel(double left, double top, double point)
        {
            return new Label()
            {
                Name = "Axis",
                Content = point,
                Foreground = Brushes.Gray,
                IsEnabled = false,
                Focusable = false,
                Margin = new Thickness(left + 6, top + 6, 0, 0)
            };
        }

        public override double[] GetPoint(Point point)
        {
            //double x = Math.Round(point.X - vectorCenter[0]);
            //double y = Math.Round(vectorCenter[1] - point.Y);
            double x = point.X - vectorCenter[0];
            double y = vectorCenter[1] - point.Y;
            return new double[2] { x, y };
        }

        public override void SetOffsetVector(double[] value)
        {
            vectorOffset = (value ?? vectorOffset);
            var offsetX = vectorOffset[0];
            var offsetY = vectorOffset[1];
            vectorCenter = new double[2] { offsetX, offsetY };
        }

        public override void SetScale(int scale)
        {
            this.Scale = scale;
        }

        public override double[] ToNormalCoordinates(Point point)
        {
            var x = point.X + vectorCenter[0];
            var y = vectorCenter[1] - point.Y;
            return new double[2] { x, y };
        }
    }
}
