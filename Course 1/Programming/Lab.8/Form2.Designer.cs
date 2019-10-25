namespace Lab._8
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.table = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.midname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьИзБинарногоФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьИзТекстовогоДокументаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьИзMicrosoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьВНовыйБинарныйФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьВТекстовыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьВДокументMicrosoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьСтудентаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбранногоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.успеваемостьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.студентыСЗадолженностьюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дисциплиныСЗадолженностьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимРедактированияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.grouptext = new System.Windows.Forms.TextBox();
            this.adddis = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.label6 = new System.Windows.Forms.Label();
            this.addresstext = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.datetext = new System.Windows.Forms.DateTimePicker();
            this.midnametext = new System.Windows.Forms.TextBox();
            this.nametext = new System.Windows.Forms.TextBox();
            this.surnametext = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.enter = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.surname,
            this.name,
            this.midname,
            this.group,
            this.date,
            this.address,
            this.marks});
            this.table.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.table.Location = new System.Drawing.Point(315, 27);
            this.table.Name = "table";
            this.table.ReadOnly = true;
            this.table.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.table.ShowCellErrors = false;
            this.table.Size = new System.Drawing.Size(850, 576);
            this.table.TabIndex = 0;
            this.table.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.table_RowPrePaint);
            // 
            // id
            // 
            this.id.Frozen = true;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 30;
            // 
            // surname
            // 
            this.surname.Frozen = true;
            this.surname.HeaderText = "Фамилия";
            this.surname.Name = "surname";
            this.surname.ReadOnly = true;
            // 
            // name
            // 
            this.name.HeaderText = "Имя";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // midname
            // 
            this.midname.HeaderText = "Отчество";
            this.midname.Name = "midname";
            this.midname.ReadOnly = true;
            // 
            // group
            // 
            this.group.HeaderText = "Группа";
            this.group.Name = "group";
            this.group.ReadOnly = true;
            // 
            // date
            // 
            this.date.HeaderText = "Дата рождения";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // address
            // 
            this.address.HeaderText = "Адрес";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // marks
            // 
            this.marks.HeaderText = "Дисциплины";
            this.marks.Name = "marks";
            this.marks.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.базаДанныхToolStripMenuItem,
            this.успеваемостьToolStripMenuItem,
            this.режимРедактированияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1158, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.создатьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьИзБинарногоФайлаToolStripMenuItem,
            this.открытьИзТекстовогоДокументаToolStripMenuItem,
            this.открытьИзMicrosoftExcelToolStripMenuItem});
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            // 
            // открытьИзБинарногоФайлаToolStripMenuItem
            // 
            this.открытьИзБинарногоФайлаToolStripMenuItem.Name = "открытьИзБинарногоФайлаToolStripMenuItem";
            this.открытьИзБинарногоФайлаToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.открытьИзБинарногоФайлаToolStripMenuItem.Text = "Открыть из бинарного файла";
            this.открытьИзБинарногоФайлаToolStripMenuItem.Click += new System.EventHandler(this.открытьИзБинарногоФайлаToolStripMenuItem_Click);
            // 
            // открытьИзТекстовогоДокументаToolStripMenuItem
            // 
            this.открытьИзТекстовогоДокументаToolStripMenuItem.Name = "открытьИзТекстовогоДокументаToolStripMenuItem";
            this.открытьИзТекстовогоДокументаToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.открытьИзТекстовогоДокументаToolStripMenuItem.Text = "Открыть из текстового документа";
            this.открытьИзТекстовогоДокументаToolStripMenuItem.Click += new System.EventHandler(this.открытьИзТекстовогоДокументаToolStripMenuItem_Click);
            // 
            // открытьИзMicrosoftExcelToolStripMenuItem
            // 
            this.открытьИзMicrosoftExcelToolStripMenuItem.Name = "открытьИзMicrosoftExcelToolStripMenuItem";
            this.открытьИзMicrosoftExcelToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.открытьИзMicrosoftExcelToolStripMenuItem.Text = "Открыть из Microsoft Excel";
            this.открытьИзMicrosoftExcelToolStripMenuItem.Click += new System.EventHandler(this.открытьИзMicrosoftExcelToolStripMenuItem_Click);
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьВНовыйБинарныйФайлToolStripMenuItem,
            this.сохранитьВТекстовыToolStripMenuItem,
            this.сохранитьВДокументMicrosoftExcelToolStripMenuItem});
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.создатьToolStripMenuItem.Text = "Сохранить";
            // 
            // сохранитьВНовыйБинарныйФайлToolStripMenuItem
            // 
            this.сохранитьВНовыйБинарныйФайлToolStripMenuItem.Name = "сохранитьВНовыйБинарныйФайлToolStripMenuItem";
            this.сохранитьВНовыйБинарныйФайлToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.сохранитьВНовыйБинарныйФайлToolStripMenuItem.Text = "Сохранить в новый бинарный файл";
            this.сохранитьВНовыйБинарныйФайлToolStripMenuItem.Click += new System.EventHandler(this.сохранитьВНовыйБинарныйФайлToolStripMenuItem_Click);
            // 
            // сохранитьВТекстовыToolStripMenuItem
            // 
            this.сохранитьВТекстовыToolStripMenuItem.Name = "сохранитьВТекстовыToolStripMenuItem";
            this.сохранитьВТекстовыToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.сохранитьВТекстовыToolStripMenuItem.Text = "Сохранить в текстовый документ";
            this.сохранитьВТекстовыToolStripMenuItem.Click += new System.EventHandler(this.сохранитьВТекстовыToolStripMenuItem_Click);
            // 
            // сохранитьВДокументMicrosoftExcelToolStripMenuItem
            // 
            this.сохранитьВДокументMicrosoftExcelToolStripMenuItem.Name = "сохранитьВДокументMicrosoftExcelToolStripMenuItem";
            this.сохранитьВДокументMicrosoftExcelToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.сохранитьВДокументMicrosoftExcelToolStripMenuItem.Text = "Сохранить в документ Microsoft Excel";
            this.сохранитьВДокументMicrosoftExcelToolStripMenuItem.Click += new System.EventHandler(this.сохранитьВДокументMicrosoftExcelToolStripMenuItem_Click);
            // 
            // базаДанныхToolStripMenuItem
            // 
            this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьСтудентаToolStripMenuItem});
            this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.базаДанныхToolStripMenuItem.Text = "База данных";
            // 
            // удалитьСтудентаToolStripMenuItem
            // 
            this.удалитьСтудентаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбранногоToolStripMenuItem,
            this.поIDToolStripMenuItem});
            this.удалитьСтудентаToolStripMenuItem.Name = "удалитьСтудентаToolStripMenuItem";
            this.удалитьСтудентаToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.удалитьСтудентаToolStripMenuItem.Text = "Удалить студента";
            // 
            // выбранногоToolStripMenuItem
            // 
            this.выбранногоToolStripMenuItem.Name = "выбранногоToolStripMenuItem";
            this.выбранногоToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.выбранногоToolStripMenuItem.Text = "Выбранного";
            this.выбранногоToolStripMenuItem.Click += new System.EventHandler(this.выбранногоToolStripMenuItem_Click);
            // 
            // поIDToolStripMenuItem
            // 
            this.поIDToolStripMenuItem.Name = "поIDToolStripMenuItem";
            this.поIDToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.поIDToolStripMenuItem.Text = "По ID";
            this.поIDToolStripMenuItem.Click += new System.EventHandler(this.поIDToolStripMenuItem_Click);
            // 
            // успеваемостьToolStripMenuItem
            // 
            this.успеваемостьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.студентыСЗадолженностьюToolStripMenuItem,
            this.дисциплиныСЗадолженностьToolStripMenuItem});
            this.успеваемостьToolStripMenuItem.Name = "успеваемостьToolStripMenuItem";
            this.успеваемостьToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.успеваемостьToolStripMenuItem.Text = "Успеваемость";
            // 
            // студентыСЗадолженностьюToolStripMenuItem
            // 
            this.студентыСЗадолженностьюToolStripMenuItem.Name = "студентыСЗадолженностьюToolStripMenuItem";
            this.студентыСЗадолженностьюToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.студентыСЗадолженностьюToolStripMenuItem.Text = "Студенты с задолженностью";
            this.студентыСЗадолженностьюToolStripMenuItem.Click += new System.EventHandler(this.студентыСЗадолженностьюToolStripMenuItem_Click);
            // 
            // дисциплиныСЗадолженностьToolStripMenuItem
            // 
            this.дисциплиныСЗадолженностьToolStripMenuItem.Name = "дисциплиныСЗадолженностьToolStripMenuItem";
            this.дисциплиныСЗадолженностьToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.дисциплиныСЗадолженностьToolStripMenuItem.Text = "Дисциплины с задолженность";
            this.дисциплиныСЗадолженностьToolStripMenuItem.Click += new System.EventHandler(this.дисциплиныСЗадолженностьToolStripMenuItem_Click);
            // 
            // режимРедактированияToolStripMenuItem
            // 
            this.режимРедактированияToolStripMenuItem.Name = "режимРедактированияToolStripMenuItem";
            this.режимРедактированияToolStripMenuItem.Size = new System.Drawing.Size(149, 20);
            this.режимРедактированияToolStripMenuItem.Text = "Режим редактирования";
            this.режимРедактированияToolStripMenuItem.Click += new System.EventHandler(this.режимРедактированияToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(12, 541);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 53);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Удаление";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(152, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Удалить выделенные";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(10, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Удалить всё";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.grouptext);
            this.groupBox1.Controls.Add(this.adddis);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.addresstext);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datetext);
            this.groupBox1.Controls.Add(this.midnametext);
            this.groupBox1.Controls.Add(this.nametext);
            this.groupBox1.Controls.Add(this.surnametext);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 484);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавление";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ПИ-16-1",
            "ПИ-16-2",
            "ПИ-17-1",
            "ПИ-17-2",
            "ПИ-18-1",
            "ПИ-18-2",
            "ПИ-18-3",
            "БИ-16-1",
            "БИ-16-2",
            "БИ-17-1",
            "БИ-17-2",
            "БИ-18-1",
            "БИ-18-2",
            "БИ-18-3",
            "Э-16-1",
            "Э-16-2",
            "Э-16-3",
            "Э-17-1",
            "Э-17-2",
            "Э-17-3",
            "Э-18-1",
            "Э-18-2",
            "М-16-1",
            "М-16-2",
            "М-16-3",
            "М-17-1",
            "М-17-2",
            "М-17-3",
            "И-16-1",
            "И-17-1",
            "И-18-1",
            "Ю-16-1",
            "Ю-16-2",
            "Ю-17-1",
            "Ю-17-2",
            "Ю-18-1",
            "Ю-18-2",
            "Ю-18-3",
            "УБ-18-1",
            "УБ-18-2",
            "УБ-18-3"});
            this.comboBox1.Location = new System.Drawing.Point(78, 118);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 37;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // grouptext
            // 
            this.grouptext.Location = new System.Drawing.Point(78, 119);
            this.grouptext.Name = "grouptext";
            this.grouptext.Size = new System.Drawing.Size(100, 20);
            this.grouptext.TabIndex = 36;
            this.grouptext.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // adddis
            // 
            this.adddis.Location = new System.Drawing.Point(6, 207);
            this.adddis.Name = "adddis";
            this.adddis.Size = new System.Drawing.Size(136, 23);
            this.adddis.TabIndex = 32;
            this.adddis.Text = "Добавить дисциплину";
            this.adddis.UseVisualStyleBackColor = true;
            this.adddis.Click += new System.EventHandler(this.adddis_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.vScrollBar1);
            this.groupBox3.Location = new System.Drawing.Point(6, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(285, 242);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Дисциплины";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(265, 16);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 223);
            this.vScrollBar1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Отчество";
            // 
            // addresstext
            // 
            this.addresstext.Location = new System.Drawing.Point(124, 181);
            this.addresstext.MaxLength = 35;
            this.addresstext.Name = "addresstext";
            this.addresstext.Size = new System.Drawing.Size(138, 20);
            this.addresstext.TabIndex = 14;
            this.addresstext.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Дата рождения";
            // 
            // datetext
            // 
            this.datetext.Location = new System.Drawing.Point(124, 147);
            this.datetext.MaxDate = new System.DateTime(2047, 12, 31, 0, 0, 0, 0);
            this.datetext.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.datetext.Name = "datetext";
            this.datetext.Size = new System.Drawing.Size(138, 20);
            this.datetext.TabIndex = 8;
            // 
            // midnametext
            // 
            this.midnametext.Location = new System.Drawing.Point(78, 90);
            this.midnametext.MaxLength = 25;
            this.midnametext.Name = "midnametext";
            this.midnametext.Size = new System.Drawing.Size(190, 20);
            this.midnametext.TabIndex = 7;
            this.midnametext.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // nametext
            // 
            this.nametext.Location = new System.Drawing.Point(78, 59);
            this.nametext.MaxLength = 25;
            this.nametext.Name = "nametext";
            this.nametext.Size = new System.Drawing.Size(190, 20);
            this.nametext.TabIndex = 6;
            this.nametext.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // surnametext
            // 
            this.surnametext.Location = new System.Drawing.Point(78, 27);
            this.surnametext.MaxLength = 25;
            this.surnametext.Name = "surnametext";
            this.surnametext.Size = new System.Drawing.Size(190, 20);
            this.surnametext.TabIndex = 5;
            this.surnametext.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Адрес проживания";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Группа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Имя";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(219, 517);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(75, 23);
            this.enter.TabIndex = 16;
            this.enter.Text = "Добавить";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.enter_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 600);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.table);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "Работа с базой данных";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
            this.ResizeEnd += new System.EventHandler(this.MainMenu_AnyResize);
            this.Resize += new System.EventHandler(this.MainMenu_AnyResize);
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьСтудентаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбранногоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem успеваемостьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem студентыСЗадолженностьюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дисциплиныСЗадолженностьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button adddis;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox addresstext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datetext;
        private System.Windows.Forms.TextBox midnametext;
        private System.Windows.Forms.TextBox nametext;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button enter;
        protected System.Windows.Forms.TextBox surnametext;
        private System.Windows.Forms.TextBox grouptext;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn midname;
        private System.Windows.Forms.DataGridViewTextBoxColumn group;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn marks;
        private System.Windows.Forms.ToolStripMenuItem сохранитьВНовыйБинарныйФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьВТекстовыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьВДокументMicrosoftExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьИзБинарногоФайлаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьИзТекстовогоДокументаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьИзMicrosoftExcelToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem режимРедактированияToolStripMenuItem;
    }
}