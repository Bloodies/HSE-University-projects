using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using MSMQ;


namespace MSMQ
{
    public partial class frmMain : Form
    {
        private HashSet<string> _setClients = new HashSet<string>();
        private MessageQueue _inputMQ = null; // очередь сообщений
        private Thread _thread = null;              // поток, отвечающий за работу с очередью сообщений
        private bool _continue = true;              // флаг, указывающий продолжается ли работа с мэйлслотом
        private const string NAME = ".\\private$\\";

        // конструктор формы
        public frmMain()
        {
            InitializeComponent();
            string path = Dns.GetHostName() + "\\private$\\ServerQueue";    // путь к очереди сообщений, Dns.GetHostName() - метод, возвращающий имя текущей машины

            // если очередь сообщений с указанным путем существует, то открываем ее, иначе создаем новую
            if (MessageQueue.Exists(path))
                _inputMQ = new MessageQueue(path);
            else
                _inputMQ = MessageQueue.Create(path);

            // задаем форматтер сообщений в очереди
            _inputMQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

            // вывод пути к очереди сообщений в заголовок формы, чтобы можно было его использовать для ввода имени в форме клиента, запущенного на другом вычислительном узле
            this.Text += "     " + _inputMQ.Path;

            // создание потока, отвечающего за работу с очередью сообщений
            _thread = new Thread(ReceiveMessage);
            _thread.Start();
        }

        // получение сообщения
        private void ReceiveMessage()
        {
            if (_inputMQ == null) return;

            System.Messaging.Message msg = null;
            bool write;
            string messageResult;
            MessageQueue outputMessageQueue;

            // входим в бесконечный цикл работы с очередью сообщений
            try
            {
                while (_continue)
                {
                    if (_inputMQ.Peek() != null)              // если в очереди есть сообщение, выполняем его чтение, интервал до следующей попытки чтения равен 10 секундам
                        msg = _inputMQ.Receive(TimeSpan.FromSeconds(10.0));

                    if (!_continue)
                        break;

                    write = true;
                    messageResult = (string)msg.Body;

                    //Добавить Клиента в список
                    if (messageResult.Contains("NEW_MESSAGE_QUEUE:"))
                    {
                        string name = messageResult.Split(':').ElementAt(1);
                        _setClients.Add(name);
                        write = false;
                    }

                    //Удалить клиента из списка
                    if (messageResult.Contains("REMOVE_MESSAGE_QUEUE:"))
                    {
                        string name = messageResult.Split(':').ElementAt(1);
                        _setClients.Remove(name);
                        write = false;
                    }

                    //Отправить сообщение всем
                    if (!write)
                        continue;

                    rtbMessages.Invoke((MethodInvoker)delegate
                    {
                        if (msg != null)
                            rtbMessages.Text += "\n >> " + msg.Body;// выводим полученное сообщение на форму
                    });

                    foreach (var x in _setClients)
                    {
                        string path = NAME + x;
                        if (MessageQueue.Exists(path))
                            outputMessageQueue = new MessageQueue(path);
                        else
                            outputMessageQueue = MessageQueue.Create(path);

                        outputMessageQueue.Send(messageResult);
                    }

                    Thread.Sleep(500);                              // приостанавливаем работу потока перед тем, как приcтупить к обслуживанию очередного клиента
                }
            }
            catch (ThreadInterruptedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            MessageQueue outputMessageQueue;

            foreach (var x in _setClients)
            {
                string path = NAME + x;
                if (MessageQueue.Exists(path))
                    outputMessageQueue = new MessageQueue(path);
                else
                    outputMessageQueue = MessageQueue.Create(path);

                outputMessageQueue.Send("inicialization");
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_setClients.Count == 0)
            {
                _continue = false;                                  // сообщаем, что работа с очередью сообщений завершена

                try
                {
                    _inputMQ.Send("get out");

                    if (_thread != null)
                    {
                        _thread.Interrupt();                        // завершаем поток
                        _thread.Join();
                    }

                    if (_inputMQ != null)
                    {
                        MessageQueue.Delete(_inputMQ.Path);   // в случае необходимости удаляем очередь сообщений
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Еще есть клиенты");
                e.Cancel = true;
            }
        }
    }
}