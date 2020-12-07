using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic_redactor.src.Forms
{
    
    public partial class Bug_reporter : Form
    {
        
        public Bug_reporter(string strText)
        {
            InitializeComponent();
            this.Bug_display.Text = strText;
        }
    }
}
