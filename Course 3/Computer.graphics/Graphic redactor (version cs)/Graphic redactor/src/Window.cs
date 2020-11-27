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
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }
        #region Вывод заданий
        private void Task_1_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = true;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = false;
        }

        private void Task_2_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = true;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = false;
        }

        private void Task_3_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = true;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = false;
        }

        private void Task_4_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = true;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = false;
        }

        private void Task_5_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = true;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = false;
        }

        private void Task_6_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = true;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = false;
        }

        private void Task_7_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = true;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = false;
        }

        private void Task_8_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = true;
            //Task_9_group.Visible = false;
        }

        private void Task_9_show_Click(object sender, EventArgs e)
        {
            Task_1_group.Visible = false;
            Task_2_group.Visible = false;
            //Task_3_group.Visible = false;
            //Task_4_group.Visible = false;
            //Task_5_group.Visible = false;
            //Task_6_group.Visible = false;
            //Task_7_group.Visible = false;
            //Task_8_group.Visible = false;
            //Task_9_group.Visible = true;
        }
        #endregion
    }
}