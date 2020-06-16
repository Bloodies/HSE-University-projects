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
    public partial class RGBControl : UserControl
    {
        public RGBControl()
        {
            InitializeComponent();
            RedRGBNum.TextChanged += Change;
            GreenRGBNum.TextChanged += Change;
            BlueRGBNum.TextChanged += Change;
            Change(null, null);
            NumSystem = NumSys.Dec;
        }
        private void Change(object sender, EventArgs e)
        {
            Color = Color.FromArgb(RedRGBNum.Number, GreenRGBNum.Number, BlueRGBNum.Number);
            ChangeColor?.Invoke(color);
        }
        private NumSys numSys;
        private Color color;

        public event Action<Color> ChangeColor;
        private Color Color
        {
            set
            {
                color = value;
                displayColor1.Color = color;
            }
        }
        public NumSys NumSystem
        {
            get
            {
                return numSys;
            }
            set
            {
                numSys = value;
                RedRGBNum.NumSystem = numSys;
                GreenRGBNum.NumSystem = numSys;
                BlueRGBNum.NumSystem = numSys;
            }
        }

        private void RadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked == true && radioButton.Text == "Dec")
                NumSystem = NumSys.Dec;
            else if (radioButton.Checked == true && radioButton.Text == "Hex")
                NumSystem = NumSys.Hex;
        }
    }
}