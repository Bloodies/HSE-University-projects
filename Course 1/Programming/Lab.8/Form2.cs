using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab._8
{
    public partial class MainMenu : Form
    {
        public static int count_courses;
        public const int start_left = 12, start_text_left = start_left + 150;
        public string course = "";
        public static string text = "";
        int index;

        public MainMenu()
        {
            InitializeComponent();
            table.AllowUserToAddRows = false;
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            MainMenu_AnyResize(new Object(), null);
            enter.Enabled = false;

            string[] lessons = Core.AllLessons();

            count_courses = lessons.Length;

            datetext.Value = new DateTime(2000, 01, 01);

            Label label = new Label
            {
                Location = new Point(start_text_left, 20),
                Name = "label_mark",
                Text = "Оценки"
            };

            groupBox3.Controls.Add(label);

            for (int i = 0; i < lessons.Length; i++)
            {
                int start_top = 45 + i * (17 + 5);

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

                groupBox3.Controls.Add(textBox);
                groupBox3.Controls.Add(checkBox);
            }

            if (Core.student != null)
            {
                Person pers = Core.student;
                this.Text = "Изменение информации о студенте";
                this.nametext.Text = pers.name;
                this.surnametext.Text = pers.surname;
                this.midnametext.Text = pers.midname;
                this.grouptext.Text = pers.group;
                this.datetext.Value = pers.date;
                this.addresstext.Text = pers.address;
                enter.Enabled = true;

                Mark help = pers.marks;
                while (help != null)
                {
                    for (int i = 0; i < lessons.Length; i++)
                    {
                        if ((groupBox3.Controls["checkBox" + (i + 1)] as CheckBox).Text == help.name)
                        {
                            CheckBox checkbox = groupBox3.Controls["checkBox" + (i + 1)] as CheckBox;
                            checkbox.Checked = true;
                            TextBox textbox = groupBox3.Controls["mark" + (i + 1)] as TextBox;
                            textbox.Text = Convert.ToString(help.mark);
                        }
                    }

                    help = help.next;
                }
            }
            WritingInGrid();
        }
        private void enter_Click_1(object sender, EventArgs e)
        {
            MainMenu form = (MainMenu)this.Owner;
            DateTime dt = datetext.Value;
            Mark mk = new Mark(), help_mk = mk;
            Person help_pers = null;

            for (int i = 0; i < count_courses; i++)
            {
                if ((groupBox3.Controls["checkBox" + (i + 1)] as CheckBox).Checked && (groupBox3.Controls["mark" + (i + 1)].Text != ""))
                {
                    if (mk == null || mk.name == null)
                    {
                        mk = new Mark(Convert.ToInt32(groupBox3.Controls["mark" + (i + 1)].Text), groupBox3.Controls["checkBox" + (i + 1)].Text);
                        help_mk = mk;
                    }
                    else
                    {
                        mk.next = new Mark(Convert.ToInt32(groupBox3.Controls["mark" + (i + 1)].Text), groupBox3.Controls["checkBox" + (i + 1)].Text);
                        mk = mk.next;
                    }
                }
            }
            mk = help_mk;

            if (Core.student != null)
            {
                help_pers = new Person(Core.student.id, Core.StringName(nametext.Text), Core.StringName(surnametext.Text), Core.StringName(midnametext.Text), grouptext.Text, addresstext.Text, dt, mk);
                Core.EditStudent(help_pers);
            }
            else
            {
                help_pers = new Person(Core.FindFreeID(), Core.StringName(nametext.Text), Core.StringName(surnametext.Text), Core.StringName(midnametext.Text), grouptext.Text, addresstext.Text, dt, mk);
                if (Core.core_id == -1)
                    Core.AddStudent(help_pers);
                else Core.AddStudent(help_pers, Core.core_id);
            }
            WritingInGrid();

            Core.student = null;
        }


        private void Check()
        {
            if ((surnametext.Text != "") && (nametext.Text != "") && (grouptext.Text != "") && (datetext.Text != "")) enter.Enabled = true;
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
                        nametext.Focus();
                        break;
                    case "name":
                        midnametext.Focus();
                        break;
                    case "midname":
                        grouptext.Focus();
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
                            datetext.Focus();
                            enter.Enabled = true;
                        }
                        break;
                    case "date":
                        addresstext.Focus();
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

            groupBox3.Controls[str].Enabled = !groupBox3.Controls[str].Enabled;
        }

        private void adddis_Click(object sender, EventArgs e)
        {
            AddCourse form = new AddCourse
            {
                Owner = this
            };
            form.ShowDialog();

            int start_top = 45 + count_courses * (17 + 5);

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
            groupBox3.Controls.Add(textBox);
            groupBox3.Controls.Add(checkBox);
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

        private void вКонецToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.core_id = -1;
            AddStud f = new AddStud
            {
                Owner = this
            };
            f.ShowDialog();
        }

        public void WritingInGrid()
        {
            bool ok = false;
            Person[] pers = new Person[0];
            try { pers = Core.ReadAllStudents(Core.core_file); ok = true; }
            catch { MessageBox.Show("В файле сохранены данные другого типа", "Ошибка открытия файла", MessageBoxButtons.OK); }
            if (ok)
            {
                while (table.Rows.Count != 0)
                {
                    table.Rows.Remove(table.Rows[table.Rows.Count - 1]);
                }

                for (int i = 0; i < pers.Length; i++)
                {
                    if (pers[i] != null && !pers[i].delete)
                        table.Rows.Add(pers[i].id, pers[i].surname, pers[i].name, pers[i].midname, pers[i].group, pers[i].date.ToString("dd.MM.yyyy"), pers[i].address, pers[i].marks.ToString());
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.DeleteDataBase(Core.core_file);
            while (table.Rows.Count != 0)
            {
                table.Rows.Remove(table.Rows[table.Rows.Count - 1]);
            }
        }

        private void MainMenu_AnyResize(object sender, EventArgs e)
        {
            this.table.Width = this.Width - (table.Left - 10);
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

        private void выбранногоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (table.SelectedCells.Count != 0)
            {
                int selRowNum = table.SelectedCells[0].RowIndex;
                Core.DeleteStudent(Convert.ToInt32(table.Rows[selRowNum].Cells[0].Value.ToString()));
                table.Rows.Remove(table.Rows[selRowNum]);
                if (Core.DeletedMoreThanHalfCount()) Core.CleanFile();
            }
        }

        private void поIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputID form = new InputID();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK) Core.DeleteStudent(Core.core_id);
            WritingInGrid();
        }

        private void включитьИзмененияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = (ToolStripMenuItem)sender;
            if (btn.Text == "Режим редактирования")
            {
                this.table.ReadOnly = false;
                this.id.ReadOnly = true;
                btn.Text = "Выйти из режима редактирования";
            }
            else
            {
                this.table.ReadOnly = true;
                this.id.ReadOnly = true;
                btn.Text = "Режим редактирования";
            }
        }

        private void вУказаннуюПозициюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputNum form = new InputNum
            {
                Owner = this
            };
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                AddStud f = new AddStud
                {
                    Owner = this
                };
                f.ShowDialog();
            }
        }

        private void студентыСЗадолженностьюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BadStud form = new BadStud();
            form.ShowDialog();
        }

        private void дисциплиныСЗадолженностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Core.BadLessons(), "Дисциплины с задолженностью", MessageBoxButtons.OK);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            grouptext.Text = comboBox1.SelectedItem.ToString();
        }

        private void table_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.table.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.table.Rows[index].HeaderCell.Value = indexStr;
        }

        private void сохранитьВНовыйБинарныйФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowNewFolderButton = false;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                string str = FBD.SelectedPath;
                Core.new_file = true;
                AddCourse form = new AddCourse();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    str += @"\" + MainMenu.text + ".bin";
                    Core.core_file = str;
                    WritingInGrid();
                }
            }
        }

        private void сохранитьВТекстовыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStr;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "Текстовый документ (*.txt)|*.txt",
                DefaultExt = "*.txt",
                Title = "Укажите директорию и имя файла для сохранения"
            };
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStr = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter writetxt = new StreamWriter(myStr);
                    try
                    {
                        for (int i = 0; i < table.RowCount - 1; i++)
                        {
                            for (int j = 0; j < table.ColumnCount; j++)
                            {
                                string data = table.Rows[i].Cells[j].Value.ToString().Replace("$", "[simbol]");
                                writetxt.Write(data + "$");
                            }
                            writetxt.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        writetxt.Close();
                    }
                }
            }
        }

        private void сохранитьВДокументMicrosoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int i = 1; i < table.RowCount + 1; i++)
            {
                for (int j = 1; j < table.ColumnCount + 1; j++)
                {
                    worksheet.Rows[i].Columns[j] = table.Rows[i - 1].Cells[j - 1].Value;
                }
            }

            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "MS Excel dosuments (*.xlsx)|*.xlsx",
                DefaultExt = "*.xlsx",
                Title = "Укажите директорию и имя файла для сохранения"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(sfd.FileName);
            }
            else
            {
                excelapp.Quit();
            }
            excelapp.Quit();
        }

        private void открытьИзБинарногоФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы bin|*.bin";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in OPF.FileNames)
                {
                    bool ok = false;
                    try { System.IO.FileStream f = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite); ; ok = true; f.Close(); }
                    catch (UnauthorizedAccessException) { MessageBox.Show("Отказано в доступе", "Ошибка доступа", MessageBoxButtons.OK); }
                    catch { MessageBox.Show("Неизвестная ошибка", "Ошибка", MessageBoxButtons.OK); }
                    if (ok)
                    {
                        Core.core_file = file;
                        WritingInGrid();
                    }
                }
            }
        }

        private void открытьИзТекстовогоДокументаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream mystr = null;
            openFileDialog1.Filter = "Текстовый документ (*.txt)|*.txt";
            openFileDialog1.Title = "Укажите директорию и имя файла";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((mystr = openFileDialog1.OpenFile()) != null)
                {
                    StreamReader readtxt = new StreamReader(mystr);
                    string[] str;
                    int num = 0;
                    try
                    {
                        string[] str1 = readtxt.ReadToEnd().Split('\n');
                        num = str1.Count();
                        table.RowCount = num;
                        for (int i = 0; i < num; i++)
                        {
                            str = str1[i].Split('$');
                            for (int j = 0; j < table.ColumnCount; j++)
                            {
                                try
                                {
                                    string data = str[j].Replace("[symbol]", "$");
                                    table.Rows[i].Cells[j].Value = data;
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        readtxt.Close();
                    }
                }
            }
        }

        private void открытьИзMicrosoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл Excel|*.XLSX;*.XLS";
            openDialog.ShowDialog();

            try
            {
                Excel.Application ObjExcel = new Excel.Application();
                Excel._Workbook ObjWorkBook = ObjExcel.Workbooks.Open(openDialog.FileName);
                Excel.Worksheet ObjWorkSheet = ObjExcel.ActiveSheet as Excel.Worksheet;
                Excel.Range rg = null;

                Int32 row = 1;
                table.Rows.Clear();
                List<String> arr = new List<string>();
                while (ObjWorkSheet.get_Range("a" + row, "a" + row).Value != null)
                {
                    rg = ObjWorkSheet.get_Range("a" + row, "u" + row);
                    foreach (Excel.Range item in rg)
                    {
                        try
                        {
                            arr.Add(item.Value.ToString().Trim());
                        }
                        catch { arr.Add(""); }
                    }
                    table.Rows.Add(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], arr[8]);
                    arr.Clear();
                    row++;
                }
                MessageBox.Show("Файл успешно считан!", "Считывание файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка при считывании excel файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void режимРедактированияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = (ToolStripMenuItem)sender;
            if (btn.Text == "Режим редактирования")
            {
                this.table.ReadOnly = false;
                this.id.ReadOnly = true;
                btn.Text = "Выйти из режима редактирования";

            }
            else
            {
                this.table.ReadOnly = true;
                this.id.ReadOnly = true;
                btn.Text = "Режим редактирования";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int ind = table.SelectedCells[0].RowIndex;
            table.Rows.RemoveAt(ind);
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Core.CleanFile();
        }

    }
}
