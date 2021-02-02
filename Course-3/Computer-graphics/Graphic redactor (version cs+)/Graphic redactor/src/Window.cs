using System;
using System.Collections.Generic;
using System.ComponentModel;
using Graphic_redactor.src.Libraries;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Graphic_redactor.src
{
    enum captures { TAKE_PT1, TAKE_PT2, TAKE_CENTR, TAKE_NONE, TAKE_TURN };
    //режим рисования: рисования, перемешение, удаление
    enum modes { MODE_DROW, MODE_MOVE, MODE_DELETE };

    enum penType { line, poligon };
    public partial class Window : Form
    {
        editor state = new editor();

        bool MODE_DROW = false;
        bool MODE_MOVE = false;

        bool Redacting_button_pressed = false;
        bool Pencil_button_pressed = false;
        bool Line_button_pressed = false;
        bool Polygon_button_pressed = false;
        bool Bezyie_button_pressed = false;
        bool Group_button_pressed = false;
        bool Ungroup_button_pressed = false;
        bool Axes_button_pressed = false;
        bool Magic_line_button_pressed = false;

        public Window()
        {
            InitializeComponent();
            Color_square.BackColor = Current_color;
            Main_menu.Renderer = new MyRenderer(); 
        }

        #region Цвета интерфейса
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color ButtonSelectedGradientBegin { get { return Color.Yellow; } }
            public override Color ButtonSelectedGradientEnd { get { return Color.Yellow; } }
            public override Color ButtonCheckedGradientBegin { get { return Color.Yellow; } }
            public override Color ButtonCheckedGradientEnd { get { return Color.Yellow; } }
            public override Color ButtonPressedGradientBegin { get { return Color.Yellow; } }
            public override Color ButtonPressedGradientEnd { get { return Color.Yellow; } }
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
            //Open_file.ShowDialog();
            //if (Open_file.FileName != "")
            //{
            //    canvas_picture = (Bitmap)Image.FromFile(Open_file.FileName);
            //    Canvas.Image = canvas_picture;
            //}
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            Save_file.ShowDialog();
            if (Save_file.FileName != "")
            {
                //canvas_picture.Save(Save_file.FileName);
            }
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
        private void Cursor_button_Click(object sender, EventArgs e)
        {
            state.curModes = (int)modes.MODE_MOVE;

            Cursor_button.Enabled = false;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;
            Bezyie.Enabled = true;

            Redacting_button_pressed = true;
            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
            Bezyie_button_pressed = false;

            MODE_DROW = false;
            MODE_MOVE = true;

        }

        private void Draw_pencil_Click(object sender, EventArgs e)
        {
            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = false;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;
            Bezyie.Enabled = true;

            Redacting_button_pressed = false;
            Pencil_button_pressed = true;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
            Bezyie_button_pressed = false;

            MODE_DROW = false;
            MODE_MOVE = false;
        }

        private void Draw_line_Click(object sender, EventArgs e)
        {
            state.curModes = (int)modes.MODE_DROW;
            state.resetIndexLine();
            state.drawingSciene();

            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = false;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;
            Bezyie.Enabled = true;

            Redacting_button_pressed = false;
            Pencil_button_pressed = false;
            Line_button_pressed = true;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
            Bezyie_button_pressed = false;
        }

        private void Draw_polygon_Click(object sender, EventArgs e)
        {
            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = false;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;
            Bezyie.Enabled = true;

            Redacting_button_pressed = false;
            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = true;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
            Bezyie_button_pressed = false;
        }

        private void Group_lines_Click(object sender, EventArgs e)
        {
            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = false;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;
            Bezyie.Enabled = true;

            Redacting_button_pressed = false;
            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = true;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
            Bezyie_button_pressed = false;
        }

        private void Ungroup_lines_Click(object sender, EventArgs e)
        {
            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = false;
            Axes.Enabled = true;
            Magic_line.Enabled = true;
            Bezyie.Enabled = true;

            Redacting_button_pressed = false;
            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = true;
            Axes_button_pressed = false;
            Magic_line_button_pressed = false;
            Bezyie_button_pressed = false;
        }

        private void Axes_Click(object sender, EventArgs e)
        {
            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = false;
            Magic_line.Enabled = true;
            Bezyie.Enabled = true;

            Redacting_button_pressed = false;
            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = true;
            Magic_line_button_pressed = false;
            Bezyie_button_pressed = false;
        }

        private void Magic_line_Click(object sender, EventArgs e)
        {
            state.curModes = (int)modes.MODE_DROW;
            state.resetIndexLine();
            state.drawingSciene();

            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = false;
            Bezyie.Enabled = true;

            Redacting_button_pressed = false;
            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = true;
            Bezyie_button_pressed = false;
        }

        private void Bezyie_button_Click(object sender, EventArgs e)
        {
            Cursor_button.Enabled = true;
            Draw_pencil.Enabled = true;
            Draw_line.Enabled = true;
            Draw_polygon.Enabled = true;
            Group_lines.Enabled = true;
            Ungroup_lines.Enabled = true;
            Axes.Enabled = true;
            Magic_line.Enabled = true;
            Bezyie.Enabled = false;

            Redacting_button_pressed = false;
            Pencil_button_pressed = false;
            Line_button_pressed = false;
            Polygon_button_pressed = false;
            Group_button_pressed = false;
            Ungroup_button_pressed = false;
            Axes_button_pressed = false;
            Magic_line_button_pressed = true;
            Bezyie_button_pressed = false;
        }
        #endregion

        #region Canvas acting
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Canvas.Cursor = Cursors.Cross; 

            if (Redacting_button_pressed == true && e.Button == MouseButtons.Left)
            {
                state.drawingDown(e);
            }
            else if (Pencil_button_pressed == true && e.Button == MouseButtons.Left)
            {
                
            }
            else if (Line_button_pressed == true && e.Button == MouseButtons.Left)
            {
                state.drawingDown(e);
            }
            else if (Polygon_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Bezyie_button_pressed == true && e.Button == MouseButtons.Left)
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
                state.drawingDown(e);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor_location.Text = $"Cursor place: X = [{e.X}] Y = [{Canvas_background.Size.Height - e.Y}] Z = [0]";
            
            if (Redacting_button_pressed == true && e.Button == MouseButtons.Left)
            {
                state.drawingSciene(Canvas, e);
            }
            else if (Pencil_button_pressed == true && e.Button == MouseButtons.Left)
            {
                
            }
            else if (Line_button_pressed == true && e.Button == MouseButtons.Left)
            {
                state.drawingSciene(Canvas, e);
            }
            else if (Polygon_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Bezyie_button_pressed == true && e.Button == MouseButtons.Left)
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
                state.drawingSciene(Canvas, e);
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Canvas.Cursor = Cursors.Default;

            if (Redacting_button_pressed == true && e.Button == MouseButtons.Left)
            {
                state.drawingUp(e);
                state.pointsDebug();
            }
            else if (Pencil_button_pressed == true && e.Button == MouseButtons.Left)
            {
               
            }
            else if (Line_button_pressed == true && e.Button == MouseButtons.Left)
            {
                state.drawingUp(e);
                state.pointsDebug();
            }
            else if (Polygon_button_pressed == true && e.Button == MouseButtons.Left)
            {

            }
            else if (Bezyie_button_pressed == true && e.Button == MouseButtons.Left)
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
                state.drawingUp(e);
                state.pointsDebug();
            }
        }
        #endregion
    }
}