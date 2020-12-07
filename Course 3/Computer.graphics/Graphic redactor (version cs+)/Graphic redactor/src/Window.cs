using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic_redactor.src
{
    #region test of func
    //public const int WM_NCLBUTTONDOWN = 0xA1;
    //public const int HT_CAPTION = 0x2;

    //[DllImportAttribute("user32.dll")]
    //public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    //[DllImportAttribute("user32.dll")]
    //public static extern bool ReleaseCapture();
    #endregion

    public partial class Window : Form
    {
        bool Pencil_button_pressed = false;
        bool Line_button_pressed = false;
        bool Polygon_button_pressed = false;
        bool Group_button_pressed = false;
        bool Ungroup_button_pressed = false;
        bool Axes_button_pressed = false;
        bool Magic_line_button_pressed = false;

        private List<Point> points = new List<Point>();
        Point Current_point;
        Point Previous_point;
        float x_started, y_started;

        Bitmap canvas_picture;

        Graphics _graphics;

        public Window()
        {
            InitializeComponent();

            Color_square.BackColor = Current_color;

            Main_menu.Renderer = new MyRenderer();            
            canvas_picture = new Bitmap(1000, 1000);
            _graphics = Graphics.FromImage(canvas_picture);
            _graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            x_started = y_started = 0;

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Color_circle.BackColor = Color.Transparent;
        }

        #region Цвета интерфейса
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {            
            #region Menu
            public override Color MenuItemSelectedGradientBegin { get { return Color.FromArgb(10, 35, 75); } }
            public override Color MenuItemSelectedGradientEnd   { get { return Color.FromArgb(10, 35, 75); } }
            public override Color MenuItemPressedGradientBegin  { get { return Color.FromArgb(10, 35, 75); } }
            public override Color MenuItemPressedGradientEnd    { get { return Color.FromArgb(10, 35, 75); } }
            public override Color MenuItemBorder                { get { return Color.FromArgb(10, 35, 75); } }
            public override Color MenuItemSelected              { get { return Color.FromArgb(51, 85, 139); } }
            public override Color MenuBorder                    { get { return Color.FromArgb(10, 35, 75); } }
            #endregion 
            #region Tool_strip
            public override Color ToolStripDropDownBackground   { get { return Color.FromArgb(10, 35, 75); } }
            public override Color ToolStripBorder               { get { return Color.FromArgb(10, 35, 75); } }
            #endregion 
            #region Tool_strip_pics
            public override Color ImageMarginGradientBegin      { get { return Color.FromArgb(10, 35, 75); } }
            public override Color ImageMarginGradientMiddle     { get { return Color.FromArgb(10, 35, 75); } }
            public override Color ImageMarginGradientEnd        { get { return Color.FromArgb(10, 35, 75); } }
            #endregion

            #region Test (просто хз где)
            public override Color GripDark
                { get { return Color.Yellow; } }
            public override Color GripLight
            { get { return Color.Yellow; } }
            public override Color OverflowButtonGradientBegin
            { get { return Color.Yellow; } }
            public override Color OverflowButtonGradientEnd
            { get { return Color.Yellow; } }
            public override Color StatusStripGradientBegin
            { get { return Color.Yellow; } }
            public override Color StatusStripGradientEnd
            { get { return Color.Yellow; } }
            public override Color ToolStripPanelGradientBegin
            { get { return Color.Yellow; } }
            public override Color ToolStripPanelGradientEnd
            { get { return Color.Yellow; } }            
            public override Color ImageMarginRevealedGradientBegin
            { get { return Color.Yellow; } }
            public override Color ImageMarginRevealedGradientEnd
            { get { return Color.Yellow; } }
            #endregion
        }
        #endregion 

        #region Движение окна
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void Main_menu_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void Main_menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void Main_menu_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion

        #region Acting window
        private void Pic_close_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        private void Exit_dop_button_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void Pic_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region Menu bar
        #region File        
        private void Make_button_Click(object sender, EventArgs e)
        {
            Canvas.Refresh();
        }

        private void Save_button_Click(object sender, EventArgs e)
        {

        }

        private void Save_as_button_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region Changing
        private void Undo_button_Click(object sender, EventArgs e)
        {

        }

        private void Redo_button_Click(object sender, EventArgs e)
        {

        }

        private void Copy_button_Click(object sender, EventArgs e)
        {

        }

        private void Input_button_Click(object sender, EventArgs e)
        {

        }

        private void Choose_all_button_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void Tasks_window_Click(object sender, EventArgs e)
        {

        }

        #region Other 3D
        #endregion
        #endregion

        #region Color changing
        Color Current_color = Color.Black;

        private void Color_square_Click(object sender, EventArgs e)
        {
            DialogResult Color_dialog_action = Color_dialog.ShowDialog();
            if (Color_dialog_action == DialogResult.OK)
            {
                Current_color = Color_dialog.Color;
            }
            Color_square.BackColor = Current_color;
        }
        private void Color_circle_Click(object sender, EventArgs e)
        {
            DialogResult Color_dialog_action = Color_dialog.ShowDialog();
            if(Color_dialog_action == DialogResult.OK)
            {
                Current_color = Color_dialog.Color;
            }
            Color_square.BackColor = Current_color;
        }
        #endregion

        #region Drawing bar
        private void Draw_pencil_Click(object sender, EventArgs e)
        {
            Draw_pencil.Enabled = false;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;

            Pencil_button_pressed = true;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
        }

        private void Draw_line_Click(object sender, EventArgs e)
        {
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = false;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;

            Pencil_button_pressed = false;
            Line_button_pressed = true;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;            
        }

        private void Draw_polygon_Click(object sender, EventArgs e)
        {
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = false;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;

            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = true;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
        }

        private void Group_lines_Click(object sender, EventArgs e)
        {
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = false;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;

            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = true;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
        }

        private void Ungroup_lines_Click(object sender, EventArgs e)
        {
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = false;
            Axes.Enabled = true;
            Magic_line.Enabled = true;

            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = true;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
        }

        private void Axes_Click(object sender, EventArgs e)
        {
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = false;
            Magic_line.Enabled = true;

            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = true;
            Magic_line_button_pressed = false;
        }

        private void Magic_line_Click(object sender, EventArgs e)
        {
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = false;

            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = true;
        }
        #endregion

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Canvas.Cursor = Cursors.Default;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            
            Cursor_location.Text = $"Cursor place: X = [{e.X}] Y = [{Canvas_background.Size.Height - e.Y}] Z = [0]";

            if (Pencil_button_pressed == true)
            {
                //Bitmap bmp = new Bitmap(Scale_bar.Value, Scale_bar.Value);
                //Previous_point = Current_point;
                //Current_point = e.Location;..
                Pen new_pen = new Pen(Current_color, Scale_bar.Value);
                new_pen.StartCap = new_pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                if (e.Button == MouseButtons.Left)
                {
                    Canvas.Image = canvas_picture;
                    _graphics.DrawLine(new_pen, x_started, y_started, e.X, e.Y);//Previous_point, Current_point);
                }   
                x_started = e.X;
                y_started = e.Y;
            }
            else if (Line_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Polygon_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Group_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Ungroup_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Axes_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Magic_line_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Canvas.Cursor = Cursors.Cross;
            if (Pencil_button_pressed == true)
            {
                Current_point = e.Location;
                Previous_point = Current_point;
                points = new List<Point>();

                Pen new_pen = new Pen(Current_color, Scale_bar.Value);
                new_pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                new_pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                if (e.Button == MouseButtons.Left)
                {

                    Canvas.Image = canvas_picture;
                    //_graphics.DrawLine(new_pen, x_started, y_started, e.X, e.Y);//Previous_point, Current_point);
                    points.Add(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
                    this.Invalidate();
                    //_graphics.DrawLine(new_pen, Previous_point, Current_point);
                    //_graphics.Draw
                }
                x_started = e.X;
                y_started = e.Y;
            }
            else if (Line_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Polygon_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Group_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Ungroup_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Axes_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Magic_line_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
        }

        private void Window_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(1, 1);
            bmp.SetPixel(0, 0, Color.Black);
            foreach (Point p in points)
                e.Graphics.DrawImage(bmp, p);
        }

        private void Icon_pic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Forms.Bug_reporter show_bug = new Forms.Bug_reporter($"Arguments now:\nPencil_button_pressed={Pencil_button_pressed}\nLine_button_pressed={Line_button_pressed}\nPolygon_button_pressed={Polygon_button_pressed}\nGroup_button_pressed={Group_button_pressed}\nUngroup_button_pressed={Ungroup_button_pressed}\nAxes_button_pressed={Axes_button_pressed}\nMagic_line_button_pressed={Magic_line_button_pressed}");            
            show_bug.ShowDialog();
        }
    }
}