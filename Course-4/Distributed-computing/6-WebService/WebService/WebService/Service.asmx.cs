using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service : System.Web.Services.WebService
    {
        private static readonly Dictionary<string, Exchange> Converters = new Dictionary<string, Exchange>
        {
            {"доллар", new Exchange(122.12, 0.0082, 37, 77.39, "рост")},
            {"евро", new Exchange(133.86, 0.0075, 35, 87.68, "рост")},
            {"иена", new Exchange(1.03, 0.97, 35, 0.67, "рост")},
            {"юань", new Exchange(19.19, 0.052, 36, 12.22, "рост")}
        };


        // метод, отдающий курс валюты
        [WebMethod]
        public ExchangeResponse GetCurrency(Currency str)
        {
            var found = Converters.TryGetValue(str.Value.ToLower(), out var exchange);
            return new ExchangeResponse
            {
                Found = found,
                Exchange = exchange,
            };
        }
    }

    // класс, определяющий словарь
    public class Currency
    {
        public string Value { get; set; }
    }

    public class ExchangeResponse
    {
        public bool Found { get; set; }
        public Exchange Exchange { get; set; }
    }

    public class Exchange
    {
        public Exchange() { }

        public Exchange(double currentToOther, double otherToCurrent, int percentage, double currentToOtherOld, string trend)
        {
            CurrentToOther = currentToOther;
            OtherToCurrent = otherToCurrent;
            Percentage = percentage;
            CurrentToOtherOld = currentToOtherOld;
            Trend = trend;
        }

        public double CurrentToOther { get; set; }
        public double OtherToCurrent { get; set; }
        public int Percentage { get; set; }
        public double CurrentToOtherOld { get; set; }
        public string Trend { get; set; }
    }
}
