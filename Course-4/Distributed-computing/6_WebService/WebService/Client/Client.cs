using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Client.ServiceReference;

namespace ServiceClient
{
    public partial class Client : Form
    {
        // конструктор формы
        public Client()
        {
            InitializeComponent();
        }

        private void btnCurrency_Click(object sender, EventArgs e)
        {
            if (cbCurrency.Text == null || String.IsNullOrEmpty(cbCurrency.Text))
            {
                MessageBox.Show("Не введено название города. Введите город");
                return;
            }

            string symb = null;
            string changedText = null;
            var currency = new Currency { Value = cbCurrency.Text };
            var service = new ServiceSoapClient();
            var exchangeResponse = service.GetCurrency(currency);
            switch (cbCurrency.Text)
            {
                case "Доллар":
                    changedText = "Доллару";
                    symb = "$";
                    break;
                case "Евро":
                    changedText = "Евро";
                    symb = "€";
                    break;
                case "Иена":
                    changedText = "Иене";
                    symb = "¥";
                    break;
                case "Юань":
                    changedText = "Юаню";
                    symb = "¥";
                    break;
            }
            if (exchangeResponse.Found)
            {
                var response = exchangeResponse.Exchange;
                MessageBox.Show($"{cbCurrency.Text} к Рублю: 1 {symb} = {response.CurrentToOther} ₽ \n" +
                                $"Рубль к {changedText}: 1 ₽ = {response.OtherToCurrent} {symb}, \n" +
                                $"{response.Trend} на {response.Percentage}% за месяц ({response.CurrentToOtherOld} месяц назад)",
                                $"Курс валют ({symb})");
            }
            else
            {
                MessageBox.Show("Для данного города прогноз погоды не найден");
            }
        }
    }
}