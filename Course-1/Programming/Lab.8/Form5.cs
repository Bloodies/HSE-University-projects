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
    public partial class BadStud : Form
    {
        public BadStud()
        {
            InitializeComponent();
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            BadStud_AnyResize(new object(), null);
            WritingInGrid();
        }

        private void BadStud_AnyResize(object sender, EventArgs e)
        {
            this.table.Width = this.Width - table.Left * 3;
            this.table.Height = this.Height - table.Top * 3;
            int width = (this.table.Width - this.id.Width - 40) / 7;
            this.surname.Width = width;
            this.name.Width = width;
            this.midname.Width = width;
            this.group.Width = width;
            this.date.Width = width;
            this.address.Width = width;
            this.marks.Width = width;
        }

        public void WritingInGrid()
        {
            while (table.Rows.Count != 0)
            {
                table.Rows.Remove(table.Rows[table.Rows.Count - 1]);
            }

            Person[] pers = Core.ReadBadStudents();
            for (int i = 0; i < pers.Length; i++)
            {
                if (pers[i] != null && !pers[i].delete)
                    table.Rows.Add(pers[i].id, pers[i].surname, pers[i].name, pers[i].midname, pers[i].group, pers[i].date.ToString("dd.MM.yyyy"), pers[i].address, pers[i].marks.ToString());
            }
        }
    }
}
