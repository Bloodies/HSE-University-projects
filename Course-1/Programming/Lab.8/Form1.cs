using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab._8
{
    public partial class AddStud : Form
    {
        public static int count_courses;
        public const int start_left = 12, start_text_left = start_left + 150;
        public string course = "";

        public AddStud()
        {
            InitializeComponent();
            enter.Enabled = false;

            string[] lessons = Core.AllLessons();

            count_courses = lessons.Length;

            date.Value = new DateTime(2000, 01, 01);

            Label label = new Label
            {
                Location = new Point(start_text_left, label7.Top),
                Name = "label_mark",
                Text = "Оценки"
            };

            this.Controls.Add(label);

            for (int i = 0; i < lessons.Length; i++)
            {
                int start_top = label7.Top + (label7.Height + 10) + i * (17 + 5);

                CheckBox checkBox = new CheckBox
                {
                    Location = new Point(start_left, start_top),
                    Name = "checkBox" + (i + 1),
                    Size = new Size(80, 17),
                    Text = lessons[i],
                    AutoSize = true
                };
                checkBox.CheckedChanged += new EventHandler(lessonCheckBox_CheckedChanged);

                TextBox textBox = new TextBox
                {
                    Location = new Point(start_text_left, start_top),
                    Name = "mark" + (i + 1),
                    Enabled = false
                };
                textBox.TextChanged += new EventHandler(this.CheckNum);

                this.Controls.Add(textBox);
                this.Controls.Add(checkBox);
            }

            if (Core.student != null)
            {
                Person pers = Core.student;
                this.Text = "Изменение информации о студенте";
                this.name.Text = pers.name;
                this.surname.Text = pers.surname;
                this.midname.Text = pers.midname;
                this.group.Text = pers.group;
                this.date.Value = pers.date;
                this.address.Text = pers.address;
                enter.Enabled = true;

                Mark help = pers.marks;
                while (help != null)
                {
                    for (int i = 0; i < lessons.Length; i++)
                    {
                        if ((this.Controls["checkBox" + (i + 1)] as CheckBox).Text == help.name)
                        {
                            CheckBox checkbox = this.Controls["checkBox" + (i + 1)] as CheckBox;
                            checkbox.Checked = true;
                            TextBox textbox = this.Controls["mark" + (i + 1)] as TextBox;
                            textbox.Text = Convert.ToString(help.mark);
                        }
                    }

                    help = help.next;
                }
            }
        }

        private void enter_Click(object sender, EventArgs e)
        {
            MainMenu form = (MainMenu)this.Owner;
            DateTime dt = date.Value;
            Mark mk = new Mark(), help_mk = mk;
            Person help_pers = null;

            for (int i = 0; i < count_courses; i++)
            {
                if ((this.Controls["checkBox" + (i + 1)] as CheckBox).Checked && (this.Controls["mark" + (i + 1)].Text != ""))
                {
                    if (mk == null || mk.name == null)
                    {
                        mk = new Mark(Convert.ToInt32(this.Controls["mark" + (i + 1)].Text), this.Controls["checkBox" + (i + 1)].Text);
                        help_mk = mk;
                    }
                    else
                    {
                        mk.next = new Mark(Convert.ToInt32(this.Controls["mark" + (i + 1)].Text), this.Controls["checkBox" + (i + 1)].Text);
                        mk = mk.next;
                    }
                }
            }
            mk = help_mk;

            if (Core.student != null)
            {
                help_pers = new Person(Core.student.id, Core.StringName(name.Text), Core.StringName(surname.Text), Core.StringName(midname.Text), group.Text, address.Text, dt, mk);
                Core.EditStudent(help_pers);
            }
            else
            {
                help_pers = new Person(Core.FindFreeID(), Core.StringName(name.Text), Core.StringName(surname.Text), Core.StringName(midname.Text), group.Text, address.Text, dt, mk);
                if (Core.core_id == -1)
                    Core.AddStudent(help_pers);
                else Core.AddStudent(help_pers, Core.core_id);
            }
            form.WritingInGrid();

            this.Dispose();
            Core.student = null;
        }

        private void Check()
        {
            if ((surname.Text != "") && (name.Text != "") && (group.Text != "") && (date.Text != "")) enter.Enabled = true;
            else enter.Enabled = false;
        }

        private void main_TextChanged(object sender, EventArgs e)
        {
            Check();
            TextBox sndr = (TextBox)sender;
            string text = sndr.Text;
            if (sndr.Name == "name" || sndr.Name == "surname" || sndr.Name == "midname")
            {
                //try
                //{
                //    string help = "" + text[0];
                //    help.ToUpper();
                //    for (int i = 1; i < text.Length; i++) help += text[i];
                //    sndr.Text = "";
                //    for (int i = 0; i < text.Length; i++) sndr.Text += help[i];
                //}
                //catch { }
                string pattern = @"^[A-Z|А-Я]{1}[A-Z|А-Я|-|']*$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                if (text != "" && !regex.IsMatch(text.Trim()))
                {
                    MessageBox.Show("В имени, фамилии или отчестве могут присутствовать только буквы русского или английского алфавитов, дефис или апостроф", "Ошибка ввода", MessageBoxButtons.OK);
                    string str = "";
                    int start = sndr.SelectionStart;
                    for (int i = 0; i < text.Length; i++)
                    {
                        string help = String.Copy(str) + text[i];
                        if (regex.IsMatch(help.Trim())) str = String.Copy(help);
                    }
                    sndr.Text = String.Copy(str);
                    sndr.SelectionStart = start - 1;
                }
            }

            if ((text != "") && ((text[text.Length - 1] == '\n') || ((text[text.Length - 1] == ' ') && (sndr.Name != "address")) || (text[text.Length - 1] == '\t')))
            {
                sndr.Text = sndr.Text.Trim();
                switch (sndr.Name)
                {
                    case "surname":
                        name.Focus();
                        break;
                    case "name":
                        midname.Focus();
                        break;
                    case "midname":
                        group.Focus();
                        break;
                    case "group":
                        string pattern = @"^[A-Z|А-Я]+[-]{1}\d{2}[-]{1}\d{1}$";
                        Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);
                        if (text != "" && !regex.IsMatch(text.Trim()))
                        {
                            MessageBox.Show("Название группы строится по формату А-00-0, где вместо А может быть любое количество заглавных букв, а место 0 любая цифра", "Ошибка ввода", MessageBoxButtons.OK);
                            enter.Enabled = false;
                        }
                        else
                        {
                            date.Focus();
                            enter.Enabled = true;
                        }
                        break;
                    case "date":
                        address.Focus();
                        break;
                    case "address":
                        enter.Focus();
                        break;
                }
                //string str = String.Copy(sndr.Text);
                //sndr.Text = "";
                //for (int i = 0; i < str.Length - 1; i++) sndr.Text += str[i];

            }
        }

        public void lessonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox sndr = (CheckBox)sender;
            string str = "mark" + sndr.Name[sndr.Name.Length - 1];

            this.Controls[str].Enabled = !this.Controls[str].Enabled;
        }

        private void to_database_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void group_Leave(object sender, EventArgs e)
        {
            string pattern = @"^[A-Z|А-Я]+[-]{1}\d{2}[-]{1}\d{1}$";
            Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);
            if (group.Text != "" && !regex.IsMatch(group.Text.Trim()))
            {
                MessageBox.Show("Название группы строится по формату А-00-0, где вместо А может быть любое количество заглавных букв, а место 0 любая цифра", "Ошибка ввода", MessageBoxButtons.OK);
                enter.Enabled = false;
            }
            else enter.Enabled = true;
        }

        private void добавитьДисциплинуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCourse form = new AddCourse
            {
                Owner = this
            };
            form.ShowDialog();

            int start_top = label7.Top + (label7.Height + 10) + count_courses * (17 + 5);

            CheckBox checkBox = new CheckBox
            {
                Location = new Point(start_left, start_top),
                Name = "checkBox" + ++count_courses,
                Size = new Size(80, 17),
                Text = course,
                AutoSize = true
            };
            checkBox.CheckedChanged += new EventHandler(lessonCheckBox_CheckedChanged);

            TextBox textBox = new TextBox
            {
                Location = new Point(start_text_left, start_top),
                Name = "mark" + count_courses,
                Enabled = false
            };

            textBox.TextChanged += new EventHandler(this.CheckNum);
            this.Controls.Add(textBox);
            this.Controls.Add(checkBox);
        }

        private void CheckNum(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string pattern = @"^\d{0,2}$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (textBox.Text != "" && (!regex.IsMatch(textBox.Text.Trim()) || Convert.ToInt32(textBox.Text) > 10))
            {
                MessageBox.Show("Оценки за дисциплину - числа от 0 до 10", "Ошибка ввода", MessageBoxButtons.OK);
                string str = "";
                int start = textBox.SelectionStart;
                for (int i = 0; i < textBox.Text.Length; i++)
                {
                    string help = String.Copy(str) + textBox.Text[i];
                    if (regex.IsMatch(help.Trim()) && Convert.ToInt32(help) < 10) str = String.Copy(help);
                }
                textBox.Text = String.Copy(str);
                textBox.SelectionStart = start - 1;
            }
        }
    }
}
