using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewWcfService
{
    public class CyptoWCFService : ICyptoWCFService
    {
        private static string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";    // строка, содержащая все буквы английского алфавита
        private int n = alphabet.Length;    // мощность алфавита
        private int k = 20;                 // ключ шифрования

        // метод, выполняющий шифрование строки
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
}