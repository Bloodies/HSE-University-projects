namespace MSMQ
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIP = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.lblIPath = new System.Windows.Forms.Label();
            this.tbClient = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(0, 41);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(18, 13);
            this.lblIP.TabIndex = 2;
            this.lblIP.Text = "ID";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(231, 37);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(91, 21);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(36, 38);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(189, 20);
            this.tbPath.TabIndex = 0;
            // 
            // rtbMessages
            // 
            this.rtbMessages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbMessages.Location = new System.Drawing.Point(0, 106);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.Size = new System.Drawing.Size(334, 155);
            this.rtbMessages.TabIndex = 3;
            this.rtbMessages.Text = "";
            // 
            // lblIPath
            // 
            this.lblIPath.AutoSize = true;
            this.lblIPath.Location = new System.Drawing.Point(0, 14);
            this.lblIPath.Name = "lblIPath";
            this.lblIPath.Size = new System.Drawing.Size(84, 13);
            this.lblIPath.TabIndex = 4;
            this.lblIPath.Text = "Путь к очереди";
            this.lblIPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbClient
            // 
            this.tbClient.Location = new System.Drawing.Point(90, 11);
            this.tbClient.Name = "tbClient";
            this.tbClient.Size = new System.Drawing.Size(135, 20);
            this.tbClient.TabIndex = 5;
            this.tbClient.Text = ".\\private$\\ServerQueue";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(231, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 21);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Подключиться";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 261);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbClient);
            this.Controls.Add(this.lblIPath);
            this.Controls.Add(this.rtbMessages);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.btnConnect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Клиент";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.RichTextBox rtbMessages;
        private System.Windows.Forms.Label lblIPath;
        private System.Windows.Forms.TextBox tbClient;
        private System.Windows.Forms.Button btnSave;
    }
}

