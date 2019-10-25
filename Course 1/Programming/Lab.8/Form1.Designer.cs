namespace Lab._8
{
    partial class AddStud
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            MainMenu form = (MainMenu)this.Owner;
            form.WritingInGrid();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.surname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.midname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.group = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.enter = new System.Windows.Forms.Button();
            this.to_database = new System.Windows.Forms.Button();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.добавитьДисциплинуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // surname
            // 
            this.surname.Location = new System.Drawing.Point(12, 53);
            this.surname.Multiline = true;
            this.surname.Name = "surname";
            this.surname.Size = new System.Drawing.Size(224, 20);
            this.surname.TabIndex = 0;
            this.surname.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(242, 53);
            this.name.Multiline = true;
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(224, 20);
            this.name.TabIndex = 2;
            this.name.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Имя";
            // 
            // midname
            // 
            this.midname.Location = new System.Drawing.Point(472, 53);
            this.midname.Multiline = true;
            this.midname.Name = "midname";
            this.midname.Size = new System.Drawing.Size(224, 20);
            this.midname.TabIndex = 4;
            this.midname.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(472, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Группа";
            // 
            // group
            // 
            this.group.Location = new System.Drawing.Point(12, 114);
            this.group.Multiline = true;
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(100, 20);
            this.group.TabIndex = 7;
            this.group.TextChanged += new System.EventHandler(this.main_TextChanged);
            this.group.Leave += new System.EventHandler(this.group_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(167, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Дата рождения";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Адрес";
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(12, 178);
            this.address.Multiline = true;
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(684, 20);
            this.address.TabIndex = 11;
            this.address.TextChanged += new System.EventHandler(this.main_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Дисциплины";
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(363, 367);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(103, 54);
            this.enter.TabIndex = 14;
            this.enter.Text = "Ввод";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // to_database
            // 
            this.to_database.Location = new System.Drawing.Point(593, 367);
            this.to_database.Name = "to_database";
            this.to_database.Size = new System.Drawing.Size(103, 54);
            this.to_database.TabIndex = 15;
            this.to_database.Text = "Отмена";
            this.to_database.UseVisualStyleBackColor = true;
            this.to_database.Click += new System.EventHandler(this.to_database_Click);
            // 
            // date
            // 
            this.date.Location = new System.Drawing.Point(170, 114);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(200, 20);
            this.date.TabIndex = 16;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьДисциплинуToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(710, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // добавитьДисциплинуToolStripMenuItem
            // 
            this.добавитьДисциплинуToolStripMenuItem.Name = "добавитьДисциплинуToolStripMenuItem";
            this.добавитьДисциплинуToolStripMenuItem.Size = new System.Drawing.Size(141, 20);
            this.добавитьДисциплинуToolStripMenuItem.Text = "Добавить дисциплину";
            this.добавитьДисциплинуToolStripMenuItem.Click += new System.EventHandler(this.добавитьДисциплинуToolStripMenuItem_Click);
            // 
            // AddStud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 434);
            this.Controls.Add(this.date);
            this.Controls.Add(this.to_database);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.address);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.group);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.midname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.surname);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(726, 473);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(726, 473);
            this.Name = "AddStud";
            this.Text = "Добавление студента";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox surname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox midname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox group;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox address;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Button to_database;
        private System.Windows.Forms.DateTimePicker date;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьДисциплинуToolStripMenuItem;
    }
}

