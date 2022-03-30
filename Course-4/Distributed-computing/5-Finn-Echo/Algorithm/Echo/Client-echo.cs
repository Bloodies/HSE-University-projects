using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace Echo
{
    public partial class Client : Form
    {
        static readonly object _locker = new object();
        private const int DELAY = 2000;

        private List<Thread> Threads = new List<Thread>();      // список потоков приложения (кроме родительского)
        private List<object[]> Neighbours = new List<object[]>();
        private List<object[]> ClosedNeighbours = new List<object[]>();
        private Socket NodeSocket;                              // сокет этого узла (сокет сервера)
        private Random rnd = new Random();
        public IPAddress IP;
        public int Port = 0;                                    // порт, который будет указан при создании сокета

        private string exoMessage = "";
        private bool _continue = true;                          // флаг, указывающий продолжается ли работа с сокетами
        private bool exoInitiator = false;
        private int exoParentIndex = -1;
        private int exoCount = 0;

        public Client()
        {
            InitializeComponent();

            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());    // информация об IP-адресах и имени машины, на которой запущено приложение
            IP = hostEntry.AddressList[0];                                  // IP-адрес, который будет указан при создании сокета

            // определяем IP-адрес машины в формате IPv4
            foreach (IPAddress address in hostEntry.AddressList)
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP = address;
                    break;
                }

            // вывод IP-адреса машины и номера порта в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text = $"{IP}  :  {Port}";

            tbIP.Text = IP.ToString();
            tbPort.Text = rnd.Next(1000, 8080).ToString();
        }

        // работа с клиентскими сокетами
        private void AcceptClients()
        {
            try
            {
                while (_continue)
                {
                    Socket socket = NodeSocket.Accept();                // получаем ссылку на очередной клиентский сокет
                    Neighbours.Add(new object[] { "client", socket });
                    Threads.Add(new Thread(ReceiveMessageFromClient));  // создаем и запускаем поток, обслуживающий конкретный клиентский сокет
                    Threads[Threads.Count - 1].Start(socket);

                    IPAddress address = ((IPEndPoint)socket.RemoteEndPoint).Address;
                    int port = ((IPEndPoint)socket.RemoteEndPoint).Port;

                    lbConnections.Invoke((MethodInvoker)delegate
                    {
                        lbConnections.Items.Add($"[client] {address} : {port}");
                    });
                }
            }
            catch (Exception e)
            {
                if (_continue) MessageBox.Show(e.Message);
            }
        }

        // получение сообщений от конкретного клиента
        private void ReceiveMessageFromClient(object Socket)
        {
            Socket socket = (Socket)Socket;
            GetSocketInfo(socket, out IPAddress ip, out int port, out string cos);
            string msg = "";                                                    // полученное сообщение

            try
            {
                while (_continue)
                {
                    byte[] buff = new byte[1024];                               // буфер прочитанных из сокета байтов
                    socket.Receive(buff);                                       // получаем последовательность байтов из сокета в буфер buff
                    msg = Encoding.Unicode.GetString(buff).Replace("\0", "");   // выполняем преобразование байтов в последовательность символов

                    if (msg != "")
                    {
                        if (!ReceiveMessage(socket, msg)) break;
                    }

                    Thread.Sleep(500);
                }
            }
            catch (SocketException e)
            {
                if (_continue)
                {
                    object[] item = Neighbours.First(a => (Socket)a[1] == socket);
                    ((Socket)item[1]).Close();
                    Neighbours.Remove(item);

                    lbConnections.Invoke((MethodInvoker)delegate
                    {
                        lbConnections.Items.Remove($"[{cos}] {ip} : {port}");
                    });

                    MessageBox.Show(e.Message);
                }
            }
        }

        private void ReceiveMessageFromServer(object Socket)
        {
            Socket socket = (Socket)Socket;
            GetSocketInfo(socket, out IPAddress ip, out int port, out string cos);

            try
            {
                while (_continue)
                {
                    // получаем ответ
                    byte[] buff = new byte[1024]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes; // количество полученных байт
                    do
                    {
                        bytes = socket.Receive(buff, buff.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(buff, 0, bytes));
                    } while (socket.Available > 0);

                    string msg = builder.ToString().Replace("\0", "");
                    if (msg != "")
                    {
                        if (!ReceiveMessage(socket, msg)) break;
                    }
                    Thread.Sleep(500);
                }
            }
            catch (SocketException e)
            {
                if (_continue)
                {
                    object[] item = Neighbours.First(a => (Socket)a[1] == socket);
                    ((Socket)item[1]).Close();
                    Neighbours.Remove(item);

                    lbConnections.Invoke((MethodInvoker)delegate
                    {
                        lbConnections.Items.Remove($"[{cos}] {ip} : {port}");
                    });

                    MessageBox.Show(e.Message);
                }
            }
        }

        private bool ReceiveMessage(Socket socket, string msg)
        {
            lock (_locker)
            {
                GetSocketInfo(socket, out IPAddress ip, out int port, out string cos);

                MsgBody msgBody = JsonSerializer.Deserialize<MsgBody>(msg);
                if (msgBody.Body != null && msgBody.Body.StartsWith("logout_"))
                {
                    object[] item = Neighbours.First(a => (Socket)a[1] == socket);
                    Neighbours.Remove(item);
                    ClosedNeighbours.Add(item);

                    lbConnections.Invoke((MethodInvoker)delegate
                    {
                        lbConnections.Items.Remove($"[{cos}] {ip} : {port}");
                    });
                    return false;
                }

                if (msgBody.Exo)
                {
                    Echo_algorithm(socket, msgBody);
                }

                rtbMessages.Invoke((MethodInvoker)delegate
                {
                    // выводим полученное сообщение на форму
                    rtbMessages.Text += $"\n\n >> [{cos}] {ip} : {port}\n{msg}"; 
                    rtbMessages.SelectionStart = rtbMessages.TextLength;
                    rtbMessages.ScrollToCaret();
                });
                return true;
            }
        }

        private void GetSocketInfo(Socket socket, out IPAddress ip, out int port, out string cos)
        {
            ip = ((IPEndPoint)socket.RemoteEndPoint).Address;
            port = ((IPEndPoint)socket.RemoteEndPoint).Port;
            cos = "";

            if (Neighbours.FindAll(a => (Socket)a[1] == socket).Count == 1)
                cos = Neighbours.First(a => (Socket)a[1] == socket)[0].ToString();
        }

        private void SendMessages(Socket socket, MsgBody m)
        {
            string msg = JsonSerializer.Serialize(m);
            byte[] buff = Encoding.Unicode.GetBytes(msg);   // выполняем преобразование сообщения (вместе с идентификатором машины) в последовательность байт
            
            if (socket != null)
                socket.Send(buff);
        }

        private void Echo_algorithm(Socket socket, MsgBody msgBody)
        {
            if (exoInitiator)
            {
                exoCount += 1;
                exoMessage += $"{msgBody.Body} ";
                if (exoCount == Neighbours.Count)
                {
                    rtbMessages.Text += $"Последовательность: {exoMessage}";
                    exoCount = 0;
                    exoInitiator = false;
                    btnCheck.BackColor = Color.FromName("Control");
                    exoMessage = "";
                }
            }
            else
            {
                btnCheck.BackColor = Color.Green;
                if (exoParentIndex == -1)
                {
                    if (Neighbours.Count == 1)
                    {
                        exoCount = 0;
                        Thread.Sleep(DELAY);
                        btnCheck.BackColor = Color.FromName("Control");

                        msgBody.Body = tbMessage.Text;
                        SendMessages((Socket)Neighbours[0][1], msgBody);
                    }
                    else
                    {
                        exoParentIndex = Neighbours.FindIndex(a => (Socket)a[1] == socket);

                        Thread.Sleep(DELAY);
                        btnCheck.BackColor = Color.Red;

                        for (int i = 0; i < Neighbours.Count; i++)
                            if (i != exoParentIndex)
                                SendMessages((Socket)Neighbours[i][1], msgBody);
                    }
                }
                else
                {
                    exoCount += 1;
                    exoMessage += $"{msgBody.Body} ";
                    if (exoCount == Neighbours.Count - 1)
                    {
                        exoCount = 0;
                        Thread.Sleep(DELAY);
                        btnCheck.BackColor = Color.FromName("Control");

                        msgBody.Body = $"{exoMessage} > {tbMessage.Text}";
                        SendMessages((Socket)Neighbours[exoParentIndex][1], msgBody);
                        exoParentIndex = -1;
                        exoMessage = "";
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                Port = Int32.Parse(tbPort.Text);
                this.Text = $"{IP} : {Port}";
                tbMessage.Text = Port.ToString();

                btnStart.Enabled = false;
                tbPort.Enabled = false;

                tbConnectPort.Enabled = true;
                btnConnect.Enabled = true;
                btnCheck.Enabled = true;

                tbConnectIP.Text = IP.ToString();

                NodeSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // связываем сокет с локальной точкой, по которой будем принимать данные
                NodeSocket.Bind(new IPEndPoint(IP, Port));

                // начинаем прослушивание
                NodeSocket.Listen(10);  // backlog - Максимальная длина очереди ожидающих подключений.

                // создаем и запускаем поток, выполняющий обслуживание серверного сокета
                Threads.Clear();
                Threads.Add(new Thread(AcceptClients));
                Threads[Threads.Count - 1].Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                int port = Int32.Parse(tbConnectPort.Text);
                if (port == Port) throw new Exception("Попытка подключения узла к самому себе");
                IPAddress ip = IPAddress.Parse(tbConnectIP.Text);      // разбор IP-адреса сервера

                foreach (var o in Neighbours)
                {
                    Socket s = (Socket)o[1];
                    IPAddress a = ((IPEndPoint)s.RemoteEndPoint).Address;
                    int p = ((IPEndPoint)s.RemoteEndPoint).Port;

                    if (port == p && ip.ToString() == a.ToString()) throw new Exception("С таким узлом уже есть канал связи");
                }

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(new IPEndPoint(ip, port));

                Neighbours.Add(new object[] { "server", socket });
                lbConnections.Items.Add($"[server] {ip} : {port}");

                Thread t = new Thread(ReceiveMessageFromServer);
                t.Start(socket);
                Threads.Add(t);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (Neighbours.Count == 0) return;
            btnCheck.BackColor = Color.Blue;
            exoInitiator = true;

            MsgBody msg = new MsgBody()
            {
                Exo = true,
                Port = this.Port,
                IpAddress = this.IP.ToString()
            };
            foreach (object[] neighbour in Neighbours)
            {
                SendMessages((Socket)neighbour[1], msg);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continue = false;      // сообщаем, что работа с сокетами завершена

            foreach (var n in Neighbours)
            {
                if (n[0].ToString() == "server")
                {
                    MsgBody b = new MsgBody
                    {
                        Body = "logout_",
                        Port = this.Port,
                        IpAddress = this.IP.ToString()
                    };
                    SendMessages((Socket)n[1], b);
                }
            }

            foreach (var n in Neighbours)
            {
                if (n[0].ToString() == "server")
                    ((Socket)n[1]).Close();
            }

            foreach (var n in Neighbours)
            {
                if (n[0].ToString() == "client")
                    ((Socket)n[1]).Close();
            }

            foreach (var n in ClosedNeighbours)
            {
                ((Socket)n[1]).Close();
            }

            // приостанавливаем "прослушивание" серверного сокета
            if (NodeSocket != null)
                NodeSocket.Close();

            // завершаем все потоки
            foreach (Thread t in Threads)
            {
                t.Abort();
                t.Join(500);
            }
        }
    }
}
