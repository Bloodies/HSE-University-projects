using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Messaging;
using System.Threading;

namespace Client
{
    public partial class Client : Form
    {
        // очереди сообщений, в которую будет производиться запись сообщений
        private MessageQueue _outputMQ = null;
        private MessageQueue _inputMQ = null;

        private Thread _threadReceivingMessages = null;
        private string _clientName = null;
        private bool _continueReceivingMessages = true;

        private const string DIRECTORY_NAME = ".\\private$\\";

        // конструктор формы
        public Client()
        {
            InitializeComponent();
            btnLogin.Enabled = true;
            btnSend.Enabled = false;
            btnConnect.Enabled = false;
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

                    if (result == "REMOVE_MESSAGE_QUEUE")
                        continue;

                    rtbMessages.Invoke((MethodInvoker)delegate
                    {

                        if (msg != null)
                            rtbMessages.Text += "\n >> " + msg.Body;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLogin.Text))
            {
                _clientName = tbLogin.Text;
                if (MessageQueue.Exists(DIRECTORY_NAME + _clientName))
                {
                    MessageBox.Show("Выберите другой логин");
                    return;
                }
                else
                {
                    tbLogin.Enabled = false;
                    btnLogin.Enabled = false;
                    btnConnect.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
                return;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (MessageQueue.Exists(tbPath.Text))
            {
                // если очередь, путь к которой указан в поле tbPath существует, то открываем ее
                _outputMQ = new MessageQueue(tbPath.Text);
                btnSend.Enabled = true;
                tbPath.Enabled = false;
                btnConnect.Enabled = false;

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

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage(_clientName + " : " + tbMessage.Text);
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
    }
}
