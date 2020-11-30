
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
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pic_close = new System.Windows.Forms.PictureBox();
            this.Pic_minimize = new System.Windows.Forms.PictureBox();
            this.Main_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_minimize)).BeginInit();
            this.SuspendLayout();
            // 
            // Main_menu
            // 
            this.Main_menu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.Main_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.Main_menu.Location = new System.Drawing.Point(0, 0);
            this.Main_menu.Name = "Main_menu";
            this.Main_menu.Size = new System.Drawing.Size(800, 24);
            this.Main_menu.TabIndex = 0;
            this.Main_menu.Text = "Main_menu";
            this.Main_menu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseDown);
            this.Main_menu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseMove);
            this.Main_menu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseUp);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // Pic_close
            // 
            this.Pic_close.Image = global::Graphic_redactor.Properties.Resources.close;
            this.Pic_close.Location = new System.Drawing.Point(772, 0);
            this.Pic_close.Name = "Pic_close";
            this.Pic_close.Size = new System.Drawing.Size(28, 24);
            this.Pic_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_close.TabIndex = 1;
            this.Pic_close.TabStop = false;
            this.Pic_close.Click += new System.EventHandler(this.Pic_close_Click);
            // 
            // Pic_minimize
            // 
            this.Pic_minimize.Image = global::Graphic_redactor.Properties.Resources.minimize;
            this.Pic_minimize.Location = new System.Drawing.Point(747, 0);
            this.Pic_minimize.Name = "Pic_minimize";
            this.Pic_minimize.Size = new System.Drawing.Size(28, 24);
            this.Pic_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_minimize.TabIndex = 2;
            this.Pic_minimize.TabStop = false;
            this.Pic_minimize.Click += new System.EventHandler(this.Pic_minimize_Click);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Pic_minimize);
            this.Controls.Add(this.Pic_close);
            this.Controls.Add(this.Main_menu);
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
            ((System.ComponentModel.ISupportInitialize)(this.Pic_minimize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Main_menu;
        private System.Windows.Forms.PictureBox Pic_close;
        private System.Windows.Forms.PictureBox Pic_minimize;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
    }
}