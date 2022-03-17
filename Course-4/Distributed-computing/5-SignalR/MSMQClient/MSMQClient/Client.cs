using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MSMQ;

namespace MSMQ
{
    public partial class frmMain : Form
    {
        private static Random random = new Random();

        // очереди сообщений, в которую будет производиться запись сообщений
        private MessageQueue _outputMQ = null;
        private MessageQueue _inputMQ = null;

        private Thread _threadReceivingMessages = null;
        private int cpu = 0;
        private string _clientName = null;
        private bool _continueReceivingMessages = true;

        private const string DIRECTORY_NAME = ".\\private$\\";

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();
            btnConnect.Enabled = true;
        }

        private void ReceiveMessage()
        {
            if (_inputMQ == null)
                return;

            System.Messaging.Message msg = null;

            try
            {
                while (_continueReceivingMessages)
                {
                    if (_inputMQ.Peek() != null)
                        msg = _inputMQ.Receive(TimeSpan.FromSeconds(10.0));

                    if (!_continueReceivingMessages)
                        break;
                    string result = (string)msg.Body;

                    if (result == "inicialization")
                        SendMessage(_clientName + " | " + $"{random.Next(0, 100)}");

                    if (result == "REMOVE_MESSAGE_QUEUE")
                        continue;

                    if (result.Replace(" ", "").Split('|')[0].Split(':').Count() == 1)
                    {
                        cpu = Convert.ToInt32(result.Replace(" ", "").Split('|')[1]);
                    }
                    else if (result.Replace(" ", "").Split('|')[0].Split(':').Count() > 1)
                    {
                        if (cpu > Convert.ToInt32(result.Replace(" ", "").Split('|')[1]))
                        {
                            SendMessage($"{result.Replace(" ", "").Split('|')[0]}; up");
                        }
                        else if (cpu < Convert.ToInt32(result.Replace(" ", "").Split('|')[1]))
                        {
                            SendMessage($"{result.Replace(" ", "").Split('|')[0]}; down");
                        }
                        else if (cpu == Convert.ToInt32(result.Replace(" ", "").Split('|')[1]))
                        {
                            SendMessage($"{result.Replace(" ", "").Split('|')[0]}; done");
                        }
                    }

                    if (result.Replace(" ", "").Split(';')[1] == "up")
                    {
                        cpu += 1;
                        SendMessage(_clientName + " | " + $"{cpu}");
                    }
                    else if (result.Replace(" ", "").Split(';')[1] == "down")
                    {
                        cpu -= 1;
                        SendMessage(_clientName + " | " + $"{cpu}");
                    }

                    rtbMessages.Invoke((MethodInvoker)delegate
                    {

                        if (msg != null)
                            rtbMessages.Text += "\n" + msg.Body;
                    });
                    Thread.Sleep(500);
                }
            }
            catch (ThreadInterruptedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendMessage(string text)
        {
            _outputMQ.Send(text);
            //// выполняем отправку сообщения в очередь
            //q.Send(tbMessage.Text, Dns.GetHostName());
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPath.Text))
            {
                _clientName = tbPath.Text;
                if (MessageQueue.Exists(DIRECTORY_NAME + _clientName))
                {
                    if (_clientName.Split(':').Count() > 1)
                    {
                        if (MessageQueue.Exists(DIRECTORY_NAME + _clientName.Split(':')[0]))
                        {
                            _clientName = _clientName.Split(':')[0] + $"{(Convert.ToInt32(_clientName.Split(':')[1]) + 1)}";
                        }
                    }
                }
                else
                {
                    btnConnect.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
                return;
            }

            if (MessageQueue.Exists(tbPath.Text))
            {
                // если очередь, путь к которой указан в поле tbPath существует, то открываем ее
                _outputMQ = new MessageQueue(tbPath.Text);
                tbPath.Enabled = false;

                //Создать свою очередь

                if (MessageQueue.Exists(DIRECTORY_NAME + _clientName))
                {
                    _inputMQ = new MessageQueue(DIRECTORY_NAME + _clientName);
                }
                else
                {
                    _inputMQ = MessageQueue.Create(DIRECTORY_NAME + _clientName);
                }

                _inputMQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

                string path = _clientName;
                SendMessage("NEW_MESSAGE_QUEUE:" + path);

                _threadReceivingMessages = new Thread(ReceiveMessage);
                _threadReceivingMessages.Start();
            }
            else
                MessageBox.Show("Указан неверный путь к очереди, либо очередь не существует");

            SendMessage(_clientName + " | " + $"{random.Next(0, 100)}");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _continueReceivingMessages = false;
            try
            {
                if (_outputMQ != null)
                {
                    string path = _clientName;
                    SendMessage("REMOVE_MESSAGE_QUEUE:" + path);
                }
                if (_inputMQ != null)
                    _inputMQ.Send("KILL_YOURSELF_MESSAGE_QUEUE");

                if (_threadReceivingMessages != null)
                {
                    _threadReceivingMessages.Interrupt();
                    _threadReceivingMessages.Join();
                }
                if (_inputMQ != null)
                {
                    MessageQueue.Delete(_inputMQ.Path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageQueue.Exists(tbPath.Text))
            {
                // если очередь, путь к которой указан в поле tbPath существует, то открываем ее
                _outputMQ = new MessageQueue(tbPath.Text);
                tbPath.Enabled = false;

                //Создать свою очередь

                if (MessageQueue.Exists(DIRECTORY_NAME + _clientName))
                {
                    _inputMQ = new MessageQueue(DIRECTORY_NAME + _clientName);
                }
                else
                {
                    _inputMQ = MessageQueue.Create(DIRECTORY_NAME + _clientName);
                }

                _inputMQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

                string path = _clientName;
                SendMessage("NEW_MESSAGE_QUEUE:" + path);

                _threadReceivingMessages = new Thread(ReceiveMessage);
                _threadReceivingMessages.Start();
            }
            else
                MessageBox.Show("Указан неверный путь к очереди, либо очередь не существует");
        }
    }
}