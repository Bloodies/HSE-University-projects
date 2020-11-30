
namespace Graphic_redactor.src
{
    partial class Window
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
            this.Main_menu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитькакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отменадействияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отменадействияToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.выделитьвсеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pic_close = new System.Windows.Forms.PictureBox();
            this.Icon_pic = new System.Windows.Forms.PictureBox();
            this.Pic_maximize = new System.Windows.Forms.PictureBox();
            this.Pic_minimize = new System.Windows.Forms.PictureBox();
            this.Main_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_maximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_minimize)).BeginInit();
            this.SuspendLayout();
            // 
            // Main_menu
            // 
            this.Main_menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Main_menu.Dock = System.Windows.Forms.DockStyle.None;
            this.Main_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem1,
            this.правкаToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.Main_menu.Location = new System.Drawing.Point(33, 4);
            this.Main_menu.Name = "Main_menu";
            this.Main_menu.Size = new System.Drawing.Size(180, 24);
            this.Main_menu.TabIndex = 0;
            this.Main_menu.Text = "Main_menu";
            this.Main_menu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseDown);
            this.Main_menu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseMove);
            this.Main_menu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseUp);
            // 
            // файлToolStripMenuItem1
            // 
            this.файлToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.файлToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.toolStripSeparator,
            this.сохранитьToolStripMenuItem,
            this.сохранитькакToolStripMenuItem,
            this.toolStripSeparator1,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Control;
            this.файлToolStripMenuItem1.Name = "файлToolStripMenuItem1";
            this.файлToolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem1.Text = "&Файл";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.создатьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.создатьToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.edit;
            this.создатьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(170, 6);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.сохранитьToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.save;
            this.сохранитьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // сохранитькакToolStripMenuItem
            // 
            this.сохранитькакToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.сохранитькакToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.сохранитькакToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.eye;
            this.сохранитькакToolStripMenuItem.Name = "сохранитькакToolStripMenuItem";
            this.сохранитькакToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.сохранитькакToolStripMenuItem.Text = "Сохранить &как";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.выходToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.выходToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.window_close;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отменадействияToolStripMenuItem,
            this.отменадействияToolStripMenuItem1,
            this.toolStripSeparator3,
            this.копироватьToolStripMenuItem,
            this.вставкаToolStripMenuItem,
            this.toolStripSeparator4,
            this.выделитьвсеToolStripMenuItem});
            this.правкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // отменадействияToolStripMenuItem
            // 
            this.отменадействияToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.отменадействияToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.undo_alt;
            this.отменадействияToolStripMenuItem.Name = "отменадействияToolStripMenuItem";
            this.отменадействияToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.отменадействияToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.отменадействияToolStripMenuItem.Text = "Отмена действия";
            // 
            // отменадействияToolStripMenuItem1
            // 
            this.отменадействияToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Window;
            this.отменадействияToolStripMenuItem1.Image = global::Graphic_redactor.Properties.Resources.redo_alt;
            this.отменадействияToolStripMenuItem1.Name = "отменадействияToolStripMenuItem1";
            this.отменадействияToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.отменадействияToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.отменадействияToolStripMenuItem1.Text = "Отмена действия";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.копироватьToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.copy;
            this.копироватьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.копироватьToolStripMenuItem.Text = "&Копировать";
            // 
            // вставкаToolStripMenuItem
            // 
            this.вставкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.вставкаToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.image;
            this.вставкаToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.вставкаToolStripMenuItem.Name = "вставкаToolStripMenuItem";
            this.вставкаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.вставкаToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.вставкаToolStripMenuItem.Text = "Вставка";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(206, 6);
            // 
            // выделитьвсеToolStripMenuItem
            // 
            this.выделитьвсеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.выделитьвсеToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.vector_square;
            this.выделитьвсеToolStripMenuItem.Name = "выделитьвсеToolStripMenuItem";
            this.выделитьвсеToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.выделитьвсеToolStripMenuItem.Text = "Выделить &все";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.опрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            // 
            // опрограммеToolStripMenuItem
            // 
            this.опрограммеToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.опрограммеToolStripMenuItem.Image = global::Graphic_redactor.Properties.Resources.question_circle;
            this.опрограммеToolStripMenuItem.Name = "опрограммеToolStripMenuItem";
            this.опрограммеToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.опрограммеToolStripMenuItem.Text = "О программе...";
            // 
            // Pic_close
            // 
            this.Pic_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_close.Image = global::Graphic_redactor.Properties.Resources.window_close;
            this.Pic_close.Location = new System.Drawing.Point(847, 3);
            this.Pic_close.Name = "Pic_close";
            this.Pic_close.Size = new System.Drawing.Size(24, 20);
            this.Pic_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_close.TabIndex = 1;
            this.Pic_close.TabStop = false;
            this.Pic_close.Click += new System.EventHandler(this.Pic_close_Click);
            // 
            // Icon_pic
            // 
            this.Icon_pic.Image = global::Graphic_redactor.Properties.Resources.pencil_ruler;
            this.Icon_pic.Location = new System.Drawing.Point(5, 3);
            this.Icon_pic.Name = "Icon_pic";
            this.Icon_pic.Size = new System.Drawing.Size(25, 25);
            this.Icon_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Icon_pic.TabIndex = 3;
            this.Icon_pic.TabStop = false;
            // 
            // Pic_maximize
            // 
            this.Pic_maximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_maximize.Image = global::Graphic_redactor.Properties.Resources.window_maximize;
            this.Pic_maximize.Location = new System.Drawing.Point(813, 3);
            this.Pic_maximize.Name = "Pic_maximize";
            this.Pic_maximize.Size = new System.Drawing.Size(24, 20);
            this.Pic_maximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_maximize.TabIndex = 2;
            this.Pic_maximize.TabStop = false;
            this.Pic_maximize.Click += new System.EventHandler(this.Pic_maximize_Click);
            // 
            // Pic_minimize
            // 
            this.Pic_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_minimize.Image = global::Graphic_redactor.Properties.Resources.window_minimize;
            this.Pic_minimize.Location = new System.Drawing.Point(779, 3);
            this.Pic_minimize.Name = "Pic_minimize";
            this.Pic_minimize.Size = new System.Drawing.Size(24, 20);
            this.Pic_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_minimize.TabIndex = 4;
            this.Pic_minimize.TabStop = false;
            this.Pic_minimize.Click += new System.EventHandler(this.Pic_minimize_Click);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.ClientSize = new System.Drawing.Size(875, 500);
            this.Controls.Add(this.Pic_minimize);
            this.Controls.Add(this.Main_menu);
            this.Controls.Add(this.Pic_close);
            this.Controls.Add(this.Pic_maximize);
            this.Controls.Add(this.Icon_pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.Main_menu;
            this.Name = "Window";
            this.Text = "Window";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Window_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Window_MouseUp);
            this.Main_menu.ResumeLayout(false);
            this.Main_menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_maximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_minimize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Main_menu;
        private System.Windows.Forms.PictureBox Pic_close;
        private System.Windows.Forms.PictureBox Icon_pic;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитькакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отменадействияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отменадействияToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem выделитьвсеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem опрограммеToolStripMenuItem;
        private System.Windows.Forms.PictureBox Pic_maximize;
        private System.Windows.Forms.PictureBox Pic_minimize;
    }
}