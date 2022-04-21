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
using System.Collections;
using System.IO;

namespace Sockets
{
    public partial class frmMain : Form
    {
       
        private Thread thread;              // список потоков приложения (кроме родительского)
        private bool _continue = true;      // флаг, указывающий продолжается ли работа с сокетами
        private IPAddress IP = IPAddress.Parse("192.168.0.191");// IP-адрес клиента
        private Dictionary<int, TcpClient> portsAndTCPClients = new Dictionary<int, TcpClient>();// список потоков приложения (кроме родительского)

        UdpClient receivingUdpClient = new UdpClient(PORT);        
        private const int PORT = 1010;      // порт, который будет указан при создании сокета

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();
           
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());    // информация об IP-адресах и имени машины, на которой запущено приложение
            IPAddress[] t = hostEntry.AddressList;
            //IP = t[0];                                                      // IP-адрес, который будет указан при создании сокета
            
            //for (int i = 1; i < t.Length; i++)                              // определяем IP-адрес машины в формате IPv4
            //{
            //    if (t[i].AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        IP = t[i];
            //        break;
            //    }
            //}

            // создаем и запускаем поток, выполняющий обслуживание серверного сокета
            thread = new Thread(ReceiveMessage);
            thread.Start();

            // вывод IP-адреса машины и номера порта в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text += "     " + IP.ToString() + "  :  " + PORT.ToString();
        }

        // работа с клиентскими сокетами
        private void ReceiveMessage()
        {
            IPEndPoint RemoteIpEndPoint = null;
            bool write = true;
            // входим в бесконечный цикл для работы с клиентскими сокетом
            while (_continue)
            {
                byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint); // буфер прочитанных из сокета байтов

                string msg = Encoding.Unicode.GetString(receiveBytes);                  // выполняем преобразование байтов в последовательность символов

                //Добавление клиента
                write = true;
                if (msg.Contains("AddClient:"))
                {                    
                    int portNumber = int.Parse(msg.Split(':')[1]);                      //Получаем порт

                    if (portsAndTCPClients.ContainsKey(portNumber)) continue;

                    TcpClient tcpClient = new TcpClient();                              //Создаем tcp лиснер и конектимся к отправителю по Ip и порту
                    tcpClient.Connect(IP, portNumber);
                    portsAndTCPClients.Add(portNumber, tcpClient);                      //Сохраняю, чтобы потом отключить его
                    write = false;
                }

                //Удаление клиента из рассылки
                if (msg.Contains("DeleteClient:"))
                {
                    int portNumber = int.Parse(msg.Split(':')[1]);
                    portsAndTCPClients.Remove(portNumber);
                    write = false;
                }

                //Отправка сообщения по клиентам
                if (write)
                {
                    rtbMessages.Invoke((MethodInvoker)delegate
                    {
                        if (msg.Replace("\0", "") != "")
                            rtbMessages.Text += "\n >> " + msg;                         // выводим полученное сообщение на форму
                        byte[] newbuff = Encoding.Unicode.GetBytes(msg);                // буфер прочитанных из сокета байтов
                        //Отправление по клиентам, тоесть клиент, который отправил сообщение увидит его только, если сервер отправит ему
                        //(Клиент не записывает сообщение сам себе)
                        foreach (TcpClient tcpClient in portsAndTCPClients.Values)
                        {
                            Stream stm = tcpClient.GetStream();                         // получаем файловый поток сокета клиента
                            stm.Write(newbuff, 0, receiveBytes.Length);
                        }
                    });
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;                                      // сообщаем, что работа с сокетами завершена

            receivingUdpClient.Close();

            // завершаем все потоки
            thread.Abort();
            thread.Join();
        }
    }
}