using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab._5__User_controls_._3__Colors
{
    public enum NumSys
    {
        Dec,
        Hex
    }
    public partial class RGBNumber : TextBox
    {
        private int number = 0;
        public int Number
        {
            get
            {
                return number;
            }
        }
        private NumSys numSys = NumSys.Dec;
        public NumSys NumSystem
        {
            get
            {
                return numSys;
            }
            set
            {
                numSys = value;
                if (value == NumSys.Dec)
                    this.Text = number.ToString("G");
                else if (value == NumSys.Hex)
                    this.Text = number.ToString("X");
            }
        }
        public RGBNumber()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void RGBNumber_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.Text))
            {
                try
                {
                    if (NumSystem == NumSys.Dec)
                        number = Convert.ToInt32(this.Text);
                    else
                        number = Convert.ToInt32(this.Text, 16);
                }
                catch
                {
                    if (NumSystem == NumSys.Dec)
                        this.Text = number.ToString();
                    else
                        this.Text = number.ToString("X");
                }
                if (number < 0)
                {
                    this.Text = "0";
                    number = 0;
                }
                else if (number > 255)
                {
                    if (NumSystem == NumSys.Dec)
                        this.Text = "255";

                    else
                        this.Text = "FF";
                    number = 255;
                }
                this.SelectionStart = this.Text.Length;
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && NumSystem == NumSys.Dec && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!((e.KeyChar >= 65 && e.KeyChar <= 70) || (e.KeyChar >= 97 && e.KeyChar <= 102) || char.IsDigit(e.KeyChar)) && NumSystem == NumSys.Hex && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void RGBNumber_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Text))
                this.Text = "0";
        }
    }
}
