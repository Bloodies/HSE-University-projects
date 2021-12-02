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

namespace Pipes
{
    public partial class frmMain : Form
    {
        private Int32 PipeHandle;                                                       // дескриптор канала
        private string PipeName = "\\\\" + Dns.GetHostName() + "\\pipe\\ServerPipe";    // имя канала, Dns.GetHostName() - метод, возвращающий имя машины, на которой запущено приложение
        private Thread t;                                                               // поток для обслуживания канала
        private bool _continue = true;                                                  // флаг, указывающий продолжается ли работа с каналом
        private List<string> connectedUsers = new List<string>();

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();

            // создание именованного канала
            PipeHandle = DIS.Import.CreateNamedPipe("\\\\.\\pipe\\ServerPipe", 
                DIS.Types.PIPE_ACCESS_DUPLEX, 
                DIS.Types.PIPE_TYPE_BYTE | DIS.Types.PIPE_WAIT, 
                DIS.Types.PIPE_UNLIMITED_INSTANCES, 
                0, 
                1024, 
                DIS.Types.NMPWAIT_WAIT_FOREVER, 
                (uint)0);

            // вывод имени канала в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text += "     " + PipeName;
            rtbUsers.Text += "\n";
            rtbMessages.Text += "\n";

            // создание потока, отвечающего за работу с каналом
            t = new Thread(ReceiveMessage);
            t.Start();
        }

        private void ReceiveMessage()
        {
            string msg = "";            // прочитанное сообщение
            uint realBytesReaded = 0;   // количество реально прочитанных из канала байтов

            // входим в бесконечный цикл работы с каналом
            while (_continue)
            {
                if (DIS.Import.ConnectNamedPipe(PipeHandle, 0))
                {
                    byte[] buf = new byte[1024];                                           // буфер прочитанных из канала байтов
                    DIS.Import.FlushFileBuffers(PipeHandle);                                // "принудительная" запись данных, расположенные в буфере операционной системы, в файл именованного канала
                    DIS.Import.ReadFile(PipeHandle, buf, 1024, ref realBytesReaded, 0);    // считываем последовательность байтов из канала в буфер buff
                    msg = Encoding.Unicode.GetString(buf);                                 // выполняем преобразование байтов в последовательность символов

                    string username = msg.Split(new string[] { ": " }, StringSplitOptions.None)[0];
                    rtbMessages.Invoke((MethodInvoker)delegate
                    {
                        if (msg != "")
                        {
                            rtbMessages.Text += "\n" + msg;                             // выводим полученное сообщение на форму
                            
                        }
                    });

                    if (!connectedUsers.Contains(username))
                    {
                        connectedUsers.Add(username);
                        rtbMessages.Invoke((MethodInvoker)delegate { rtbUsers.Text += "\n" + username; });
                    }

                    DIS.Import.DisconnectNamedPipe(PipeHandle);                             // отключаемся от канала клиента 
                    foreach (string user in connectedUsers)
                    {
                        string name = "\\\\.\\pipe\\" + user;
                        uint BytesWritten = 0;                              // количество реально записанных в канал байт
                        byte[] buff = Encoding.Unicode.GetBytes(msg);   // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт

                        // открываем именованный канал, имя которого указано в поле tbPipe
                        int pipeSender = DIS.Import.CreateFile(name,
                            DIS.Types.EFileAccess.GenericWrite,
                            DIS.Types.EFileShare.Read,
                            0,
                            DIS.Types.ECreationDisposition.OpenExisting,
                            0,
                            0);
                        DIS.Import.WriteFile(pipeSender, buff, Convert.ToUInt32(buff.Length), ref BytesWritten, 0);         // выполняем запись последовательности байт в канал
                        DIS.Import.CloseHandle(pipeSender);                 // закрываем дескриптор канала
                    }
                    Thread.Sleep(500);                                                      // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
                }
            }
        }

        private void SendMessage(string username, string message)
        {
            string name = "\\\\.\\pipe\\" + username;
            uint BytesWritten = 0;                              // количество реально записанных в канал байт
            byte[] buff = Encoding.Unicode.GetBytes(message);   // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт

            // открываем именованный канал, имя которого указано в поле tbPipe
            int pipeSender = DIS.Import.CreateFile(name, 
                DIS.Types.EFileAccess.GenericWrite, 
                DIS.Types.EFileShare.Read, 
                0, 
                DIS.Types.ECreationDisposition.OpenExisting, 
                0, 
                0);
            DIS.Import.WriteFile(pipeSender, buff, Convert.ToUInt32(buff.Length), ref BytesWritten, 0);         // выполняем запись последовательности байт в канал
            DIS.Import.CloseHandle(pipeSender);                 // закрываем дескриптор канала
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;                          // сообщаем, что работа с каналом завершена

            if (t != null)
                t.Abort();                              // завершаем поток
            
            if (PipeHandle != -1)
                DIS.Import.CloseHandle(PipeHandle);     // закрываем дескриптор канала
        }
    }
}