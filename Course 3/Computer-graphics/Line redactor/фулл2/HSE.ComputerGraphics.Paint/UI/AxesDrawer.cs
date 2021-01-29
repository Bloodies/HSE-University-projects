using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HSE.ComputerGraphics.Paint.UI
{
    public static class AxesDrawer
    {
        private static readonly double margin = 10;
        private static readonly int step = 50;
        private static Path XAxis;
        private static Path YAxis;
        private static List<Label> labels = new List<Label>();

        // http://csharphelper.com/blog/2014/09/draw-graph-wpf-c/
        public static void DrawAxes(Canvas canvas)
        {
            double xmax = canvas.ActualWidth - margin;
            double ymin = margin;
            double xmin = margin;
            double ymax = canvas.ActualHeight - margin;

            // Make the X axis.
            GeometryGroup xAxis_geom = new GeometryGroup();
            xAxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymax), new Point(canvas.ActualWidth, ymax)));
            for (double x = xmin + step; x <= xmax; x += step)
            {
                xAxis_geom.Children.Add(new LineGeometry(
                    new Point(x - margin, ymax - margin / 2),
                    new Point(x - margin, ymax + margin / 2)));
                //Draw current tick label
                DrawText(canvas, (x - margin).ToString(), new Point(x - margin, ymax - margin * 3), 10, HorizontalAlignment.Center, VerticalAlignment.Top);
            }

            XAxis = new Path();
            XAxis.StrokeThickness = 1;
            XAxis.Stroke = Brushes.Black;
            XAxis.Data = xAxis_geom;

            canvas.Children.Add(XAxis);

            // Make the Y axis.
            GeometryGroup yAxis_geom = new GeometryGroup();
            yAxis_geom.Children.Add(new LineGeometry(
                new Point(xmin, 0), new Point(xmin, canvas.ActualHeight)));

            List<Point> labelTickPositions = new List<Point>(); //
            for (double y = step; y <= ymax; y += step)
            {
                yAxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin - margin / 2, y),
                    new Point(xmin + margin / 2, y)));
                // костыль
                labelTickPositions.Add(new Point(xmin + margin / 2, y));
            }

            List<double> tickYPositions = labelTickPositions.Select(x => x.Y).Reverse().ToList();
            for (int i = 0; i < labelTickPositions.Count; i++)
                DrawText(canvas, tickYPositions[i].ToString(), labelTickPositions[i], 10, HorizontalAlignment.Left, VerticalAlignment.Center);

            YAxis = new Path();
            YAxis.StrokeThickness = 1;
            YAxis.Stroke = Brushes.Black;
            YAxis.Data = yAxis_geom;

            canvas.Children.Add(YAxis);
        }

        // http://csharphelper.com/blog/2014/09/draw-a-graph-with-labels-wpf-c/
        // Position a label at the indicated point.
        private static void DrawText(Canvas canvas, string text, Point location,
            double fontSize,
            HorizontalAlignment hAlign, VerticalAlignment vAlign)
        {
            // Make the label.
            Label label = new Label();
            label.Content = text;
            label.FontSize = fontSize;
            canvas.Children.Add(label);

            // Position the label.
            label.Measure(new Size(double.MaxValue, double.MaxValue));

            double x = location.X;
            if (hAlign == HorizontalAlignment.Center)
                x -= label.DesiredSize.Width / 2;
            else if (hAlign == HorizontalAlignment.Right)
                x -= label.DesiredSize.Width;
            Canvas.SetLeft(label, x);

            double y = location.Y;
            if (vAlign == VerticalAlignment.Center)
                y -= label.DesiredSize.Height / 2;
            else if (vAlign == VerticalAlignment.Bottom)
                y -= label.DesiredSize.Height;
            Canvas.SetTop(label, y);

            labels.Add(label);
        }

        public static void RemoveAxes(Canvas canvas)
        {
            if (XAxis != null)
            {
                canvas.Children.Remove(XAxis);
                canvas.Children.Remove(YAxis);

                foreach (Label label in labels)
                    canvas.Children.Remove(label);
                labels.Clear();
            }
        }
    }
}
