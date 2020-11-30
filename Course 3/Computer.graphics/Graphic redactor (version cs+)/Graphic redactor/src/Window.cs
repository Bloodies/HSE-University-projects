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
            public override Color MenuStripGradientBegin 
                { get { return Color.Yellow; } }
            public override Color MenuStripGradientEnd
            { get { return Color.Yellow; } }
            public override Color ToolStripGradientBegin
            { get { return Color.Yellow; } }
            public override Color ToolStripGradientEnd
            { get { return Color.Yellow; } }
            public override Color ToolStripContentPanelGradientBegin
            { get { return Color.Yellow; } }
            public override Color ToolStripContentPanelGradientEnd
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

        private void Pic_close_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void Pic_maximize_Click(object sender, EventArgs e)
        {
            
        }

        private void Pic_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}