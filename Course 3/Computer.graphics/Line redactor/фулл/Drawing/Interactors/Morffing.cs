using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drawing.Interactors
{
    class Morffing
    {
        private double step = 1;
        private List<Line> start = new List<Line>();
        private List<Line> end = new List<Line>();

        public double Step
        {
            get
            {
                return step;
            }
            set { step = value; }
        }

        public Morffing(double step, List<Line> start, List<Line> end)
        {
            this.step = step;
            this.start = start;
            this.end = end;
        }

        public Morffing() { }

        public List<Line> MorffingLines()
        {
            List<Line> lines = new List<Line>();
            for(var i = 0; i < step; ++i)
            {
                for(var j = 0; j < start.Count; ++j)
                {
                    var t = i / step;
                    var x1 = start[j].X1 * (1 - t) + end[j].X1 * t;
                    var x2 = start[j].X2 * (1 - t) + end[j].X2 * t;
                    var y1 = start[j].Y1 * (1 - t) + end[j].Y1 * t;
                    var y2 = start[j].Y2 * (1 - t) + end[j].Y2 * t;
                    lines.Add(new Line()
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        Stroke = Brushes.Gray,
                        StrokeThickness = 3,
                        Name = "Morffing"
                    });
                }
            }
            return lines;
        }

        public Polyline MorffingShapes(List<Point[]> points, double[] proportions, int numberPoints)
        {
            var segment = new Polyline()
            {
                Stroke = Brushes.Gray,
                StrokeThickness = 5,
                Name = "Morffing"
            };
            var sumProportions = proportions.Sum();

            for(var i = 0; i < numberPoints; ++i)
            {
                var x = 0D;
                var y = 0D;
                for(var j = 0; j < points.Count; ++j)
                {
                    x += points[j][i].X * proportions[j];
                    y += points[j][i].Y * proportions[j];
                }

                x /= sumProportions;
                y /= sumProportions;
                segment.Points.Add(new Point(x, y));
            }
            segment.Points.Add(segment.Points[0]);
            return segment;
        }
    }
}
