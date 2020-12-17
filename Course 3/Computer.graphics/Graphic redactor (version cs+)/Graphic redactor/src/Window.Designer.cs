using System.Drawing;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.Main_menu = new System.Windows.Forms.MenuStrip();
            this.File_toolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.Make_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Save_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_as_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit_dop_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Change_toolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.Undo_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Redo_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Copy_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Input_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator_4 = new System.Windows.Forms.ToolStripSeparator();
            this.Choose_all_button = new System.Windows.Forms.ToolStripMenuItem();
            this.Tasks_window = new System.Windows.Forms.ToolStripMenuItem();
            this.дополнительные3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Status_strip = new System.Windows.Forms.StatusStrip();
            this.Cursor_location = new System.Windows.Forms.ToolStripStatusLabel();
            this.Workflow = new System.Windows.Forms.Panel();
            this.Tool_strip_cont = new System.Windows.Forms.ToolStripContainer();
            this.Canvas_background = new System.Windows.Forms.Panel();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.Work_strip = new System.Windows.Forms.ToolStrip();
            this.Cursor_button = new System.Windows.Forms.ToolStripButton();
            this.Draw_pencil = new System.Windows.Forms.ToolStripButton();
            this.Draw_line = new System.Windows.Forms.ToolStripButton();
            this.Draw_polygon = new System.Windows.Forms.ToolStripButton();
            this.Bezyie = new System.Windows.Forms.ToolStripButton();
            this.Group_lines = new System.Windows.Forms.ToolStripButton();
            this.Ungroup_lines = new System.Windows.Forms.ToolStripButton();
            this.Axes = new System.Windows.Forms.ToolStripButton();
            this.Magic_line = new System.Windows.Forms.ToolStripButton();
            this.Pic_scale_min = new System.Windows.Forms.PictureBox();
            this.Scale_bar = new System.Windows.Forms.TrackBar();
            this.Pic_scale_max = new System.Windows.Forms.PictureBox();
            this.Color_circle = new System.Windows.Forms.PictureBox();
            this.Color_square = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Color_dialog = new System.Windows.Forms.ColorDialog();
            this.Save_file = new System.Windows.Forms.SaveFileDialog();
            this.Open_file = new System.Windows.Forms.OpenFileDialog();
            this.Pic_minimize = new System.Windows.Forms.PictureBox();
            this.Pic_close = new System.Windows.Forms.PictureBox();
            this.Icon_pic = new System.Windows.Forms.PictureBox();
            this.Main_menu.SuspendLayout();
            this.Status_strip.SuspendLayout();
            this.Workflow.SuspendLayout();
            this.Tool_strip_cont.ContentPanel.SuspendLayout();
            this.Tool_strip_cont.LeftToolStripPanel.SuspendLayout();
            this.Tool_strip_cont.SuspendLayout();
            this.Canvas_background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.Work_strip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_scale_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_scale_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Color_circle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Color_square)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // Main_menu
            // 
            this.Main_menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Main_menu.Dock = System.Windows.Forms.DockStyle.None;
            this.Main_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_toolstrip,
            this.Change_toolstrip,
            this.Tasks_window,
            this.дополнительные3DToolStripMenuItem});
            this.Main_menu.Location = new System.Drawing.Point(33, 4);
            this.Main_menu.Name = "Main_menu";
            this.Main_menu.Size = new System.Drawing.Size(356, 24);
            this.Main_menu.TabIndex = 0;
            this.Main_menu.Text = "Main_menu";
            this.Main_menu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseDown);
            this.Main_menu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseMove);
            this.Main_menu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_menu_MouseUp);
            // 
            // File_toolstrip
            // 
            this.File_toolstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.File_toolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Make_button,
            this.Separator_1,
            this.Save_button,
            this.Save_as_button,
            this.Separator_2,
            this.Exit_dop_button});
            this.File_toolstrip.ForeColor = System.Drawing.SystemColors.Control;
            this.File_toolstrip.Name = "File_toolstrip";
            this.File_toolstrip.Size = new System.Drawing.Size(48, 20);
            this.File_toolstrip.Text = "Файл";
            // 
            // Make_button
            // 
            this.Make_button.BackColor = System.Drawing.SystemColors.Control;
            this.Make_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Make_button.Image = global::Graphic_redactor.Properties.Resources.edit;
            this.Make_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Make_button.Name = "Make_button";
            this.Make_button.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.Make_button.Size = new System.Drawing.Size(173, 22);
            this.Make_button.Text = "Создать";
            this.Make_button.Click += new System.EventHandler(this.Make_button_Click);
            // 
            // Separator_1
            // 
            this.Separator_1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Separator_1.Name = "Separator_1";
            this.Separator_1.Size = new System.Drawing.Size(170, 6);
            // 
            // Save_button
            // 
            this.Save_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Save_button.Image = global::Graphic_redactor.Properties.Resources.save;
            this.Save_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save_button.Name = "Save_button";
            this.Save_button.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.Save_button.Size = new System.Drawing.Size(173, 22);
            this.Save_button.Text = "Сохранить";
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // Save_as_button
            // 
            this.Save_as_button.BackColor = System.Drawing.SystemColors.Control;
            this.Save_as_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Save_as_button.Image = global::Graphic_redactor.Properties.Resources.eye;
            this.Save_as_button.Name = "Save_as_button";
            this.Save_as_button.Size = new System.Drawing.Size(173, 22);
            this.Save_as_button.Text = "Сохранить как";
            this.Save_as_button.Click += new System.EventHandler(this.Save_as_button_Click);
            // 
            // Separator_2
            // 
            this.Separator_2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Separator_2.Name = "Separator_2";
            this.Separator_2.Size = new System.Drawing.Size(170, 6);
            // 
            // Exit_dop_button
            // 
            this.Exit_dop_button.BackColor = System.Drawing.SystemColors.Control;
            this.Exit_dop_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Exit_dop_button.Image = global::Graphic_redactor.Properties.Resources.window_close;
            this.Exit_dop_button.Name = "Exit_dop_button";
            this.Exit_dop_button.Size = new System.Drawing.Size(173, 22);
            this.Exit_dop_button.Text = "Выход";
            this.Exit_dop_button.Click += new System.EventHandler(this.Exit_dop_button_Click);
            // 
            // Change_toolstrip
            // 
            this.Change_toolstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Change_toolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Undo_button,
            this.Redo_button,
            this.Separator_3,
            this.Copy_button,
            this.Input_button,
            this.Separator_4,
            this.Choose_all_button});
            this.Change_toolstrip.ForeColor = System.Drawing.SystemColors.Window;
            this.Change_toolstrip.Name = "Change_toolstrip";
            this.Change_toolstrip.Size = new System.Drawing.Size(59, 20);
            this.Change_toolstrip.Text = "Правка";
            // 
            // Undo_button
            // 
            this.Undo_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Undo_button.Image = global::Graphic_redactor.Properties.Resources.undo_alt;
            this.Undo_button.Name = "Undo_button";
            this.Undo_button.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.Undo_button.Size = new System.Drawing.Size(209, 22);
            this.Undo_button.Text = "Отмена действия";
            this.Undo_button.Click += new System.EventHandler(this.Undo_button_Click);
            // 
            // Redo_button
            // 
            this.Redo_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Redo_button.Image = global::Graphic_redactor.Properties.Resources.redo_alt;
            this.Redo_button.Name = "Redo_button";
            this.Redo_button.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.Redo_button.Size = new System.Drawing.Size(209, 22);
            this.Redo_button.Text = "Отмена действия";
            this.Redo_button.Click += new System.EventHandler(this.Redo_button_Click);
            // 
            // Separator_3
            // 
            this.Separator_3.Name = "Separator_3";
            this.Separator_3.Size = new System.Drawing.Size(206, 6);
            // 
            // Copy_button
            // 
            this.Copy_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Copy_button.Image = global::Graphic_redactor.Properties.Resources.copy;
            this.Copy_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Copy_button.Name = "Copy_button";
            this.Copy_button.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.Copy_button.Size = new System.Drawing.Size(209, 22);
            this.Copy_button.Text = "Копировать";
            this.Copy_button.Click += new System.EventHandler(this.Copy_button_Click);
            // 
            // Input_button
            // 
            this.Input_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Input_button.Image = global::Graphic_redactor.Properties.Resources.image;
            this.Input_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Input_button.Name = "Input_button";
            this.Input_button.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.Input_button.Size = new System.Drawing.Size(209, 22);
            this.Input_button.Text = "Вставка";
            this.Input_button.Click += new System.EventHandler(this.Input_button_Click);
            // 
            // Separator_4
            // 
            this.Separator_4.Name = "Separator_4";
            this.Separator_4.Size = new System.Drawing.Size(206, 6);
            // 
            // Choose_all_button
            // 
            this.Choose_all_button.ForeColor = System.Drawing.SystemColors.Window;
            this.Choose_all_button.Image = global::Graphic_redactor.Properties.Resources.vector_square;
            this.Choose_all_button.Name = "Choose_all_button";
            this.Choose_all_button.Size = new System.Drawing.Size(209, 22);
            this.Choose_all_button.Text = "Выделить &все";
            this.Choose_all_button.Click += new System.EventHandler(this.Choose_all_button_Click);
            // 
            // Tasks_window
            // 
            this.Tasks_window.ForeColor = System.Drawing.SystemColors.Window;
            this.Tasks_window.Name = "Tasks_window";
            this.Tasks_window.Size = new System.Drawing.Size(109, 20);
            this.Tasks_window.Text = "Список Заданий";
            this.Tasks_window.Click += new System.EventHandler(this.Tasks_window_Click);
            // 
            // дополнительные3DToolStripMenuItem
            // 
            this.дополнительные3DToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.дополнительные3DToolStripMenuItem.Name = "дополнительные3DToolStripMenuItem";
            this.дополнительные3DToolStripMenuItem.Size = new System.Drawing.Size(132, 20);
            this.дополнительные3DToolStripMenuItem.Text = "Дополнительные 3D";
            // 
            // Status_strip
            // 
            this.Status_strip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Status_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Cursor_location});
            this.Status_strip.Location = new System.Drawing.Point(0, 645);
            this.Status_strip.Name = "Status_strip";
            this.Status_strip.Size = new System.Drawing.Size(1308, 22);
            this.Status_strip.SizingGrip = false;
            this.Status_strip.TabIndex = 5;
            this.Status_strip.Text = "statusStrip1";
            // 
            // Cursor_location
            // 
            this.Cursor_location.ForeColor = System.Drawing.SystemColors.Window;
            this.Cursor_location.Name = "Cursor_location";
            this.Cursor_location.Size = new System.Drawing.Size(118, 17);
            this.Cursor_location.Text = "toolStripStatusLabel1";
            // 
            // Workflow
            // 
            this.Workflow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Workflow.Controls.Add(this.Tool_strip_cont);
            this.Workflow.Controls.Add(this.Pic_scale_min);
            this.Workflow.Controls.Add(this.Scale_bar);
            this.Workflow.Controls.Add(this.Pic_scale_max);
            this.Workflow.Controls.Add(this.Color_circle);
            this.Workflow.Controls.Add(this.Color_square);
            this.Workflow.Location = new System.Drawing.Point(0, 31);
            this.Workflow.Name = "Workflow";
            this.Workflow.Size = new System.Drawing.Size(1308, 611);
            this.Workflow.TabIndex = 6;
            // 
            // Tool_strip_cont
            // 
            // 
            // Tool_strip_cont.ContentPanel
            // 
            this.Tool_strip_cont.ContentPanel.BackgroundImage = global::Graphic_redactor.Properties.Resources.color1;
            this.Tool_strip_cont.ContentPanel.Controls.Add(this.Canvas_background);
            this.Tool_strip_cont.ContentPanel.Size = new System.Drawing.Size(1277, 586);
            this.Tool_strip_cont.Dock = System.Windows.Forms.DockStyle.Bottom;
            // 
            // Tool_strip_cont.LeftToolStripPanel
            // 
            this.Tool_strip_cont.LeftToolStripPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Tool_strip_cont.LeftToolStripPanel.Controls.Add(this.Work_strip);
            this.Tool_strip_cont.Location = new System.Drawing.Point(0, 25);
            this.Tool_strip_cont.Name = "Tool_strip_cont";
            // 
            // Tool_strip_cont.RightToolStripPanel
            // 
            this.Tool_strip_cont.RightToolStripPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Tool_strip_cont.Size = new System.Drawing.Size(1308, 586);
            this.Tool_strip_cont.TabIndex = 0;
            this.Tool_strip_cont.Text = "toolStripContainer1";
            // 
            // Tool_strip_cont.TopToolStripPanel
            // 
            this.Tool_strip_cont.TopToolStripPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Tool_strip_cont.TopToolStripPanel.Enabled = false;
            // 
            // Canvas_background
            // 
            this.Canvas_background.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Canvas_background.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Canvas_background.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(35)))), ((int)(((byte)(75)))));
            this.Canvas_background.Controls.Add(this.Canvas);
            this.Canvas_background.Location = new System.Drawing.Point(121, 39);
            this.Canvas_background.Name = "Canvas_background";
            this.Canvas_background.Size = new System.Drawing.Size(1000, 500);
            this.Canvas_background.TabIndex = 1;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1000, 500);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // Work_strip
            // 
            this.Work_strip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.Work_strip.Dock = System.Windows.Forms.DockStyle.None;
            this.Work_strip.GripMargin = new System.Windows.Forms.Padding(0);
            this.Work_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Cursor_button,
            this.Draw_pencil,
            this.Draw_line,
            this.Draw_polygon,
            this.Bezyie,
            this.Group_lines,
            this.Ungroup_lines,
            this.Axes,
            this.Magic_line});
            this.Work_strip.Location = new System.Drawing.Point(0, 8);
            this.Work_strip.Name = "Work_strip";
            this.Work_strip.Size = new System.Drawing.Size(31, 214);
            this.Work_strip.TabIndex = 0;
            // 
            // Cursor_button
            // 
            this.Cursor_button.AutoSize = false;
            this.Cursor_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Cursor_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Cursor_button.Image = ((System.Drawing.Image)(resources.GetObject("Cursor_button.Image")));
            this.Cursor_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Cursor_button.Name = "Cursor_button";
            this.Cursor_button.Size = new System.Drawing.Size(30, 20);
            this.Cursor_button.Text = ".";
            this.Cursor_button.Click += new System.EventHandler(this.Cursor_button_Click);
            // 
            // Draw_pencil
            // 
            this.Draw_pencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Draw_pencil.Image = global::Graphic_redactor.Properties.Resources.pencil_alt;
            this.Draw_pencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Draw_pencil.Name = "Draw_pencil";
            this.Draw_pencil.Size = new System.Drawing.Size(29, 20);
            this.Draw_pencil.Text = "toolStripButton1";
            this.Draw_pencil.Click += new System.EventHandler(this.Draw_pencil_Click);
            // 
            // Draw_line
            // 
            this.Draw_line.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Draw_line.Image = global::Graphic_redactor.Properties.Resources.pencil_ruler1;
            this.Draw_line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Draw_line.Name = "Draw_line";
            this.Draw_line.Size = new System.Drawing.Size(29, 20);
            this.Draw_line.Text = "toolStripButton2";
            this.Draw_line.Click += new System.EventHandler(this.Draw_line_Click);
            // 
            // Draw_polygon
            // 
            this.Draw_polygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Draw_polygon.Image = global::Graphic_redactor.Properties.Resources.draw_polygon;
            this.Draw_polygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Draw_polygon.Name = "Draw_polygon";
            this.Draw_polygon.Size = new System.Drawing.Size(29, 20);
            this.Draw_polygon.Text = "toolStripButton3";
            this.Draw_polygon.Click += new System.EventHandler(this.Draw_polygon_Click);
            // 
            // Bezyie
            // 
            this.Bezyie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Bezyie.Image = global::Graphic_redactor.Properties.Resources.project_diagram;
            this.Bezyie.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Bezyie.Name = "Bezyie";
            this.Bezyie.Size = new System.Drawing.Size(29, 20);
            this.Bezyie.Text = "toolStripButton1";
            this.Bezyie.Click += new System.EventHandler(this.Bezyie_button_Click);
            // 
            // Group_lines
            // 
            this.Group_lines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Group_lines.Image = global::Graphic_redactor.Properties.Resources.object_ungroup;
            this.Group_lines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Group_lines.Name = "Group_lines";
            this.Group_lines.Size = new System.Drawing.Size(29, 20);
            this.Group_lines.Text = "toolStripButton4";
            this.Group_lines.Click += new System.EventHandler(this.Group_lines_Click);
            // 
            // Ungroup_lines
            // 
            this.Ungroup_lines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Ungroup_lines.Image = global::Graphic_redactor.Properties.Resources.object_group;
            this.Ungroup_lines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Ungroup_lines.Name = "Ungroup_lines";
            this.Ungroup_lines.Size = new System.Drawing.Size(29, 20);
            this.Ungroup_lines.Text = "toolStripButton5";
            this.Ungroup_lines.Click += new System.EventHandler(this.Ungroup_lines_Click);
            // 
            // Axes
            // 
            this.Axes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Axes.Image = global::Graphic_redactor.Properties.Resources.ruler_combined;
            this.Axes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Axes.Name = "Axes";
            this.Axes.Size = new System.Drawing.Size(29, 20);
            this.Axes.Text = "toolStripButton6";
            this.Axes.Click += new System.EventHandler(this.Axes_Click);
            // 
            // Magic_line
            // 
            this.Magic_line.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Magic_line.Image = global::Graphic_redactor.Properties.Resources.magic;
            this.Magic_line.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Magic_line.Name = "Magic_line";
            this.Magic_line.Size = new System.Drawing.Size(29, 20);
            this.Magic_line.Text = "toolStripButton7";
            this.Magic_line.Click += new System.EventHandler(this.Magic_line_Click);
            // 
            // Pic_scale_min
            // 
            this.Pic_scale_min.Image = global::Graphic_redactor.Properties.Resources.square;
            this.Pic_scale_min.Location = new System.Drawing.Point(12, 9);
            this.Pic_scale_min.Name = "Pic_scale_min";
            this.Pic_scale_min.Size = new System.Drawing.Size(15, 10);
            this.Pic_scale_min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_scale_min.TabIndex = 1;
            this.Pic_scale_min.TabStop = false;
            // 
            // Scale_bar
            // 
            this.Scale_bar.Location = new System.Drawing.Point(27, 3);
            this.Scale_bar.Name = "Scale_bar";
            this.Scale_bar.Size = new System.Drawing.Size(104, 45);
            this.Scale_bar.TabIndex = 1;
            // 
            // Pic_scale_max
            // 
            this.Pic_scale_max.Image = global::Graphic_redactor.Properties.Resources.square;
            this.Pic_scale_max.Location = new System.Drawing.Point(124, 3);
            this.Pic_scale_max.Name = "Pic_scale_max";
            this.Pic_scale_max.Size = new System.Drawing.Size(30, 22);
            this.Pic_scale_max.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_scale_max.TabIndex = 2;
            this.Pic_scale_max.TabStop = false;
            // 
            // Color_circle
            // 
            this.Color_circle.BackColor = System.Drawing.Color.Transparent;
            this.Color_circle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Color_circle.Image = global::Graphic_redactor.Properties.Resources.Color_circle;
            this.Color_circle.Location = new System.Drawing.Point(189, 3);
            this.Color_circle.Name = "Color_circle";
            this.Color_circle.Size = new System.Drawing.Size(24, 22);
            this.Color_circle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Color_circle.TabIndex = 4;
            this.Color_circle.TabStop = false;
            this.Color_circle.Click += new System.EventHandler(this.Color_circle_Click);
            // 
            // Color_square
            // 
            this.Color_square.BackColor = System.Drawing.Color.Red;
            this.Color_square.Location = new System.Drawing.Point(204, 3);
            this.Color_square.Name = "Color_square";
            this.Color_square.Size = new System.Drawing.Size(23, 22);
            this.Color_square.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Color_square.TabIndex = 3;
            this.Color_square.TabStop = false;
            this.Color_square.Click += new System.EventHandler(this.Color_square_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Open_file
            // 
            this.Open_file.FileName = "openFileDialog1";
            // 
            // Pic_minimize
            // 
            this.Pic_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_minimize.Image = global::Graphic_redactor.Properties.Resources.window_minimize;
            this.Pic_minimize.Location = new System.Drawing.Point(1250, 3);
            this.Pic_minimize.Name = "Pic_minimize";
            this.Pic_minimize.Size = new System.Drawing.Size(24, 20);
            this.Pic_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_minimize.TabIndex = 4;
            this.Pic_minimize.TabStop = false;
            this.Pic_minimize.Click += new System.EventHandler(this.Pic_minimize_Click);
            // 
            // Pic_close
            // 
            this.Pic_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic_close.Image = global::Graphic_redactor.Properties.Resources.window_close;
            this.Pic_close.Location = new System.Drawing.Point(1280, 3);
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
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(85)))), ((int)(((byte)(139)))));
            this.ClientSize = new System.Drawing.Size(1308, 667);
            this.Controls.Add(this.Workflow);
            this.Controls.Add(this.Status_strip);
            this.Controls.Add(this.Pic_minimize);
            this.Controls.Add(this.Main_menu);
            this.Controls.Add(this.Pic_close);
            this.Controls.Add(this.Icon_pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Main_menu;
            this.Name = "Window";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Window";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Window_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Window_MouseUp);
            this.Main_menu.ResumeLayout(false);
            this.Main_menu.PerformLayout();
            this.Status_strip.ResumeLayout(false);
            this.Status_strip.PerformLayout();
            this.Workflow.ResumeLayout(false);
            this.Workflow.PerformLayout();
            this.Tool_strip_cont.ContentPanel.ResumeLayout(false);
            this.Tool_strip_cont.LeftToolStripPanel.ResumeLayout(false);
            this.Tool_strip_cont.LeftToolStripPanel.PerformLayout();
            this.Tool_strip_cont.ResumeLayout(false);
            this.Tool_strip_cont.PerformLayout();
            this.Canvas_background.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.Work_strip.ResumeLayout(false);
            this.Work_strip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_scale_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Scale_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_scale_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Color_circle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Color_square)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Main_menu;
        private System.Windows.Forms.PictureBox Pic_close;
        private System.Windows.Forms.PictureBox Icon_pic;
        private System.Windows.Forms.ToolStripMenuItem File_toolstrip;
        private System.Windows.Forms.ToolStripMenuItem Make_button;
        private System.Windows.Forms.ToolStripSeparator Separator_1;
        private System.Windows.Forms.ToolStripMenuItem Save_button;
        private System.Windows.Forms.ToolStripMenuItem Save_as_button;
        private System.Windows.Forms.ToolStripSeparator Separator_2;
        private System.Windows.Forms.ToolStripMenuItem Exit_dop_button;
        private System.Windows.Forms.ToolStripMenuItem Change_toolstrip;
        private System.Windows.Forms.ToolStripMenuItem Undo_button;
        private System.Windows.Forms.ToolStripMenuItem Redo_button;
        private System.Windows.Forms.ToolStripSeparator Separator_3;
        private System.Windows.Forms.ToolStripMenuItem Copy_button;
        private System.Windows.Forms.ToolStripMenuItem Input_button;
        private System.Windows.Forms.ToolStripSeparator Separator_4;
        private System.Windows.Forms.ToolStripMenuItem Choose_all_button;
        private System.Windows.Forms.PictureBox Pic_minimize;
        private System.Windows.Forms.StatusStrip Status_strip;
        private System.Windows.Forms.ToolStripStatusLabel Cursor_location;
        private System.Windows.Forms.Panel Workflow;
        private System.Windows.Forms.ToolStripContainer Tool_strip_cont;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.ToolStrip Work_strip;
        private System.Windows.Forms.ToolStripButton Draw_pencil;
        private System.Windows.Forms.ToolStripButton Draw_line;
        private System.Windows.Forms.ToolStripButton Draw_polygon;
        private System.Windows.Forms.ToolStripButton Group_lines;
        private System.Windows.Forms.ToolStripButton Ungroup_lines;
        private System.Windows.Forms.ToolStripButton Axes;
        private System.Windows.Forms.ToolStripButton Magic_line;
        private System.Windows.Forms.TrackBar Scale_bar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.PictureBox Pic_scale_max;
        private System.Windows.Forms.PictureBox Pic_scale_min;
        private System.Windows.Forms.ToolStripMenuItem Tasks_window;
        private System.Windows.Forms.ToolStripMenuItem дополнительные3DToolStripMenuItem;
        private System.Windows.Forms.Panel Canvas_background;
        private System.Windows.Forms.PictureBox Color_square;
        private System.Windows.Forms.PictureBox Color_circle;
        private System.Windows.Forms.ColorDialog Color_dialog;
        private System.Windows.Forms.SaveFileDialog Save_file;
        private System.Windows.Forms.OpenFileDialog Open_file;
        private System.Windows.Forms.ToolStripButton Bezyie;
        private System.Windows.Forms.ToolStripButton Cursor_button;
    }
}