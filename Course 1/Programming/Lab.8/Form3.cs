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
    public partial class InputID : Form
    {
        public InputID()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ID = 0;
            bool ok = false;
            try { ID = Convert.ToInt32(textBox1.Text); ok = true; }
            catch { MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButtons.OK); }
            if (ok)
            {
                if (Core.CheckID(ID) != -1)
                {
                    Core.core_id = ID;
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else MessageBox.Show("Введенный ID не найден", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
