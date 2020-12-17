using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HSE.ComputerGraphics.Paint.UI
{
    [Serializable]
    public class MyLine : ICanvasObject
    {
        [NonSerialized]
        public Line Line;
        public LineGroup Group { get; set; }
        public bool IsSelected { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public MyLine()
        {

        }

        public void SetLineValues()
        {
            Line = new Line
            {
                X1 = X1,
                Y1 = Y1,
                X2 = X2,
                Y2 = Y2,
                Stroke = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = 2,
                Cursor = Cursors.SizeAll
            };

        }

        public MyLine(Line line)
        {
            Line = line;
            X1 = line.X1;
            Y1 = line.Y1;
            X2 = line.X2;
            Y2 = line.Y2;
        }

        public override int GetHashCode()
        {
            return this.Line.GetHashCode();
        }

        public void Move(Vector delta)
        {
            Line.X1 -= delta.X;
            Line.X2 -= delta.X;
            Line.Y1 -= delta.Y;
            Line.Y2 -= delta.Y;
        }

        public void Select()
        {
            Line.Stroke = Brushes.Red;
            IsSelected = true;
        }

        public void Deselect()
        {
            Line.Stroke = Brushes.Black;
            IsSelected = false;
        }

        public List<Line> GetLines()
        {
            return new List<Line> { Line };
        }
    }
}
