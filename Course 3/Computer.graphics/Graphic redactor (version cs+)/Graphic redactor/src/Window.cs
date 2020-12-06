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
        
        public Window()
        {
            InitializeComponent();
            Main_menu.Renderer = new MyRenderer();
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

        private void Color_circle_Click(object sender, EventArgs e)
        {

        }

        #region Drawing bar
        private void Draw_pencil_Click(object sender, EventArgs e)
        {

        }

        private void Draw_line_Click(object sender, EventArgs e)
        {

        }

        private void Draw_polygon_Click(object sender, EventArgs e)
        {

        }

        private void Group_lines_Click(object sender, EventArgs e)
        {

        }

        private void Ungroup_lines_Click(object sender, EventArgs e)
        {

        }

        private void Axes_Click(object sender, EventArgs e)
        {

        }

        private void Magic_line_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}