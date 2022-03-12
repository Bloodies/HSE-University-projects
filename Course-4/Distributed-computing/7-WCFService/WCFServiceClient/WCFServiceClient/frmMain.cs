using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceClient
{
    public partial class frmMain : Form
    {
        // конструктор формы
        public frmMain()
        {
            InitializeComponent();
        }

        // шифрование строки
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            // если строка не введена, то выходим из обработчика нажатия кнопки
            if (tbEncrypt.Text == null || String.IsNullOrEmpty(tbEncrypt.Text))
            {
                MessageBox.Show("Не введена строка для шифрования. Введите текст");
                return;
            }

            // создаем экземпляр класса CryptoStr и присваиваем ему значение поля tbEncrypt
            WCFServiceClient.CyptoWCFServiceReference.CryptoStr str = new WCFServiceClient.CyptoWCFServiceReference.CryptoStr();
            str.SourceString = tbEncrypt.Text;

            // вызываем метод WCF-сервиса по шифрованию строки
            WCFServiceClient.CyptoWCFServiceReference.CyptoWCFServiceClient service = new WCFServiceClient.CyptoWCFServiceReference.CyptoWCFServiceClient();

            // выводим результат шифрования строки
            MessageBox.Show(service.Encrypt(str).SourceString);
        }

        // дешифрование строки
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            // если строка не введена, то выходим из обработчика нажатия кнопки
            if (tbDecrypt.Text == null || String.IsNullOrEmpty(tbDecrypt.Text))
            {
                MessageBox.Show("Не введена строка для дешифрования. Введите текст");
                return;
            }

            // создаем экземпляр класса CryptoStr и присваиваем ему значение поля tbDecrypt
            WCFServiceClient.CyptoWCFServiceReference.CryptoStr str = new WCFServiceClient.CyptoWCFServiceReference.CryptoStr();
            str.SourceString = tbDecrypt.Text;

            // вызываем метод WCF-сервиса по дешифрованию строки
            WCFServiceClient.CyptoWCFServiceReference.CyptoWCFServiceClient service = new WCFServiceClient.CyptoWCFServiceReference.CyptoWCFServiceClient();

            // выводим результат дешифрования строки
            MessageBox.Show(service.Decrypt(str).SourceString);
        }
    }
}