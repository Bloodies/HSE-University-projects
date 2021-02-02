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
    public partial class InputNum : Form
    {
        public InputNum()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Num = 0;
            MainMenu form = (MainMenu)Owner;
            bool ok = false;
            try { Num = Convert.ToInt32(textBox1.Text); ok = true; }
            catch { MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButtons.OK); }
            if (ok)
            {
                if ((Num <= (form.table.Rows.Count + 1)) && (Num > 0))
                {
                    Core.core_id = Num;
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else MessageBox.Show("Веденное значение находится вне диапозона базы данных", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
