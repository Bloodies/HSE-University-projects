using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace Client
{
    public partial class Client : Form
    {
        private Int32 _mailServerSlot;   // дескриптор мэйлслота
        private string _userName;
        private Guid _guid;
        private int _ourOwnClientMailSlot;
        private bool _continue = true;
        private Thread _thredForReceivingMessages = null;
        string _connection = @"\\*\mailslot\ServerMailslot";//"\\\\.\\mailslot\\ServerMailslot";

        // конструктор формы
        public Client()
        {
            InitializeComponent();
            this.Text += "     " + Dns.GetHostName();   // выводим имя текущей машины в заголовок формы
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLogin.Text))
            {
                _guid = new Guid();
                _userName = tbLogin.Text;
                this.Text = _userName;
                btnSave.Enabled = false;

                try
                {
                    // открываем мэйлслот с название нашего сервера
                    _mailServerSlot = DIS.Import.CreateFile
                        (@"\\.\mailslot\ServerMailslot",
                        DIS.Types.EFileAccess.GenericWrite,
                        DIS.Types.EFileShare.Read,
                        0,
                        DIS.Types.ECreationDisposition.OpenExisting,
                        0,
                        0);

                    if (_mailServerSlot != -1)
                    {

                        btnSend.Enabled = true;

                        // создание собсбтвенного мейлслота для нашего приема сообщений
                        _ourOwnClientMailSlot =
                            DIS.Import.CreateMailslot(
                           "\\\\.\\mailslot\\" + _userName,
                           0,
                           DIS.Types.MAILSLOT_WAIT_FOREVER,
                           0);

                        this.Text = _ourOwnClientMailSlot.ToString() + " " + _mailServerSlot.ToString();



                        _thredForReceivingMessages = new Thread(ReceiveMessage);
                        _thredForReceivingMessages.Start();

                        sendMessage(_userName + ":" + _ourOwnClientMailSlot + "_NAME_");

                    }
                    else
                        MessageBox.Show("Не удалось подключиться к мейлслоту");
                }
                catch
                {
                    MessageBox.Show("Не удалось подключиться к мейлслоту");
                }
            }
            else
            {
                MessageBox.Show("Empty Name");
            }
        }

        private void ReceiveMessage()
        {
            string msg = "";            // прочитанное сообщение
            int MailslotSize = 0;       // максимальный размер сообщения
            int lpNextSize = 0;         // размер следующего сообщения
            int MessageCount = 0;       // количество сообщений в мэйлслоте
            uint realBytesReaded = 0;   // количество реально прочитанных из мэйлслота байтов

            try
            {
                // входим в бесконечный цикл работы с мэйлслотом
                while (_continue)
                {
                    // получаем информацию о состоянии мэйлслота
                    DIS.Import.GetMailslotInfo(_ourOwnClientMailSlot,
                        MailslotSize,
                        ref lpNextSize,
                        ref MessageCount,
                        0);
                    // если есть сообщения в мэйлслоте, то обрабатываем каждое из них
                    for (int i = 0; i < MessageCount; i++)
                    {
                        byte[] buff = new byte[1024];                           // буфер прочитанных из мэйлслота байтов
                        DIS.Import.FlushFileBuffers(_ourOwnClientMailSlot);      // "принудительная" запись данных, расположенные в буфере операционной системы, в файл мэйлслота
                        DIS.Import.ReadFile(_ourOwnClientMailSlot, buff, 1024, ref realBytesReaded, 0);      // считываем последовательность байтов из мэйлслота в буфер buff
                        msg = Encoding.Unicode.GetString(buff);                 // выполняем преобразование байтов в последовательность символов

                        bool isMessageToSend = true;

                        if (msg.Contains("_NAME_"))
                        {

                            string s = msg.Split('_')[0].Split(':')[0];

                            int number = int.Parse(msg.Split('_')[0].Split(':')[1]);

                            //dick.Add(s, number);
                            isMessageToSend = false;
                        }
                        if (msg.Contains("_DELETE_"))
                        {
                            //_clients.Remove(msg.Split('_')[0]);
                            isMessageToSend = false;
                        }
                        if (msg.Contains("_NO_"))
                        {
                            isMessageToSend = false;
                        }
                        if (!string.IsNullOrEmpty(msg) && isMessageToSend)
                        {
                            rtbMessages.Invoke((MethodInvoker)delegate
                            {
                                if (msg != "")
                                    rtbMessages.Text += "\n >> " + msg;     // выводим полученное сообщение на форму
                            });
                            //SendMessageToAllClients("\n >> " + msg);
                        }// приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
                    }
                    Thread.Sleep(500);
                }
            }
            catch (ThreadInterruptedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sendMessage(string text)
        {
            uint BytesWritten = 0;  // количество реально записанных в мэйлслот байт
            byte[] buff = Encoding.Unicode.GetBytes(text);    // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт
            DIS.Import.WriteFile(_mailServerSlot,
                buff,
                Convert.ToUInt32(buff.Length),
                ref BytesWritten,
                0);     // выполняем запись последовательности байт в мэйлслот

        }

        // отправка сообщения
        private void btnSend_Click(object sender, EventArgs e)
        {
            sendMessage(_userName + " >> " + tbMessage.Text + "\n");
        }

        private void Disconnect()
        {
            sendMessage(_userName + _guid.ToString() + "_DELETE_");

            if (_thredForReceivingMessages != null)
            {
                _thredForReceivingMessages.Interrupt();
                _thredForReceivingMessages.Join();
            }

            if (_ourOwnClientMailSlot != -1)
                DIS.Import.CloseHandle(_ourOwnClientMailSlot);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
