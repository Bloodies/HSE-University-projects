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

namespace Lab._5__User_controls_._4__Clocks
{
    /// <summary>
    /// Логика взаимодействия для ClockControl.xaml
    /// </summary>
    public partial class ClockControl : UserControl
    {
        public ClockControl()
        {
            InitializeComponent();
            PaintTick();
            TimeSpan = DateTime.Now;
        }
        private TimeSpan time;
        public DateTime TimeSpan
        {
            set
            {
                time = new TimeSpan(value.Hour, value.Minute, value.Second);
                TransformHands();
            }
        }

        private void PaintTick()
        {
            double x1, y1, x2, y2, rad;
            int r1 = 124;
            int r2 = 115;
            int r3 = 110;
            for (int i = 0; i < 360; i += 6)
            {
                rad = i * (Math.PI / 180);
                x1 = 125 + r1 * Math.Cos(rad);
                y1 = 125 + r1 * Math.Sin(rad);
                Line line = new Line();
                if (!(i % 30 == 0))
                {
                    x2 = 125 + r2 * Math.Cos(rad);
                    y2 = 125 + r2 * Math.Sin(rad);
                    line.StrokeThickness = 1;
                }
                else
                {
                    x2 = 125 + r3 * Math.Cos(rad);
                    y2 = 125 + r3 * Math.Sin(rad);
                    line.StrokeThickness = 2;
                }
                line.X1 = x1;
                line.Y1 = y1;
                line.X2 = x2;
                line.Y2 = y2;
                line.Stroke = ellipse1.Stroke;
                grid.Children.Add(line);
            }
        }
        private void TransformHands()
        {
            ClockControl.hourLine.RenderTransform = new RotateTransform(30 * (time.TotalHours % 12));
            minLine.RenderTransform = new RotateTransform(6 * (time.TotalMinutes % 60));
            secLine.RenderTransform = new RotateTransform(6 * (time.TotalSeconds % 60));
        }
    }
}
