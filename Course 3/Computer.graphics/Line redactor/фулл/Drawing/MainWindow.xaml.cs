using Drawing.Composite;
using Drawing.Interactors;
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
using Drawing.Data;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace Drawing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LineInteractor interactor = new LineInteractor();
        private CoordinateSystemInteractor coordinateSystem;
        private Line currentLine = new Line();
        private Composite.MainShape shape;
        private bool downed = false;
        private Point oldPoint;
        private double zc = 10;
        private int id = 0;
        private List<LineData> dataLine = new List<LineData>();
        private bool median = false;
        private bool height = false;
        private bool localCS = false;
        private bool enabledCS = false;
        private byte r = 100;
        private byte g = 50;
        private byte b = 150;
        private GroupInteractor group = new GroupInteractor();
        private int groupId = 0;

        public MainWindow()
        {
            InitializeComponent();
            coordinateSystem = new CoordinateSystem2DInteractor(canvas.Width, canvas.Height);
            canvas.Children.Add(currentLine);
            shape = new Composite.MainShape(currentLine, coordinateSystem);
            ActivateDoZ();
            MouseMove += dragElement_MouseMove;
            MouseDown += pickElement_MouseDown;
            MouseUp += dropElement_MouseUp;
            KeyDown += pickFewElements_KeyDown;
            KeyUp += upKey_KeyUp;
            PreviewMouseUp += upMouse;
            ChangeEnabling(false);
            ActivateMergeBecauseOfLines();
        }

        #region Create and Delete line Methods
        private void createLine_Click(object sender, RoutedEventArgs e)
        {
            var line = interactor.CreateRandomLine(canvas.Width, canvas.Height, Brushes.Black, 5, id);
            canvas.Children.Add(line);
            dataLine.Add(new LineData()
            {
                Id = id,
                X1 = line.X1,
                Y1 = line.Y1,
                X2 = line.X2,
                Y2 = line.Y2,
                Z1 = 0,
                Z2 = 0,
                StrokeThickness = line.StrokeThickness
            });
            ++id;

            ActivateMergeBecauseOfLines();
        }

        private void deleteLine_Click(object sender, RoutedEventArgs e)
        {
            shape.RemoveAll(dataLine);
            currentLine = new Line();
            canvas.Children.Add(currentLine);
            shape = new Composite.MainShape(currentLine, coordinateSystem);

            ActivateMergeBecauseOfLines();
            ActivateMorffingButton();
        }
        #endregion

        #region Pick, Up and Move
        private void upMouse(object sender, MouseButtonEventArgs e)
        {
            var cnt = shape.GetShapes().Count;
            if (cnt < 1)
            {
                ChangeEnabling(false);
            }
            else
            {
                ChangeEnabling(true);
                if (cnt < 2)
                {
                    CreateBiss.IsEnabled = false;
                }
            }
        }

        private void pickFewElements_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
                downed = true;
        }

        private void pickElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (median)
            {
                var line = shape.GetLastShape() as Line;
                var medianLine = interactor.CreateMedian(e.GetPosition(canvas), line, id);
                canvas.Children.Add(medianLine);
                dataLine.Add(new LineData
                {
                    Id = id,
                    X1 = medianLine.X1,
                    Y1 = medianLine.Y1,
                    Z1 = 0,
                    X2 = medianLine.X2,
                    Y2 = medianLine.Y2,
                    Z2 = 0,
                    StrokeThickness = medianLine.StrokeThickness
                });
                ++id;
                median = false;
            }
            else if (height)
            {
                var line = shape.GetLastShape() as Line;
                var heightLine = interactor.CreateHeight(e.GetPosition(canvas), line, id);
                canvas.Children.Add(heightLine);
                dataLine.Add(new LineData
                {
                    Id = id,
                    X1 = heightLine.X1,
                    Y1 = heightLine.Y1,
                    Z1 = 0,
                    X2 = heightLine.X2,
                    Y2 = heightLine.Y2,
                    Z2 = 0,
                    StrokeThickness = heightLine.StrokeThickness
                });
                ++id;
                height = false;
            }

            if (localCS)
            {
                var lines = canvas.Children.Cast<FrameworkElement>().Where(x => x.Name == "Axis").ToArray();
                ClearCoordinateSystem(lines);
                var point = e.GetPosition(canvas);
                coordinateSystem.SetOffsetVector(new double[] { point.X, point.Y });
                localCS = false;
                if (enabledCS == true)
                {
                    AddCoordinateSystem();
                }
            }

            if (e.Source is Shape)
            {
                if (!downed)
                {
                    if(!group.ContainsShape(shape))
                        shape.ClearColor();
                    currentLine = new Line();
                    canvas.Children.Add(currentLine);
                    shape = new Composite.MainShape(currentLine, coordinateSystem);
                }

                shape.PickAddShape(e.Source as Shape, dataLine);

                ShowCoordinates();
            }

            ActivateMergeBecauseOfLines();
            ActivateMorffingButton();
        }

        private void dragElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(canvas).X < canvas.Width)
            {
                if (enabledCS && localCS)
                {
                    var lines = canvas.Children.Cast<FrameworkElement>().Where(x => x.Name == "Axis").ToArray();
                    ClearCoordinateSystem(lines);
                    var point = e.GetPosition(canvas);
                    coordinateSystem.SetOffsetVector(new double[] { point.X, point.Y });
                    AddCoordinateSystem();
                }
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    shape.MoveShape(oldPoint, e.GetPosition(canvas));
                    var lines = shape.GetShapes().Select(x => x as Line);
                    foreach (var line in lines)
                    {
                        var data = dataLine.Where(x => x.Id == (int)line.Tag).First();
                        data.X1 = line.X1;
                        data.Y1 = line.Y1;
                        data.X2 = line.X2;
                        data.Y2 = line.Y2;
                    }
                }
                var curPos = coordinateSystem.GetPoint(e.GetPosition(canvas));
                mousePosition.Content = "x = [" + Math.Round(curPos[0], 0) + "] y = [" + curPos[1] + "]";
            }
            oldPoint = e.GetPosition(canvas);
        }

        private void dropElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Shape)
            {
                ShowEquation();

                ShowCoordinates();
            }
        }
        #endregion

        #region Coordinate System
        private void coordinateSystem_Button_Click(object sender, RoutedEventArgs e)
        {
            enabledCS = true ? enabledCS == false : false;
            var lines = canvas.Children.Cast<FrameworkElement>().Where(x => x.Name == "Axis").ToArray();
            if (!lines.Any())
            {
                AddCoordinateSystem();
            }
            else
            {
                ClearCoordinateSystem(lines);
            }
        }

        private void AddCoordinateSystem()
        {
            var createdLines = coordinateSystem.CreateAxes(canvas.Width, canvas.Height, 50);
            foreach (var l in createdLines)
            {
                canvas.Children.Add(l);
            }
        }

        private void ClearCoordinateSystem(FrameworkElement[] lines)
        {
            for (var i = 0; i < lines.Length; ++i)
            {
                canvas.Children.Remove(lines[i]);
            }
        }

        private void SetLocalCoordinate_Click(object sender, RoutedEventArgs e)
        {
            localCS = true;
        }

        private void BackOriginCS_Click(object sender, RoutedEventArgs e)
        {
            var lines = canvas.Children.Cast<FrameworkElement>().Where(x => x.Name == "Axis").ToArray();
            ClearCoordinateSystem(lines);
            coordinateSystem.SetOffsetVector(new double[] { canvas.Width / 2, canvas.Height / 2 });
            if (enabledCS)
            {
                AddCoordinateSystem();
            }
        }
        #endregion

        #region Morffing
        private void sliderMorffing_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var morfs = canvas.Children.Cast<FrameworkElement>().Where(x => x.Name == "Morffing").ToArray();
            if (morfs.Any())
            {
                for (var i = 0; i < morfs.Length; ++i)
                {
                    canvas.Children.Remove(morfs[i]);
                }
            }

            var start = shape.GetShapes().Select(x => x as Line).ToList();
            var end = canvas.Children.Cast<UIElement>()
                .Where(x => x is Line).Select(x => x as Line)
                .Where(x => x.Tag != null && x.Tag.ToString() != "Axis").Except(start).ToList();
            Morffing morffing = new Morffing(Math.Round(e.NewValue), start, end);
            var lines = morffing.MorffingLines();
            foreach (var l in lines)
            {
                canvas.Children.Add(l);
            }
        }

        private void MergeFigures_Click(object sender, RoutedEventArgs e)
        {
            var morfs = canvas.Children.Cast<FrameworkElement>().Where(x => x.Name == "Polyline").ToArray();
            if (morfs.Any())
            {
                for (var i = 0; i < morfs.Length; ++i)
                {
                    canvas.Children.Remove(morfs[i]);
                }
            }

            /*var figureA = shape.GetShapes().Select(x => x as Line).ToList();
            var figureB = canvas.Children.Cast<UIElement>()
                .Where(x => x is Line).Select(x => x as Line)
                .Where(x => x.Tag != null && x.Tag.ToString() != "Axis").Except(figureA).ToList();*/
            Morffing morffing = new Morffing();
            var numberPoints = int.Parse(MakePoints.Text);
            /*var A = int.Parse(ProportionA.Text);
            var B = int.Parse(ProportionB.Text);
            var linePoints = new List<Point[]>();
            var points = BreakLines(figureA, numberPoints).ToArray();
            linePoints.Add(points);
            points = BreakLines(figureB, numberPoints).ToArray();
            linePoints.Add(points);*/
            var figures = group.GetFigures().Select(x => x.Select(y => y as Line).ToList()).ToList();
            var proportions = new double[stackpanelProportions.Children.Count];
            var linePoints = new List<Point[]>();
            foreach(var f in figures)
            {
                linePoints.Add(BreakLines(f, numberPoints).ToArray());
            }
            for(var i = 0; i < proportions.Length; ++i)
            {
                proportions[i] = int.Parse((stackpanelProportions.Children[i] as TextBox).Text);
            }
            //var polyLine = morffing.MorffingShapes(linePoints, new double[] { A, B }, numberPoints);
            var polyLine = morffing.MorffingShapes(linePoints, proportions, numberPoints);
            canvas.Children.Add(polyLine);
        }

        private List<Point> BreakLines(List<Line> lines, int numberPoints)
        {
            var points = new List<Point>();
            var pointsPerLine = numberPoints / lines.Count;
            var additionalPoints = numberPoints % lines.Count;

            for (var i = 0; i < lines.Count; ++i)
            {
                var deltaX = Math.Abs(lines[i].X1 - lines[i].X2) / (pointsPerLine + additionalPoints + 1);
                var deltaY = Math.Abs(lines[i].Y1 - lines[i].Y2) / (pointsPerLine + additionalPoints + 1);
                var x = Math.Min(lines[i].X1, lines[i].X2);
                var y = Math.Min(lines[i].Y1, lines[i].Y2);

                for (var j = 0; j < pointsPerLine + additionalPoints; ++j)
                {
                    x += deltaX;
                    y += deltaY;

                    points.Add(new Point(x, y));
                }

                additionalPoints = 0;
            }

            return points;
        }

        private void ClearMorffing_Click(object sender, RoutedEventArgs e)
        {
            var lines = canvas.Children.Cast<FrameworkElement>().Where(x => x.Name == "Morffing").ToList();
            for (var i = 0; i < lines.Count; ++i)
            {
                canvas.Children.Remove(lines[i]);
            }
        }
        #endregion

        #region 3D Operations
        private void doZ_Click(object sender, RoutedEventArgs e)
        {
            var z1 = double.Parse(addZ1TextBlock.Text);
            var z2 = double.Parse(addZ2TextBlock.Text);
            shape.AddZ(new double[] { z1, z2 }, dataLine);

            ShowCoordinates();
        }

        private void phiSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var phi = Math.Round(e.NewValue - e.OldValue);

            var md = new MatrixData(phi, zc);
            var rotateMatrix = new double[,]
            {
                { 1, 0, 0, 0},
                { 0, md.Cos, md.Sin, 0 },
                { 0, -md.Sin, md.Cos, 0 },
                { 0, 0, 0, 1 }
            };

            Compute3DOperation(rotateMatrix, md.Zc);
        }

        private void thetaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var theta = Math.Round(e.NewValue - e.OldValue);

            var md = new MatrixData(theta, zc);
            var rotateMatrix = new double[,]
            {
                { md.Cos, 0, -md.Sin, 0},
                { 0, 1, 0, 0 },
                { md.Sin, 0,  md.Cos, 0 },
                { 0, 0, 0, 1 }
            };

            Compute3DOperation(rotateMatrix, md.Zc);
        }

        private void gammaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var gamma = Math.Round(e.NewValue - e.OldValue);

            var md = new MatrixData(gamma, zc);
            var rotateMatrix = new double[,]
            {
                { md.Cos, md.Sin, 0, 0},
                { -md.Sin, md.Cos, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            Compute3DOperation(rotateMatrix, md.Zc);
        }

        private void zcSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            zc = Math.Round(e.NewValue) * 10;
            MatrixData md = new MatrixData(0, zc);

            shape.ProjectReal3D(md.Zc);
        }

        private void xTransportSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var m = Math.Round(e.NewValue * 10 - e.OldValue * 10);

            var transportMatrix = new double[,]
            {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, 1, 0 },
                {m, 0, 0, 1 }
            };

            Compute3DOperation(transportMatrix, zc);
        }

        private void yTransportSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var n = Math.Round(e.NewValue * 10 - e.OldValue * 10);

            var transportMatrix = new double[,]
            {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, 1, 0 },
                {0, n, 0, 1 }
            };

            Compute3DOperation(transportMatrix, zc);
        }

        private void zTransportSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var k = Math.Round(e.NewValue * 10 - e.OldValue * 10);

            var transportMatrix = new double[,]
            {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, 1, 0 },
                {0, 0, k, 1 }
            };

            Compute3DOperation(transportMatrix, zc);
        }

        private void Compute3DOperation(double[,] operation, double zc)
        {
            shape.ComputeReal3D(operation);

            shape.ProjectReal3D(zc);

            ShowCoordinates();

            shape.SetDataLineCoordinates(dataLine);
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            var a = double.Parse(aScaleTextBox.Text);
            var d = double.Parse(dScaleTextBox.Text);
            var es = double.Parse(eScaleTextBox.Text);

            var scaleMatrix = new double[,]
            {
                {a, 0, 0, 0 },
                {0, d, 0, 0 },
                {0, 0, es, 0 },
                {0, 0, 0, 1 }
            };

            Compute3DOperation(scaleMatrix, zc);

            aScaleTextBox.Text = "1";
            dScaleTextBox.Text = "1";
            eScaleTextBox.Text = "1";
        }

        private void TransportButton_Click(object sender, RoutedEventArgs e)
        {
            var x = double.Parse(xTransportTextBox.Text);
            var y = double.Parse(yTransportTextBox.Text);
            var z = double.Parse(zTransportTextBox.Text);

            var scaleMatrix = new double[,]
            {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, 1, 0 },
                {x, y, z, 1 }
            };

            Compute3DOperation(scaleMatrix, zc);

            xTransportTextBox.Text = "0";
            yTransportTextBox.Text = "0";
            zTransportTextBox.Text = "0";
        }

        private void MirrorStartButton_Click(object sender, RoutedEventArgs e)
        {
            var mirrorMatrix = new double[,]
            {
                {-1, 0, 0, 0 },
                {0, -1, 0, 0 },
                {0, 0, -1, 0 },
                {0, 0, 0, 1 }
            };

            Compute3DOperation(mirrorMatrix, zc);
        }

        private void MirrorZButton_Click(object sender, RoutedEventArgs e)
        {
            var mirrorMatrix = new double[,]
            {
                {-1, 0, 0, 0 },
                {0, -1, 0, 0 },
                {0, 0, 1, 0 },
                {0, 0, 0, 1 }
            };

            Compute3DOperation(mirrorMatrix, zc);
        }

        private void MirrorX0Button_Click(object sender, RoutedEventArgs e)
        {
            var mirrorMatrix = new double[,]
            {
                {-1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, 1, 0 },
                {0, 0, 0, 1 }
            };

            Compute3DOperation(mirrorMatrix, zc);
        }
        #endregion

        #region Median, Height, Biss
        private void createMedian_Click(object sender, RoutedEventArgs e)
        {
            median = true;
            height = false;
        }

        private void CreateHeight_Click(object sender, RoutedEventArgs e)
        {
            height = true;
            median = false;
        }

        private void CreateBiss_Click(object sender, RoutedEventArgs e)
        {
            var lines = shape.GetShapes();
            if (lines.Count != 2)
                return;
            var biss = interactor.CreateBiss(lines[0] as Line, lines[1] as Line, id);
            canvas.Children.Add(biss);
            dataLine.Add(new LineData()
            {
                Id = id,
                X1 = biss.X1,
                Y1 = biss.Y1,
                X2 = biss.X2,
                Y2 = biss.Y2,
                Z1 = 0,
                Z2 = 0,
                StrokeThickness = biss.StrokeThickness
            });
            ++id;
        }
        #endregion

        #region Save and Load
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xml files (*.xml)|*.xml";

            if (sfd.ShowDialog() == true)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<LineData>));

                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    formatter.Serialize(fs, dataLine);
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml files (*.xml)|*.xml";

            if (ofd.ShowDialog() == true)
            {
                id = 0;
                canvas.Children.Clear();
                dataLine.Clear();
                currentLine = new Line();
                canvas.Children.Add(currentLine);
                shape = new MainShape(currentLine, coordinateSystem);

                XmlSerializer formatter = new XmlSerializer(typeof(List<LineData>));

                using (var fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    dataLine = formatter.Deserialize(fs) as List<LineData>;
                }
            }

            foreach (var d in dataLine)
            {
                var line = new Line()
                {
                    X1 = d.X1,
                    Y1 = d.Y1,
                    X2 = d.X2,
                    Y2 = d.Y2,
                    StrokeThickness = d.StrokeThickness,
                    Stroke = Brushes.Black,
                    Tag = id
                };
                canvas.Children.Add(line);
                id++;
            }
        }
        #endregion

        #region Show Info
        private void ShowEquation()
        {
            var equation = shape.GetEquation().Select(x => Math.Round(x)).ToArray();
            lineEquation.Content = "Ур-е: " + equation[0] + "x + " + equation[1] + "y - " + equation[2] + " = 0";
        }

        private void ShowCoordinates()
        {
            var endCoordinates = shape.GetCoordinates().Select(x => Math.Round(x)).ToArray();
            endsCoord.Content =
                "1 конец линии: (" + endCoordinates[0] + "x; " + endCoordinates[1] + "y; " + endCoordinates[4] + "z)" + "     ||     2 конец линии: (" + endCoordinates[2] + "x; " + endCoordinates[3] + "y; " + endCoordinates[5] + "z)";
        }
        #endregion

        #region Checking Morffing
        private void ActivateMorffingButton()
        {
            var lines = shape.GetShapes();
            var freeLines = canvas.Children.Cast<UIElement>()
                        .Where(x => x is Line).Select(x => x as Line)
                        .Where(x => x.Tag != null && x.Tag.ToString() != "Axis").Except(lines).ToList();

            if(lines.Count == freeLines.Count && lines.Count > 0)
            {
                sliderMorffing.IsEnabled = true;
            }
            else
            {
                sliderMorffing.IsEnabled = false;
            }
        }
        #endregion

        #region Checking MergeFigures
        private void KeyUpMakePoints(object sender, KeyEventArgs e)
        {
            ActivateMerge();
        }

        private void ProportionAKeyUp(object sender, KeyEventArgs e)
        {
            ActivateMerge();
        }

        private void ProportionBKeyUp(object sender, KeyEventArgs e)
        {
            ActivateMerge();
        }

        private void ActivateMerge()
        {
            var regex = @"(^0+|\D)";
            var makePointsMatch = Regex.IsMatch(MakePoints.Text, regex);
            var proportionAMatch = Regex.IsMatch(ProportionA.Text, regex);
            var proportionBMatch = Regex.IsMatch(ProportionB.Text, regex);
            if (makePointsMatch || proportionAMatch || proportionBMatch/* || textBox.Text == ""*/)
                MergeFigures.IsEnabled = false;
            else
                MergeFigures.IsEnabled = true;
        }

        private void ActivateMergeBecauseOfLines()
        {
            var lines = shape.GetShapes();
            var freeLines = canvas.Children.Cast<UIElement>()
                        .Where(x => x is Line).Select(x => x as Line)
                        .Where(x => x.Tag != null && x.Tag.ToString() != "Axis").Except(lines).ToList();

            if (lines.Count > 0 && freeLines.Count > 0)
            {
                MergeFigures.IsEnabled = true;
            }
            else
            {
                MergeFigures.IsEnabled = false;
            }
        }
        #endregion

        #region Checking 3D Figures
        private void KeyUpZ1(object sender, KeyEventArgs e)
        {
            ActivateDoZ();
        }

        private void ActivateDoZ()
        {
            try
            {
                var z1 = double.Parse(addZ1TextBlock.Text);
                var z2 = double.Parse(addZ2TextBlock.Text);
                doZ.IsEnabled = true;
            }
            catch
            {
                doZ.IsEnabled = false;
            }
        }

        private void KeyUpZ2(object sender, KeyEventArgs e)
        {
            ActivateDoZ();
        }

        private void Activate3DButtons(TextBox tbA, TextBox tbB, TextBox tbC, Button button)
        {
            try
            {
                double.Parse(tbA.Text);
                double.Parse(tbB.Text);
                double.Parse(tbC.Text);
                button.IsEnabled = true;
            }
            catch
            {
                button.IsEnabled = false;
            }
        }

        private void aScaleKeyUp(object sender, KeyEventArgs e)
        {
            Activate3DButtons((TextBox)sender, dScaleTextBox, eScaleTextBox, ScaleButton);
        }

        private void dScaleKeyUp(object sender, KeyEventArgs e)
        {
            Activate3DButtons((TextBox)sender, aScaleTextBox, eScaleTextBox, ScaleButton);
        }

        private void eScaleKeyUp(object sender, KeyEventArgs e)
        {
            Activate3DButtons((TextBox)sender, aScaleTextBox, dScaleTextBox, ScaleButton);
        }

        private void xTransportKeyUp(object sender, KeyEventArgs e)
        {
            Activate3DButtons((TextBox)sender, yTransportTextBox, zTransportTextBox, TransportButton);
        }

        private void yTransportKeyUp(object sender, KeyEventArgs e)
        {
            Activate3DButtons((TextBox)sender, xTransportTextBox, zTransportTextBox, TransportButton);
        }

        private void zTransportKeyUp(object sender, KeyEventArgs e)
        {
            Activate3DButtons((TextBox)sender, xTransportTextBox, yTransportTextBox, TransportButton);
        }
        #endregion

        #region Checking
        private void ChangeEnabling(bool enable)
        {
            phiSlider.IsEnabled = enable;
            thetaSlider.IsEnabled = enable;
            gammaSlider.IsEnabled = enable;
            doZ.IsEnabled = enable;
            ScaleButton.IsEnabled = enable;
            TransportButton.IsEnabled = enable;
            MirrorZButton.IsEnabled = enable;
            MirrorX0Button.IsEnabled = enable;
            MirrorStartButton.IsEnabled = enable;
            createMedian.IsEnabled = enable;
            createHeight.IsEnabled = enable;
            CreateBiss.IsEnabled = enable;
        }

        private void upKey_KeyUp(object sender, KeyEventArgs e)
        {
            downed = false;
        }

        #endregion

        private void addGroup_Click(object sender, RoutedEventArgs e)
        {
            if (group.AddGroup(shape, new SolidColorBrush(Color.FromRgb(r, g, b))))
            {
                r += 29;
                g -= 22;
                b += 57;
                TextBox textBox = new TextBox()
                {
                    Text = "1",
                    Width = 50,
                    Height = 20,
                    Tag = groupId,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                textBox.KeyUp += TextBox_KeyUp;
                groupId++;
                stackpanelProportions.Children.Add(textBox);
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var regex = @"(^0+|\D)";
            bool ok = false;
            foreach(var p in stackpanelProportions.Children)
            {
                ok = ok || Regex.IsMatch((p as TextBox).Text, regex);
            }

            if (ok)
                MergeFigures.IsEnabled = false;
            else
                MergeFigures.IsEnabled = true;
        }

        private void clearGroups_Click(object sender, RoutedEventArgs e)
        {
            group.ClearColors(shape);
            group = new GroupInteractor();
            stackpanelProportions.Children.RemoveRange(0, stackpanelProportions.Children.Count);
        }
    }
}
