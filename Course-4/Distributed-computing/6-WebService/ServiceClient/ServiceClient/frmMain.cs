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
            CryptoServiceReference.CryptoStr str = new CryptoServiceReference.CryptoStr();
            str.SourceString = tbEncrypt.Text;

            // вызываем метод веб-сервиса по шифрованию строки
            CryptoServiceReference.CryptoServiceSoapClient service = new CryptoServiceReference.CryptoServiceSoapClient();

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
            CryptoServiceReference.CryptoStr str = new CryptoServiceReference.CryptoStr();
            str.SourceString = tbDecrypt.Text;

            // вызываем метод веб-сервиса по дешифрованию строки
            CryptoServiceReference.CryptoServiceSoapClient service = new CryptoServiceReference.CryptoServiceSoapClient();

            // выводим результат дешифрования строки
            MessageBox.Show(service.Decrypt(str).SourceString);
        }
    }
}