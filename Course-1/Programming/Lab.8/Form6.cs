using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab._8
{
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
            if (Core.new_file)
            {
                label1.Text = "Введите название создаваемого файла (без расширения)";
                this.Text = "Создание новного файла";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainMenu form = (MainMenu)Owner;

            if (Core.new_file)
            {
                MainMenu.text = textBox1.Text;
            }
            else
            {
                form.course = textBox1.Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
