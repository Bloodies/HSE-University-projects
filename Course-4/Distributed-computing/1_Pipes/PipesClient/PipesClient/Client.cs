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
using System.IO;
using System.Threading;
using PipesClient;

namespace Pipes
{
    public partial class frmMain : Form
    {
        public static string login = "";

        private Int32 PipeHandle;   // дескриптор канала
        private Thread t;                      // поток для обслуживания канала
        private bool _continue = true;         // флаг, указывающий продолжается ли работа с каналом


        // конструктор формы
        public frmMain()
        {
            Login form = new Login();
            if (form.ShowDialog() == DialogResult.OK) login = form.login;

            InitializeComponent();

            lblShowLogin.Text = login;

            // создание именованного канала
            PipeHandle = DIS.Import.CreateNamedPipe(
                "\\\\.\\pipe\\" + login,
                DIS.Types.PIPE_ACCESS_DUPLEX,
                DIS.Types.PIPE_TYPE_BYTE | DIS.Types.PIPE_WAIT,
                DIS.Types.PIPE_UNLIMITED_INSTANCES,
                0,
                1024,
                DIS.Types.NMPWAIT_WAIT_FOREVER,
                (uint)0);

            // создание потока, отвечающего за работу с каналом
            t = new Thread(ReceiveMessage);
            t.Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tbMessage.Text == "")
            {
                MessageBox.Show("Введите сообщение");
                return;
            }
            
            uint BytesWritten = 0;  // количество реально записанных в канал байт
            byte[] buff = Encoding.Unicode.GetBytes(login + ": " + tbMessage.Text);    // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт

            // открываем именованный канал, имя которого указано в поле tbPipe
            int pipeSender = DIS.Import.CreateFile(tbPipe.Text, 
                DIS.Types.EFileAccess.GenericWrite, 
                DIS.Types.EFileShare.Read, 
                0, 
                DIS.Types.ECreationDisposition.OpenExisting, 
                0, 
                0);
            DIS.Import.WriteFile(pipeSender, buff, Convert.ToUInt32(buff.Length), ref BytesWritten, 0);         // выполняем запись последовательности байт в канал
            DIS.Import.CloseHandle(pipeSender);              // закрываем дескриптор канала
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
                    byte[] buff = new byte[1024];                                           // буфер прочитанных из канала байтов
                    DIS.Import.FlushFileBuffers(PipeHandle);                                // "принудительная" запись данных, расположенные в буфере операционной системы, в файл именованного канала
                    DIS.Import.ReadFile(PipeHandle, buff, 1024, ref realBytesReaded, 0);    // считываем последовательность байтов из канала в буфер buff
                    msg = Encoding.Unicode.GetString(buff);                                 // выполняем преобразование байтов в последовательность символов
                    rtbMessages.Invoke((MethodInvoker)delegate
                    {
                        if (msg != "")
                            rtbMessages.Text += "\n" + msg;                                // выводим полученное сообщение на форму
                    });

                    DIS.Import.DisconnectNamedPipe(PipeHandle);                             // отключаемся от канала клиента 
                    Thread.Sleep(500);                                                      // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;      // сообщаем, что работа с каналом завершена

            if (t != null)
                t.Abort();          // завершаем поток

            if (PipeHandle != -1)
                DIS.Import.CloseHandle(PipeHandle);     // закрываем дескриптор канала
        }
    }
}
