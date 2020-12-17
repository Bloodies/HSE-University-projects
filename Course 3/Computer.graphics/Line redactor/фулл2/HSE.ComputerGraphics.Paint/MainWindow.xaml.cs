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
using HSE.ComputerGraphics.Paint.UI;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace HSE.ComputerGraphics.Paint
{
    public partial class MainWindow : Window
    {
        private Line lastClickedLine;
        private List<ICanvasObject> currentSelection = new List<ICanvasObject>();
        private List<LineGroup> currentGroupSelection = new List<LineGroup>();
        private List<LineGroup> lineGroups = new List<LineGroup>();
        private Dictionary<Line, ICanvasObject> lines = new Dictionary<Line, ICanvasObject>();
        private Point previousMousePosition;
        private bool isMousePressed;
        private bool medianMode;
        private bool heightMode;
        private Line firstMorphingLine;
        private Line secondMorphingLine;
        private Line morphingLine;
        private LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();

        public MainWindow()
        {
            InitializeComponent();

            myLinearGradientBrush.StartPoint = new Point(0, 0);
            myLinearGradientBrush.EndPoint = new Point(1, 1);
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Yellow, 0.0));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Red, 0.25));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.Blue, 0.75));
            myLinearGradientBrush.GradientStops.Add(
                new GradientStop(Colors.LimeGreen, 1.0));
        }

        private void btnAddLine_Click(object sender, RoutedEventArgs e)
        {
            Line newLine = GetRandomLine();
            MyLine line = new MyLine(newLine);
            lines.Add(newLine, line);

            MainCanvas.Children.Add(newLine);
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if (canvas == null)
                return;
            if (medianMode)
            {
                if (lastClickedLine != null)
                {
                    Line median = lastClickedLine.GetMedian(e.GetPosition(canvas));
                    MyLine line = new MyLine(median);
                    lines.Add(median, line);
                    MainCanvas.Children.Add(median);

                    medianMode = false;
                }

                return;
            }

            if (heightMode)
            {
                if (lastClickedLine != null)
                {
                    Line height = lastClickedLine.GetHeight(e.GetPosition(canvas));
                    MyLine line = new MyLine(height);
                    lines.Add(height, line);
                    MainCanvas.Children.Add(height);

                    heightMode = false;
                }

                return;
            }

            HitTestResult hitTestResult = VisualTreeHelper.HitTest(canvas, e.GetPosition(canvas));

            if (hitTestResult.VisualHit is Line selectedLine)
            {
                if (morphingLine != null && selectedLine == morphingLine)
                    return;
                ICanvasObject myLine = lines[selectedLine];

                if (Keyboard.IsKeyDown(Key.LeftCtrl) == false)
                {
                    foreach (var line in currentSelection)
                        line.Deselect();
                    currentSelection.Clear();
                    currentGroupSelection.Clear();
                }

                //Select new element
                if (currentSelection.Contains(myLine) == false)
                {
                    myLine.Select();
                    currentSelection.Add(myLine);
                    currentSelection.Last().Select();
                    lastClickedLine = selectedLine;
                    lbEquation.Text = $"Уравнение: {lastClickedLine.GetLineConstants()}";
                }
            }
            else if (hitTestResult.VisualHit is Canvas)
            {
                foreach (var line in currentSelection)
                    line.Deselect();
                currentSelection.Clear();
            }

            //Save mouse position
            previousMousePosition = e.GetPosition(canvas);
            isMousePressed = true;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if (canvas == null)
                return;

            Point currentMousePosition = e.GetPosition(canvas);

            Point cartesianPosition = ConvertToCartesianCoords(MainCanvas, currentMousePosition);
            lbMousePosition.Text = $"X: {Math.Round(cartesianPosition.X, 0)} Y:{Math.Round(cartesianPosition.Y, 0)}";

            if (currentSelection.Any())
            {
                ICanvasObject currentLine = lines[lastClickedLine];
                lbEquation.Text = $"Уравнение: {lastClickedLine.GetLineConstants()}";

                float radius = 10;
                bool isMouseNearBegin = Math.Pow(lastClickedLine.X1 - previousMousePosition.X, 2) + Math.Pow(lastClickedLine.Y1 - previousMousePosition.Y, 2) < Math.Pow(radius, 2);
                bool isMouseNearEnd = Math.Pow(lastClickedLine.X2 - previousMousePosition.X, 2) + Math.Pow(lastClickedLine.Y2 - previousMousePosition.Y, 2) < Math.Pow(radius, 2);

                if (isMouseNearBegin || isMouseNearEnd)
                    lastClickedLine.Cursor = Cursors.SizeNWSE;
                else
                    lastClickedLine.Cursor = Cursors.SizeAll;

                if (isMousePressed)
                {
                    if (isMouseNearBegin)
                    {
                        lastClickedLine.X1 = currentMousePosition.X;
                        lastClickedLine.Y1 = currentMousePosition.Y;
                    }
                    else if (isMouseNearEnd)
                    {
                        lastClickedLine.X2 = currentMousePosition.X;
                        lastClickedLine.Y2 = currentMousePosition.Y;
                    }
                    else
                    {
                        Vector delta = previousMousePosition - currentMousePosition;
                        currentLine.Move(delta);
                    }
                }
            }

            previousMousePosition = currentMousePosition;

        }

        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMousePressed = false;
        }

        private void btnRemoveLine_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelection.Any() == false)
                return;

            foreach (var line in currentSelection)
            {
                List<Line> linesToDelete = line.GetLines();
                foreach (var lineToDelete in linesToDelete)
                    MainCanvas.Children.Remove(lineToDelete);
            }

            currentSelection.Clear();
            lbEquation.Text = "";
        }

        private Line GetRandomLine()
        {
            Random rand = new Random();
            int randX1 = rand.Next(0, (int)Math.Round(MainCanvas.ActualWidth));
            int randX2 = rand.Next(0, (int)Math.Round(MainCanvas.ActualWidth));
            int randY1 = rand.Next(0, (int)Math.Round(MainCanvas.ActualHeight));
            int randY2 = rand.Next(0, (int)Math.Round(MainCanvas.ActualHeight));

            Line newLine = new Line
            {
                X1 = randX1,
                Y1 = randY1,
                X2 = randX2,
                Y2 = randY2,
                Stroke = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                StrokeThickness = 2,
                Cursor = Cursors.SizeAll
            };

            return newLine;
        }

        private void MainCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AxesDrawer.RemoveAxes(canvas: MainCanvas);
            AxesDrawer.DrawAxes(canvas: MainCanvas);
        }

        private Point ConvertToCartesianCoords(Canvas canvas, Point point)
        {
            Point output = new Point(point.X, canvas.ActualHeight - point.Y);
            return output;
        }

        private void btnLmao_Click(object sender, RoutedEventArgs e)
        {
            //MediaElement myMediaElement = new MediaElement();
            //myMediaElement.Source = new Uri("Resources\\ricardo.mp4", UriKind.Relative);
            //myMediaElement.IsMuted = false;

            //VisualBrush myVisualBrush = new VisualBrush();
            //myVisualBrush.Viewport = new Rect(0, 0, 0.5, 0.5);
            //myVisualBrush.TileMode = TileMode.Tile;
            //myVisualBrush.Visual = myMediaElement;

            //MainCanvas.Background = myVisualBrush;
        }

        private void btnGroup_Click(object sender, RoutedEventArgs e)
        {
            LineGroup newLineGroup = new LineGroup(currentSelection);
            foreach (var myLine in currentSelection)
            {
                foreach (var line in myLine.GetLines())
                {
                    lines[line] = newLineGroup;
                }
            }
            lineGroups.Add(newLineGroup);
            currentSelection.Clear();
            currentSelection.Add(lineGroups.Last());
        }

        private void btnUngroup_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelection.Count == 1)
            {
                if (currentSelection.First() is LineGroup group)
                {
                    lineGroups.Remove(group);
                    foreach (var lastState in group.GroupedObjects)
                    {
                        if (lastState is LineGroup gr)
                        {
                            lineGroups.Add(gr);
                            foreach (var line in gr.GetLines())
                            {
                                lines[line] = gr;
                            }
                        }
                        else if (lastState is MyLine line)
                        {
                            lines[line.Line] = line;
                        }
                    }
                }
            }
        }

        private void btnMedian_Click(object sender, RoutedEventArgs e)
        {
            medianMode = true;
        }

        private void btnHeight_Click(object sender, RoutedEventArgs e)
        {
            heightMode = true;
        }

        private void btnBisector_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelection.Count == 2 && currentSelection.First() is MyLine first && currentSelection.Last() is MyLine second)
            {
                Line bisect = LineExtension.GetBisector(first.Line, second.Line);
                MyLine line = new MyLine(bisect);
                lines.Add(bisect, line);
                MainCanvas.Children.Add(bisect);
            }
        }

        private void btnMorphingFirstLine_Click(object sender, RoutedEventArgs e)
        {
            firstMorphingLine = lastClickedLine;
            btnMorphingFirstLine.Background = Brushes.LawnGreen;

        }

        private void btnMorphingSecondLine_Click(object sender, RoutedEventArgs e)
        {
            secondMorphingLine = lastClickedLine;
            btnMorphingSecondLine.Background = Brushes.LawnGreen;
        }

        private void btnMorphingReset_Click(object sender, RoutedEventArgs e)
        {
            firstMorphingLine = null;
            secondMorphingLine = null;
            btnMorphingFirstLine.ClearValue(Button.BackgroundProperty);
            btnMorphingSecondLine.ClearValue(Button.BackgroundProperty);

            MainCanvas.Children.Remove(morphingLine);
            morphingLine = null;
        }

        private void sliderMorphing_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (firstMorphingLine != null && secondMorphingLine != null && morphingLine != null)
            {
                morphingLine.X1 = MorphingValue(firstMorphingLine.X1, secondMorphingLine.X1, sliderMorphing.Value);
                morphingLine.Y1 = MorphingValue(firstMorphingLine.Y1, secondMorphingLine.Y1, sliderMorphing.Value);
                morphingLine.X2 = MorphingValue(firstMorphingLine.X2, secondMorphingLine.X2, sliderMorphing.Value);
                morphingLine.Y2 = MorphingValue(firstMorphingLine.Y2, secondMorphingLine.Y2, sliderMorphing.Value);
            }
        }

        private void btnMorphingBegin_Click(object sender, RoutedEventArgs e)
        {
            if (firstMorphingLine != null && secondMorphingLine != null)
            {
                morphingLine = new Line
                {
                    X1 = firstMorphingLine.X1,
                    Y1 = firstMorphingLine.Y1,
                    X2 = firstMorphingLine.X2,
                    Y2 = firstMorphingLine.Y2,
                    Stroke = myLinearGradientBrush,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    StrokeThickness = 2
                };

                MainCanvas.Children.Add(morphingLine);
            }
        }

        private double MorphingValue(double begin, double end, double t)
        {
            return begin * (1 - t) + end * t;
        }

        private void menuLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "bin files (*.bin)|*.bin" };
            if (ofd.ShowDialog() == true)
            {
                LoadScene(ofd.FileName);
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "bin files (*.bin)|*.bin" };
            if (sfd.ShowDialog() == true)
            {
                SaveScene(sfd.FileName);
            }
        }

        private void SaveScene(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                List<ICanvasObject> linesWithoutGroups = lines.Values.Where(x => x is MyLine).ToList();
                binaryFormatter.Serialize(fs, linesWithoutGroups);
                binaryFormatter.Serialize(fs, lineGroups);
                MessageBox.Show("Save completed!");
            }
        }

        private void LoadScene(string filename)
        {
            lines.Clear();
            lineGroups.Clear();
            MainCanvas.Children.Clear();
            currentSelection.Clear();
            lastClickedLine = null;

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                List<ICanvasObject> linesWithoutGroups = (List<ICanvasObject>)binaryFormatter.Deserialize(fs);
                lineGroups = (List<LineGroup>)binaryFormatter.Deserialize(fs);

                foreach (var line in linesWithoutGroups)
                {
                    MyLine myLine = line as MyLine;
                    myLine.SetLineValues();
                    MainCanvas.Children.Add(myLine.Line);
                    lines.Add(myLine.Line, myLine);
                }

                foreach (var group in lineGroups)
                {
                    LineGroup myGroup = group as LineGroup;
                    foreach (var line in myGroup.Lines)
                    {
                        line.SetLineValues();
                        MainCanvas.Children.Add(line.Line);
                        lines.Add(line.Line, myGroup);
                    }
                }

                MessageBox.Show("Load completed!");
            }
        }
    }
}
