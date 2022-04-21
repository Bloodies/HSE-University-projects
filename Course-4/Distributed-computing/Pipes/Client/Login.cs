using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public string login;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text == "")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.login = tbLogin.Text;
            }
        }
    }
}
