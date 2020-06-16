using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab._5__User_controls_._3__Colors
{
    public partial class DisplayColor : UserControl
    {
        public DisplayColor()
        {
            InitializeComponent();
        }
        private Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                BackColor = color;
            }
        }
    }
}
