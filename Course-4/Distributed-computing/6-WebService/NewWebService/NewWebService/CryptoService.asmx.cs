using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace NewWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CryptoService : System.Web.Services.WebService
    {
        private static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";    // строка, содержащая все буквы английского алфавита
        private int n = alphabet.Length;    // мощность алфавита
        private int k = 20;                 // ключ шифрования

        // метод, выполняющий шифрование строки
        [WebMethod]
        public CryptoStr Encrypt(CryptoStr str)
        {
            // создаем новый объект класса CryptoStr
            CryptoStr EncryptStr = new CryptoStr();

            // выполняем шифрование по формуле вида (y = (x + k) mod n), где x - шифруемый символ, k - ключ, n - мощность алфавита, y - результат шифрования, mod - операция взятия остатка от целочисленного деления
            for (int i = 0; i < str.Length; i++)
            {
                if (alphabet.IndexOf(str.SourceString[i]) == -1)
                    EncryptStr.SourceString += str.SourceString[i];     // если символа в алфавите нет, то оставляем его без изменений (например, пробелы, знаки препинания и др.)
                else
                    EncryptStr.SourceString += alphabet[(alphabet.IndexOf(str.SourceString[i]) + k) % n];   // если символ есть в алфавите, то выполняем его шифрование по вышеуказанной формуле
            }

            return EncryptStr;
        }

        // метод, выполняющий дешифрование строки
        [WebMethod]
        public CryptoStr Decrypt(CryptoStr str)
        {
            // создаем новый объект класса CryptoStr
            CryptoStr DecryptStr = new CryptoStr();

            // выполняем дешифрование по формуле вида (y = (x - k + n) mod n), где x - дешифруемый символ, k - ключ, n - мощность алфавита, y - результат дешифрования, mod - операция взятия остатка от целочисленного деления
            for (int i = 0; i < str.Length; i++)
                if (alphabet.IndexOf(str.SourceString[i]) == -1)
                    DecryptStr.SourceString += str.SourceString[i];     // если символа в алфавите нет, то оставляем его без изменений (например, пробелы, знаки препинания и др.)
                else
                    DecryptStr.SourceString += alphabet[(alphabet.IndexOf(str.SourceString[i]) - k + n) % n];   // если символ есть в алфавите, то выполняем его дешифрование по вышеуказанной формуле

            return DecryptStr;
        }
    }

    // класс, определяющий строку шифрования
    public class CryptoStr
    {
        private string _str;

        // конструктор
        public CryptoStr()
        {
            _str = "";
        }

        // строка
        public string SourceString { get { return _str; } set { _str = value; } }

        // длина строки
        public int Length { get { return _str.Length; } }
    }
}