using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewWcfService
{
    [ServiceContract]
    public interface ICyptoWCFService
    {
        // интерфейс метода, выполняющего шифрование строки
        [OperationContract]
        CryptoStr Encrypt(CryptoStr str);

        // интерфейс метода, выполняющего дешифрование строки
        [OperationContract]
        CryptoStr Decrypt(CryptoStr str);
    }

    // класс, определяющий строку шифрования
    [DataContract]
    public class CryptoStr
    {
        private string _str;

        // конструктор
        public CryptoStr()
        {
            _str = "";
        }

        // строка
        [DataMember]
        public string SourceString { get { return _str; } set { _str = value; } }

        // длина строки
        public int Length { get { return _str.Length; } }
    }
}