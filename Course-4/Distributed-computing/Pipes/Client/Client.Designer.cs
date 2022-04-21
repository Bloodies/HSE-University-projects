
namespace Client
{
    partial class Client
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
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.lblShowLogin = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.lblPipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPipe.Location = new System.Drawing.Point(3, 27);
            this.lblPipe.Name = "lblPipe";
            this.lblPipe.Size = new System.Drawing.Size(77, 28);
            this.lblPipe.TabIndex = 1;
            this.lblPipe.Text = "Имя канала:";
            this.lblPipe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPipe
            // 
            this.tbPipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPipe.Enabled = false;
            this.tbPipe.Location = new System.Drawing.Point(86, 30);
            this.tbPipe.Name = "tbPipe";
            this.tbPipe.Size = new System.Drawing.Size(164, 20);
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
            // rtbMessages
            // 
            this.rtbMessages.HideSelection = false;
            this.rtbMessages.Location = new System.Drawing.Point(12, 64);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.ReadOnly = true;
            this.rtbMessages.Size = new System.Drawing.Size(352, 280);
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
            this.lblLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogin.Location = new System.Drawing.Point(3, 0);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(77, 27);
            this.lblLogin.TabIndex = 3;
            this.lblLogin.Text = "Логин:";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.80632F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.19367F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.Controls.Add(this.lblLogin, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPipe, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPipe, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(348, 55);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // btnConnect
            // 
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(256, 30);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(89, 22);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 383);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.rtbMessages);
            this.Controls.Add(this.lblShowLogin);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.btnSend);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Клиент";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.RichTextBox rtbMessages;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TextBox tbPipe;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblShowLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblPipe;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnConnect;
    }
}

