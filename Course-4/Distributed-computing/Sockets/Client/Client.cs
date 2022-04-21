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

namespace Client
{
    public partial class Client : Form
    {
        TcpClient tcpClient = new TcpClient();// клиентский сокет        
        IPAddress ip = IPAddress.Parse("192.168.0.191");// IP-адрес клиента
        Socket clientSocket;
        TcpListener tcpListener;
        Thread thredForReceiving = null;

        const int PORT = 1010;
        int clientPort;
        string login = string.Empty; //Ник клиента
        bool _continue = true;

        public Client()
        {
            InitializeComponent();

            btnSend.Enabled = false;

            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName()); // информация об IP-адресах и имени машины, на которой запущено приложение

            tcpListener = new TcpListener(ip, 0);
            tcpListener.Start();

            clientPort = ((IPEndPoint)tcpListener.LocalEndpoint).Port;

            thredForReceiving = new Thread(ReadMessages);
            thredForReceiving.Start();
        }
        private void ReadMessages()
        {
            string msg = "";
            clientSocket = tcpListener.AcceptSocket();
            try
            {
                while (_continue)
                {

                    byte[] buff = new byte[1024];
                    clientSocket.Receive(buff);
                    msg = Encoding.Unicode.GetString(buff);

                    rtbMessages.Invoke((MethodInvoker)delegate
                    {
                        if (msg.Replace("\0", "") != "")
                            rtbMessages.Text += "\n >> " + msg;
                        // выводим полученное сообщение на форму
                    });
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex) { }
        }

        private void SendMessage(string text)
        {
            UdpClient udpClient = new UdpClient();

            IPEndPoint endPoint = new IPEndPoint(ip, PORT);
            byte[] buff = Encoding.Unicode.GetBytes(text);
            try
            {
                udpClient.Send(buff, buff.Length, endPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                udpClient.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLogin.Text))
            {
                login = tbLogin.Text;
                tbLogin.Enabled = false;
                btnSave.Enabled = false;
                SendMessage("AddClient:" + clientPort.ToString());
                btnSend.Enabled = true;
            }
        }

        // отправка сообщения
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(login + " >> " + tbMessage.Text);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpClient.Connected)
            {
                clientSocket.Close();
                tcpClient.Close();
            }
            SendMessage("DeleteClient:" + clientPort.ToString());
            if (clientSocket != null)
                clientSocket.Close();
            if (tcpListener != null)
                tcpListener.Stop();

            thredForReceiving.Abort();
            thredForReceiving.Join();
        }
    }
}
