using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DLLibs.Enums;
using DLLibs.Shapes;
using DLLibs.Shapes.Vector;
using DLLibs.Shapes.Dots;
using DLLibs.Files;
using System.Linq;

namespace VectorRedactor.UI
{
    public partial class MainWindowForm : Form
    {
        private enum LoggedActions
        {
            None = 0,
            AddVector = 1,
            RemoveVector = 2,
        }
        private enum SpecialAction
        {
            None = 0,
            Median = 1,
            Height = 2,
            Bisector = 4,
        }
        private int Counter { get; set; }
        private Pen WorkPen { get; } = new Pen(Color.Black, 5);
        private Pen SelectedPen { get; } = new Pen(Color.Red, 5);
        private Pen EraserPen { get; } = new Pen(Color.Snow, 5);
        private Pen GroupPen { get; } = new Pen(Color.Blue, 5);
        private Pen HouseWorkPen { get; } = new Pen(Color.Black, 2);
        private Pen HouseEraserPen { get; } = new Pen(Color.Snow, 2);
        private Pen MorphingPen { get; } = new Pen(Color.LightGreen, 2);

        private IDot Middle;
        private SpecialAction specialAction = SpecialAction.None;
        private Graphics Graphics { get; set; }
        private Random Random { get; set; } = new Random();
        private List<IShape> Shapes { get; set; } = new List<IShape>();
        private Dictionary<IShape, VectorActions> Groupped { get; set; } = new Dictionary<IShape, VectorActions>();
        private bool IsMousePressed { get; set; }
        private Point OldPoint { get; set; }

        private IShape[] morphingList { get; set; } = new IShape[2];
        private IShape morphingFigure { get; set; }

        #region Private Functions

        private void DrawCoordinateLines()
        {
            var pen = ShowCoordinateChb.Checked ? Pens.LightGray : Pens.White;

            Graphics.DrawLine(pen, 10, Global.UI_Data.CONST_Y_SIZE_O, Canvas.Width - 10, Global.UI_Data.CONST_Y_SIZE_O);
            Graphics.DrawLine(pen, Global.UI_Data.CONST_X_SIZE_O, 10, Global.UI_Data.CONST_X_SIZE_O, Canvas.Height - 10);

            for (int i = 10; i <= Canvas.Width - 10; i += 10)
            {
                Graphics.DrawLine(pen, i, Global.UI_Data.CONST_Y_SIZE_O + 3, i, Global.UI_Data.CONST_Y_SIZE_O - 3);

                if (i >= 100 && i % 100 == 0)
                {
                    Graphics.DrawLine(pen, i, Global.UI_Data.CONST_Y_SIZE_O + 5, i, Global.UI_Data.CONST_Y_SIZE_O - 5);
                }
            }

            for (int i = Canvas.Height - 10; i >= 10; i -= 10)
            {
                Graphics.DrawLine(pen, Global.UI_Data.CONST_X_SIZE_O - 3, i, Global.UI_Data.CONST_X_SIZE_O + 3, i);

                if (i >= 100 && i % 100 == 0)
                {
                    Graphics.DrawLine(pen, Global.UI_Data.CONST_X_SIZE_O - 5, i, Global.UI_Data.CONST_X_SIZE_O + 5, i);
                }
            }
        }

        private IDot GetRandomDot2D()
        {
            return new Dot2D(
              x: Random.Next(10, Canvas.Image.Width - 10) - Global.UI_Data.CONST_X_SIZE_O,
              y: Global.UI_Data.CONST_Y_SIZE_O - Random.Next(10, Canvas.Image.Height - 10),
              uc: 1
            );
        }

        private IDot GetRandomDot3D()
        {
            return new Dot3D(
              x: Random.Next(10, Canvas.Image.Width - 10) - Global.UI_Data.CONST_X_SIZE_O,
              y: Global.UI_Data.CONST_Y_SIZE_O - Random.Next(10, Canvas.Image.Height - 10),
              z: Random.Next(-200, 200),
              unifCoordinate: 1
            );
        }

        private void UpdateGeneralInfo(Point point, LoggedActions action)
        {
            var vector2d = Groupped.Keys.LastOrDefault() as Vector2D;
            var vector3d = Groupped.Keys.LastOrDefault() as Vector3D;

            if (action == LoggedActions.AddVector)
            {
                CountOfVectorsLbl.Text = $"{(int.Parse(CountOfVectorsLbl.Text) + 1)}";
            }
            else if (action == LoggedActions.RemoveVector)
            {
                int count = 0;
                foreach (var i in Shapes)
                {
                    count++;
                }
                CountOfVectorsLbl.Text = $"{count}";
            }

            CursorCoordinateLbl.Text = point.IsEmpty ? "N/A" : $"({point.X - Global.UI_Data.CONST_X_SIZE_O}, {Global.UI_Data.CONST_Y_SIZE_O - point.Y})";
            VectorCoordinateLbl.Text =
                vector2d == null && vector3d == null
                    ? "N/A"
                    : vector2d == null
                        ? $"{{{vector3d.StartPoint.ToString()}, {vector3d.EndPoint.ToString()}}}"
                        : $"{{{vector2d.StartPoint.ToString()}, {vector2d.EndPoint.ToString()}}}";

            EquationOfVectorLbl.Text =
                vector2d == null && vector3d == null
                    ? "N/A"
                    : vector2d == null
                        ? $"{vector3d.ToString()}"
                        : $"{vector2d.ToString()}";
        }

        private void UpdateBttnEnabled()
        {
            var vectorCount = int.Parse(CountOfVectorsLbl.Text == "" ? "0" : CountOfVectorsLbl.Text);

            AddVectorBtn.Enabled = !HomeModeChb.Checked;
            RemoveVectorBtn.Enabled = vectorCount > 0 && !HomeModeChb.Checked;
            GroupBttn.Enabled = vectorCount >= 2 && !HomeModeChb.Checked;
            UngroupBttn.Enabled = vectorCount >= 2 && !HomeModeChb.Checked;
            HomeModeChb.Enabled = TurnOn3DChb.Checked;
        }

        private void Select(Point point)
        {
            switch (ModifierKeys)
            {
                case Keys.Shift:
                    {
                        Groupped.Clear();
                        var (shape, crossing) = GetNearestShapeWithoutGroup(
                        point,
                        (x) =>
                        {
                            x.Draw(Graphics, WorkPen);
                        });

                        if (shape != null && crossing != VectorActions.None)
                        {
                            Groupped.Add(shape, crossing);
                            shape.Draw(Graphics, WorkPen);
                        }

                        break;
                    }
                default:
                    {
                        Groupped.Clear();
                        var (shape, crossing) = GetNearestShape(
                         point,
                         (x) =>
                         {
                             x.Draw(Graphics, WorkPen);
                         });

                        if (shape != null && crossing != VectorActions.None)
                        {
                            Groupped.Add(shape, crossing);
                            shape.Draw(Graphics, shape is Vector2D || shape is Vector3D ? WorkPen : GroupPen);
                        }

                        break;
                    }
                case Keys.Control:
                    {
                        var (shape, crossing) = GetNearestShape(
                         point,
                         (x) =>
                         {
                             if (Groupped.ContainsKey(x))
                             {
                                 Groupped[x] = VectorActions.Body;
                             }
                         });

                        if (shape != null && crossing != VectorActions.None)
                        {
                            if (Groupped.ContainsKey(shape))
                            {
                                Groupped.Remove(shape);
                                shape.Draw(Graphics, WorkPen);
                            }
                            else
                            {
                                Groupped.Add(shape, crossing);
                                shape.Draw(Graphics, shape is Vector2D || shape is Vector3D ? SelectedPen : GroupPen);
                            }
                        }

                        break;
                    }
            }

            Canvas.Refresh();
        }

        private (IShape, VectorActions) GetNearestShape(Point point, Action<IShape> agregate)
        {
            float minDistance = float.MaxValue;
            IShape currentShape = null;
            VectorActions currCrossing = VectorActions.None;

            foreach (var i in Shapes)
            {
                agregate?.Invoke(i);
                var crossing = i.GetCrossingType(point, 5);
                var distance = TurnOn3DChb.Checked ? i.GetDistance((Dot3D)point) : i.GetDistance((Dot2D)point);

                if (distance <= minDistance && crossing != VectorActions.None)
                {
                    minDistance = distance;
                    currentShape = i;
                    currCrossing = crossing;
                }
            }

            return (currentShape, currCrossing);
        }

        private (IShape, VectorActions) GetNearestShapeWithoutGroup(Point point, Action<IShape> agregate)
        {
            float minDistance = float.MaxValue;
            IShape currentShape = null;
            VectorActions currCrossing = VectorActions.None;

            foreach (var i in Shapes)
            {
                agregate?.Invoke(i);
                if (i is Group) continue;
                var crossing = i.GetCrossingType(point, 5);
                var distance = TurnOn3DChb.Checked ? i.GetDistance((Dot3D)point) : i.GetDistance((Dot2D)point);

                if (distance <= minDistance && crossing != VectorActions.None)
                {
                    minDistance = distance;
                    currentShape = i;
                    currCrossing = crossing;
                }
            }

            return (currentShape, currCrossing);
        }

        private bool DeformSelectedCut(float offsetX, float offsetY)
        {
            if (Groupped.Count > 1)
            {
                return false;
            }

            Vector2D vector2d = Groupped.Keys.FirstOrDefault() as Vector2D;
            Vector3D vector3d = Groupped.Keys.FirstOrDefault() as Vector3D;
            var vector = Groupped.Keys.FirstOrDefault();

            if (
                (vector is Vector2D || vector is Vector3D) &&
                (Groupped[vector] == VectorActions.StartPoint || Groupped[vector] == VectorActions.EndPoint)
                )
            {
                vector.Draw(Graphics, EraserPen);

                if (Groupped[vector] == VectorActions.StartPoint)
                {
                    if (vector2d == null)
                    {
                        vector3d.StartPoint = vector3d.StartPoint.Offset(offsetX, offsetY, 0);
                    }
                    else
                    {
                        vector2d.StartPoint = vector2d.StartPoint.Offset(offsetX, offsetY);
                    }
                }
                else
                {
                    if (vector2d == null)
                    {
                        vector3d.EndPoint = vector3d.EndPoint.Offset(offsetX, offsetY, 0);
                    }
                    else
                    {
                        vector2d.EndPoint = vector2d.EndPoint.Offset(offsetX, offsetY);
                    }
                }

                if (vector2d == null)
                {
                    vector = vector3d;
                }
                else
                {
                    vector = vector2d;
                }

                Shapes.Remove(vector);
                if (ShowCoordinateChb.Checked)
                {
                    DrawCoordinateLines();
                }
                foreach (var i in Shapes)
                {
                    i.Draw(Graphics, WorkPen);
                }

                Shapes.Add(vector);
                vector.Draw(Graphics, SelectedPen);
                Canvas.Refresh();
                return true;
            }

            return false;
        }

        private void MoveSelectedShapes(OffsetTypes offsetType, params float[] offsets)
        {
            if (ShowCoordinateChb.Checked)
            {
                DrawCoordinateLines();
            }

            foreach (var i in Shapes)
            {
                i.Draw(Graphics, WorkPen);
            }

            foreach (var i in Groupped.Keys)
            {
                i.Draw(Graphics, EraserPen);
                if (i is Vector3D)
                {
                    switch (offsetType)
                    {
                        case OffsetTypes.HouseOffset:
                        case OffsetTypes.MatrixOffset:
                            {
                                i.Offset(offsetType, offsets);
                                break;
                            }
                        case OffsetTypes.Usual:
                            {
                                i.Offset(offsetType, offsets[0], offsets[1], 0);
                                break;
                            }
                    }
                }
                else
                {
                    i.Offset(offsetType, offsets[0], offsets[1]);
                }
                i.Draw(Graphics, i is Vector2D || i is Vector3D ? SelectedPen : GroupPen);
                Shapes.Remove(i);
            }

            foreach (var i in Groupped.Keys)
            {
                Shapes.Add(i);
            }

            if (morphingFigure != null)
            {
                morphingFigure.Draw(Graphics, MorphingPen);
            }

            Canvas.Refresh();
        }

        private void VisibleMatrixOfActions(bool visible)
        {
            if (visible)
            {
                X1Coordinate.Text = X2Coordinate.Text = X3Coordinate.Text = X4Coordinate.Text =
                    Y1Coordinate.Text = Y2Coordinate.Text = Y3Coordinate.Text = Y4Coordinate.Text =
                    Z1Coordinate.Text = Z2Coordinate.Text = Z3Coordinate.Text = Z4Coordinate.Text =
                    OK1Coordinate.Text = OK2Coordinate.Text = OK3Coordinate.Text = OK4Coordinate.Text = "0";
            }

            X1Coordinate.Visible = X2Coordinate.Visible = X3Coordinate.Visible = X4Coordinate.Visible =
                    Y1Coordinate.Visible = Y2Coordinate.Visible = Y3Coordinate.Visible = Y4Coordinate.Visible =
                    Z1Coordinate.Visible = Z2Coordinate.Visible = Z3Coordinate.Visible = Z4Coordinate.Visible =
                    OK1Coordinate.Visible = OK2Coordinate.Visible = OK3Coordinate.Visible = OK4Coordinate.Visible = visible;
            CoordinateValueSB.Visible = visible;
            XCoordinateTitle.Visible = YCoordinateTitle.Visible = ZCoordinateTitle.Visible = UnifCoordinateTitle.Visible = visible;
            CoordinateValueSB.Enabled = false;
            CoordinateValueSB.Value = 0;
        }

        private void VisibleHomeParams(bool visible)
        {
            if (visible)
            {
                ZcValueLbl.Text = "300";
                ZcValueSB.Value = 300;
                TettaValueLbl.Text = "0";
                TettaValueSB.Value = 0;
                PhiValueLbl.Text = "0";
                PhiValueSB.Value = 0;
            }

            ZcTitle.Visible = ZcValueLbl.Visible = ZcValueSB.Visible =
                TettaTitle.Visible = TettaValueLbl.Visible = TettaValueSB.Visible =
                PhiTitle.Visible = PhiValueLbl.Visible = PhiValueSB.Visible = visible;
            DiscardBttn.Visible = visible;
        }

        #endregion

        #region General

        private void ChangeVisibilityOfCoordinates(object sender, EventArgs e)
        {
            DrawCoordinateLines();
            if (!HomeModeChb.Checked)
            {
                foreach (var i in Shapes)
                {
                    i.Draw(Graphics, WorkPen);
                }
                houseGroupBox.Visible = false;
                MorphingButtons.Visible = true;
                HouseButtons.Visible = true;
            }
            else
            {
                A_X_Coordinate.Text = A_Y_Coordinate.Text = A_Z_Coordinate.Text =
                B_X_Coordinate.Text = B_Y_Coordinate.Text = B_Z_Coordinate.Text =
                C_X_Coordinate.Text = C_Y_Coordinate.Text = C_Z_Coordinate.Text =
                D_X_Coordinate.Text = D_Y_Coordinate.Text = D_Z_Coordinate.Text =
                E_X_Coordinate.Text = E_Y_Coordinate.Text = E_Z_Coordinate.Text =
                F_X_Coordinate.Text = F_Y_Coordinate.Text = F_Z_Coordinate.Text =
                K_X_Coordinate.Text = K_Y_Coordinate.Text = K_Z_Coordinate.Text =
                L_X_Coordinate.Text = L_Y_Coordinate.Text = L_Z_Coordinate.Text =
                G_X_Coordinate.Text = G_Y_Coordinate.Text = G_Z_Coordinate.Text =
                H_X_Coordinate.Text = H_Y_Coordinate.Text = H_Z_Coordinate.Text = "0";
                houseGroupBox.Visible = true;
                HouseButtons.Visible = false;
                MorphingButtons.Visible = false;
            }
            UpdateGeneralInfo(Point.Empty, LoggedActions.None);
            Canvas.Refresh();
        }

        private void ChangeFlagOf3DVisibility(object sender, EventArgs e)
        {
            if (!TurnOn3DChb.Checked)
            {
                HouseButtons.Height = Global.UI_Data.CONST_Y_SIZE_3D_Group_OFF;
                HomeModeChb.Checked = false;
            }
            else
            {
                HouseButtons.Height = Global.UI_Data.CONST_Y_SIZE_3D_Group_ON;
                VisibleHomeParams(false);
            }
            VisibleMatrixOfActions(TurnOn3DChb.Checked);
            ExecuteBttn.Visible = TurnOn3DChb.Checked;
            Canvas.Image = new Bitmap(
                width: Canvas.Width,
                height: Canvas.Height
            );
            Graphics = Graphics.FromImage(Canvas.Image);
            CountOfVectorsLbl.Text = "0";
            Shapes.Clear();
            Groupped.Clear();
            UpdateBttnEnabled();
        }

        private void HomeModeFlagChanged(object sender, EventArgs e)
        {
            Canvas.Image = new Bitmap(
                width: Canvas.Width,
                height: Canvas.Height
            );
            Graphics = Graphics.FromImage(Canvas.Image);
            CountOfVectorsLbl.Text = "0";
            Shapes.Clear();
            Groupped.Clear();
            UpdateBttnEnabled();
            VisibleMatrixOfActions(!HomeModeChb.Checked);
            VisibleHomeParams(HomeModeChb.Checked);
            houseModeDublicate.Checked = HomeModeChb.Checked;
            if (HomeModeChb.Checked)
            {
                A_X_Coordinate.Text = A_Y_Coordinate.Text = A_Z_Coordinate.Text =
                B_X_Coordinate.Text = B_Y_Coordinate.Text = B_Z_Coordinate.Text =
                C_X_Coordinate.Text = C_Y_Coordinate.Text = C_Z_Coordinate.Text =
                D_X_Coordinate.Text = D_Y_Coordinate.Text = D_Z_Coordinate.Text =
                E_X_Coordinate.Text = E_Y_Coordinate.Text = E_Z_Coordinate.Text =
                F_X_Coordinate.Text = F_Y_Coordinate.Text = F_Z_Coordinate.Text =
                K_X_Coordinate.Text = K_Y_Coordinate.Text = K_Z_Coordinate.Text =
                L_X_Coordinate.Text = L_Y_Coordinate.Text = L_Z_Coordinate.Text =
                G_X_Coordinate.Text = G_Y_Coordinate.Text = G_Z_Coordinate.Text =
                H_X_Coordinate.Text = H_Y_Coordinate.Text = H_Z_Coordinate.Text = "0";
                houseGroupBox.Visible = true;
                HouseButtons.Visible = false;
                MorphingButtons.Visible = false;
                DrawMedianBttn.Enabled = DrawBisectorBttn.Enabled = DrawHeightBttn.Enabled = !HomeModeChb.Checked;
            }
            else
            {
                houseGroupBox.Visible = false;
                MorphingButtons.Visible = true;
                HouseButtons.Visible = true;
            }
        }

        private void HomeModeFlagChanged_Dublicate(object sender, EventArgs e)
        {
            HomeModeChb.Checked = houseModeDublicate.Checked;
        }

        #endregion

        #region Init

        public MainWindowForm()
        {
            InitializeComponent();
            Canvas.Image = new Bitmap(
                width: Canvas.Width,
                height: Canvas.Height
            );
            Counter = 0;
            Graphics = Graphics.FromImage(Canvas.Image);
        }

        private void HandleLoadMainWindow(object sender, EventArgs e)
        {
            CountOfVectorsLbl.Text = "0";
            UpdateBttnEnabled();
            VisibleMatrixOfActions(false);
            VisibleHomeParams(false);
            ExecuteBttn.Visible = false;
            HouseButtons.Height = Global.UI_Data.CONST_Y_SIZE_3D_Group_OFF;
            houseGroupBox.Visible = false;
            DrawHeightBttn.Enabled = DrawBisectorBttn.Enabled = DrawMedianBttn.Enabled = false;
        }

        #endregion

        #region Manipulations on vectors

        private void HandleClickInsertVector(object sender, EventArgs e)
        {
            Counter++;
            IShape vector;
            if (TurnOn3DChb.Checked)
            {
                vector = new Vector3D(Counter.ToString(), GetRandomDot3D(), GetRandomDot3D());
            }
            else
            {
                vector = new Vector2D(Counter.ToString(), GetRandomDot2D(), GetRandomDot2D());
            }

            Shapes.Add(vector);
            vector.Draw(Graphics, WorkPen);
            Canvas.Refresh();
            UpdateGeneralInfo(Point.Empty, LoggedActions.AddVector);
            UpdateBttnEnabled();
        }

        private void HandleClickRemoveVector(object sender, EventArgs e)
        {
            foreach (var i in Groupped.Keys)
            {
                i.Draw(Graphics, EraserPen);
                Shapes.Remove(i);
            }

            foreach (var i in Shapes)
            {
                i.Draw(Graphics, WorkPen);
            }

            Groupped.Clear();
            Canvas.Refresh();
            UpdateGeneralInfo(Point.Empty, LoggedActions.RemoveVector);
            UpdateBttnEnabled();
        }

        private void GroupVectorsClick(object sender, EventArgs e)
        {
            if (Groupped.Count > 1)
            {
                var group = new Group();

                foreach (var i in Groupped.Keys)
                {
                    i.Draw(Graphics, GroupPen);
                    group.Data.Add(i);
                    if (!(i is Group))
                    {
                        if (TurnOn3DChb.Checked)
                        {
                            ((Vector3D)i).IsInGroup = false;
                        }
                        else
                        {
                            ((Vector2D)i).IsInGroup = true;
                        }
                    }
                    else
                    {
                        Shapes.Remove(i);
                    }
                }

                Groupped.Clear();
                Shapes.Add(group);
                Canvas.Refresh();
            }
            UpdateGeneralInfo(Point.Empty, LoggedActions.None);
        }

        private void UngroupVectorsClick(object sender, EventArgs e)
        {
            foreach (var i in Groupped.Keys.Where(x => x is Group))
            {
                Shapes.Remove(i);

                foreach (var j in (i as Group).Data)
                {
                    if (!(j is Group))
                    {
                        if (TurnOn3DChb.Checked)
                        {
                            for (var k = 0; k < Shapes.Count; k++)
                            {
                                if (Shapes[k] is Vector3D)
                                {
                                    if (((Vector3D)Shapes[k]).IsInGroup && ((Vector3D)Shapes[k]).Name == ((Vector3D)j).Name)
                                    {
                                        Shapes[k] = j;
                                        break;
                                    }   
                                }
                            }
                        }
                        else
                        {
                            for (var k = 0; k < Shapes.Count; k++)
                            {
                                if (Shapes[k] is Vector2D)
                                {
                                    if (((Vector2D)Shapes[k]).IsInGroup && ((Vector2D)Shapes[k]).Name == ((Vector2D)j).Name)
                                    {
                                        Shapes[k] = j;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Shapes.Add(j);

                    if (j is Vector2D || j is Vector3D)
                    {
                        j.Draw(Graphics, SelectedPen);
                    }
                    else
                    {
                        j.Draw(Graphics, GroupPen);
                    }
                }
            }

            Groupped.Clear();
            Canvas.Refresh();
            UpdateGeneralInfo(Point.Empty, LoggedActions.None);
        }

        private void Execute3DAction(object sender, EventArgs e)
        {
            if (HomeModeChb.Checked)
            {
                ExecuteHouse();
            }
            else
            {
                var matrix = new float[16];
                try
                {
                    var x1 = float.Parse(X1Coordinate.Text);
                    var x2 = float.Parse(X2Coordinate.Text);
                    var x3 = float.Parse(X3Coordinate.Text);
                    var x4 = float.Parse(X4Coordinate.Text);

                    var y1 = float.Parse(Y1Coordinate.Text);
                    var y2 = float.Parse(Y2Coordinate.Text);
                    var y3 = float.Parse(Y3Coordinate.Text);
                    var y4 = float.Parse(Y4Coordinate.Text);

                    var z1 = float.Parse(Z1Coordinate.Text);
                    var z2 = float.Parse(Z2Coordinate.Text);
                    var z3 = float.Parse(Z3Coordinate.Text);
                    var z4 = float.Parse(Z4Coordinate.Text);

                    var uc1 = float.Parse(OK1Coordinate.Text);
                    var uc2 = float.Parse(OK2Coordinate.Text);
                    var uc3 = float.Parse(OK3Coordinate.Text);
                    var uc4 = float.Parse(OK4Coordinate.Text);

                    matrix = new float[] {
                        x1, y1, z1, uc1,
                        x2, y2, z2, uc2,
                        x3, y3, z3, uc3,
                        x4, y4, z4, uc4,
                    };
                }
                catch
                {
                    MessageBox.Show("Impossible value in the cell of matrix", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MoveSelectedShapes(OffsetTypes.MatrixOffset, matrix);
                MoveSelectedShapes(OffsetTypes.Usual, 0, 0);
            }
        }

        #endregion

        #region Canvas

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (HomeModeChb.Checked) return;
            if (specialAction == SpecialAction.Median)
            {
                IDot m;
                IShape shape = Groupped.Keys.FirstOrDefault();
                if (TurnOn3DChb.Checked)
                {
                    Dot3D startPoint = (Dot3D)((Vector3D)shape).StartPoint;
                    Dot3D endPoint = (Dot3D)((Vector3D)shape).EndPoint;
                    m = new Dot3D(
                        (startPoint.X + endPoint.X) / 2,
                        (startPoint.Y + endPoint.Y) / 2,
                        (startPoint.Z + endPoint.Z) / 2,
                        1
                        );
                    var median = new Vector3D(new Dot3D(e.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - e.Y, 0, 1), m);
                    Shapes.Add(median);
                    median.Draw(Graphics, WorkPen);
                }
                else
                {
                    Dot2D startPoint = (Dot2D)((Vector2D)shape).StartPoint;
                    Dot2D endPoint = (Dot2D)((Vector2D)shape).EndPoint;
                    m = new Dot2D(
                        (startPoint.X + endPoint.X) / 2,
                        (startPoint.Y + endPoint.Y) / 2,
                        1
                        );
                    var median = new Vector2D(new Dot2D(e.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - e.Y, 1), m);
                    Shapes.Add(median);
                    median.Draw(Graphics, WorkPen);
                }
                DrawMedianBttn.BackColor = Color.Transparent;
                specialAction = SpecialAction.None;
                Canvas.Refresh();
                return;
            }
            if (specialAction == SpecialAction.Height)
            {
                IDot h;
                IShape shape = Groupped.Keys.FirstOrDefault();
                if (TurnOn3DChb.Checked)
                {
                    var startPoint = (Dot3D)((Vector3D)shape).StartPoint;
                    var endPoint = (Dot3D)((Vector3D)shape).EndPoint;
                    var A = startPoint.Y - endPoint.Y;
                    var B = endPoint.X - startPoint.X;
                    var C = startPoint.X * endPoint.Y - endPoint.X * startPoint.Y;
                    var X = (-A * C - B * A * (Global.UI_Data.CONST_Y_SIZE_O - e.Y) + B * B * (e.X - Global.UI_Data.CONST_X_SIZE_O)) / (B * B + A * A);
                    var Y = (A * A * (Global.UI_Data.CONST_Y_SIZE_O - e.Y) - A * B * (e.X - Global.UI_Data.CONST_X_SIZE_O) - C * B) / (B * B + A * A);
                    var Z = default(float);
                    if (Math.Abs(B) > 0.00001)
                    {
                        Z = (startPoint.Z + (endPoint.Z - startPoint.Z) * ((X - startPoint.X) / A));
                    }
                    else
                    {
                        Z = (startPoint.Z + (endPoint.Z - startPoint.Z) * ((Y - startPoint.Y) / B));
                    }
                    h = new Dot3D(X, Y, Z, 1);
                    var height = new Vector3D(
                        new Dot3D(e.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - e.Y, 0, 1),
                        h
                        );
                    Shapes.Add(height);
                    height.Draw(Graphics, WorkPen);
                }
                else
                {
                    var startPoint = (Dot2D)((Vector2D)shape).StartPoint;
                    var endPoint = (Dot2D)((Vector2D)shape).EndPoint;
                    var A = startPoint.Y - endPoint.Y;
                    var B = endPoint.X - startPoint.X;
                    var C = startPoint.X * endPoint.Y - endPoint.X * startPoint.Y;
                    var X = (-A * C - B * A * (Global.UI_Data.CONST_Y_SIZE_O - e.Y) + B * B * (e.X - Global.UI_Data.CONST_X_SIZE_O)) / (B * B + A * A);
                    var Y = (A * A * (Global.UI_Data.CONST_Y_SIZE_O - e.Y) - A * B * (e.X - Global.UI_Data.CONST_X_SIZE_O) - C * B) / (B * B + A * A);
                    h = new Dot2D(X, Y, 1);
                    var height = new Vector2D(
                        new Dot2D(e.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - e.Y, 1),
                        h
                        );
                    Shapes.Add(height);
                    height.Draw(Graphics, WorkPen);
                }
                DrawHeightBttn.BackColor = Color.Transparent;
                specialAction = SpecialAction.None;
                Canvas.Refresh();
                return;
            }
            Select(e.Location);
            OldPoint = new Point(e.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - e.Y);
            IsMousePressed = true;
            UpdateGeneralInfo(e.Location, LoggedActions.None);
            DrawHeightBttn.Enabled = Groupped.Keys.Count == 1;
            DrawMedianBttn.Enabled = Groupped.Keys.Count == 1;
            DrawBisectorBttn.Enabled = Groupped.Keys.Count == 2;
        }

        private void HandleMouseLeave(object sender, EventArgs e)
        {
            UpdateGeneralInfo(Point.Empty, LoggedActions.None);
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMousePressed)
            {
                var offsetX = (e.X - Global.UI_Data.CONST_X_SIZE_O) - OldPoint.X;
                var offsetY = (Global.UI_Data.CONST_Y_SIZE_O - e.Y) - OldPoint.Y;

                if (!DeformSelectedCut(offsetX, offsetY))
                {
                    MoveSelectedShapes(OffsetTypes.Usual, offsetX, offsetY);
                }
                OldPoint = new Point(e.X - Global.UI_Data.CONST_X_SIZE_O, Global.UI_Data.CONST_Y_SIZE_O - e.Y);
            }

            UpdateGeneralInfo(e.Location, LoggedActions.None);
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            IsMousePressed = false;
            UpdateGeneralInfo(e.Location, LoggedActions.None);
        }

        #endregion

        #region Selector coordinate

        private TextBox textBox;

        private void OnCoordinateEnter(object sender, EventArgs e)
        {
            textBox = (TextBox)sender;
            CoordinateValueSB.Enabled = true;
            var value = 0.0;
            try
            {
                value = float.Parse(textBox.Text);
            }
            catch
            {
                throw new ArgumentException("Insert into cells only digits");
            }
            CoordinateValueSB.Value = (int)(value * 10);
        }

        private void OnCoordinateLeave(object sender, EventArgs e)
        {
            textBox = null;
            CoordinateValueSB.Value = 0;
            CoordinateValueSB.Enabled = false;
        }

        private void CoordinateValueChange(object sender, ScrollEventArgs e)
        {
            textBox.Text = (CoordinateValueSB.Value * 1.0 / 10).ToString();
        }

        #endregion

        #region Scroll Phi

        private void ScrollPhi(object sender, ScrollEventArgs e)
        {
            PhiValueLbl.Text = (PhiValueSB.Value * 1.0 / 10).ToString();
        }

        private void ScrollTetta(object sender, ScrollEventArgs e)
        {
            TettaValueLbl.Text = (TettaValueSB.Value * 1.0 / 10).ToString();
        }

        private void ZcScroll(object sender, ScrollEventArgs e)
        {
            ZcValueLbl.Text = (ZcValueSB.Value).ToString();
        }

        #endregion

        #region House

        private void ExecuteHouse()
        {
            foreach (var i in Shapes)
            {
                i.Draw(Graphics, HouseEraserPen);
            }

            foreach (var i in Shapes)
            {
                i.Offset(OffsetTypes.HouseOffset, (float)PhiValueSB.Value / 10, (float)TettaValueSB.Value / 10, ZcValueSB.Value);
                ((Brink)i).FindEquation(true);
                ((Brink)i).DefineMatrix((Dot3D)Middle);
                bool isDraw = ((Brink)i).ShouldBeDraw(ZcValueSB.Value, (Dot3D)Middle);
                if (isDraw)
                {
                    i.Draw(Graphics, HouseWorkPen);
                }
                else
                {
                    if (true)
                    {

                    }
                }
            }

            Canvas.Refresh();
        }

        private void DiscardChangesHouse(object sender, EventArgs e)
        {
            Canvas.Image = new Bitmap(
                width: Canvas.Width,
                height: Canvas.Height
            );
            Graphics = Graphics.FromImage(Canvas.Image);
            CountOfVectorsLbl.Text = "0";
            Shapes.Clear();
            Groupped.Clear();
            ZcValueLbl.Text = "300";
            ZcValueSB.Value = 300;
            TettaValueLbl.Text = "0";
            TettaValueSB.Value = 0;
            PhiValueLbl.Text = "0";
            PhiValueSB.Value = 0;
            foreach (var i in Shapes)
            {
                i.Draw(Graphics, HouseEraserPen);
            }
            Canvas.Refresh();
            A_X_Coordinate.Text = A_Y_Coordinate.Text = A_Z_Coordinate.Text =
                B_X_Coordinate.Text = B_Y_Coordinate.Text = B_Z_Coordinate.Text =
                C_X_Coordinate.Text = C_Y_Coordinate.Text = C_Z_Coordinate.Text =
                D_X_Coordinate.Text = D_Y_Coordinate.Text = D_Z_Coordinate.Text =
                E_X_Coordinate.Text = E_Y_Coordinate.Text = E_Z_Coordinate.Text =
                F_X_Coordinate.Text = F_Y_Coordinate.Text = F_Z_Coordinate.Text =
                K_X_Coordinate.Text = K_Y_Coordinate.Text = K_Z_Coordinate.Text =
                L_X_Coordinate.Text = L_Y_Coordinate.Text = L_Z_Coordinate.Text =
                G_X_Coordinate.Text = G_Y_Coordinate.Text = G_Z_Coordinate.Text =
                H_X_Coordinate.Text = H_Y_Coordinate.Text = H_Z_Coordinate.Text = "0";
            houseGroupBox.Visible = true;
            HouseButtons.Visible = false;
            MorphingButtons.Visible = false;
        }

        private void DrawHouse(Dot3D[] dots)
        {
            var A = dots[0];
            var B = dots[1];
            var C = dots[2];
            var D = dots[3];
            var E = dots[4];
            var F = dots[5];
            var K = dots[6];
            var L = dots[7];
            var G = dots[8];
            var H = dots[9];

            Middle = new Dot3D(
                (
                    A.LocalX + B.LocalX + C.LocalX + D.LocalX +
                    E.LocalX + F.LocalX + K.LocalX + L.LocalX +
                    G.LocalX + H.LocalX
                ) / 10,
                (
                    A.LocalY + B.LocalY + C.LocalY + D.LocalY +
                    E.LocalY + F.LocalY + K.LocalY + L.LocalY +
                    G.LocalY + H.LocalY
                ) / 10,
                (
                    A.LocalZ + B.LocalZ + C.LocalZ + D.LocalZ +
                    E.LocalZ + F.LocalZ + K.LocalZ + L.LocalZ +
                    G.LocalZ + H.LocalZ
                ) / 10,
                1
            );

            var AB = new Vector3D(A, B);
            var CB = new Vector3D(C, B);
            var CD = new Vector3D(C, D);
            var AD = new Vector3D(A, D);
            var AE = new Vector3D(A, E);
            var BF = new Vector3D(B, F);
            var CK = new Vector3D(C, K);
            var DL = new Vector3D(D, L);
            var EF = new Vector3D(E, F);
            var FK = new Vector3D(F, K);
            var LK = new Vector3D(L, K);
            var EL = new Vector3D(E, L);
            var EG = new Vector3D(E, G);
            var GF = new Vector3D(G, F);
            var LH = new Vector3D(L, H);
            var KH = new Vector3D(K, H);
            var GH = new Vector3D(G, H);

            var ABCD = new Brink("ABCD");
            ABCD.Data.AddRange(new IShape[] { AB, CB, CD, AD });
            var ABFE = new Brink("ABFE");
            ABFE.Data.AddRange(new IShape[] { AB, AE, BF, EF });
            var BCKF = new Brink("BCKF");
            BCKF.Data.AddRange(new IShape[] { CB, CK, FK, BF });
            var DCKL = new Brink("DCKL");
            DCKL.Data.AddRange(new IShape[] { CD, CK, DL, LK });
            var ADLE = new Brink("ADLE");
            ADLE.Data.AddRange(new IShape[] { AD, AE, DL, EL });
            var EFKL = new Brink("EFKL");
            EFKL.Data.AddRange(new IShape[] { EF, FK, LK, EL });
            var EFG = new Brink("EFG");
            EFG.Data.AddRange(new IShape[] { EF, EG, GF });
            var LHK = new Brink("LHK");
            LHK.Data.AddRange(new IShape[] { LH, KH, LK });
            var EGHL = new Brink("EGHL");
            EGHL.Data.AddRange(new IShape[] { EG, GH, LH, EL });
            var GFKH = new Brink("GFKH");
            GFKH.Data.AddRange(new IShape[] { GF, FK, KH, GH });

            Shapes.AddRange(new[] {
                ABCD,
                ABFE, BCKF, DCKL, ADLE,
                EFG, LHK,
                EFKL,
                EGHL, GFKH
            });
            ExecuteHouse();
        }

        private void DrawDefaultHouse(object sender, EventArgs e)
        {
            DrawHouse(
                new Dot3D[]
                {
                    new Dot3D(0, 0, 40, 1),
                    new Dot3D(100, 0, 40, 1),
                    new Dot3D(100, 0, 0, 1),
                    new Dot3D(0, 0, 0, 1),
                    new Dot3D(0, 100, 40, 1),
                    new Dot3D(100, 100, 40, 1),
                    new Dot3D(100, 100, 0, 1),
                    new Dot3D(0, 100, 0, 1),
                    new Dot3D(50, 150, 40, 1),
                    new Dot3D(50, 150, 0, 1),
                }
            );
            houseGroupBox.Visible = false;
            MorphingButtons.Visible = true;
            HouseButtons.Visible = true;
        }

        private void DrawSelectedHouse(object sender, EventArgs e)
        {
            try
            {
                var A = new Dot3D(
                    float.Parse(A_X_Coordinate.Text) * 10,
                    float.Parse(A_Y_Coordinate.Text) * 10,
                    float.Parse(A_Z_Coordinate.Text) * 10,
                    1
                    );
                var B = new Dot3D(
                    float.Parse(B_X_Coordinate.Text) * 10,
                    float.Parse(B_Y_Coordinate.Text) * 10,
                    float.Parse(B_Z_Coordinate.Text) * 10,
                    1
                    );
                var C = new Dot3D(
                    float.Parse(C_X_Coordinate.Text) * 10,
                    float.Parse(C_Y_Coordinate.Text) * 10,
                    float.Parse(C_Z_Coordinate.Text) * 10,
                    1
                    );
                var D = new Dot3D(
                    float.Parse(D_X_Coordinate.Text) * 10,
                    float.Parse(D_Y_Coordinate.Text) * 10,
                    float.Parse(D_Z_Coordinate.Text) * 10,
                    1
                    );
                var E = new Dot3D(
                    float.Parse(E_X_Coordinate.Text) * 10,
                    float.Parse(E_Y_Coordinate.Text) * 10,
                    float.Parse(E_Z_Coordinate.Text) * 10,
                    1
                    );
                var F = new Dot3D(
                    float.Parse(F_X_Coordinate.Text) * 10,
                    float.Parse(F_Y_Coordinate.Text) * 10,
                    float.Parse(F_Z_Coordinate.Text) * 10,
                    1
                    );
                var K = new Dot3D(
                    float.Parse(K_X_Coordinate.Text) * 10,
                    float.Parse(K_Y_Coordinate.Text) * 10,
                    float.Parse(K_Z_Coordinate.Text) * 10,
                    1
                    );
                var L = new Dot3D(
                    float.Parse(L_X_Coordinate.Text) * 10,
                    float.Parse(L_Y_Coordinate.Text) * 10,
                    float.Parse(L_Z_Coordinate.Text) * 10,
                    1
                    );
                var G = new Dot3D(
                    float.Parse(G_X_Coordinate.Text) * 10,
                    float.Parse(G_Y_Coordinate.Text) * 10,
                    float.Parse(G_Z_Coordinate.Text) * 10,
                    1
                    );
                var H = new Dot3D(
                    float.Parse(H_X_Coordinate.Text) * 10,
                    float.Parse(H_Y_Coordinate.Text) * 10,
                    float.Parse(H_Z_Coordinate.Text) * 10,
                    1
                    );

                DrawHouse(new Dot3D[]
                {
                    A, B, C, D,
                    E, F, K, L,
                    G, H
                });
                houseGroupBox.Visible = false;
                MorphingButtons.Visible = true;
                HouseButtons.Visible = true;
            }
            catch
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Morphing

        private Group EqualFigures(Group morphing, int count)
        {
            var morphingCopy = new Group();
            foreach (var j in morphing.Data)
            {
                morphingCopy.Data.Add(j);
            }
            var i = 0;
            while (morphingCopy.Data.Count < count)
            {
                var data = morphingCopy.Data;
                if (TurnOn3DChb.Checked)
                {
                    var startPoint = ((Vector3D)data[i]).StartPoint;
                    var endPoint = ((Vector3D)data[i]).EndPoint;

                    var newDot = new Dot3D(
                        (int)((endPoint.X + startPoint.X) / 2),
                        (int)((endPoint.Y + startPoint.Y) / 2),
                        (int)((endPoint.Z + startPoint.Z) / 2),
                        1
                        );

                    morphingCopy.Data.RemoveAt(i);
                    morphingCopy.Data.Insert(i, new Vector3D(startPoint, newDot));
                    morphingCopy.Data.Add(new Vector3D(newDot, endPoint));
                }
                else
                {
                    var startPoint = ((Vector2D)data[i]).StartPoint;
                    var endPoint = ((Vector2D)data[i]).EndPoint;

                    var newDot = new Dot2D(
                        (int)((endPoint.X + startPoint.X) / 2),
                        (int)((endPoint.Y + startPoint.Y) / 2),
                        1
                        );

                    morphingCopy.Data.RemoveAt(i);
                    morphingCopy.Data.Insert(i, new Vector2D(startPoint, newDot));
                    morphingCopy.Data.Add(new Vector2D(newDot, endPoint));
                }

                i++;
            }

            return morphingCopy;
        }

        private void Morphing(object sender, EventArgs e)
        {
            if (morphingList[0] != null &&
                morphingList[1] != null)
            {
                if (morphingFigure != null)
                {
                    morphingFigure.Draw(Graphics, EraserPen);
                }

                float morphingK = (float)morphingCoefTb.Value / 10;
                var firstFigures = ((Group)morphingList[0]).Data;
                var secondFigures = ((Group)morphingList[1]).Data;

                if (firstFigures.Count < secondFigures.Count)
                {
                    var group = EqualFigures((Group)morphingList[0], secondFigures.Count);
                    firstFigures = ((Group)group).Data;
                }
                else if (firstFigures.Count > secondFigures.Count)
                {
                    var group = EqualFigures((Group)morphingList[1], firstFigures.Count);
                    secondFigures = ((Group)group).Data;
                }
                morphingFigure = new Group();
                for (var i = 0; i < firstFigures.Count; ++i)
                {
                    IShape newLine;
                    if (TurnOn3DChb.Checked)
                    {
                        var getFirstFigureType = (Vector3D)firstFigures[i];
                        var getSecondFigureType = (Vector3D)secondFigures[i];

                        var StartPoint = new Dot3D(
                            (int)(getFirstFigureType.StartPoint.X * (1 - morphingK) + getSecondFigureType.StartPoint.X * morphingK),
                            (int)(getFirstFigureType.StartPoint.Y * (1 - morphingK) + getSecondFigureType.StartPoint.Y * morphingK),
                            (int)(getFirstFigureType.StartPoint.Z * (1 - morphingK) + getSecondFigureType.StartPoint.Z * morphingK),
                            1
                        );
                        var EndPoint = new Dot3D(
                            (int)(getFirstFigureType.EndPoint.X * (1 - morphingK) + getSecondFigureType.EndPoint.X * morphingK),
                            (int)(getFirstFigureType.EndPoint.Y * (1 - morphingK) + getSecondFigureType.EndPoint.Y * morphingK),
                            (int)(getFirstFigureType.EndPoint.Z * (1 - morphingK) + getSecondFigureType.EndPoint.Z * morphingK),
                            1
                        );

                        newLine = new Vector3D(StartPoint, EndPoint);
                    }
                    else
                    {
                        var getFirstFigureType = (Vector2D)firstFigures[i];
                        var getSecondFigureType = (Vector2D)secondFigures[i];

                        var StartPoint = new Dot2D(
                            (int)(getFirstFigureType.StartPoint.X * (1 - morphingK) + getSecondFigureType.StartPoint.X * morphingK),
                            (int)(getFirstFigureType.StartPoint.Y * (1 - morphingK) + getSecondFigureType.StartPoint.Y * morphingK),
                            1
                        );
                        var EndPoint = new Dot2D(
                            (int)(getFirstFigureType.EndPoint.X * (1 - morphingK) + getSecondFigureType.EndPoint.X * morphingK),
                            (int)(getFirstFigureType.EndPoint.Y * (1 - morphingK) + getSecondFigureType.EndPoint.Y * morphingK),
                            1
                        );

                        newLine = new Vector2D(StartPoint, EndPoint);
                    }
                    ((Group)morphingFigure).Data.Add(newLine);
                }
                MoveSelectedShapes(OffsetTypes.Usual, 0, 0);
            }
        }

        private void AddFirstFigureToMorphing(object sender, EventArgs e)
        {
            if (Groupped.Count >= 1)
            {
                var group = new Group();

                foreach (var i in Groupped.Keys)
                {
                    group.Data.Add(i);
                    Shapes.Remove(i);
                }

                Groupped.Clear();
                morphingList[0] = group;
                Shapes.Add(group);
                firstFigureMorphingBtn.BackColor = Color.LightGreen;
            }
        }

        private void AddSecondFigureToMorphing(object sender, EventArgs e)
        {
            if (Groupped.Count >= 1)
            {
                var group = new Group();

                foreach (var i in Groupped.Keys)
                {
                    group.Data.Add(i);
                    Shapes.Remove(i);
                }

                Groupped.Clear();
                morphingList[1] = group;
                Shapes.Add(group);
                secondFigureMorphingBtn.BackColor = Color.LightGreen;
            }
        }

        private void ClearMorphing(object sender, EventArgs e)
        {
            if (morphingFigure != null)
            {
                morphingFigure.Draw(Graphics, EraserPen);
            }
            morphingFigure = null;
            morphingList = new IShape[2];
            firstFigureMorphingBtn.BackColor = Color.Transparent;
            secondFigureMorphingBtn.BackColor = Color.Transparent;
            MoveSelectedShapes(OffsetTypes.Usual, 0, 0);
        }

        #endregion

        #region Draw Special lines

        private void DrawHeight(object sender, EventArgs e)
        {
            if (Groupped.Keys.Count == 1)
            {
                specialAction = SpecialAction.Height;
                DrawHeightBttn.BackColor = Color.LightGreen;
            }
        }

        private void DrawMedian(object sender, EventArgs e)
        {
            if (Groupped.Keys.Count == 1)
            {
                specialAction = SpecialAction.Median;
                DrawMedianBttn.BackColor = Color.LightGreen;
            }
        }

        private void DrawBisector(object sender, EventArgs e)
        {
            if (Groupped.Keys.Count == 2)
            {
                var firstFigure = Groupped.Keys.FirstOrDefault();
                var secondFigure = Groupped.Keys.LastOrDefault();
                if (TurnOn3DChb.Checked)
                {
                    var startPoint_first = (Dot3D)((Vector3D)firstFigure).StartPoint;
                    var endPoint_first = (Dot3D)((Vector3D)firstFigure).EndPoint;

                    var startPoint_second = (Dot3D)((Vector3D)secondFigure).StartPoint;
                    var endPoint_second = (Dot3D)((Vector3D)secondFigure).EndPoint;

                    var A1 = startPoint_first.Y - endPoint_first.Y;
                    var B1 = endPoint_first.X - startPoint_first.X;
                    var C1 = startPoint_first.X * endPoint_first.Y - endPoint_first.X * startPoint_first.Y;

                    var A2 = startPoint_second.Y - endPoint_second.Y;
                    var B2 = endPoint_second.X - startPoint_second.X;
                    var C2 = startPoint_second.X * endPoint_second.Y - endPoint_second.X * startPoint_second.Y;

                    if (A1 * B2 - A2 * B1 == 0)
                        return;
                    var firstX = (B1 * C2 - B2 * C1) / (A1 * B2 - A2 * B1);
                    var firstY = (C1 * A2 - C2 * A1) / (A1 * B2 - A2 * B1);
                    var firstZ = default(float);
                    if (Math.Abs(B1) > 0.00001)
                    {
                        firstZ = (startPoint_first.Z + (endPoint_first.Z - startPoint_first.Z) * (firstX - startPoint_first.X) / B1);
                    }
                    else
                    {
                        firstZ = (startPoint_first.Z + (endPoint_first.Z - startPoint_first.Z) * (firstY - startPoint_first.Y) / A1);
                    }

                    var leftLengthA = Math.Sqrt(Math.Pow((startPoint_first.X - firstX), 2) + Math.Pow((startPoint_first.Y - firstY), 2) +
                    Math.Pow((startPoint_first.Z - firstZ), 2));
                    var leftLengthB = Math.Sqrt(Math.Pow((endPoint_first.X - firstX), 2) + Math.Pow((endPoint_first.Y - firstY), 2) +
                        Math.Pow((endPoint_first.Z - firstZ), 2));
                    Dot3D maxLeft = leftLengthA > leftLengthB ? startPoint_first : endPoint_first;
                    var leftLength = Math.Max(leftLengthA, leftLengthB);

                    var rightLengthA = Math.Sqrt(Math.Pow((startPoint_second.X - firstX), 2) + Math.Pow((startPoint_second.Y - firstY), 2) +
                    Math.Pow((startPoint_second.Z - firstZ), 2));
                    var rightLengthB = Math.Sqrt(Math.Pow((endPoint_second.X - firstX), 2) + Math.Pow((endPoint_second.Y - firstY), 2) +
                        Math.Pow((endPoint_second.Z - firstZ), 2));
                    Dot3D maxRight = rightLengthA > rightLengthB ? startPoint_second : endPoint_second;
                    var rightLength = Math.Max(rightLengthA, rightLengthB);

                    var leftMove = leftLength / (leftLength + rightLength);
                    var rightMove = rightLength / (leftLength + rightLength);
                    var secondX = (float)(rightMove * maxLeft.X + leftMove * maxRight.X);
                    var secondY = (float)(rightMove * maxLeft.Y + leftMove * maxRight.Y);
                    var secondZ = (float)(rightMove * maxLeft.Z + leftMove * maxRight.Z);

                    var bis = new Vector3D(
                        new Dot3D(firstX, firstY, firstZ, 1),
                        new Dot3D(secondX, secondY, secondZ, 1)
                        );
                    Shapes.Add(bis);
                    bis.Draw(Graphics, WorkPen);
                }
                else
                {
                    var startPoint_first = (Dot2D)((Vector2D)firstFigure).StartPoint;
                    var endPoint_first = (Dot2D)((Vector2D)firstFigure).EndPoint;

                    var startPoint_second = (Dot2D)((Vector2D)secondFigure).StartPoint;
                    var endPoint_second = (Dot2D)((Vector2D)secondFigure).EndPoint;

                    var A1 = startPoint_first.Y - endPoint_first.Y;
                    var B1 = endPoint_first.X - startPoint_first.X;
                    var C1 = startPoint_first.X * endPoint_first.Y - endPoint_first.X * startPoint_first.Y;

                    var A2 = startPoint_second.Y - endPoint_second.Y;
                    var B2 = endPoint_second.X - startPoint_second.X;
                    var C2 = startPoint_second.X * endPoint_second.Y - endPoint_second.X * startPoint_second.Y;

                    if (A1 * B2 - A2 * B1 == 0)
                        return;
                    var firstX = (B1 * C2 - B2 * C1) / (A1 * B2 - A2 * B1);
                    var firstY = (C1 * A2 - C2 * A1) / (A1 * B2 - A2 * B1);

                    var leftLengthA = Math.Sqrt(Math.Pow((startPoint_first.X - firstX), 2) + Math.Pow((startPoint_first.Y - firstY), 2));
                    var leftLengthB = Math.Sqrt(Math.Pow((endPoint_first.X - firstX), 2) + Math.Pow((endPoint_first.Y - firstY), 2));
                    Dot2D maxLeft = leftLengthA > leftLengthB ? startPoint_first : endPoint_first;
                    var leftLength = Math.Max(leftLengthA, leftLengthB);

                    var rightLengthA = Math.Sqrt(Math.Pow((startPoint_second.X - firstX), 2) + Math.Pow((startPoint_second.Y - firstY), 2));
                    var rightLengthB = Math.Sqrt(Math.Pow((endPoint_second.X - firstX), 2) + Math.Pow((endPoint_second.Y - firstY), 2));
                    Dot2D maxRight = rightLengthA > rightLengthB ? startPoint_second : endPoint_second;
                    var rightLength = Math.Max(rightLengthA, rightLengthB);

                    var leftMove = leftLength / (leftLength + rightLength);
                    var rightMove = rightLength / (leftLength + rightLength);
                    var secondX = (float)(rightMove * maxLeft.X + leftMove * maxRight.X);
                    var secondY = (float)(rightMove * maxLeft.Y + leftMove * maxRight.Y);

                    var bis = new Vector2D(
                        new Dot2D(firstX, firstY, 1),
                        new Dot2D(secondX, secondY, 1)
                        );
                    Shapes.Add(bis);
                    bis.Draw(Graphics, WorkPen);
                }

                Canvas.Refresh();
            }
        }

        #endregion

        #region File menu

        private void NewCanvas(object sender, EventArgs e)
        {
            Canvas.Image = new Bitmap(
                width: Canvas.Width,
                height: Canvas.Height
            );
            Graphics = Graphics.FromImage(Canvas.Image);
            CountOfVectorsLbl.Text = "0";
            UpdateBttnEnabled();
            VisibleMatrixOfActions(false);
            VisibleHomeParams(false);
            ExecuteBttn.Visible = false;
            HouseButtons.Height = Global.UI_Data.CONST_Y_SIZE_3D_Group_OFF;
            houseGroupBox.Visible = false;
            DrawHeightBttn.Enabled = DrawBisectorBttn.Enabled = DrawMedianBttn.Enabled = false;
            DrawHeightBttn.BackColor = DrawBisectorBttn.BackColor = DrawMedianBttn.BackColor = Color.Transparent;
            TurnOn3DChb.Checked = false;
            Shapes.Clear();
            Groupped.Clear();
            firstFigureMorphingBtn.BackColor = Color.Transparent;
            secondFigureMorphingBtn.BackColor = Color.Transparent;
        }

        private void SaveAsCanvas(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Canvas file (*.grf)|*.grf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CustomSerializer.Serialize(sfd.FileName, Shapes, Groupped);
                }
                catch
                {

                }
            }
        }

        private void LoadCanvas(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Canvas file (*.grf)|*.grf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<IShape> shapes = null;
                    Dictionary<IShape, VectorActions> groupped = null;
                    CustomSerializer.Deserialize(ofd.FileName, ref shapes, ref groupped);
                    Shapes = shapes;
                    Groupped = groupped;
                    MoveSelectedShapes(OffsetTypes.Usual, 0, 0);
                }
                catch
                {

                }
            }
        }

        #endregion
    }
}