using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;

namespace MailSlots
{
    public partial class frmMain : Form
    {
        private int _ourServiceMailSlot;       // дескриптор мэйлслота
        private string _mailSlotName =
            "\\\\" + Dns.GetHostName() + "\\mailslot\\ServerMailslot";    // имя мэйлслота, Dns.GetHostName() - метод, возвращающий имя машины, на которой запущено приложение
        private Thread _threadReceivingMessages;                       // поток для обслуживания мэйлслота
        private bool _continue = true;          // флаг, указывающий продолжается ли работа с мэйлслотом
        private List<string> _clients;
        private Dictionary<string, int> storageOfMailslot = new Dictionary<string, int>();
        string _connection = "\\\\.\\mailslot\\ServerMailslot";
        string _connetcionToOther = "\\\\.\\mailslot\\";
        string _previousMessage = string.Empty;

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();

            _clients = new List<string>();

            // создание мэйлслота
            _ourServiceMailSlot =
                DIS.Import.CreateMailslot(
                _connection,
                0,
                DIS.Types.MAILSLOT_WAIT_FOREVER,
                0);

            // вывод имени мэйлслота в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text += "     " + _ourServiceMailSlot;

            // создание потока, отвечающего за работу с мэйлслотом
            _threadReceivingMessages = new Thread(ReceiveMessage);
            _threadReceivingMessages.Start();
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
                    DIS.Import.GetMailslotInfo(_ourServiceMailSlot,
                        MailslotSize,
                        ref lpNextSize,
                        ref MessageCount,
                        0);
                    // если есть сообщения в мэйлслоте, то обрабатываем каждое из них
                    for (int i = 0; i < MessageCount; i++)
                    {
                        byte[] buff = new byte[1024];
                        DIS.Import.FlushFileBuffers(_ourServiceMailSlot);      // "принудительная" запись данных, расположенные в буфере операционной системы, в файл мэйлслота
                        DIS.Import.ReadFile(_ourServiceMailSlot,
                            buff,
                            1024,
                            ref realBytesReaded,
                            0);      // считываем последовательность байтов из мэйлслота в буфер buff
                        msg = Encoding.Unicode.GetString(buff);                 // выполняем преобразование байтов в последовательность символов
                        if (_previousMessage.Equals(msg))
                            continue;
                        bool isMessageToSend = true;
                        if (string.IsNullOrEmpty(msg))
                            break;
                        if (msg.Contains("_NAME_"))
                        {
                            _clients.Add(msg.Split('_')[0]);

                            string s = msg.Split('_')[0].Split(':')[0];

                            int number = int.Parse(msg.Split('_')[0].Split(':')[1]);

                            if (storageOfMailslot.ContainsKey(s))
                                ;
                            else
                                storageOfMailslot.Add(s, number);
                            isMessageToSend = false;
                        }
                        if (msg.Contains("_DELETE_"))
                        {
                            _clients.Remove(msg.Split('_')[0]);
                            isMessageToSend = false;
                        }
                        if (msg.Contains("_NO_"))
                        {
                            isMessageToSend = false;
                        }
                        if (isMessageToSend)
                        {
                            rtbMessages.Invoke((MethodInvoker)delegate
                            {
                                if (msg != "")
                                    rtbMessages.Text += "\n >> " + msg + " \n";     // выводим полученное сообщение на форму
                            });
                            foreach (string _connection in storageOfMailslot.Keys)
                            {
                                //отправить сообщение обратно
                                int _mailServerSlot = DIS.Import.CreateFile
                                    (@"\\.\mailslot\" + _connection,
                                    DIS.Types.EFileAccess.GenericWrite,
                                    DIS.Types.EFileShare.Read,
                                    0,
                                    DIS.Types.ECreationDisposition.OpenExisting,
                                    0,
                                    0);

                                if (_mailServerSlot != -1)
                                {
                                    uint BytesWritten = 0;  // количество реально записанных в мэйлслот байт
                                    byte[] buff1 = Encoding.Unicode.GetBytes(msg);    // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт
                                    DIS.Import.WriteFile(_mailServerSlot,
                                        buff1,
                                        Convert.ToUInt32(buff1.Length),
                                        ref BytesWritten,
                                        0);
                                }
                            }

                            // SendMessageToAllClients("\n >> " + msg+ "\n");
                        }// приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
                        _previousMessage = msg;

                    }
                    Thread.Sleep(500);
                }
            }
            catch (ThreadInterruptedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;      // сообщаем, что работа с мэйлслотом завершена

            if (_clients.Count >= 0)
            {
                //Соединяем потоки
                if (_threadReceivingMessages != null)
                {
                    _threadReceivingMessages.Interrupt();
                    _threadReceivingMessages.Join();
                }

                if (_ourServiceMailSlot != -1)
                    DIS.Import.CloseHandle(_ourServiceMailSlot);  // закрываем дескриптор мэйлслота
            }
            else
            {
                MessageBox.Show("Еще есть клиенты");
                e.Cancel = true;
            }          
        }
    }
}