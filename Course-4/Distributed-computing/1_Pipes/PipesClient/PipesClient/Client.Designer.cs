namespace Pipes
{
    partial class frmMain
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
            this.btnSend = new System.Windows.Forms.Button();
            this.lblPipe = new System.Windows.Forms.Label();
            this.tbPipe = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.Login_panel = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.lblPLogin = new System.Windows.Forms.Label();
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.lblShowLogin = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.Login_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(285, 350);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 22);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblPipe
            // 
            this.lblPipe.Location = new System.Drawing.Point(16, 34);
            this.lblPipe.Name = "lblPipe";
            this.lblPipe.Size = new System.Drawing.Size(72, 17);
            this.lblPipe.TabIndex = 1;
            this.lblPipe.Text = "Имя канала:";
            // 
            // tbPipe
            // 
            this.tbPipe.Location = new System.Drawing.Point(94, 31);
            this.tbPipe.Name = "tbPipe";
            this.tbPipe.Size = new System.Drawing.Size(188, 20);
            this.tbPipe.TabIndex = 0;
            this.tbPipe.Text = "\\\\.\\pipe\\ServerPipe";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(20, 353);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(68, 13);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Сообщение:";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(92, 351);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(190, 20);
            this.tbMessage.TabIndex = 1;
            // 
            // Login_panel
            // 
            this.Login_panel.Controls.Add(this.btnLogin);
            this.Login_panel.Controls.Add(this.tbLogin);
            this.Login_panel.Controls.Add(this.lblPLogin);
            this.Login_panel.Location = new System.Drawing.Point(366, 13);
            this.Login_panel.Name = "Login_panel";
            this.Login_panel.Size = new System.Drawing.Size(352, 359);
            this.Login_panel.TabIndex = 4;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(259, 8);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Применить";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(65, 10);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(188, 20);
            this.tbLogin.TabIndex = 3;
            // 
            // lblPLogin
            // 
            this.lblPLogin.Location = new System.Drawing.Point(22, 13);
            this.lblPLogin.Name = "lblPLogin";
            this.lblPLogin.Size = new System.Drawing.Size(48, 17);
            this.lblPLogin.TabIndex = 2;
            this.lblPLogin.Text = "Логин:";
            // 
            // rtbMessages
            // 
            this.rtbMessages.HideSelection = false;
            this.rtbMessages.Location = new System.Drawing.Point(12, 57);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.ReadOnly = true;
            this.rtbMessages.Size = new System.Drawing.Size(352, 287);
            this.rtbMessages.TabIndex = 5;
            this.rtbMessages.Text = "";
            // 
            // lblShowLogin
            // 
            this.lblShowLogin.Location = new System.Drawing.Point(63, 12);
            this.lblShowLogin.Name = "lblShowLogin";
            this.lblShowLogin.Size = new System.Drawing.Size(295, 16);
            this.lblShowLogin.TabIndex = 4;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(16, 12);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(41, 13);
            this.lblLogin.TabIndex = 3;
            this.lblLogin.Text = "Логин:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 383);
            this.Controls.Add(this.Login_panel);
            this.Controls.Add(this.rtbMessages);
            this.Controls.Add(this.lblShowLogin);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblPipe);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbPipe);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Клиент";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Login_panel.ResumeLayout(false);
            this.Login_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblPipe;
        private System.Windows.Forms.TextBox tbPipe;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Panel Login_panel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Label lblPLogin;
        private System.Windows.Forms.Label lblShowLogin;
        private System.Windows.Forms.RichTextBox rtbMessages;
    }
}