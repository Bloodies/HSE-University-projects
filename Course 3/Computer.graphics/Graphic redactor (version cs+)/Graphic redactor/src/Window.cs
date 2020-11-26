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
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Window()
        {
            InitializeComponent();
        }
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
    }
}